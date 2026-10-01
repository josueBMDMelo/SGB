        using SGB.Data;
namespace SGB
{
    partial class FrmCadastroUsuario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            btnCadastro = new Button();
            btnListar = new Button();
            btnEditar = new Button();
            btnDeletar = new Button();
            btnSair = new Button();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            label4 = new Label();
            btnCadastrar = new Button();
            label5 = new Label();
            cmbTipoUsuarioCadastro = new ComboBox();
            cmbPerfilCadastro = new ComboBox();
            txtSenhaCadastro = new TextBox();
            txtEmailCadastro = new TextBox();
            txtNomeCadastro = new TextBox();
            groupBoxCadastro = new GroupBox();
            groupBoxEdicao = new GroupBox();
            txtNomeEdicao = new TextBox();
            txtEmailEdicao = new TextBox();
            txtSenhaEdicao = new TextBox();
            cmbPerfilEdicao = new ComboBox();
            cmbBoxEdicao = new ComboBox();
            label6 = new Label();
            btnConfirmar = new Button();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            gridUsuarios = new DataGridView();
            Nome = new DataGridViewTextBoxColumn();
            Email = new DataGridViewTextBoxColumn();
            Perfil = new DataGridViewTextBoxColumn();
            TipoUsuario = new DataGridViewTextBoxColumn();
            btnConfirmaDeletar = new Button();
            groupBoxCadastro.SuspendLayout();
            groupBoxEdicao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridUsuarios).BeginInit();
            SuspendLayout();
            // 
            // btnCadastro
            // 
            btnCadastro.BackColor = Color.GreenYellow;
            btnCadastro.Cursor = Cursors.Hand;
            btnCadastro.FlatStyle = FlatStyle.Flat;
            btnCadastro.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCadastro.Location = new Point(12, 12);
            btnCadastro.Name = "btnCadastro";
            btnCadastro.Size = new Size(92, 31);
            btnCadastro.TabIndex = 11;
            btnCadastro.Text = "Cadastro";
            btnCadastro.UseVisualStyleBackColor = false;
            btnCadastro.Click += btnCadastro_Click;
            // 
            // btnListar
            // 
            btnListar.BackColor = Color.DodgerBlue;
            btnListar.Cursor = Cursors.Hand;
            btnListar.FlatStyle = FlatStyle.Flat;
            btnListar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnListar.Location = new Point(110, 12);
            btnListar.Name = "btnListar";
            btnListar.Size = new Size(103, 31);
            btnListar.TabIndex = 12;
            btnListar.Text = "Listar usuários";
            btnListar.UseVisualStyleBackColor = false;
            btnListar.Click += btnListar_Click;
            // 
            // btnEditar
            // 
            btnEditar.BackColor = Color.Yellow;
            btnEditar.Cursor = Cursors.Hand;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEditar.Location = new Point(219, 12);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(100, 31);
            btnEditar.TabIndex = 13;
            btnEditar.Text = "Editar usuários";
            btnEditar.UseVisualStyleBackColor = false;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnDeletar
            // 
            btnDeletar.BackColor = Color.Red;
            btnDeletar.Cursor = Cursors.Hand;
            btnDeletar.FlatStyle = FlatStyle.Flat;
            btnDeletar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDeletar.Location = new Point(325, 12);
            btnDeletar.Name = "btnDeletar";
            btnDeletar.Size = new Size(107, 31);
            btnDeletar.TabIndex = 14;
            btnDeletar.Text = "Deletar usuário";
            btnDeletar.UseVisualStyleBackColor = false;
            btnDeletar.Click += btnDeletar_Click;
            // 
            // btnSair
            // 
            btnSair.BackColor = Color.Transparent;
            btnSair.Cursor = Cursors.Hand;
            btnSair.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSair.Location = new Point(681, 12);
            btnSair.Name = "btnSair";
            btnSair.Size = new Size(107, 31);
            btnSair.TabIndex = 16;
            btnSair.Text = "Sair";
            btnSair.UseVisualStyleBackColor = false;
            btnSair.Click += btnSair_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 51);
            label2.Name = "label2";
            label2.Size = new Size(44, 15);
            label2.TabIndex = 7;
            label2.Text = "E-mail:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 80);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 8;
            label3.Text = "Senha:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 22);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 6;
            label1.Text = "Nome:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 114);
            label4.Name = "label4";
            label4.Size = new Size(37, 15);
            label4.TabIndex = 9;
            label4.Text = "Perfil:";
            // 
            // btnCadastrar
            // 
            btnCadastrar.Location = new Point(46, 179);
            btnCadastrar.Name = "btnCadastrar";
            btnCadastrar.Size = new Size(168, 35);
            btnCadastrar.TabIndex = 5;
            btnCadastrar.Text = "Cadastrar";
            btnCadastrar.UseVisualStyleBackColor = true;
            btnCadastrar.Click += btnCadastrar_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 148);
            label5.Name = "label5";
            label5.Size = new Size(92, 15);
            label5.TabIndex = 10;
            label5.Text = "Tipo de usuário:";
            // 
            // cmbTipoUsuarioCadastro
            // 
            cmbTipoUsuarioCadastro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoUsuarioCadastro.FormattingEnabled = true;
            cmbTipoUsuarioCadastro.Items.AddRange(new object[] { "Aluno", "Professor", "Funcionario", "Externo" });
            cmbTipoUsuarioCadastro.Location = new Point(105, 144);
            cmbTipoUsuarioCadastro.Name = "cmbTipoUsuarioCadastro";
            cmbTipoUsuarioCadastro.Size = new Size(121, 23);
            cmbTipoUsuarioCadastro.TabIndex = 4;
            // 
            // cmbPerfilCadastro
            // 
            cmbPerfilCadastro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPerfilCadastro.FormattingEnabled = true;
            cmbPerfilCadastro.Items.AddRange(new object[] { "Usuario", "Bibliotecario", "Administrador" });
            cmbPerfilCadastro.Location = new Point(57, 111);
            cmbPerfilCadastro.Name = "cmbPerfilCadastro";
            cmbPerfilCadastro.Size = new Size(144, 23);
            cmbPerfilCadastro.TabIndex = 3;
            // 
            // txtSenhaCadastro
            // 
            txtSenhaCadastro.Location = new Point(57, 77);
            txtSenhaCadastro.Name = "txtSenhaCadastro";
            txtSenhaCadastro.Size = new Size(143, 23);
            txtSenhaCadastro.TabIndex = 2;
            // 
            // txtEmailCadastro
            // 
            txtEmailCadastro.Location = new Point(57, 48);
            txtEmailCadastro.Name = "txtEmailCadastro";
            txtEmailCadastro.Size = new Size(143, 23);
            txtEmailCadastro.TabIndex = 1;
            // 
            // txtNomeCadastro
            // 
            txtNomeCadastro.Location = new Point(56, 22);
            txtNomeCadastro.Name = "txtNomeCadastro";
            txtNomeCadastro.Size = new Size(144, 23);
            txtNomeCadastro.TabIndex = 0;
            // 
            // groupBoxCadastro
            // 
            groupBoxCadastro.Controls.Add(txtNomeCadastro);
            groupBoxCadastro.Controls.Add(txtEmailCadastro);
            groupBoxCadastro.Controls.Add(txtSenhaCadastro);
            groupBoxCadastro.Controls.Add(cmbPerfilCadastro);
            groupBoxCadastro.Controls.Add(cmbTipoUsuarioCadastro);
            groupBoxCadastro.Controls.Add(label5);
            groupBoxCadastro.Controls.Add(btnCadastrar);
            groupBoxCadastro.Controls.Add(label4);
            groupBoxCadastro.Controls.Add(label1);
            groupBoxCadastro.Controls.Add(label3);
            groupBoxCadastro.Controls.Add(label2);
            groupBoxCadastro.Location = new Point(12, 71);
            groupBoxCadastro.Name = "groupBoxCadastro";
            groupBoxCadastro.Size = new Size(254, 220);
            groupBoxCadastro.TabIndex = 15;
            groupBoxCadastro.TabStop = false;
            groupBoxCadastro.Text = "Cadastro";
            // 
            // groupBoxEdicao
            // 
            groupBoxEdicao.Controls.Add(txtNomeEdicao);
            groupBoxEdicao.Controls.Add(txtEmailEdicao);
            groupBoxEdicao.Controls.Add(txtSenhaEdicao);
            groupBoxEdicao.Controls.Add(cmbPerfilEdicao);
            groupBoxEdicao.Controls.Add(cmbBoxEdicao);
            groupBoxEdicao.Controls.Add(label6);
            groupBoxEdicao.Controls.Add(btnConfirmar);
            groupBoxEdicao.Controls.Add(label7);
            groupBoxEdicao.Controls.Add(label8);
            groupBoxEdicao.Controls.Add(label9);
            groupBoxEdicao.Controls.Add(label10);
            groupBoxEdicao.Location = new Point(12, 71);
            groupBoxEdicao.Name = "groupBoxEdicao";
            groupBoxEdicao.Size = new Size(254, 220);
            groupBoxEdicao.TabIndex = 17;
            groupBoxEdicao.TabStop = false;
            groupBoxEdicao.Text = "Edição";
            // 
            // txtNomeEdicao
            // 
            txtNomeEdicao.Location = new Point(56, 22);
            txtNomeEdicao.Name = "txtNomeEdicao";
            txtNomeEdicao.Size = new Size(144, 23);
            txtNomeEdicao.TabIndex = 0;
            // 
            // txtEmailEdicao
            // 
            txtEmailEdicao.Location = new Point(57, 48);
            txtEmailEdicao.Name = "txtEmailEdicao";
            txtEmailEdicao.Size = new Size(143, 23);
            txtEmailEdicao.TabIndex = 1;
            // 
            // txtSenhaEdicao
            // 
            txtSenhaEdicao.Location = new Point(57, 77);
            txtSenhaEdicao.Name = "txtSenhaEdicao";
            txtSenhaEdicao.Size = new Size(143, 23);
            txtSenhaEdicao.TabIndex = 2;
            // 
            // cmbPerfilEdicao
            // 
            cmbPerfilEdicao.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPerfilEdicao.FormattingEnabled = true;
            cmbPerfilEdicao.Items.AddRange(new object[] { "Usuario", "Bibliotecario", "Administrador" });
            cmbPerfilEdicao.Location = new Point(57, 111);
            cmbPerfilEdicao.Name = "cmbPerfilEdicao";
            cmbPerfilEdicao.Size = new Size(144, 23);
            cmbPerfilEdicao.TabIndex = 3;
            // 
            // cmbBoxEdicao
            // 
            cmbBoxEdicao.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBoxEdicao.FormattingEnabled = true;
            cmbBoxEdicao.Items.AddRange(new object[] { "Aluno", "Professor", "Funcionario", "Externo" });
            cmbBoxEdicao.Location = new Point(105, 144);
            cmbBoxEdicao.Name = "cmbBoxEdicao";
            cmbBoxEdicao.Size = new Size(121, 23);
            cmbBoxEdicao.TabIndex = 4;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(7, 148);
            label6.Name = "label6";
            label6.Size = new Size(92, 15);
            label6.TabIndex = 10;
            label6.Text = "Tipo de usuário:";
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(46, 179);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(168, 35);
            btnConfirmar.TabIndex = 5;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(7, 114);
            label7.Name = "label7";
            label7.Size = new Size(37, 15);
            label7.TabIndex = 9;
            label7.Text = "Perfil:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(7, 22);
            label8.Name = "label8";
            label8.Size = new Size(43, 15);
            label8.TabIndex = 6;
            label8.Text = "Nome:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(7, 80);
            label9.Name = "label9";
            label9.Size = new Size(42, 15);
            label9.TabIndex = 8;
            label9.Text = "Senha:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(7, 51);
            label10.Name = "label10";
            label10.Size = new Size(44, 15);
            label10.TabIndex = 7;
            label10.Text = "E-mail:";
            // 
            // gridUsuarios
            // 
            gridUsuarios.BackgroundColor = SystemColors.ControlLightLight;
            gridUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridUsuarios.Columns.AddRange(new DataGridViewColumn[] { Nome, Email, Perfil, TipoUsuario });
            gridUsuarios.GridColor = SystemColors.InactiveCaptionText;
            gridUsuarios.Location = new Point(286, 71);
            gridUsuarios.Name = "gridUsuarios";
            gridUsuarios.Size = new Size(502, 354);
            gridUsuarios.TabIndex = 18;
            // 
            // Nome
            // 
            Nome.HeaderText = "Nome";
            Nome.Name = "Nome";
            // 
            // Email
            // 
            Email.HeaderText = "Email";
            Email.Name = "Email";
            Email.Width = 120;
            // 
            // Perfil
            // 
            Perfil.HeaderText = "Perfil";
            Perfil.Name = "Perfil";
            Perfil.Width = 90;
            // 
            // TipoUsuario
            // 
            TipoUsuario.HeaderText = "Tipo de Usuário";
            TipoUsuario.Name = "TipoUsuario";
            TipoUsuario.Width = 150;
            // 
            // btnConfirmaDeletar
            // 
            btnConfirmaDeletar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConfirmaDeletar.Location = new Point(45, 349);
            btnConfirmaDeletar.Name = "btnConfirmaDeletar";
            btnConfirmaDeletar.Size = new Size(168, 35);
            btnConfirmaDeletar.TabIndex = 16;
            btnConfirmaDeletar.Text = "DELETAR";
            btnConfirmaDeletar.UseVisualStyleBackColor = true;
            // 
            // FrmCadastroUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnConfirmaDeletar);
            Controls.Add(gridUsuarios);
            Controls.Add(groupBoxEdicao);
            Controls.Add(groupBoxCadastro);
            Controls.Add(btnSair);
            Controls.Add(btnDeletar);
            Controls.Add(btnEditar);
            Controls.Add(btnListar);
            Controls.Add(btnCadastro);
            Name = "FrmCadastroUsuario";
            Text = "Cadastro";
            Load += FrmCadastroUsuario_Load;
            groupBoxCadastro.ResumeLayout(false);
            groupBoxCadastro.PerformLayout();
            groupBoxEdicao.ResumeLayout(false);
            groupBoxEdicao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridUsuarios).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button btnCadastro;
        private Button btnListar;
        private Button btnEditar;
        private Button btnDeletar;
        private Button btnSair;
        private Label label2;
        private Label label3;
        private Label label1;
        private Label label4;
        private Button btnCadastrar;
        private Label label5;
        private ComboBox cmbTipoUsuarioCadastro;
        private ComboBox cmbPerfilCadastro;
        private TextBox txtSenhaCadastro;
        private TextBox txtEmailCadastro;
        private TextBox txtNomeCadastro;
        private GroupBox groupBoxCadastro;
        private GroupBox groupBoxEdicao;
        private TextBox txtNomeEdicao;
        private TextBox txtEmailEdicao;
        private TextBox txtSenhaEdicao;
        private ComboBox cmbPerfilEdicao;
        private ComboBox cmbBoxEdicao;
        private Label label6;
        private Button btnConfirmar;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private DataGridView gridUsuarios;
        private DataGridViewTextBoxColumn Nome;
        private DataGridViewTextBoxColumn Email;
        private DataGridViewTextBoxColumn Perfil;
        private DataGridViewTextBoxColumn TipoUsuario;
        private Button btnConfirmaDeletar;
    }
}