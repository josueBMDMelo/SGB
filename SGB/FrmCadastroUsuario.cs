using Microsoft.Data.SqlClient;
using SGB.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            AplicarPermissoes();
        }

        private void AplicarPermissoes()
        {
            bool ehAdministrador = string.Equals(SessaoUsuario.Perfil, "Administrador", StringComparison.OrdinalIgnoreCase);

            // Se não for Administrador, fixa o perfil como "Usuario" e impede alterações
            if (!ehAdministrador)
            {
                cmbPerfil.SelectedItem = "Usuario";
                cmbPerfil.Enabled = false;
            }
            else
            {
                cmbPerfil.Enabled = true;
                if (cmbPerfil.SelectedIndex < 0)
                {
                    cmbPerfil.SelectedItem = "Usuario";
                }
            }

            if (cmbTipoUsuario.SelectedIndex < 0)
            {
                cmbTipoUsuario.SelectedIndex = 0;
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtSenha.Text) ||
                cmbPerfil.SelectedItem == null ||
                cmbTipoUsuario.SelectedItem == null)
            {
                MessageBox.Show("Preencha todos os campos.");
                return;
            }

            string perfilSelecionado = cmbPerfil.SelectedItem.ToString();
            bool ehAdministrador = string.Equals(SessaoUsuario.Perfil, "Administrador", StringComparison.OrdinalIgnoreCase);

            // Validação de segurança (RF02): impede escalação de privilégios
            if (!ehAdministrador && !string.Equals(perfilSelecionado, "Usuario", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Apenas administradores podem cadastrar utilizadores com perfil elevado.",
                                "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = @"INSERT INTO Usuarios (Nome, Email, SenhaHash, Perfil, TipoUsuario)
                           VALUES (@Nome, @Email, @Senha, @Perfil, @Tipo)";

            var parametros = new[]
            {
                new SqlParameter("@Nome", txtNome.Text.Trim()),
                new SqlParameter("@Email", txtEmail.Text.Trim()),
                new SqlParameter("@Senha", BCrypt.Net.BCrypt.HashPassword(txtSenha.Text.Trim())),
                new SqlParameter("@Perfil", perfilSelecionado),
                new SqlParameter("@Tipo", cmbTipoUsuario.SelectedItem.ToString()),
            };

            try
            {
                ConexaoBanco.ExecutarComando(sql, parametros);
                MessageBox.Show("Usuário cadastrado com sucesso.");
                txtNome.Clear();
                txtEmail.Clear();
                txtSenha.Clear();

                AplicarPermissoes();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Não foi possível cadastrar: " + ex.Message);
            }
        }
    }
}