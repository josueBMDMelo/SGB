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

            string sql = @"INSERT INTO Usuarios (Nome, Email, SenhaHash, Perfil, TipoUsuario)
                   VALUES (@Nome, @Email, @Senha, @Perfil, @Tipo)";

            var parametros = new[]
            {
                new SqlParameter("@Nome", txtNome.Text.Trim()),
                new SqlParameter("@Email", txtEmail.Text.Trim()),
                new SqlParameter("@Senha", BCrypt.Net.BCrypt.HashPassword(txtSenha.Text.Trim())),
                new SqlParameter("@Perfil", cmbPerfil.SelectedItem.ToString()),
                new SqlParameter("@Tipo", cmbTipoUsuario.SelectedItem.ToString()),
            };

            try
            {
                ConexaoBanco.ExecutarComando(sql, parametros);
                MessageBox.Show("Usuário cadastrado.");
                txtNome.Clear();
                txtEmail.Clear();
                txtSenha.Clear();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Não foi possível cadastrar: " + ex.Message);
            }
        }
    }
}
