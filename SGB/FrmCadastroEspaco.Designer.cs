namespace SGB
{
    partial class FrmCadastroEspaco
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
            txtNome = new TextBox();
            txtTipo = new TextBox();
            txtLocalizacao = new TextBox();
            txtCapacidade = new TextBox();
            btnCadastrar = new Button();
            dgvEspacos = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnListar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvEspacos).BeginInit();
            SuspendLayout();
            // 
            // txtNome
            // 
            txtNome.Location = new Point(140, 35);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(100, 23);
            txtNome.TabIndex = 0;
            // 
            // txtTipo
            // 
            txtTipo.Location = new Point(140, 64);
            txtTipo.Name = "txtTipo";
            txtTipo.Size = new Size(100, 23);
            txtTipo.TabIndex = 1;
            // 
            // txtLocalizacao
            // 
            txtLocalizacao.Location = new Point(140, 93);
            txtLocalizacao.Name = "txtLocalizacao";
            txtLocalizacao.Size = new Size(100, 23);
            txtLocalizacao.TabIndex = 2;
            // 
            // txtCapacidade
            // 
            txtCapacidade.Location = new Point(140, 122);
            txtCapacidade.Name = "txtCapacidade";
            txtCapacidade.Size = new Size(100, 23);
            txtCapacidade.TabIndex = 3;
            // 
            // btnCadastrar
            // 
            btnCadastrar.Location = new Point(13, 174);
            btnCadastrar.Name = "btnCadastrar";
            btnCadastrar.Size = new Size(227, 23);
            btnCadastrar.TabIndex = 4;
            btnCadastrar.Text = "Cadastrar";
            btnCadastrar.UseVisualStyleBackColor = true;
            btnCadastrar.Click += btnCadastrar_Click;
            // 
            // dgvEspacos
            // 
            dgvEspacos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEspacos.Location = new Point(262, 35);
            dgvEspacos.Name = "dgvEspacos";
            dgvEspacos.Size = new Size(526, 374);
            dgvEspacos.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 38);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 7;
            label1.Text = "Nome:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 67);
            label2.Name = "label2";
            label2.Size = new Size(34, 15);
            label2.TabIndex = 8;
            label2.Text = "Tipo:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 96);
            label3.Name = "label3";
            label3.Size = new Size(71, 15);
            label3.TabIndex = 9;
            label3.Text = "Localização:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 125);
            label4.Name = "label4";
            label4.Size = new Size(72, 15);
            label4.TabIndex = 10;
            label4.Text = "Capacidade:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(262, 14);
            label5.Name = "label5";
            label5.Size = new Size(101, 15);
            label5.TabIndex = 11;
            label5.Text = "Salas cadastradas:";
            // 
            // btnListar
            // 
            btnListar.Location = new Point(262, 415);
            btnListar.Name = "btnListar";
            btnListar.Size = new Size(526, 23);
            btnListar.TabIndex = 12;
            btnListar.Text = "Listar";
            btnListar.UseVisualStyleBackColor = true;
            btnListar.Click += btnListar_Click;
            // 
            // FrmCadastroEspaco
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnListar);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvEspacos);
            Controls.Add(btnCadastrar);
            Controls.Add(txtCapacidade);
            Controls.Add(txtLocalizacao);
            Controls.Add(txtTipo);
            Controls.Add(txtNome);
            Name = "FrmCadastroEspaco";
            Text = "Cadastro de Espaço";
            Load += FrmCadastroEspaco_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEspacos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNome;
        private TextBox txtTipo;
        private TextBox txtLocalizacao;
        private TextBox txtCapacidade;
        private Button btnCadastrar;
        private DataGridView dgvEspacos;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button btnListar;
    }
}