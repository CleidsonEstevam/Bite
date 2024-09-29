#region Manutenção
/*
PROGRAMADOR: CLSILVA
DATA: 26/09/2024
AÇÃO: Implementação inicial
*/
#endregion

using Bite.Model;
using Npgsql;
using System.Data;

namespace Bite.DAL
{
    public class UsuarioDAL : BaseDAL
    {
        #region Constantes
        /// <summary>
        /// Armazena o nome da stored procedure que cadastra usuario
        /// </summary>
        private const string BT_CRIARUSUARIO = "bt_sp_criausuario";

        /// <summary>
        /// Armazena o nome da stored procedure usada para validar o usuário.
        /// </summary>
        private const string BT_VALIDA_USUARIO = "bt_sp_validausuario";
        #endregion

        #region Métodos
        /// <summary>
        /// Insere um novo usuário no sistema.
        /// </summary>
        /// <param name="empresaId">ID da empresa associada.</param>
        /// <param name="nome">Nome completo do usuário.</param>
        /// <param name="email">E-mail do usuário.</param>
        /// <param name="senha">Senha do usuário.</param>
        /// <param name="telefone">Telefone do usuário.</param>
        /// <param name="role">Função do usuário.</param>
        /// <returns>ID do usuário criado.</returns>
        public int InsereUsuario(Usuario user)
        {
            NpgsqlParameter outputIdParameter = new NpgsqlParameter
            {
                ParameterName = "p_id",
                NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer,
                Direction = ParameterDirection.Output // Indica que este é um parâmetro de saída
            };
     
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("p_empresa_id", user.EmpresaId),
                new NpgsqlParameter("p_nome", user.Nome),
                new NpgsqlParameter("p_email", user.Email),
                new NpgsqlParameter("p_senha_hash", user.Senha), 
                new NpgsqlParameter("p_telefone", user.Telefone),
                new NpgsqlParameter("p_role", user.Role),
                outputIdParameter 
            };

            ExecuteNonQuery(BT_CRIARUSUARIO, parameters);

            return (int)outputIdParameter.Value;
        }

        /// <summary>
        /// Valida se o usuário existe no sistema.
        /// </summary>
        /// <param name="email">Email do usuário.</param>
        /// <param name="senha">Senha do usuário.</param>
        /// <returns>Retorna verdadeiro se o usuário for válido, caso contrário, falso.</returns>
        public bool ValidarUsuario(string email, string senha)
        {
            NpgsqlParameter[] parameters = {
        new NpgsqlParameter("p_EMAIL", email),
        new NpgsqlParameter("p_SENHA", senha) };

            var result = ExecuteValue(BT_VALIDA_USUARIO, parameters);

            return Convert.ToInt32(result) > 0;
        }
        #endregion
    }
}
