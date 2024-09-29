using Bite.UI.Controllers.Base;
using Bite.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bite.UI.Controllers
{
    public class UsuarioController : BaseController
    {
        public IActionResult CadastroUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(UsuarioVM usuario)
        {
            if (!ModelState.IsValid)
            {
                var errors = GetModelErrors();
                return Json(new { success = false, errors });
            }

            // Lógica para cadastrar o usuário...
            return Json(new { success = true });
        }

    }
}
