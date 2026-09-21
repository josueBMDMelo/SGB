using System.Configuration;
using SGB.Data;

namespace SGB
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            try
            {
                string cs = ConfigurationManager.ConnectionStrings["SGB"].ConnectionString;
                DatabaseInitializer.Inicializar(cs);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível preparar o banco de dados:\n" + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new FrmLogin());
        }
    }
}