using Microsoft.Data.SqlClient;
using SGB.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGB
{
    public partial class FrmCadastroExemplar : Form
    {
        public FrmCadastroExemplar()
        {
            InitializeComponent();
        }

        private void FrmCadastroExemplar_Load(object sender, EventArgs e)
        {
            CarregarLivrosNoCombo();
            CarregarExemplares();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (cmbLivro.SelectedValue == null || string.IsNullOrWhiteSpace(txtCodigoPatrimonio.Text))
            {
                MessageBox.Show("Selecione o livro e informe o código de patrimônio.");
                return;
            }

            int livroId = Convert.ToInt32(cmbLivro.SelectedValue);

            try
            {
                ConexaoBanco.ExecutarComando(
                    "INSERT INTO Exemplares (LivroId, CodigoPatrimonio) VALUES (@LivroId, @Codigo)",
                    new SqlParameter("@LivroId", livroId),
                    new SqlParameter("@Codigo", txtCodigoPatrimonio.Text.Trim()));

                MessageBox.Show("Exemplar cadastrado.");
                txtCodigoPatrimonio.Clear();
                CarregarExemplares();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Não foi possível cadastrar (código de patrimônio já usado?): " + ex.Message);
            }
        }
        private void CarregarExemplares()
        {
            string sql = @"SELECT e.Id, l.Titulo, e.CodigoPatrimonio, e.Status
                           FROM Exemplares e
                           JOIN Livros l ON e.LivroId = l.Id
                           ORDER BY l.Titulo, e.CodigoPatrimonio";

            dgvExemplares.DataSource = ConexaoBanco.ExecutarConsulta(sql);
            dgvExemplares.Columns["Id"].Visible = false;
            dgvExemplares.Columns["Titulo"].HeaderText = "Livro";
            dgvExemplares.Columns["CodigoPatrimonio"].HeaderText = "Patrimônio";
            dgvExemplares.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void CarregarLivrosNoCombo()
        {
            var livros = ConexaoBanco.ExecutarConsulta("SELECT Id, Titulo FROM Livros WHERE Ativo = 1 ORDER BY Titulo");
            cmbLivro.DataSource = livros;
            cmbLivro.DisplayMember = "Titulo";
            cmbLivro.ValueMember = "Id";
        }
    }
}
