using Microsoft.Data.SqlClient;
using SGB.Data;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

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

            // Data
            dtpData.Format = DateTimePickerFormat.Custom;
            dtpData.CustomFormat = "dd/MM/yyyy";
            dtpData.ShowUpDown = false;

            // Hora início
            dtpHoraInicio.Format = DateTimePickerFormat.Custom;
            dtpHoraInicio.CustomFormat = "HH:mm";
            dtpHoraInicio.ShowUpDown = true;

            // Hora fim
            dtpHoraFim.Format = DateTimePickerFormat.Custom;
            dtpHoraFim.CustomFormat = "HH:mm";
            dtpHoraFim.ShowUpDown = true;
        }

        private void FrmReservaEspaco_Load(object sender, EventArgs e)
        {
            // Somente Bibliotecário e Administrador
            if (SessaoUsuario.Perfil != "Bibliotecario" &&
                SessaoUsuario.Perfil != "Administrador")
            {
                MessageBox.Show(
                    "Você não possui permissão para acessar o gerenciamento de reservas.",
                    "Acesso negado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                Close();
                return;
            }

            ConfigurarFiltros();
            CarregarUsuarios();
            CarregarEspacos();
            CarregarReservas();
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
                SELECT
                    Id,
                    Nome
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
                SELECT
                    Id,
                    Nome
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
                    r.Status
                FROM ReservasEspaco r
                INNER JOIN Usuarios u
                    ON r.UsuarioId = u.Id
                INNER JOIN Espacos e
                    ON r.EspacoId = e.Id
                ORDER BY r.DataHoraInicio DESC";

            _reservas = ConexaoBanco.ExecutarConsulta(sql);

            dgvMinhasReservas.DataSource = _reservas.DefaultView;

            ConfigurarGrid();
            AplicarFiltro();
        }

        private void ConfigurarGrid()
        {
            if (dgvMinhasReservas.Columns.Count == 0)
                return;

            dgvMinhasReservas.Columns["Id"].Visible = false;

            dgvMinhasReservas.Columns["Usuario"].HeaderText =
                "Usuário";

            dgvMinhasReservas.Columns["Espaco"].HeaderText =
                "Espaço";

            dgvMinhasReservas.Columns["DataHoraInicio"].HeaderText =
                "Início";

            dgvMinhasReservas.Columns["DataHoraFim"].HeaderText =
                "Fim";

            dgvMinhasReservas.Columns["Status"].HeaderText =
                "Status";

            dgvMinhasReservas.Columns["DataHoraInicio"]
                .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            dgvMinhasReservas.Columns["DataHoraFim"]
                .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            dgvMinhasReservas.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvMinhasReservas.RowHeadersVisible = false;
            dgvMinhasReservas.ReadOnly = true;
            dgvMinhasReservas.AllowUserToAddRows = false;
            dgvMinhasReservas.MultiSelect = false;

            dgvMinhasReservas.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnReservar_Click(object sender, EventArgs e)
        {
            if (cmbUsuario.SelectedValue == null)
            {
                MostrarMensagem(
                    "Selecione o usuário para quem a reserva será feita.",
                    Color.Red);

                return;
            }

            if (cmbEspaco.SelectedValue == null)
            {
                MostrarMensagem(
                    "Selecione um espaço.",
                    Color.Red);

                return;
            }

            int usuarioId =
                Convert.ToInt32(cmbUsuario.SelectedValue);

            int espacoId =
                Convert.ToInt32(cmbEspaco.SelectedValue);

            DateTime inicio =
                dtpData.Value.Date +
                dtpHoraInicio.Value.TimeOfDay;

            DateTime fim =
                dtpData.Value.Date +
                dtpHoraFim.Value.TimeOfDay;

            // Verifica horário
            if (fim <= inicio)
            {
                MostrarMensagem(
                    "A hora final deve ser maior que a hora inicial.",
                    Color.Red);

                return;
            }

            // Verifica conflito de horário
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
                MostrarMensagem(
                    "O espaço já possui uma reserva nesse horário.",
                    Color.Red);

                return;
            }

            // Cria a reserva
            ConexaoBanco.ExecutarComando(@"
                INSERT INTO ReservasEspaco
                (
                    UsuarioId,
                    EspacoId,
                    DataHoraInicio,
                    DataHoraFim,
                    Status
                )
                VALUES
                (
                    @usuarioId,
                    @espacoId,
                    @inicio,
                    @fim,
                    'Confirmada'
                )",
                new SqlParameter("@usuarioId", usuarioId),
                new SqlParameter("@espacoId", espacoId),
                new SqlParameter("@inicio", inicio),
                new SqlParameter("@fim", fim));

            MostrarMensagem(
                "Reserva realizada com sucesso.",
                Color.Green);

            LimparCampos();
            CarregarReservas();
        }

        private void btnCancelarReserva_Click(object sender, EventArgs e)
        {
            if (dgvMinhasReservas.CurrentRow == null)
            {
                MostrarMensagem(
                    "Selecione uma reserva para cancelar.",
                    Color.Red);

                return;
            }

            int reservaId = Convert.ToInt32(
                dgvMinhasReservas.CurrentRow.Cells["Id"].Value);

            string status = Convert.ToString(
                dgvMinhasReservas.CurrentRow.Cells["Status"].Value);

            // Não permite cancelar uma reserva já concluída
            if (status == "Concluida")
            {
                MostrarMensagem(
                    "Uma reserva concluída não pode ser cancelada.",
                    Color.Red);

                return;
            }

            // Não permite cancelar uma reserva já cancelada
            if (status == "Cancelada")
            {
                MostrarMensagem(
                    "Essa reserva já está cancelada.",
                    Color.Red);

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
        WHERE Id = @id
          AND Status = 'Confirmada'",
                new SqlParameter("@id", reservaId));

            MostrarMensagem(
                "Reserva cancelada com sucesso.",
                Color.Green);

            CarregarReservas();
        }


        private void cmbFiltroStatus_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            if (_reservas == null)
                return;

            var view = _reservas.DefaultView;

            string filtro = cmbFiltroStatus.SelectedItem?.ToString();

            switch (filtro)
            {
                case "Pendente":
                    view.RowFilter = "Status = 'Pendente'";
                    break;

                case "Confirmada":
                    view.RowFilter = "Status = 'Confirmada'";
                    break;

                case "Cancelada":
                    view.RowFilter = "Status = 'Cancelada'";
                    break;

                case "Concluida":
                    view.RowFilter = "Status = 'Concluida'";
                    break;

                default:
                    view.RowFilter = "";
                    break;
            }
        }



        private void LimparCampos()
        {
            cmbUsuario.SelectedIndex = -1;
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
            if (dgvMinhasReservas.CurrentRow == null)
            {
                MostrarMensagem(
                    "Selecione uma reserva para concluir.",
                    Color.Red);

                return;
            }

            int reservaId = Convert.ToInt32(
                dgvMinhasReservas.CurrentRow.Cells["Id"].Value);

            string status = Convert.ToString(
                dgvMinhasReservas.CurrentRow.Cells["Status"].Value);

            // Só permite concluir reservas confirmadas
            if (status != "Confirmada")
            {
                MostrarMensagem(
                    "Somente reservas confirmadas podem ser concluídas.",
                    Color.Red);

                return;
            }

            DialogResult resultado = MessageBox.Show(
                "Tem certeza que deseja concluir esta reserva?\n\n" +
                "Isso irá marcar a sala como concluída e ela estará " +
                "liberada para outra reserva.\n\n" +
                "Esta ação não pode ser desfeita.",
                "Concluir reserva",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (resultado != DialogResult.Yes)
                return;

            ConexaoBanco.ExecutarComando(@"
        UPDATE ReservasEspaco
        SET Status = 'Concluida'
        WHERE Id = @id
          AND Status = 'Confirmada'",
                new SqlParameter("@id", reservaId));

            MostrarMensagem(
                "Reserva concluída com sucesso. O espaço está liberado para nova reserva.",
                Color.Green);

            CarregarReservas();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarReservas();

            MostrarMensagem(
                "Lista de reservas atualizada.",
                Color.Green);
        }
    }
}
