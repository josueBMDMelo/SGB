using Microsoft.Data.SqlClient;
using SGB.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace SGB
{
    public partial class FrmCadastroUsuario : Form
    {
        public FrmCadastroUsuario()
        {
            InitializeComponent();
        }

        private void FrmCadastroUsuario_Load(object sender, EventArgs e)
        {
            // Valida permissão estrita de Administrador (RF02 / RF03)
            if (!ControleAcesso.ValidarAcesso(this, "Administrador"))
                return;

            InicializarCampos();
        }

        private void InicializarCampos()
        {
            cmbPerfil.Enabled = true;

            if (cmbPerfil.SelectedIndex < 0)
            {
                cmbPerfil.SelectedItem = "Usuario";
            }

            if (cmbTipoUsuario.SelectedIndex < 0)
            {
                cmbTipoUsuario.SelectedIndex = 0;
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            // Dupla checagem antes de persistir
            if (!ControleAcesso.EhAdmin())
            {
                MessageBox.Show("Apenas administradores podem cadastrar usuários.",
                                "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtSenha.Text) ||
                cmbPerfil.SelectedItem == null ||
                cmbTipoUsuario.SelectedItem == null)
            {
                MessageBox.Show("Preencha todos os campos obrigatórios.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string perfilSelecionado = cmbPerfil.SelectedItem.ToString();
            string tipoSelecionado = cmbTipoUsuario.SelectedItem.ToString();

            string sql = @"INSERT INTO Usuarios (Nome, Email, SenhaHash, Perfil, TipoUsuario)
                           VALUES (@Nome, @Email, @Senha, @Perfil, @Tipo)";

            var parametros = new[]
            {
                new SqlParameter("@Nome", txtNome.Text.Trim()),
                new SqlParameter("@Email", txtEmail.Text.Trim()),
                new SqlParameter("@Senha", BCrypt.Net.BCrypt.HashPassword(txtSenha.Text.Trim())),
                new SqlParameter("@Perfil", perfilSelecionado),
                new SqlParameter("@Tipo", tipoSelecionado),
            };

            try
            {
                ConexaoBanco.ExecutarComando(sql, parametros);
                MessageBox.Show("Usuário cadastrado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimparCampos();
                InicializarCampos();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Não foi possível cadastrar o usuário: " + ex.Message,
                                "Erro de Banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtEmail.Clear();
            txtSenha.Clear();
            txtNome.Focus();
        }
    }
}