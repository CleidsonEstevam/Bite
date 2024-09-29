using Bite.DAL;
using Bite.Model;

namespace Bite.Bo
{
    public class UsuarioBo : BaseBo
    {
        #region Construtores
        public UsuarioBo() { }
        #endregion

        public int InsereUsuario(Usuario usuario)
        {
            try
            {
                UsuarioDAL usuarioDAL = new UsuarioDAL(); 
                int resultado = usuarioDAL.InsereUsuario(usuario);
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
