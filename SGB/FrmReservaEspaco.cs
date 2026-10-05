using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using SGB.Data;

namespace SGB
{
    public partial class FrmReservaEspaco : Form
    {
        private DataTable _usuarios = new DataTable();
        private DataTable _espacos = new DataTable();
        private DataTable _reservas = new DataTable();

        public FrmReservaEspaco()
        {
            InitializeComponent();

            dtpData.Format = DateTimePickerFormat.Custom;
            dtpData.CustomFormat = "dd/MM/yyyy";
            dtpData.ShowUpDown = false;

            dtpHoraInicio.Format = DateTimePickerFormat.Custom;
            dtpHoraInicio.CustomFormat = "HH:mm";
            dtpHoraInicio.ShowUpDown = true;

            dtpHoraFim.Format = DateTimePickerFormat.Custom;
            dtpHoraFim.CustomFormat = "HH:mm";
            dtpHoraFim.ShowUpDown = true;
        }

        private void FrmReservaEspaco_Load(object sender, EventArgs e)
        {
            if (!ControleAcesso.ValidarAcesso(this, "Usuario", "Bibliotecario", "Administrador"))
                return;

            ConfigurarFiltros();
            CarregarEspacos();
            ConfigurarInterfacePorPerfil();
            CarregarReservas();
        }

        private void ConfigurarInterfacePorPerfil()
        {
            if (ControleAcesso.EhUsuarioComum())
            {
                cmbUsuario.Enabled = false;
                _usuarios = ConexaoBanco.ExecutarConsulta(@"
                    SELECT Id, Nome 
                    FROM Usuarios 
                    WHERE Id = @Id",
                    new SqlParameter("@Id", SessaoUsuario.Id));

                cmbUsuario.DataSource = _usuarios;
                cmbUsuario.DisplayMember = "Nome";
                cmbUsuario.ValueMember = "Id";

                btnConcluirReserva.Visible = false;
            }
            else
            {
                cmbUsuario.Enabled = true;
                CarregarUsuarios();
                btnConcluirReserva.Visible = true;
            }
        }

        private void ConfigurarFiltros()
        {
            cmbFiltroStatus.Items.Clear();
            cmbFiltroStatus.Items.Add("Todos");
            cmbFiltroStatus.Items.Add("Pendente");
            cmbFiltroStatus.Items.Add("Confirmada");
            cmbFiltroStatus.Items.Add("Cancelada");
            cmbFiltroStatus.Items.Add("Concluida");
            cmbFiltroStatus.SelectedIndex = 0;
        }

