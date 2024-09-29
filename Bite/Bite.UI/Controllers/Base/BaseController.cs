using Microsoft.AspNetCore.Mvc;

namespace Bite.UI.Controllers.Base
{
    public class BaseController : Controller
    {
        protected List<string> GetModelErrors()
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                .ToList();

            return errors;
        }
    }
}
