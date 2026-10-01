using System;
using System.Windows.Forms;
using SGB.Data;

namespace SGB
{
    public partial class FrmPrincipal : Form
    {
        private readonly string _perfil;
        private readonly FrmLogin? _formLoginOrigem;
        private bool _emLogoff = false;
        private const string VersaoSistema = "1.0.0";

        public FrmPrincipal(string nome, string perfil, FrmLogin? formLogin = null)
        {
            InitializeComponent();
            _perfil = perfil;
            _formLoginOrigem = formLogin;

            toolStripStatusLabelVersao.Text = $"v{VersaoSistema}";
            toolStripStatusLabelVersao.Alignment = ToolStripItemAlignment.Right;
            toolStripStatusLabel1.Text = $"Bem-vindo, {nome}!";
            AplicarPermissoesDoPerfil();
        }

        private void AplicarPermissoesDoPerfil()
        {
            bool podeGerenciar = _perfil is "Bibliotecario" or "Administrador";
            usuariosToolStripMenuItem.Visible = podeGerenciar;
        }

        private void trocarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirmacao = MessageBox.Show(
                "Deseja realmente encerrar a sessão atual e trocar de usuário?",
                "Logoff",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                _emLogoff = true;
                SessaoUsuario.Limpar();

                if (_formLoginOrigem != null)
                {
                    _formLoginOrigem.LimparECentralizar();
                    _formLoginOrigem.Show();
                }
                else
                {
                    var novoLogin = new FrmLogin();
                    novoLogin.Show();
                }

                this.Close();
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirmacao = MessageBox.Show(
                "Deseja realmente sair do sistema?",
                "Encerrar Aplicação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Se o usuário fechar pelo "X" da janela e não estiver fazendo logoff, encerra todo o aplicativo
            if (!_emLogoff)
            {
                Application.Exit();
            }
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tela = new FrmCadastroUsuario();
            tela.ShowDialog();
        }

        private void livrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tela = new FrmCadastroLivro();
            tela.ShowDialog();
        }

        private void exemplaresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tela = new FrmCadastroExemplar();
            tela.ShowDialog();
        }

        private void realizarEmprestimoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tela = new FrmRealizarEmprestimo();
            tela.ShowDialog();
        }

        private void emprestimosRealizadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tela = new FrmEmprestimosRealizados();
            tela.ShowDialog();
        }

        private void meusEmprestimosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tela = new FrmMeusEmprestimos();
            tela.ShowDialog();
        }

        private void devolucoesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tela = new FrmDevolucao();
            tela.ShowDialog();
        }

        private void espacosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tela = new FrmCadastroEspaco();
            tela.ShowDialog();
        }

        private void reservaDeEspacosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tela = new FrmReservaEspaco();
            tela.ShowDialog();
        }
    }
}