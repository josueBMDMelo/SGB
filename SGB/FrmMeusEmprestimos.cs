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
    public partial class FrmMeusEmprestimos : Form
    {
        private DataTable _emprestimos = new DataTable();

        public FrmMeusEmprestimos()
        {
            InitializeComponent();
        }

        private void FrmMeusEmprestimos_Load(object sender, EventArgs e)
        {
            cmbFiltro.Items.AddRange(new object[] { "Todos", "Em aberto", "Atrasados", "Devolvidos" });
            cmbFiltro.SelectedIndex = 0;

            CarregarEmprestimos();
        }

        private void CarregarEmprestimos()
        {
            string sql = @"
                SELECT
                    e.Id,
                    l.Titulo AS Livro,
                    ex.CodigoPatrimonio AS Patrimonio,
                    e.DataEmprestimo,
                    e.DataPrevistaDevolucao,
                    e.DataDevolucao,
                    CASE
                        WHEN e.DataDevolucao IS NOT NULL THEN 'Devolvido'
                        WHEN e.DataPrevistaDevolucao < CAST(SYSDATETIME() AS DATE) THEN 'Atrasado'
                        ELSE 'Aberto'
                    END AS StatusAtual
                FROM Emprestimos e
                JOIN Exemplares ex ON e.ExemplarId = ex.Id
                JOIN Livros l      ON ex.LivroId = l.Id
                WHERE e.UsuarioId = @usuarioId
                ORDER BY e.DataEmprestimo DESC";

            _emprestimos = ConexaoBanco.ExecutarConsulta(sql,
                new SqlParameter("@usuarioId", SessaoUsuario.Id));

            dgvEmprestimos.DataSource = _emprestimos.DefaultView;
            ConfigurarGrid();
            AplicarFiltro();
        }

        private void ConfigurarGrid()
        {
            dgvEmprestimos.Columns["Id"].Visible = false;
            dgvEmprestimos.Columns["Livro"].HeaderText = "Livro";
            dgvEmprestimos.Columns["Patrimonio"].HeaderText = "Patrimônio";
            dgvEmprestimos.Columns["DataEmprestimo"].HeaderText = "Emprestado em";
            dgvEmprestimos.Columns["DataPrevistaDevolucao"].HeaderText = "Devolução prevista";
            dgvEmprestimos.Columns["DataDevolucao"].HeaderText = "Devolvido em";
            dgvEmprestimos.Columns["StatusAtual"].HeaderText = "Status";
            dgvEmprestimos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEmprestimos.RowHeadersVisible = false;
            dgvEmprestimos.ReadOnly = true;
            dgvEmprestimos.AllowUserToAddRows = false;
        }

        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            var view = _emprestimos.DefaultView;

            view.RowFilter = cmbFiltro.SelectedItem?.ToString() switch
            {
                "Em aberto" => "StatusAtual = 'Aberto'",
                "Atrasados" => "StatusAtual = 'Atrasado'",
                "Devolvidos" => "StatusAtual = 'Devolvido'",
                _ => "",
            };
        }
    }
}

