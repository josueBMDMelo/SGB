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
    public partial class FrmCadastroLivro : Form
    {
        public FrmCadastroLivro()
        {
            InitializeComponent();
        }

        private void FrmCadastroLivro_Load(object sender, EventArgs e)
        {
            CarregarLivros();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("Informe o título do livro.");
                return;
            }

            string sql = "INSERT INTO Livros (Titulo, Autor) VALUES (@Titulo, @Autor)";
            var parametros = new[]
            {
                new SqlParameter("@Titulo", txtTitulo.Text.Trim()),
                new SqlParameter("@Autor", string.IsNullOrWhiteSpace(txtAutor.Text) ? DBNull.Value : txtAutor.Text.Trim())
            };

            try
            {
                ConexaoBanco.ExecutarComando(sql, parametros);
                txtTitulo.Clear();
                txtAutor.Clear();
                CarregarLivros();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Não foi possível cadastrar: " + ex.Message);
            }
        }
        private void btnListar_Click(object sender, EventArgs e)
        {
            CarregarLivros();
        }

        private void CarregarLivros()
        {
            string sql = "SELECT Id, Titulo, Autor FROM Livros WHERE Ativo = 1 ORDER BY Titulo";
            dgvLivros.DataSource = ConexaoBanco.ExecutarConsulta(sql);
            ConfigurarGrid();
        }

        private void ConfigurarGrid()
        {
            dgvLivros.Columns["Id"].Visible = false;
            dgvLivros.Columns["Titulo"].HeaderText = "Título";
            dgvLivros.Columns["Autor"].HeaderText = "Autor";

            dgvLivros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLivros.RowHeadersVisible = false;
            dgvLivros.AllowUserToAddRows = false;
            dgvLivros.AllowUserToDeleteRows = false;
            dgvLivros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLivros.MultiSelect = false;
        }

    }
}
