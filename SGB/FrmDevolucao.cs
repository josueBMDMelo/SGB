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
    public partial class FrmDevolucao : Form
    {
        private DataTable _emprestimos = new DataTable();
        private int _emprestimoIdSelecionado = 0;
        private decimal _multaDiaria = 0m;

        public FrmDevolucao()
        {
            InitializeComponent();
        }
        private void FrmDevolucao_Load(object sender, EventArgs e)
        {
            cmbFiltro.Items.AddRange(new object[] { "Todos", "Em aberto", "Atrasados" });
            cmbFiltro.SelectedIndex = 0;

            CarregarMultaDiaria();
            CarregarEmprestimosAbertos();
        }
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            AplicarFiltro();
        }


        private void CarregarMultaDiaria()
        {
            var tabela = ConexaoBanco.ExecutarConsulta(
                "SELECT Valor FROM Parametros WHERE Chave = @chave",
                new SqlParameter("@chave", "MultaDiaria"));

            _multaDiaria = tabela.Rows.Count > 0
                ? Convert.ToDecimal(tabela.Rows[0]["Valor"])
                : 0m;
        }

        private void CarregarEmprestimosAbertos()
        {
            string sql = @"
                SELECT
                    e.Id,
                    u.Nome AS Usuario,
                    l.Titulo AS Livro,
                    ex.CodigoPatrimonio AS Patrimonio,
                    e.DataPrevistaDevolucao,
                    CASE
                        WHEN e.DataPrevistaDevolucao < CAST(SYSDATETIME() AS DATE) THEN 'Atrasado'
                        ELSE 'Aberto'
                    END AS StatusAtual
                FROM Emprestimos e
                JOIN Usuarios u    ON e.UsuarioId = u.Id
                JOIN Exemplares ex ON e.ExemplarId = ex.Id
                JOIN Livros l      ON ex.LivroId = l.Id
                WHERE e.DataDevolucao IS NULL
                ORDER BY e.DataPrevistaDevolucao";

            _emprestimos = ConexaoBanco.ExecutarConsulta(sql);
            dgvEmprestimosAbertos.DataSource = _emprestimos.DefaultView;
            ConfigurarGrid();
            AplicarFiltro();
        }

        private void ConfigurarGrid()
        {
            dgvEmprestimosAbertos.Columns["Id"].Visible = false;
            dgvEmprestimosAbertos.Columns["Usuario"].HeaderText = "Usuário";
            dgvEmprestimosAbertos.Columns["Livro"].HeaderText = "Livro";
            dgvEmprestimosAbertos.Columns["Patrimonio"].HeaderText = "Patrimônio";
            dgvEmprestimosAbertos.Columns["DataPrevistaDevolucao"].HeaderText = "Devolução prevista";
            dgvEmprestimosAbertos.Columns["StatusAtual"].HeaderText = "Status";
            dgvEmprestimosAbertos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEmprestimosAbertos.RowHeadersVisible = false;
            dgvEmprestimosAbertos.ReadOnly = true;
            dgvEmprestimosAbertos.AllowUserToAddRows = false;
            dgvEmprestimosAbertos.MultiSelect = false;
            dgvEmprestimosAbertos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }


        private void AplicarFiltro()
        {
            _emprestimos.DefaultView.RowFilter = cmbFiltro.SelectedItem?.ToString() switch
            {
                "Em aberto" => "StatusAtual = 'Aberto'",
                "Atrasados" => "StatusAtual = 'Atrasado'",
                _ => "", // "Todos"
            };
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (dgvEmprestimosAbertos.CurrentRow == null)
            {
                labelMensagem.ForeColor = System.Drawing.Color.Red;
                labelMensagem.Text = "Selecione um empréstimo na lista.";
                return;
            }

            var linha = dgvEmprestimosAbertos.CurrentRow;
            _emprestimoIdSelecionado = Convert.ToInt32(linha.Cells["Id"].Value);

            string usuario = linha.Cells["Usuario"].Value.ToString();
            string livro = linha.Cells["Livro"].Value.ToString();
            DateTime dataPrevista = Convert.ToDateTime(linha.Cells["DataPrevistaDevolucao"].Value);

            int diasAtraso = Math.Max(0, (DateTime.Today - dataPrevista.Date).Days);
            decimal valorMulta = diasAtraso * _multaDiaria;

            lblUsuario.Text = $"Usuário: {usuario}";
            lblLivro.Text = $"Livro: {livro}";
            lblDiasAtraso.Text = $"Dias de atraso: {diasAtraso}";
            lblValorMulta.Text = $"Valor da multa: {valorMulta:C2}";

            chkMultaPaga.Visible = diasAtraso > 0;
            chkMultaPaga.Checked = false;

            labelMensagem.Text = "";
            btnConfirmarDevolucao.Enabled = true;
        }

        private void btnConfirmarDevolucao_Click(object sender, EventArgs e)
        {
            if (_emprestimoIdSelecionado == 0) return;

            var linha = dgvEmprestimosAbertos.CurrentRow;
            DateTime dataPrevista = Convert.ToDateTime(linha.Cells["DataPrevistaDevolucao"].Value);
            int exemplarPatrimonio = 0;
            string patrimonio = linha.Cells["Patrimonio"].Value.ToString();

            int diasAtraso = Math.Max(0, (DateTime.Today - dataPrevista.Date).Days);
            decimal valorMulta = diasAtraso * _multaDiaria;
            string statusFinal = diasAtraso > 0 && !chkMultaPaga.Checked ? "ComPendencia" : "Devolvido";

            try
            {
                ConexaoBanco.ExecutarEmTransacao((conexao, transacao) =>
                {
                    using var cmdDevolucao = new SqlCommand(@"
                        UPDATE Emprestimos
                        SET DataDevolucao = SYSDATETIME(),
                            DiasAtraso = @diasAtraso,
                            Status = @status
                        WHERE Id = @id", conexao, transacao);
                    cmdDevolucao.Parameters.AddWithValue("@diasAtraso", diasAtraso);
                    cmdDevolucao.Parameters.AddWithValue("@status", statusFinal);
                    cmdDevolucao.Parameters.AddWithValue("@id", _emprestimoIdSelecionado);
                    cmdDevolucao.ExecuteNonQuery();

                    using var cmdExemplar = new SqlCommand(@"
                        UPDATE Exemplares
                        SET Status = 'Disponivel'
                        WHERE CodigoPatrimonio = @patrimonio", conexao, transacao);
                    cmdExemplar.Parameters.AddWithValue("@patrimonio", patrimonio);
                    cmdExemplar.ExecuteNonQuery();

                    if (diasAtraso > 0)
                    {
                        using var cmdMulta = new SqlCommand(@"
                            INSERT INTO Cobrancas (EmprestimoId, Tipo, Valor, Pago)
                            VALUES (@emprestimoId, 'Multa', @valor, @pago)", conexao, transacao);
                        cmdMulta.Parameters.AddWithValue("@emprestimoId", _emprestimoIdSelecionado);
                        cmdMulta.Parameters.AddWithValue("@valor", valorMulta);
                        cmdMulta.Parameters.AddWithValue("@pago", chkMultaPaga.Checked);
                        cmdMulta.ExecuteNonQuery();
                    }
                });

                labelMensagem.ForeColor = System.Drawing.Color.Green;
                labelMensagem.Text = "Devolução registrada com sucesso.";
                _emprestimoIdSelecionado = 0;
                btnConfirmarDevolucao.Enabled = false;
                LimparPainelSelecao();
                CarregarEmprestimosAbertos();
            }
            catch (Exception ex)
            {
                labelMensagem.ForeColor = System.Drawing.Color.Red;
                labelMensagem.Text = "Erro ao registrar devolução: " + ex.Message;
            }
        }

        private void LimparPainelSelecao()
        {
            lblUsuario.Text = "";
            lblLivro.Text = "";
            lblDiasAtraso.Text = "";
            lblValorMulta.Text = "";
            chkMultaPaga.Visible = false;
        }

    }
}