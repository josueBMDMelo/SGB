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
            CarregarCategorias();
            CarregarLivros();
        }

        private void CarregarCategorias()
        {
            string sql = @"
                SELECT Id, Nome
                FROM Categorias
                WHERE Ativo = 1
                ORDER BY Nome";

            DataTable dt = ConexaoBanco.ExecutarConsulta(sql);

            comboBoxCategoria.DataSource = dt;
            comboBoxCategoria.DisplayMember = "Nome";
            comboBoxCategoria.ValueMember = "Id";
            comboBoxCategoria.SelectedIndex = -1;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("Informe o título do livro.");
                return;
            }

            if (comboBoxCategoria.SelectedValue == null)
            {
                MessageBox.Show("Selecione uma categoria.");
                return;
            }

            string sql = @"
                INSERT INTO Livros
                (
                    Titulo,
                    CategoriaId,
                    Autor
                )
                VALUES
                (
                    @Titulo,
                    @CategoriaId,
                    @Autor
                )";

            var parametros = new[]
            {
                new SqlParameter("@Titulo", txtTitulo.Text.Trim()),
                new SqlParameter("@CategoriaId", Convert.ToInt32(comboBoxCategoria.SelectedValue)),
                new SqlParameter("@Autor",
                    string.IsNullOrWhiteSpace(txtAutor.Text)
                        ? DBNull.Value
                        : txtAutor.Text.Trim())
            };

            try
            {
                ConexaoBanco.ExecutarComando(sql, parametros);

                txtTitulo.Clear();
                txtAutor.Clear();
                comboBoxCategoria.SelectedIndex = -1;

                CarregarLivros();

                MessageBox.Show("Livro cadastrado com sucesso!");
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
            string sql = @"
                SELECT
                    l.Id,
                    l.Titulo,
                    c.Nome AS Categoria,
                    l.Autor
                FROM Livros l
                INNER JOIN Categorias c
                    ON c.Id = l.CategoriaId
                WHERE l.Ativo = 1
                ORDER BY l.Titulo";

            dgvLivros.DataSource = ConexaoBanco.ExecutarConsulta(sql);

            ConfigurarGrid();
        }

        private void ConfigurarGrid()
        {
            if (dgvLivros.Columns.Count == 0)
                return;

            dgvLivros.Columns["Id"].Visible = false;
            dgvLivros.Columns["Titulo"].HeaderText = "Título";
            dgvLivros.Columns["Categoria"].HeaderText = "Categoria";
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