        private void CarregarUsuarios()
        {
            _usuarios = ConexaoBanco.ExecutarConsulta(@"
                SELECT Id, Nome
                FROM Usuarios
                WHERE Ativo = 1
                ORDER BY Nome");

            cmbUsuario.DataSource = _usuarios;
            cmbUsuario.DisplayMember = "Nome";
            cmbUsuario.ValueMember = "Id";
            cmbUsuario.SelectedIndex = -1;
        }

        private void CarregarEspacos()
        {
            _espacos = ConexaoBanco.ExecutarConsulta(@"
                SELECT Id, Nome
                FROM Espacos
                WHERE Ativo = 1
                ORDER BY Nome");

            cmbEspaco.DataSource = _espacos;
            cmbEspaco.DisplayMember = "Nome";
            cmbEspaco.ValueMember = "Id";
            cmbEspaco.SelectedIndex = -1;
        }

        private void CarregarReservas()
        {
            string sql = @"
                SELECT
                    r.Id,
                    u.Nome AS Usuario,
                    e.Nome AS Espaco,
                    r.DataHoraInicio,
                    r.DataHoraFim,
                    r.Status,
                    r.UsuarioId
                FROM ReservasEspaco r
                INNER JOIN Usuarios u ON r.UsuarioId = u.Id
                INNER JOIN Espacos e  ON r.EspacoId = e.Id";

            if (ControleAcesso.EhUsuarioComum())
            {
                sql += " WHERE r.UsuarioId = @UsuarioLogadoId";
            }

            sql += " ORDER BY r.DataHoraInicio DESC";

            var parametros = ControleAcesso.EhUsuarioComum()
                ? new[] { new SqlParameter("@UsuarioLogadoId", SessaoUsuario.Id) }
                : Array.Empty<SqlParameter>();

            _reservas = ConexaoBanco.ExecutarConsulta(sql, parametros);

            dgvMinhasReservas.DataSource = _reservas.DefaultView;

            ConfigurarGrid();
            AplicarFiltro();
        }

        private void ConfigurarGrid()
        {
            if (dgvMinhasReservas.Columns.Count == 0)
                return;

            dgvMinhasReservas.Columns["Id"].Visible = false;
            if (dgvMinhasReservas.Columns.Contains("UsuarioId"))
                dgvMinhasReservas.Columns["UsuarioId"].Visible = false;

            dgvMinhasReservas.Columns["Usuario"].HeaderText = "Usuário";
            dgvMinhasReservas.Columns["Espaco"].HeaderText = "Espaço";
            dgvMinhasReservas.Columns["DataHoraInicio"].HeaderText = "Início";
            dgvMinhasReservas.Columns["DataHoraFim"].HeaderText = "Fim";
            dgvMinhasReservas.Columns["Status"].HeaderText = "Status";

            dgvMinhasReservas.Columns["DataHoraInicio"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvMinhasReservas.Columns["DataHoraFim"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            dgvMinhasReservas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMinhasReservas.RowHeadersVisible = false;
            dgvMinhasReservas.ReadOnly = true;
            dgvMinhasReservas.AllowUserToAddRows = false;
            dgvMinhasReservas.MultiSelect = false;
            dgvMinhasReservas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnReservar_Click(object sender, EventArgs e)
        {
            int usuarioId;

            if (ControleAcesso.EhUsuarioComum())
            {
                usuarioId = SessaoUsuario.Id;
            }
            else
            {
                if (cmbUsuario.SelectedValue == null)
                {
                    MostrarMensagem("Selecione o usuário para quem a reserva será feita.", Color.Red);
                    return;
                }
                usuarioId = Convert.ToInt32(cmbUsuario.SelectedValue);
            }

            if (cmbEspaco.SelectedValue == null)
            {
                MostrarMensagem("Selecione um espaço.", Color.Red);
                return;
            }

            int espacoId = Convert.ToInt32(cmbEspaco.SelectedValue);

            DateTime inicio = dtpData.Value.Date + dtpHoraInicio.Value.TimeOfDay;
            DateTime fim = dtpData.Value.Date + dtpHoraFim.Value.TimeOfDay;

            if (inicio < DateTime.Now)
            {
                MostrarMensagem("Não é permitido agendar reservas em data ou horário retroativo.", Color.Red);
                return;
            }

            if (fim <= inicio)
            {
                MostrarMensagem("A hora final deve ser maior que a hora inicial.", Color.Red);
                return;
            }

            var conflito = ConexaoBanco.ExecutarConsulta(@"
                SELECT 1
                FROM ReservasEspaco
                WHERE EspacoId = @espacoId
                  AND Status IN ('Pendente', 'Confirmada')
                  AND DataHoraInicio < @fim
                  AND DataHoraFim > @inicio",
                new SqlParameter("@espacoId", espacoId),
                new SqlParameter("@inicio", inicio),
                new SqlParameter("@fim", fim));

            if (conflito.Rows.Count > 0)
            {
                MostrarMensagem("O espaço já possui uma reserva confirmada ou pendente nesse horário.", Color.Red);
                return;
            }

            ConexaoBanco.ExecutarComando(@"
                INSERT INTO ReservasEspaco (UsuarioId, EspacoId, DataHoraInicio, DataHoraFim, Status)
                VALUES (@usuarioId, @espacoId, @inicio, @fim, 'Confirmada')",
                new SqlParameter("@usuarioId", usuarioId),
                new SqlParameter("@espacoId", espacoId),
                new SqlParameter("@inicio", inicio),
                new SqlParameter("@fim", fim));

            MostrarMensagem("Reserva realizada com sucesso.", Color.Green);

            LimparCampos();
            CarregarReservas();
        }

        private void btnCancelarReserva_Click(object sender, EventArgs e)
        {
            if (dgvMinhasReservas.CurrentRow == null)
            {
                MostrarMensagem("Selecione uma reserva para cancelar.", Color.Red);
                return;
            }

            int reservaId = Convert.ToInt32(dgvMinhasReservas.CurrentRow.Cells["Id"].Value);
            string status = Convert.ToString(dgvMinhasReservas.CurrentRow.Cells["Status"].Value);

            if (status == "Concluida")
            {
                MostrarMensagem("Uma reserva concluída não pode ser cancelada.", Color.Red);
                return;
            }

            if (status == "Cancelada")
            {
                MostrarMensagem("Essa reserva já está cancelada.", Color.Red);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "Tem certeza que deseja cancelar esta reserva?",
                "Cancelar reserva",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes)
                return;

            ConexaoBanco.ExecutarComando(@"
                UPDATE ReservasEspaco
                SET Status = 'Cancelada'
                WHERE Id = @id AND Status IN ('Pendente', 'Confirmada')",
                new SqlParameter("@id", reservaId));

            MostrarMensagem("Reserva cancelada com sucesso.", Color.Green);
            CarregarReservas();
        }

        private void cmbFiltroStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            if (_reservas == null)
                return;

            var view = _reservas.DefaultView;
            string filtro = cmbFiltroStatus.SelectedItem?.ToString();

            view.RowFilter = filtro switch
            {
                "Pendente" => "Status = 'Pendente'",
                "Confirmada" => "Status = 'Confirmada'",
                "Cancelada" => "Status = 'Cancelada'",
                "Concluida" => "Status = 'Concluida'",
                _ => "",
            };
        }

        private void LimparCampos()
        {
            if (!ControleAcesso.EhUsuarioComum())
            {
                cmbUsuario.SelectedIndex = -1;
            }

            cmbEspaco.SelectedIndex = -1;
            dtpData.Value = DateTime.Now;
            dtpHoraInicio.Value = DateTime.Now;
            dtpHoraFim.Value = DateTime.Now.AddHours(1);
        }

        private void MostrarMensagem(string mensagem, Color cor)
        {
            labelMensagem.ForeColor = cor;
            labelMensagem.Text = mensagem;
        }

        private void btnConcluirReserva_Click(object sender, EventArgs e)
        {
            if (ControleAcesso.EhUsuarioComum())
            {
                MostrarMensagem("Apenas a equipe da biblioteca pode concluir reservas de salas.", Color.Red);
                return;
            }

            if (dgvMinhasReservas.CurrentRow == null)
            {
                MostrarMensagem("Selecione uma reserva para concluir.", Color.Red);
                return;
            }

            int reservaId = Convert.ToInt32(dgvMinhasReservas.CurrentRow.Cells["Id"].Value);
            string status = Convert.ToString(dgvMinhasReservas.CurrentRow.Cells["Status"].Value);

            if (status != "Confirmada")
            {
                MostrarMensagem("Somente reservas confirmadas podem ser concluídas.", Color.Red);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "Tem certeza que deseja concluir esta reserva?\n\nIsso irá marcar a sala como concluída e ela estará liberada para outra reserva.\n\nEsta ação não pode ser desfeita.",
                "Concluir reserva",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (resultado != DialogResult.Yes)
                return;

            ConexaoBanco.ExecutarComando(@"
                UPDATE ReservasEspaco
                SET Status = 'Concluida'
                WHERE Id = @id AND Status = 'Confirmada'",
                new SqlParameter("@id", reservaId));

            MostrarMensagem("Reserva concluída com sucesso. O espaço está liberado.", Color.Green);
            CarregarReservas();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarReservas();
            MostrarMensagem("Lista de reservas atualizada.", Color.Green);
        }
    }
}