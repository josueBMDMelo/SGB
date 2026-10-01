using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace SGB.Data;

public static class DatabaseInitializer
{
    public static void Inicializar(string connectionString)
    {
        GarantirBancoExiste(connectionString);
        ExecutarSchema(connectionString);
        CriarUsuariosPadraoSeNecessario(connectionString);
    }

    private static void GarantirBancoExiste(string connectionString)
    {
        var builder = new SqlConnectionStringBuilder(connectionString);
        string banco = builder.InitialCatalog;
        builder.InitialCatalog = "master";

        using var conn = new SqlConnection(builder.ConnectionString);
        conn.Open();
        using var cmd = new SqlCommand(
            $"IF DB_ID(N'{banco}') IS NULL CREATE DATABASE [{banco}];", conn);
        cmd.ExecuteNonQuery();
    }

    private static void ExecutarSchema(string connectionString)
    {
        string caminho = Path.Combine(AppContext.BaseDirectory, "Database", "schema.sql");
        if (!File.Exists(caminho))
        {
            throw new FileNotFoundException($"O script de banco de dados não foi encontrado em: {caminho}");
        }

        string script = File.ReadAllText(caminho);

        using var conn = new SqlConnection(connectionString);
        conn.Open();

        var lotes = Regex.Split(script, @"^[ \t]*GO[ \t]*\r?$",
                                RegexOptions.Multiline | RegexOptions.IgnoreCase);

        foreach (var lote in lotes)
        {
            if (string.IsNullOrWhiteSpace(lote)) continue;
            using var cmd = new SqlCommand(lote, conn);
            cmd.ExecuteNonQuery();
        }
    }


    private static void InserirUsuarioSeNaoExistir(
        SqlConnection conn,
        string nome,
        string email,
        string senha,
        string perfil,
        string tipo)
    {
        string sql = @"
        IF NOT EXISTS (SELECT 1 FROM dbo.Usuarios WHERE Email = @email)
        BEGIN
            INSERT INTO dbo.Usuarios (Nome, Email, SenhaHash, Perfil, TipoUsuario, Ativo, DataCadastro)
            VALUES (@nome, @email, @hash, @perfil, @tipo, 1, GETDATE())
        END";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@nome", nome);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@hash", BCrypt.Net.BCrypt.HashPassword(senha));
        cmd.Parameters.AddWithValue("@perfil", perfil);
        cmd.Parameters.AddWithValue("@tipo", tipo);

        cmd.ExecuteNonQuery();
    }
    private static void CriarUsuariosPadraoSeNecessario(string connectionString)
    {
        using var conn = new SqlConnection(connectionString);
        conn.Open();

        InserirUsuarioSeNaoExistir(conn, "Administrador", "admin@sgb.local", "admin123", "Administrador", "Funcionario");
        InserirUsuarioSeNaoExistir(conn, "Bibliotecário Padrão", "biblio@sgb.local", "biblio123", "Bibliotecario", "Funcionario");
        InserirUsuarioSeNaoExistir(conn, "Aluno Teste", "aluno@sgb.local", "aluno123", "Usuario", "Aluno");
    }
}