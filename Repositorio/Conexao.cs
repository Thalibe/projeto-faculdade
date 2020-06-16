using System.Data;
using MySql.Data.MySqlClient;

namespace ficha_criacao_personagem_rpg_csharp.Repositorio
{
    public class Conexao : IConexao
    {
        public IDbConnection AbrirConexao()
        {
            using (MySqlConnection conn = new MySqlConnection("Server=carlos-database.mysql.database.azure.com; Port=3306; Database=projetofaculdade; Uid=carlossouza@carlos-database; Pwd=Ka130101!; SslMode=Preferred;"))
            {
                conn.Open();
                return conn;
            }
        }
    }
}