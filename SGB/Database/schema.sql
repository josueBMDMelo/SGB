IF DB_ID('SGB') IS NULL
BEGIN
    CREATE DATABASE SGB;
END
GO

USE SGB;
GO

IF OBJECT_ID('dbo.Usuarios', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Usuarios (
        Id              INT IDENTITY(1,1) PRIMARY KEY,
        Matricula       NVARCHAR(30)    NOT NULL UNIQUE,
        Nome            NVARCHAR(120)   NOT NULL,
        Email           NVARCHAR(150)   NOT NULL UNIQUE,
        SenhaHash       NVARCHAR(256)   NOT NULL,
        Perfil          NVARCHAR(20)    NOT NULL,
        TipoUsuario     NVARCHAR(20)    NOT NULL,
        Ativo           BIT             NOT NULL DEFAULT 1,
        DataCadastro    DATETIME2       NOT NULL DEFAULT SYSDATETIME(),
        CONSTRAINT CK_Usuarios_Perfil
            CHECK (Perfil IN ('Usuario', 'Bibliotecario', 'Administrador')),
        CONSTRAINT CK_Usuarios_Tipo
            CHECK (TipoUsuario IN ('Aluno', 'Professor', 'Funcionario', 'Externo'))
    );
END
GO

IF OBJECT_ID('dbo.Livros', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Livros (
        Id              INT IDENTITY(1,1) PRIMARY KEY,
        Titulo          NVARCHAR(200)   NOT NULL,
        Autor           NVARCHAR(150)   NULL,
        Ativo           BIT             NOT NULL DEFAULT 1
    );
END
GO

IF OBJECT_ID('dbo.Exemplares', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Exemplares (
        Id                  INT IDENTITY(1,1) PRIMARY KEY,
        LivroId             INT             NOT NULL FOREIGN KEY REFERENCES dbo.Livros(Id),
        CodigoPatrimonio    NVARCHAR(50)    NOT NULL UNIQUE,
        Status              NVARCHAR(20)    NOT NULL DEFAULT 'Disponivel',
        CONSTRAINT CK_Exemplares_Status
            CHECK (Status IN ('Disponivel', 'Emprestado', 'Reservado', 'Indisponivel', 'Manutencao'))
    );
END
GO

IF OBJECT_ID('dbo.Espacos', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Espacos (
        Id              INT IDENTITY(1,1) PRIMARY KEY,
        Nome            NVARCHAR(80)    NOT NULL,
        Tipo            NVARCHAR(40)    NULL,
        Localizacao     NVARCHAR(100)   NULL,
        Capacidade      INT             NOT NULL DEFAULT 1,
        Ativo           BIT             NOT NULL DEFAULT 1,
        CONSTRAINT CK_Espacos_Capacidade CHECK (Capacidade > 0)
    );
END
GO

IF OBJECT_ID('dbo.Emprestimos', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Emprestimos (
        Id                      INT IDENTITY(1,1) PRIMARY KEY,
        UsuarioId               INT          NOT NULL FOREIGN KEY REFERENCES dbo.Usuarios(Id),
        ExemplarId              INT          NOT NULL FOREIGN KEY REFERENCES dbo.Exemplares(Id),
        DataEmprestimo          DATETIME2    NOT NULL DEFAULT SYSDATETIME(),
        DataPrevistaDevolucao   DATETIME2    NOT NULL,
        DataDevolucao           DATETIME2    NULL,
        DiasAtraso              INT          NOT NULL DEFAULT 0,
        Status                  NVARCHAR(20) NOT NULL DEFAULT 'Aberto',
        CONSTRAINT CK_Emprestimos_Status
            CHECK (Status IN ('Aberto', 'Devolvido', 'Atrasado', 'ComPendencia')),
        CONSTRAINT CK_Emprestimos_DiasAtraso CHECK (DiasAtraso >= 0)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = 'UX_Emprestimos_ExemplarAberto'
                 AND object_id = OBJECT_ID('dbo.Emprestimos'))
BEGIN
    CREATE UNIQUE INDEX UX_Emprestimos_ExemplarAberto
        ON dbo.Emprestimos (ExemplarId)
        WHERE DataDevolucao IS NULL;
END
GO

IF OBJECT_ID('dbo.Cobrancas', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Cobrancas (
        Id              INT IDENTITY(1,1) PRIMARY KEY,
        EmprestimoId    INT             NOT NULL FOREIGN KEY REFERENCES dbo.Emprestimos(Id),
        Tipo            NVARCHAR(20)    NOT NULL,
        Valor           DECIMAL(10,2)   NOT NULL,
        Pago            BIT             NOT NULL DEFAULT 0,
        DataGeracao     DATETIME2       NOT NULL DEFAULT SYSDATETIME(),
        CONSTRAINT CK_Cobrancas_Tipo  CHECK (Tipo IN ('Inicial', 'Multa')),
        CONSTRAINT CK_Cobrancas_Valor CHECK (Valor >= 0)
    );
END
GO

IF OBJECT_ID('dbo.ReservasEspaco', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ReservasEspaco (
        Id                  INT IDENTITY(1,1) PRIMARY KEY,
        UsuarioId           INT          NOT NULL FOREIGN KEY REFERENCES dbo.Usuarios(Id),
        EspacoId            INT          NOT NULL FOREIGN KEY REFERENCES dbo.Espacos(Id),
        DataHoraInicio      DATETIME2    NOT NULL,
        DataHoraFim         DATETIME2    NOT NULL,
        Status              NVARCHAR(20) NOT NULL DEFAULT 'Confirmada',
        CONSTRAINT CK_ReservasEspaco_Periodo CHECK (DataHoraFim > DataHoraInicio),
        CONSTRAINT CK_ReservasEspaco_Status
            CHECK (Status IN ('Pendente', 'Confirmada', 'Cancelada', 'Concluida'))
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = 'IX_ReservasEspaco_Espaco_Periodo'
                 AND object_id = OBJECT_ID('dbo.ReservasEspaco'))
BEGIN
    CREATE INDEX IX_ReservasEspaco_Espaco_Periodo
        ON dbo.ReservasEspaco (EspacoId, DataHoraInicio, DataHoraFim);
END
GO

IF OBJECT_ID('dbo.Parametros', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Parametros (
        Chave       NVARCHAR(50)    NOT NULL PRIMARY KEY,
        Valor       NVARCHAR(100)   NOT NULL,
        Descricao   NVARCHAR(200)   NULL
    );
END
GO

INSERT INTO dbo.Parametros (Chave, Valor, Descricao)
SELECT v.Chave, v.Valor, v.Descricao
FROM (VALUES
    ('ValorEmprestimoExterno', '5.00', 'Cobrança inicial do usuário externo (R$)'),
    ('MultaDiaria',            '2.00', 'Multa por dia de atraso (R$)'),
    ('LimiteAluno',            '3',    'Empréstimos simultâneos - Aluno'),
    ('LimiteFuncionario',      '5',    'Empréstimos simultâneos - Funcionário'),
    ('LimiteProfessor',        '5',    'Empréstimos simultâneos - Professor'),
    ('LimiteExterno',          '2',    'Empréstimos simultâneos - Usuário externo')
) AS v (Chave, Valor, Descricao)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Parametros p WHERE p.Chave = v.Chave);
GO
