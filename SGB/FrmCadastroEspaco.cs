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
    public partial class FrmCadastroEspaco : Form
    {
        public FrmCadastroEspaco()
        {
            InitializeComponent();
        }

        private void FrmCadastroEspaco_Load(object sender, EventArgs e)
        {
            CarregarEspacos();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Informe o nome do espaço.");
                txtNome.Focus();
                return;
            }

            if (!int.TryParse(txtCapacidade.Text, out int capacidade) || capacidade <= 0)
            {
                MessageBox.Show("Informe uma capacidade válida maior que zero.");
                txtCapacidade.Focus();
                return;
            }

            string sql = @"
                INSERT INTO Espacos
                    (Nome, Tipo, Localizacao, Capacidade)
                VALUES
                    (@Nome, @Tipo, @Localizacao, @Capacidade)";

            var parametros = new[]
            {
                new SqlParameter("@Nome", txtNome.Text.Trim()),

                new SqlParameter("@Tipo",
                    string.IsNullOrWhiteSpace(txtTipo.Text)
                        ? DBNull.Value
                        : txtTipo.Text.Trim()),

                new SqlParameter("@Localizacao",
                    string.IsNullOrWhiteSpace(txtLocalizacao.Text)
                        ? DBNull.Value
                        : txtLocalizacao.Text.Trim()),

                new SqlParameter("@Capacidade", capacidade)
            };

            try
            {
                ConexaoBanco.ExecutarComando(sql, parametros);

                MessageBox.Show("Espaço cadastrado com sucesso.");

                LimparCampos();
                CarregarEspacos();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Não foi possível cadastrar o espaço: " + ex.Message);
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            CarregarEspacos();
        }

        private void CarregarEspacos()
        {
            string sql = @"
                SELECT
                    Id,
                    Nome,
                    Tipo,
                    Localizacao,
                    Capacidade
                FROM Espacos
                WHERE Ativo = 1
                ORDER BY Nome";

            dgvEspacos.DataSource = ConexaoBanco.ExecutarConsulta(sql);

            ConfigurarGrid();
        }

        private void ConfigurarGrid()
        {
            dgvEspacos.Columns["Id"].Visible = false;

            dgvEspacos.Columns["Nome"].HeaderText = "Nome";
            dgvEspacos.Columns["Tipo"].HeaderText = "Tipo";
            dgvEspacos.Columns["Localizacao"].HeaderText = "Localização";
            dgvEspacos.Columns["Capacidade"].HeaderText = "Capacidade";

            dgvEspacos.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvEspacos.RowHeadersVisible = false;
            dgvEspacos.AllowUserToAddRows = false;
            dgvEspacos.AllowUserToDeleteRows = false;
            dgvEspacos.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvEspacos.MultiSelect = false;
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtTipo.Clear();
            txtLocalizacao.Clear();
            txtCapacidade.Clear();

            txtNome.Focus();
        }
    }
}
