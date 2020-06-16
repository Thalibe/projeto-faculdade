using System.Data;

namespace ficha_criacao_personagem_rpg_csharp.Repositorio
{
    public interface IConexao
    {
        IDbConnection AbrirConexao();
    }
}