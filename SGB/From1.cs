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
            bool podeGerenciar = _perfil is "Bibliotecario" or "Administrador";

            usuariosToolStripMenuItem.Visible = podeGerenciar;
            // ajuste os nomes abaixo para os itens reais do seu menuStrip1:
            // cadastroLivroToolStripMenuItem.Visible = podeGerenciar;
            // cadastroExemplarToolStripMenuItem.Visible = podeGerenciar;
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
