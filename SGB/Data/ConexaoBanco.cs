using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SGB.Data;

public static class ConexaoBanco
{
    private static string ConnectionString =>
        ConfigurationManager.ConnectionStrings["SGB"].ConnectionString;

    public static DataTable ExecutarConsulta(string sql, params SqlParameter[] parametros)
    {
        using var conexao = new SqlConnection(ConnectionString);
        using var comando = new SqlCommand(sql, conexao);
        if (parametros.Length > 0) comando.Parameters.AddRange(parametros);

        var tabela = new DataTable();
        using var adaptador = new SqlDataAdapter(comando);
        adaptador.Fill(tabela);
        return tabela;
    }

    public static int ExecutarComando(string sql, params SqlParameter[] parametros)
    {
        using var conexao = new SqlConnection(ConnectionString);
        using var comando = new SqlCommand(sql, conexao);
        if (parametros.Length > 0) comando.Parameters.AddRange(parametros);

        conexao.Open();
        return comando.ExecuteNonQuery();
    }

    public static void ExecutarEmTransacao(Action<SqlConnection, SqlTransaction> operacoes)
    {
        using var conexao = new SqlConnection(ConnectionString);
        conexao.Open();
        using var transacao = conexao.BeginTransaction();

        try
        {
            operacoes(conexao, transacao);
            transacao.Commit();
        }
        catch
        {
            transacao.Rollback();
            throw;
        }
    }
}