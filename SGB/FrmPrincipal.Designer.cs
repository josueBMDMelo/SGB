namespace SGB
{
    partial class FrmPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            contextMenuStrip1 = new ContextMenuStrip(components);
            menuStrip1 = new MenuStrip();
            cadastrosToolStripMenuItem = new ToolStripMenuItem();
            usuariosToolStripMenuItem = new ToolStripMenuItem();
            livrosToolStripMenuItem = new ToolStripMenuItem();
            exemplaresToolStripMenuItem = new ToolStripMenuItem();
            espacosToolStripMenuItem = new ToolStripMenuItem();
            emprestimosToolStripMenuItem = new ToolStripMenuItem();
            realizarEmprestimoToolStripMenuItem = new ToolStripMenuItem();
            emprestimosRealizadosToolStripMenuItem = new ToolStripMenuItem();
            meusEmprestimosToolStripMenuItem = new ToolStripMenuItem();
            devolucoesToolStripMenuItem = new ToolStripMenuItem();
            reservaDeEspacosToolStripMenuItem = new ToolStripMenuItem();
            configuracoesToolStripMenuItem = new ToolStripMenuItem();
            sessaoToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabelVersao = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { cadastrosToolStripMenuItem, emprestimosToolStripMenuItem, devolucoesToolStripMenuItem, reservaDeEspacosToolStripMenuItem, configuracoesToolStripMenuItem, sessaoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // cadastrosToolStripMenuItem
            // 
            cadastrosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { usuariosToolStripMenuItem, livrosToolStripMenuItem, exemplaresToolStripMenuItem, espacosToolStripMenuItem });
            cadastrosToolStripMenuItem.Name = "cadastrosToolStripMenuItem";
            cadastrosToolStripMenuItem.Size = new Size(71, 20);
            cadastrosToolStripMenuItem.Text = "Cadastros";
            // 
            // usuariosToolStripMenuItem
            // 
            usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem";
            usuariosToolStripMenuItem.Size = new Size(180, 22);
            usuariosToolStripMenuItem.Text = "Usuários";
            usuariosToolStripMenuItem.Click += usuariosToolStripMenuItem_Click;
            // 
            // livrosToolStripMenuItem
            // 
            livrosToolStripMenuItem.Name = "livrosToolStripMenuItem";
            livrosToolStripMenuItem.Size = new Size(180, 22);
            livrosToolStripMenuItem.Text = "Livros";
            livrosToolStripMenuItem.Click += livrosToolStripMenuItem_Click;
            // 
            // exemplaresToolStripMenuItem
            // 
            exemplaresToolStripMenuItem.Name = "exemplaresToolStripMenuItem";
            exemplaresToolStripMenuItem.Size = new Size(180, 22);
            exemplaresToolStripMenuItem.Text = "Exemplares";
            exemplaresToolStripMenuItem.Click += exemplaresToolStripMenuItem_Click;
            // 
            // espacosToolStripMenuItem
            // 
            espacosToolStripMenuItem.Name = "espacosToolStripMenuItem";
            espacosToolStripMenuItem.Size = new Size(180, 22);
            espacosToolStripMenuItem.Text = "Espaços";
            espacosToolStripMenuItem.Click += espacosToolStripMenuItem_Click;
            // 
            // emprestimosToolStripMenuItem
            // 
            emprestimosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { realizarEmprestimoToolStripMenuItem, emprestimosRealizadosToolStripMenuItem, meusEmprestimosToolStripMenuItem });
            emprestimosToolStripMenuItem.Name = "emprestimosToolStripMenuItem";
            emprestimosToolStripMenuItem.Size = new Size(88, 20);
            emprestimosToolStripMenuItem.Text = "Empréstimos";
            // 
            // realizarEmprestimoToolStripMenuItem
            // 
            realizarEmprestimoToolStripMenuItem.Name = "realizarEmprestimoToolStripMenuItem";
            realizarEmprestimoToolStripMenuItem.Size = new Size(201, 22);
            realizarEmprestimoToolStripMenuItem.Text = "Realizar Empréstimo";
            realizarEmprestimoToolStripMenuItem.Click += realizarEmprestimoToolStripMenuItem_Click;
            // 
            // emprestimosRealizadosToolStripMenuItem
            // 
            emprestimosRealizadosToolStripMenuItem.Name = "emprestimosRealizadosToolStripMenuItem";
            emprestimosRealizadosToolStripMenuItem.Size = new Size(201, 22);
            emprestimosRealizadosToolStripMenuItem.Text = "Empréstimos Realizados";
            emprestimosRealizadosToolStripMenuItem.Click += emprestimosRealizadosToolStripMenuItem_Click;
            // 
            // meusEmprestimosToolStripMenuItem
            // 
            meusEmprestimosToolStripMenuItem.Name = "meusEmprestimosToolStripMenuItem";
            meusEmprestimosToolStripMenuItem.Size = new Size(201, 22);
            meusEmprestimosToolStripMenuItem.Text = "Meus Empréstimos";
            meusEmprestimosToolStripMenuItem.Click += meusEmprestimosToolStripMenuItem_Click;
            // 
            // devolucoesToolStripMenuItem
            // 
            devolucoesToolStripMenuItem.Name = "devolucoesToolStripMenuItem";
            devolucoesToolStripMenuItem.Size = new Size(80, 20);
            devolucoesToolStripMenuItem.Text = "Devoluções";
            devolucoesToolStripMenuItem.Click += devolucoesToolStripMenuItem_Click;
            // 
            // reservaDeEspacosToolStripMenuItem
            // 
            reservaDeEspacosToolStripMenuItem.Name = "reservaDeEspacosToolStripMenuItem";
            reservaDeEspacosToolStripMenuItem.Size = new Size(120, 20);
            reservaDeEspacosToolStripMenuItem.Text = "Reserva de Espaços";
            reservaDeEspacosToolStripMenuItem.Click += reservaDeEspacosToolStripMenuItem_Click;
            // 
            // configuracoesToolStripMenuItem
            // 
            configuracoesToolStripMenuItem.Name = "configuracoesToolStripMenuItem";
            configuracoesToolStripMenuItem.Size = new Size(96, 20);
            configuracoesToolStripMenuItem.Text = "Configurações";
            // 
            // sessaoToolStripMenuItem
            // 
            sessaoToolStripMenuItem.Name = "sessaoToolStripMenuItem";
            sessaoToolStripMenuItem.Size = new Size(54, 20);
            sessaoToolStripMenuItem.Text = "Sessão";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabelVersao });
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabelVersao
            // 
            toolStripStatusLabelVersao.Name = "toolStripStatusLabelVersao";
            toolStripStatusLabelVersao.Size = new Size(118, 17);
            toolStripStatusLabelVersao.Text = "toolStripStatusLabel2";
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FrmPrincipal";
            Text = "Sistema de Gestão da Biblioteca";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ContextMenuStrip contextMenuStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem cadastrosToolStripMenuItem;
        private ToolStripMenuItem usuariosToolStripMenuItem;
        private ToolStripMenuItem livrosToolStripMenuItem;
        private ToolStripMenuItem exemplaresToolStripMenuItem;
        private ToolStripMenuItem espacosToolStripMenuItem;
        private ToolStripMenuItem emprestimosToolStripMenuItem;
        private ToolStripMenuItem devolucoesToolStripMenuItem;
        private ToolStripMenuItem reservaDeEspacosToolStripMenuItem;
        private ToolStripMenuItem configuracoesToolStripMenuItem;
        private ToolStripMenuItem sessaoToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabelVersao;
        private ToolStripMenuItem realizarEmprestimoToolStripMenuItem;
        private ToolStripMenuItem emprestimosRealizadosToolStripMenuItem;
        private ToolStripMenuItem meusEmprestimosToolStripMenuItem;
    }
}
