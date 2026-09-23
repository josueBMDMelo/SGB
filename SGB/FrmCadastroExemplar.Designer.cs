namespace SGB
{
    partial class FrmCadastroExemplar
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
            cmbLivro = new ComboBox();
            txtCodigoPatrimonio = new TextBox();
            btnCadastrar = new Button();
            dgvExemplares = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvExemplares).BeginInit();
            SuspendLayout();
            // 
            // cmbLivro
            // 
            cmbLivro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLivro.FormattingEnabled = true;
            cmbLivro.Location = new Point(188, 12);
            cmbLivro.Name = "cmbLivro";
            cmbLivro.Size = new Size(600, 23);
            cmbLivro.TabIndex = 0;
            // 
            // txtCodigoPatrimonio
            // 
            txtCodigoPatrimonio.Location = new Point(188, 41);
            txtCodigoPatrimonio.Name = "txtCodigoPatrimonio";
            txtCodigoPatrimonio.Size = new Size(231, 23);
            txtCodigoPatrimonio.TabIndex = 1;
            // 
            // btnCadastrar
            // 
            btnCadastrar.Location = new Point(425, 41);
            btnCadastrar.Name = "btnCadastrar";
            btnCadastrar.Size = new Size(363, 23);
            btnCadastrar.TabIndex = 2;
            btnCadastrar.Text = "Cadastrar";
            btnCadastrar.UseVisualStyleBackColor = true;
            btnCadastrar.Click += btnCadastrar_Click;
            // 
            // dgvExemplares
            // 
            dgvExemplares.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvExemplares.Location = new Point(12, 70);
            dgvExemplares.Name = "dgvExemplares";
            dgvExemplares.Size = new Size(776, 368);
            dgvExemplares.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 4;
            label1.Text = "Livro:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 45);
            label2.Name = "label2";
            label2.Size = new Size(152, 15);
            label2.TabIndex = 5;
            label2.Text = "Patrimônio (Código único):";
            // 
            // FrmCadastroExemplar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvExemplares);
            Controls.Add(btnCadastrar);
            Controls.Add(txtCodigoPatrimonio);
            Controls.Add(cmbLivro);
            Name = "FrmCadastroExemplar";
            Text = "Cadastro Exemplar";
            Load += FrmCadastroExemplar_Load;
            ((System.ComponentModel.ISupportInitialize)dgvExemplares).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbLivro;
        private TextBox txtCodigoPatrimonio;
        private Button btnCadastrar;
        private DataGridView dgvExemplares;
        private Label label1;
        private Label label2;
    }
}