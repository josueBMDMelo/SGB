namespace SGB
{
    partial class FrmCadastroLivro
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
            txtTitulo = new TextBox();
            txtAutor = new TextBox();
            dgvLivros = new DataGridView();
            btnCadastrar = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnListar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLivros).BeginInit();
            SuspendLayout();
            // 
            // txtTitulo
            // 
            txtTitulo.Location = new Point(63, 12);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(277, 23);
            txtTitulo.TabIndex = 0;
            // 
            // txtAutor
            // 
            txtAutor.Location = new Point(392, 12);
            txtAutor.Name = "txtAutor";
            txtAutor.Size = new Size(277, 23);
            txtAutor.TabIndex = 1;
            // 
            // dgvLivros
            // 
            dgvLivros.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLivros.Location = new Point(17, 129);
            dgvLivros.Name = "dgvLivros";
            dgvLivros.ReadOnly = true;
            dgvLivros.Size = new Size(771, 309);
            dgvLivros.TabIndex = 2;
            // 
            // btnCadastrar
            // 
            btnCadastrar.Location = new Point(17, 41);
            btnCadastrar.Name = "btnCadastrar";
            btnCadastrar.Size = new Size(112, 23);
            btnCadastrar.TabIndex = 3;
            btnCadastrar.Text = "Cadastrar";
            btnCadastrar.UseVisualStyleBackColor = true;
            btnCadastrar.Click += btnCadastrar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 15);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 4;
            label1.Text = "Título:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(346, 15);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 5;
            label2.Text = "Autor:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 111);
            label3.Name = "label3";
            label3.Size = new Size(230, 15);
            label3.TabIndex = 6;
            label3.Text = "Os livros já cadastrados irão aparecer aqui:";
            // 
            // btnListar
            // 
            btnListar.Location = new Point(135, 41);
            btnListar.Name = "btnListar";
            btnListar.Size = new Size(112, 23);
            btnListar.TabIndex = 7;
            btnListar.Text = "Listar";
            btnListar.UseVisualStyleBackColor = true;
            btnListar.Click += btnListar_Click;
            // 
            // FrmCadastroLivro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnListar);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCadastrar);
            Controls.Add(dgvLivros);
            Controls.Add(txtAutor);
            Controls.Add(txtTitulo);
            Name = "FrmCadastroLivro";
            Text = "Cadastro de Livros";
            ((System.ComponentModel.ISupportInitialize)dgvLivros).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTitulo;
        private TextBox txtAutor;
        private DataGridView dgvLivros;
        private Button btnCadastrar;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnListar;
    }
}