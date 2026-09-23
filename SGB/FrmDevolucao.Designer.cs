namespace SGB
{
    partial class FrmDevolucao
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
            cmbFiltro = new ComboBox();
            dgvEmprestimosAbertos = new DataGridView();
            btnSelecionar = new Button();
            lblLivro = new Label();
            lblUsuario = new Label();
            lblDiasAtraso = new Label();
            lblValorMulta = new Label();
            chkMultaPaga = new CheckBox();
            btnConfirmarDevolucao = new Button();
            labelMensagem = new Label();
            btnFiltrar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvEmprestimosAbertos).BeginInit();
            SuspendLayout();
            // 
            // cmbFiltro
            // 
            cmbFiltro.FormattingEnabled = true;
            cmbFiltro.Location = new Point(176, 12);
            cmbFiltro.Name = "cmbFiltro";
            cmbFiltro.Size = new Size(121, 23);
            cmbFiltro.TabIndex = 0;
            // 
            // dgvEmprestimosAbertos
            // 
            dgvEmprestimosAbertos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEmprestimosAbertos.Location = new Point(318, 112);
            dgvEmprestimosAbertos.Name = "dgvEmprestimosAbertos";
            dgvEmprestimosAbertos.Size = new Size(240, 150);
            dgvEmprestimosAbertos.TabIndex = 1;
            // 
            // btnSelecionar
            // 
            btnSelecionar.Location = new Point(347, 38);
            btnSelecionar.Name = "btnSelecionar";
            btnSelecionar.Size = new Size(75, 23);
            btnSelecionar.TabIndex = 2;
            btnSelecionar.Text = "Selecionar";
            btnSelecionar.UseVisualStyleBackColor = true;
            btnSelecionar.Click += btnSelecionar_Click;
            // 
            // lblLivro
            // 
            lblLivro.AutoSize = true;
            lblLivro.Location = new Point(61, 38);
            lblLivro.Name = "lblLivro";
            lblLivro.Size = new Size(46, 15);
            lblLivro.TabIndex = 3;
            lblLivro.Text = "lblLivro";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(61, 55);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(60, 15);
            lblUsuario.TabIndex = 4;
            lblUsuario.Text = "lblUsuario";
            // 
            // lblDiasAtraso
            // 
            lblDiasAtraso.AutoSize = true;
            lblDiasAtraso.Location = new Point(61, 70);
            lblDiasAtraso.Name = "lblDiasAtraso";
            lblDiasAtraso.Size = new Size(76, 15);
            lblDiasAtraso.TabIndex = 5;
            lblDiasAtraso.Text = "lblDiasAtraso";
            // 
            // lblValorMulta
            // 
            lblValorMulta.AutoSize = true;
            lblValorMulta.Location = new Point(61, 94);
            lblValorMulta.Name = "lblValorMulta";
            lblValorMulta.Size = new Size(77, 15);
            lblValorMulta.TabIndex = 6;
            lblValorMulta.Text = "lblValorMulta";
            // 
            // chkMultaPaga
            // 
            chkMultaPaga.AutoSize = true;
            chkMultaPaga.Location = new Point(61, 112);
            chkMultaPaga.Name = "chkMultaPaga";
            chkMultaPaga.Size = new Size(138, 19);
            chkMultaPaga.TabIndex = 7;
            chkMultaPaga.Text = "Multa recebida agora";
            chkMultaPaga.UseVisualStyleBackColor = true;
            // 
            // btnConfirmarDevolucao
            // 
            btnConfirmarDevolucao.Location = new Point(156, 293);
            btnConfirmarDevolucao.Name = "btnConfirmarDevolucao";
            btnConfirmarDevolucao.Size = new Size(166, 23);
            btnConfirmarDevolucao.TabIndex = 8;
            btnConfirmarDevolucao.Text = "Confirmar devolução";
            btnConfirmarDevolucao.UseVisualStyleBackColor = true;
            btnConfirmarDevolucao.Click += btnConfirmarDevolucao_Click;
            // 
            // labelMensagem
            // 
            labelMensagem.AutoSize = true;
            labelMensagem.Location = new Point(61, 203);
            labelMensagem.Name = "labelMensagem";
            labelMensagem.Size = new Size(79, 15);
            labelMensagem.TabIndex = 9;
            labelMensagem.Text = "lblMensagem";
            // 
            // btnFiltrar
            // 
            btnFiltrar.Location = new Point(347, 9);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(75, 23);
            btnFiltrar.TabIndex = 10;
            btnFiltrar.Text = "Filtrar";
            btnFiltrar.UseVisualStyleBackColor = true;
            btnFiltrar.Click += btnFiltrar_Click;
            // 
            // FrmDevolucao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnFiltrar);
            Controls.Add(labelMensagem);
            Controls.Add(btnConfirmarDevolucao);
            Controls.Add(chkMultaPaga);
            Controls.Add(lblValorMulta);
            Controls.Add(lblDiasAtraso);
            Controls.Add(lblUsuario);
            Controls.Add(lblLivro);
            Controls.Add(btnSelecionar);
            Controls.Add(dgvEmprestimosAbertos);
            Controls.Add(cmbFiltro);
            Name = "FrmDevolucao";
            Text = "FrmDevolucao";
            Load += FrmDevolucao_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEmprestimosAbertos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbFiltro;
        private DataGridView dgvEmprestimosAbertos;
        private Button btnSelecionar;
        private Label lblLivro;
        private Label lblUsuario;
        private Label lblDiasAtraso;
        private Label lblValorMulta;
        private CheckBox chkMultaPaga;
        private Button btnConfirmarDevolucao;
        private Label labelMensagem;
        private Button btnFiltrar;
    }
}