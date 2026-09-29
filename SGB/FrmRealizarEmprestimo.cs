using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using SGB.Data;

namespace SGB
{
    public partial class FrmRealizarEmprestimo : Form
    {
        public FrmRealizarEmprestimo()
        {
            InitializeComponent();
        }

        private void FrmRealizarEmprestimo_Load(object sender, EventArgs e)
        {
            CarregarUsuarios();
            CarregarExemplaresDisponiveis();
            dtpDataPrevista.Value = DateTime.Today.AddDays(7);
        }

        private void CarregarUsuarios()
        {
            var usuarios = ConexaoBanco.ExecutarConsulta(
                "SELECT Id, Nome FROM Usuarios WHERE Ativo = 1 ORDER BY Nome");
            cmbUsuario.DataSource = usuarios;
            cmbUsuario.DisplayMember = "Nome";
            cmbUsuario.ValueMember = "Id";
        }

        private void CarregarExemplaresDisponiveis()
        {
            string sql = @"SELECT e.Id, (l.Titulo + ' - ' + e.CodigoPatrimonio) AS Descricao
                           FROM Exemplares e
                           JOIN Livros l ON e.LivroId = l.Id
                           WHERE e.Status = 'Disponivel'
                           ORDER BY l.Titulo, e.CodigoPatrimonio";

            var exemplares = ConexaoBanco.ExecutarConsulta(sql);
            cmbExemplar.DataSource = exemplares;
            cmbExemplar.DisplayMember = "Descricao";
            cmbExemplar.ValueMember = "Id";
        }

        private void btnEmprestar_Click(object sender, EventArgs e)
        {
            if (cmbUsuario.SelectedValue == null || cmbExemplar.SelectedValue == null)
            {
                MessageBox.Show("Selecione o usuário e o exemplar.");
                return;
            }

            int usuarioId = Convert.ToInt32(cmbUsuario.SelectedValue);
            int exemplarId = Convert.ToInt32(cmbExemplar.SelectedValue);
            DateTime dataPrevista = dtpDataPrevista.Value;

            try
            {
                ConexaoBanco.ExecutarEmTransacao((conexao, transacao) =>
                {
                    // Verifica o tipo do usuário
                    var cmdUsuario = new SqlCommand(
                        "SELECT TipoUsuario FROM Usuarios WHERE Id = @Id",
                        conexao, transacao);

                    cmdUsuario.Parameters.AddWithValue("@Id", usuarioId);

                    string tipoUsuario = Convert.ToString(cmdUsuario.ExecuteScalar());

                    // Cria o empréstimo
                    var cmdEmprestimo = new SqlCommand(
                        @"INSERT INTO Emprestimos 
                  (UsuarioId, ExemplarId, DataPrevistaDevolucao)
                  VALUES 
                  (@UsuarioId, @ExemplarId, @DataPrevista);
                  SELECT SCOPE_IDENTITY();",
                        conexao, transacao);

                    cmdEmprestimo.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    cmdEmprestimo.Parameters.AddWithValue("@ExemplarId", exemplarId);
                    cmdEmprestimo.Parameters.AddWithValue("@DataPrevista", dataPrevista);

                    int emprestimoId = Convert.ToInt32(
                        cmdEmprestimo.ExecuteScalar());

                    // Atualiza o exemplar
                    var cmdUpdate = new SqlCommand(
                        "UPDATE Exemplares SET Status = 'Emprestado' WHERE Id = @Id",
                        conexao, transacao);

                    cmdUpdate.Parameters.AddWithValue("@Id", exemplarId);
                    cmdUpdate.ExecuteNonQuery();

                    // Cobra R$ 5,00 somente de usuário externo
                    if (tipoUsuario == "Externo")
                    {
                        var cmdCobranca = new SqlCommand(
                            @"INSERT INTO Cobrancas
                      (EmprestimoId, Tipo, Valor)
                      VALUES
                      (@EmprestimoId, 'Inicial', 5.00)",
                            conexao, transacao);

                        cmdCobranca.Parameters.AddWithValue(
                            "@EmprestimoId", emprestimoId);

                        cmdCobranca.ExecuteNonQuery();
                    }
                });

                MessageBox.Show("Empréstimo registrado.");

                CarregarExemplaresDisponiveis();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erro ao registrar empréstimo: " + ex.Message);
            }
        }
    }
}
