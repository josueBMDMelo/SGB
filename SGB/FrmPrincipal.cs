using System.Windows.Forms;

namespace SGB
{
    public partial class FrmPrincipal : Form
    {
        private readonly string _perfil;
        private const string VersaoSistema = "1.0.0";

        public FrmPrincipal(string nome, string perfil)
        {
            InitializeComponent();
            _perfil = perfil;
            toolStripStatusLabelVersao.Text = $"v{VersaoSistema}";
            toolStripStatusLabelVersao.Alignment = ToolStripItemAlignment.Right;
            toolStripStatusLabel1.Text = $"Bem-vindo, {nome}!";
            AplicarPermissoesDoPerfil();
        }

        private void AplicarPermissoesDoPerfil()
        {
            bool ehAdmin = ControleAcesso.EhAdmin();
            bool ehOperador = ControleAcesso.EhBibliotecario() || ehAdmin;

            // Gestão de Sistema e Cadastros Base (Apenas Administrador)
            usuariosToolStripMenuItem.Visible = ehAdmin;
            espacosToolStripMenuItem.Visible = ehAdmin;

            // Balcão de Atendimento e Gestão do Acervo (Bibliotecário e Administrador)
            livrosToolStripMenuItem.Visible = ehOperador;
            exemplaresToolStripMenuItem.Visible = ehOperador;
            realizarEmprestimoToolStripMenuItem.Visible = ehOperador;
            devolucoesToolStripMenuItem.Visible = ehOperador;
            emprestimosRealizadosToolStripMenuItem.Visible = ehOperador;

            // Autosserviço e Consultas Pessoais (Todos os perfis)
            meusEmprestimosToolStripMenuItem.Visible = true;
            reservaDeEspacosToolStripMenuItem.Visible = true;

            // Menu Sessão (Logout / Sair do sistema - liberado para todos)
            sessaoToolStripMenuItem.Visible = true;
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
