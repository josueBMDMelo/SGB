using SGB.Data;
using System;
using System.Windows.Forms;

namespace SGB
{
    public static class ControleAcesso
    {
        public static bool EhAdmin() =>
            string.Equals(SessaoUsuario.Perfil, "Administrador", StringComparison.OrdinalIgnoreCase);

        public static bool EhBibliotecario() =>
            string.Equals(SessaoUsuario.Perfil, "Bibliotecario", StringComparison.OrdinalIgnoreCase);

        public static bool EhUsuarioComum() =>
            string.Equals(SessaoUsuario.Perfil, "Usuario", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Bloqueia e fecha o Form se o perfil logado não estiver entre os perfis permitidos.
        /// </summary>
        public static bool ValidarAcesso(Form form, params string[] perfisPermitidos)
        {
            if (string.IsNullOrWhiteSpace(SessaoUsuario.Perfil))
            {
                MessageBox.Show("Sessão inválida ou não autenticada.", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                form.Close();
                return false;
            }

            foreach (var perfil in perfisPermitidos)
            {
                if (string.Equals(SessaoUsuario.Perfil, perfil, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            MessageBox.Show($"Seu perfil ({SessaoUsuario.Perfil}) não tem permissão para acessar esta tela.",
                            "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            form.Close();
            return false;
        }
    }
}