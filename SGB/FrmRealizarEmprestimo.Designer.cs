namespace SGB
{
    partial class FrmRealizarEmprestimo
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
            cmbUsuario = new ComboBox();
            cmbExemplar = new ComboBox();
            dtpDataPrevista = new DateTimePicker();
            btnEmprestar = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // cmbUsuario
            // 
            cmbUsuario.FormattingEnabled = true;
            cmbUsuario.Location = new Point(369, 41);
            cmbUsuario.Name = "cmbUsuario";
            cmbUsuario.Size = new Size(121, 23);
            cmbUsuario.TabIndex = 0;
            // 
            // cmbExemplar
            // 
            cmbExemplar.FormattingEnabled = true;
            cmbExemplar.Location = new Point(369, 70);
            cmbExemplar.Name = "cmbExemplar";
            cmbExemplar.Size = new Size(121, 23);
            cmbExemplar.TabIndex = 1;
            // 
            // dtpDataPrevista
            // 
            dtpDataPrevista.Location = new Point(369, 99);
            dtpDataPrevista.Name = "dtpDataPrevista";
            dtpDataPrevista.Size = new Size(121, 23);
            dtpDataPrevista.TabIndex = 2;
            // 
            // btnEmprestar
            // 
            btnEmprestar.Location = new Point(369, 128);
            btnEmprestar.Name = "btnEmprestar";
            btnEmprestar.Size = new Size(121, 23);
            btnEmprestar.TabIndex = 3;
            btnEmprestar.Text = "Emprestar";
            btnEmprestar.UseVisualStyleBackColor = true;
            btnEmprestar.Click += btnEmprestar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(199, 44);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 4;
            label1.Text = "Usuário:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(199, 73);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 5;
            label2.Text = "Exemplar:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(199, 105);
            label3.Name = "label3";
            label3.Size = new Size(152, 15);
            label3.TabIndex = 6;
            label3.Text = "Data prevista de devolução:";
            // 
            // FrmRealizarEmprestimo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnEmprestar);
            Controls.Add(dtpDataPrevista);
            Controls.Add(cmbExemplar);
            Controls.Add(cmbUsuario);
            Name = "FrmRealizarEmprestimo";
            Text = "FrmRealizarEmprestimo";
            Load += FrmRealizarEmprestimo_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbUsuario;
        private ComboBox cmbExemplar;
        private DateTimePicker dtpDataPrevista;
        private Button btnEmprestar;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}