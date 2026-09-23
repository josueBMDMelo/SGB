using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGB.Data
{
    public static class SessaoUsuario
    {
        public static int Id { get; private set; }
        public static string Nome { get; private set; } = "";
        public static string Perfil { get; private set; } = "";

        public static void Iniciar(int id, string nome, string perfil)
        {
            Id = id;
            Nome = nome;
            Perfil = perfil;
        }
    }
}
