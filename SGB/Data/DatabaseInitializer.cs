using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace SGB.Data;

public static class DatabaseInitializer
{
    public static void Inicializar(string connectionString)
    {
        GarantirBancoExiste(connectionString);
        ExecutarSchema(connectionString);
        CriarAdminSeNecessario(connectionString);
    }

    private static void GarantirBancoExiste(string connectionString)
    {
        var builder = new SqlConnectionStringBuilder(connectionString);
        string banco = builder.InitialCatalog;
        builder.InitialCatalog = "master";

        using var conn = new SqlConnection(builder.ConnectionString);
        conn.Open();
        using var cmd = new SqlCommand(
            $"IF DB_ID(N'{banco}') IS NULL CREATE DATABASE [{banco}];", conn);
        cmd.ExecuteNonQuery();
    }

    private static void ExecutarSchema(string connectionString)
    {
        string caminho = Path.Combine(AppContext.BaseDirectory, "Database", "schema.sql");
        string script = File.ReadAllText(caminho);

        using var conn = new SqlConnection(connectionString);
        conn.Open();

        var lotes = Regex.Split(script, @"^[ \t]*GO[ \t]*\r?$",
                                RegexOptions.Multiline | RegexOptions.IgnoreCase);

        foreach (var lote in lotes)
        {
            if (string.IsNullOrWhiteSpace(lote)) continue;
            using var cmd = new SqlCommand(lote, conn);
            cmd.ExecuteNonQuery();
        }
    }

    private static void CriarAdminSeNecessario(string connectionString)
    {
        using var conn = new SqlConnection(connectionString);
        conn.Open();

        using (var check = new SqlCommand("SELECT COUNT(*) FROM dbo.Usuarios", conn))
        {
            if ((int)check.ExecuteScalar()! > 0) return;
        }

        using var ins = new SqlCommand(
            @"INSERT INTO dbo.Usuarios (Nome, Email, SenhaHash, Perfil, TipoUsuario)
      VALUES (@nome, @email, @hash, 'Administrador', 'Funcionario')", conn);
        ins.Parameters.AddWithValue("@nome", "Administrador");
        ins.Parameters.AddWithValue("@email", "admin@sgb.local");
        ins.Parameters.AddWithValue("@hash", BCrypt.Net.BCrypt.HashPassword("admin123"));
        ins.ExecuteNonQuery();
    }
}
