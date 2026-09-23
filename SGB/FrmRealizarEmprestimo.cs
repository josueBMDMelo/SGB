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
                    var cmdInsert = new SqlCommand(
                        @"INSERT INTO Emprestimos (UsuarioId, ExemplarId, DataPrevistaDevolucao)
                          VALUES (@UsuarioId, @ExemplarId, @DataPrevista)",
                        conexao, transacao);
                    cmdInsert.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    cmdInsert.Parameters.AddWithValue("@ExemplarId", exemplarId);
                    cmdInsert.Parameters.AddWithValue("@DataPrevista", dataPrevista);
                    cmdInsert.ExecuteNonQuery();

                    var cmdUpdate = new SqlCommand(
                        "UPDATE Exemplares SET Status = 'Emprestado' WHERE Id = @ExemplarId",
                        conexao, transacao);
                    cmdUpdate.Parameters.AddWithValue("@ExemplarId", exemplarId);
                    cmdUpdate.ExecuteNonQuery();
                });

                MessageBox.Show("Empréstimo registrado.");
                CarregarExemplaresDisponiveis(); // o exemplar emprestado some da lista
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Não foi possível registrar o empréstimo: " + ex.Message);
            }
        }
    }
}
