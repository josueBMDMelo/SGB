using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using SGB.Data;

namespace SGB
{
    public partial class FrmRealizarEmprestimo : Form
    {
        private DataTable _usuarios = new DataTable();

        public FrmRealizarEmprestimo()
        {
            InitializeComponent();
        }

        private void FrmRealizarEmprestimo_Load(object sender, EventArgs e)
        {
            // RF02 / RN01: Somente Bibliotecário e Administrador operam o balcão
            if (!ControleAcesso.ValidarAcesso(this, "Bibliotecario", "Administrador"))
                return;

            CarregarUsuarios();
            CarregarExemplaresDisponiveis();
            dtpDataPrevista.Value = DateTime.Today.AddDays(7);
        }

        private void CarregarUsuarios()
        {
            string sql = "SELECT Id, Nome, TipoUsuario FROM Usuarios WHERE Ativo = 1 ORDER BY Nome";
            _usuarios = ConexaoBanco.ExecutarConsulta(sql);

            cmbUsuario.DataSource = _usuarios;
            cmbUsuario.DisplayMember = "Nome";
            cmbUsuario.ValueMember = "Id";
            cmbUsuario.SelectedIndex = -1;
        }

        private void CarregarExemplaresDisponiveis()
        {
            string sql = @"SELECT e.Id, (l.Titulo + ' - ' + e.CodigoPatrimonio) AS Descricao
                           FROM Exemplares e
                           JOIN Livros l ON e.LivroId = l.Id
                           WHERE e.Status = 'Disponivel' AND l.Ativo = 1
                           ORDER BY l.Titulo, e.CodigoPatrimonio";

            var exemplares = ConexaoBanco.ExecutarConsulta(sql);
            cmbExemplar.DataSource = exemplares;
            cmbExemplar.DisplayMember = "Descricao";
            cmbExemplar.ValueMember = "Id";
            cmbExemplar.SelectedIndex = -1;
        }

        private int ObterLimiteEmprestimos(string tipoUsuario)
        {
            return tipoUsuario switch
            {
                "Aluno" => 3,
                "Funcionario" => 5,
                "Professor" => 5,
                "Externo" => 2,
                _ => 3
            };
        }

        private decimal ObterValorCobrancaInicial(string tipoUsuario)
        {
            if (string.Equals(tipoUsuario, "Externo", StringComparison.OrdinalIgnoreCase))
            {
                var tabela = ConexaoBanco.ExecutarConsulta(
                    "SELECT Valor FROM Parametros WHERE Chave = @chave",
                    new SqlParameter("@chave", "EmprestimoExterno"));

                return tabela.Rows.Count > 0 ? Convert.ToDecimal(tabela.Rows[0]["Valor"]) : 5.00m;
            }

            return 0.00m;
        }

        private void btnEmprestar_Click(object sender, EventArgs e)
        {
            if (cmbUsuario.SelectedValue == null || cmbExemplar.SelectedValue == null)
            {
                MessageBox.Show("Selecione o usuário e o exemplar.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int usuarioId = Convert.ToInt32(cmbUsuario.SelectedValue);
            int exemplarId = Convert.ToInt32(cmbExemplar.SelectedValue);
            DateTime dataPrevista = dtpDataPrevista.Value.Date;

            if (dataPrevista <= DateTime.Today)
            {
                MessageBox.Show("A data prevista de devolução deve ser posterior à data de hoje.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView rowUsuario = (DataRowView)cmbUsuario.SelectedItem;
            string tipoUsuario = rowUsuario["TipoUsuario"]?.ToString() ?? "Aluno";

            var dtEmprestimosAtivos = ConexaoBanco.ExecutarConsulta(@"
                SELECT COUNT(1) AS Total
                FROM Emprestimos
                WHERE UsuarioId = @UsuarioId AND DataDevolucao IS NULL",
                new SqlParameter("@UsuarioId", usuarioId));

            int totalAbertos = Convert.ToInt32(dtEmprestimosAtivos.Rows[0]["Total"]);
            int limiteMaximo = ObterLimiteEmprestimos(tipoUsuario);

            if (totalAbertos >= limiteMaximo)
            {
                MessageBox.Show($"O usuário atingiu o limite de {limiteMaximo} empréstimo(s) simultâneo(s) para a categoria '{tipoUsuario}'.",
                                "Limite Excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal valorInicial = ObterValorCobrancaInicial(tipoUsuario);

            try
            {
                ConexaoBanco.ExecutarEmTransacao((conexao, transacao) =>
                {
                    using var cmdInsert = new SqlCommand(@"
                        INSERT INTO Emprestimos (UsuarioId, ExemplarId, DataEmprestimo, DataPrevistaDevolucao, Status)
                        VALUES (@UsuarioId, @ExemplarId, SYSDATETIME(), @DataPrevista, 'Aberto');
                        SELECT SCOPE_IDENTITY();", conexao, transacao);

                    cmdInsert.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    cmdInsert.Parameters.AddWithValue("@ExemplarId", exemplarId);
                    cmdInsert.Parameters.AddWithValue("@DataPrevista", dataPrevista);

                    int emprestimoId = Convert.ToInt32(cmdInsert.ExecuteScalar());

                    using var cmdUpdate = new SqlCommand(
                        "UPDATE Exemplares SET Status = 'Emprestado' WHERE Id = @ExemplarId",
                        conexao, transacao);
                    cmdUpdate.Parameters.AddWithValue("@ExemplarId", exemplarId);
                    cmdUpdate.ExecuteNonQuery();

                    if (valorInicial > 0)
                    {
                        using var cmdCobranca = new SqlCommand(@"
                            INSERT INTO Cobrancas (EmprestimoId, Tipo, Valor, Pago)
                            VALUES (@EmprestimoId, 'Emprestimo', @Valor, 1)", conexao, transacao);

                        cmdCobranca.Parameters.AddWithValue("@EmprestimoId", emprestimoId);
                        cmdCobranca.Parameters.AddWithValue("@Valor", valorInicial);
                        cmdCobranca.ExecuteNonQuery();
                    }
                });

                string msg = "Empréstimo registrado com sucesso.";
                if (valorInicial > 0)
                    msg += $"\nTaxa de empréstimo aplicada: {valorInicial:C2}";

                MessageBox.Show(msg, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarExemplaresDisponiveis();
                cmbExemplar.SelectedIndex = -1;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Não foi possível registrar o empréstimo: " + ex.Message,
                                "Erro de Banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}