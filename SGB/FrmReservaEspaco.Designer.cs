namespace SGB
{
    partial class FrmReservaEspaco
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmbEspaco = new ComboBox();
            dtpData = new DateTimePicker();
            dtpHoraInicio = new DateTimePicker();
            dtpHoraFim = new DateTimePicker();
            btnReservar = new Button();
            dgvMinhasReservas = new DataGridView();
            btnCancelarReserva = new Button();
            labelMensagem = new Label();
            cmbUsuario = new ComboBox();
            cmbFiltroStatus = new ComboBox();
            btnAtualizar = new Button();
            btnConcluirReserva = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvMinhasReservas).BeginInit();
            SuspendLayout();
            // 
            // cmbEspaco
            // 
            cmbEspaco.FormattingEnabled = true;
            cmbEspaco.Location = new Point(138, 45);
            cmbEspaco.Name = "cmbEspaco";
            cmbEspaco.Size = new Size(121, 23);
            cmbEspaco.TabIndex = 0;
            // 
            // dtpData
            // 
            dtpData.Location = new Point(138, 117);
            dtpData.Name = "dtpData";
            dtpData.Size = new Size(121, 23);
            dtpData.TabIndex = 1;
            // 
            // dtpHoraInicio
            // 
            dtpHoraInicio.Location = new Point(138, 146);
            dtpHoraInicio.Name = "dtpHoraInicio";
            dtpHoraInicio.Size = new Size(121, 23);
            dtpHoraInicio.TabIndex = 2;
            // 
            // dtpHoraFim
            // 
            dtpHoraFim.Location = new Point(138, 175);
            dtpHoraFim.Name = "dtpHoraFim";
            dtpHoraFim.Size = new Size(121, 23);
            dtpHoraFim.TabIndex = 3;
            // 
            // btnReservar
            // 
            btnReservar.Location = new Point(184, 233);
            btnReservar.Name = "btnReservar";
            btnReservar.Size = new Size(75, 23);
            btnReservar.TabIndex = 4;
            btnReservar.Text = "Reservar";
            btnReservar.UseVisualStyleBackColor = true;
            btnReservar.Click += btnReservar_Click;
            // 
            // dgvMinhasReservas
            // 
            dgvMinhasReservas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMinhasReservas.Location = new Point(279, 43);
            dgvMinhasReservas.Name = "dgvMinhasReservas";
            dgvMinhasReservas.Size = new Size(509, 366);
            dgvMinhasReservas.TabIndex = 5;
            // 
            // btnCancelarReserva
            // 
            btnCancelarReserva.Location = new Point(713, 415);
            btnCancelarReserva.Name = "btnCancelarReserva";
            btnCancelarReserva.Size = new Size(75, 23);
            btnCancelarReserva.TabIndex = 6;
            btnCancelarReserva.Text = "Cancelar reserva";
            btnCancelarReserva.UseVisualStyleBackColor = true;
            btnCancelarReserva.Click += btnCancelarReserva_Click;
            // 
            // labelMensagem
            // 
            labelMensagem.AutoSize = true;
            labelMensagem.Location = new Point(12, 287);
            labelMensagem.Name = "labelMensagem";
            labelMensagem.Size = new Size(201, 15);
            labelMensagem.TabIndex = 7;
            labelMensagem.Text = "Realize, cancele ou visualize reservas.";
            // 
            // cmbUsuario
            // 
            cmbUsuario.FormattingEnabled = true;
            cmbUsuario.Location = new Point(138, 11);
            cmbUsuario.Name = "cmbUsuario";
            cmbUsuario.Size = new Size(121, 23);
            cmbUsuario.TabIndex = 8;
            // 
            // cmbFiltroStatus
            // 
            cmbFiltroStatus.FormattingEnabled = true;
            cmbFiltroStatus.Location = new Point(586, 11);
            cmbFiltroStatus.Name = "cmbFiltroStatus";
            cmbFiltroStatus.Size = new Size(121, 23);
            cmbFiltroStatus.TabIndex = 9;
            // 
            // btnAtualizar
            // 
            btnAtualizar.Location = new Point(713, 11);
            btnAtualizar.Name = "btnAtualizar";
            btnAtualizar.Size = new Size(75, 23);
            btnAtualizar.TabIndex = 10;
            btnAtualizar.Text = "Atualizar";
            btnAtualizar.UseVisualStyleBackColor = true;
            btnAtualizar.Click += btnAtualizar_Click;
            // 
            // btnConcluirReserva
            // 
            btnConcluirReserva.Location = new Point(586, 415);
            btnConcluirReserva.Name = "btnConcluirReserva";
            btnConcluirReserva.Size = new Size(121, 23);
            btnConcluirReserva.TabIndex = 11;
            btnConcluirReserva.Text = "Concluir Reserva";
            btnConcluirReserva.UseVisualStyleBackColor = true;
            btnConcluirReserva.Click += btnConcluirReserva_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 14);
            label1.Name = "label1";
            label1.Size = new Size(106, 15);
            label1.TabIndex = 12;
            label1.Text = "Usuário da reserva:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 48);
            label2.Name = "label2";
            label2.Size = new Size(103, 15);
            label2.TabIndex = 13;
            label2.Text = "Espaço da reserva:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 123);
            label3.Name = "label3";
            label3.Size = new Size(34, 15);
            label3.TabIndex = 14;
            label3.Text = "Data:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 152);
            label4.Name = "label4";
            label4.Size = new Size(68, 15);
            label4.TabIndex = 15;
            label4.Text = "Hora início:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 181);
            label5.Name = "label5";
            label5.Size = new Size(57, 15);
            label5.TabIndex = 16;
            label5.Text = "Hora fim:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(482, 15);
            label6.Name = "label6";
            label6.Size = new Size(98, 15);
            label6.TabIndex = 17;
            label6.Text = "Status da reserva:";
            // 
            // FrmReservaEspaco
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnConcluirReserva);
            Controls.Add(btnAtualizar);
            Controls.Add(cmbFiltroStatus);
            Controls.Add(cmbUsuario);
            Controls.Add(labelMensagem);
            Controls.Add(btnCancelarReserva);
            Controls.Add(dgvMinhasReservas);
            Controls.Add(btnReservar);
            Controls.Add(dtpHoraFim);
            Controls.Add(dtpHoraInicio);
            Controls.Add(dtpData);
            Controls.Add(cmbEspaco);
            Name = "FrmReservaEspaco";
            Text = "FrmReservaEspaco";
            Load += FrmReservaEspaco_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMinhasReservas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbEspaco;
        private DateTimePicker dtpData;
        private DateTimePicker dtpHoraInicio;
        private DateTimePicker dtpHoraFim;
        private Button btnReservar;
        private DataGridView dgvMinhasReservas;
        private Button btnCancelarReserva;
        private Label labelMensagem;
        private ComboBox cmbUsuario;
        private ComboBox cmbFiltroStatus;
        private Button btnAtualizar;
        private Button btnConcluirReserva;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
    }
}