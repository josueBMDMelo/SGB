using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGB.Data;

namespace SGB
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string sql = "SELECT Id, Nome, Perfil, SenhaHash FROM Usuarios WHERE Email = @Email AND Ativo = 1";
            var parametros = new[] { new SqlParameter("@Email", txtEmail.Text.Trim()) };

            var resultado = ConexaoBanco.ExecutarConsulta(sql, parametros);

            if (resultado.Rows.Count == 1 &&
                BCrypt.Net.BCrypt.Verify(txtSenha.Text.Trim(), resultado.Rows[0]["SenhaHash"].ToString()))
            {
                int id = Convert.ToInt32(resultado.Rows[0]["Id"]);
                string nome = resultado.Rows[0]["Nome"].ToString();
                string perfil = resultado.Rows[0]["Perfil"].ToString();

                SessaoUsuario.Iniciar(id, nome, perfil);

                var principal = new FrmPrincipal(nome, perfil);
                principal.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Não foram fornecidos todos os dados para login");
                return;
            }
        }
    }
}
