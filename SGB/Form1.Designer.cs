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
            devolucoesToolStripMenuItem = new ToolStripMenuItem();
            reservaDeEspacosToolStripMenuItem = new ToolStripMenuItem();
            configuracoesToolStripMenuItem = new ToolStripMenuItem();
            sessaoToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
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
            usuariosToolStripMenuItem.Size = new Size(133, 22);
            usuariosToolStripMenuItem.Text = "Usuários";
            // 
            // livrosToolStripMenuItem
            // 
            livrosToolStripMenuItem.Name = "livrosToolStripMenuItem";
            livrosToolStripMenuItem.Size = new Size(133, 22);
            livrosToolStripMenuItem.Text = "Livros";
            // 
            // exemplaresToolStripMenuItem
            // 
            exemplaresToolStripMenuItem.Name = "exemplaresToolStripMenuItem";
            exemplaresToolStripMenuItem.Size = new Size(133, 22);
            exemplaresToolStripMenuItem.Text = "Exemplares";
            // 
            // espacosToolStripMenuItem
            // 
            espacosToolStripMenuItem.Name = "espacosToolStripMenuItem";
            espacosToolStripMenuItem.Size = new Size(133, 22);
            espacosToolStripMenuItem.Text = "Espaços";
            // 
            // emprestimosToolStripMenuItem
            // 
            emprestimosToolStripMenuItem.Name = "emprestimosToolStripMenuItem";
            emprestimosToolStripMenuItem.Size = new Size(88, 20);
            emprestimosToolStripMenuItem.Text = "Empréstimos";
            // 
            // devolucoesToolStripMenuItem
            // 
            devolucoesToolStripMenuItem.Name = "devolucoesToolStripMenuItem";
            devolucoesToolStripMenuItem.Size = new Size(80, 20);
            devolucoesToolStripMenuItem.Text = "Devoluções";
            // 
            // reservaDeEspacosToolStripMenuItem
            // 
            reservaDeEspacosToolStripMenuItem.Name = "reservaDeEspacosToolStripMenuItem";
            reservaDeEspacosToolStripMenuItem.Size = new Size(120, 20);
            reservaDeEspacosToolStripMenuItem.Text = "Reserva de Espaços";
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
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FrmPrincipal";
            Text = "Sistema de Gestão da Biblioteca";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
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
    }
}
