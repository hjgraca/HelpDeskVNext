using HelpDeskVNext.Data.Entitidades;
using HelpDeskVNext.Data.Models;
using HelpDeskVNext.Data.Models.Roles;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDeskVNext.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UtilizadoresController : Controller
    {
        private readonly IService<ApplicationUser, string> _userService;

        public UtilizadoresController(IService<ApplicationUser, string> userService)
        {
            _userService = userService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_userService.Get());
        }

        public IActionResult Editar(string id)
        {
            return View(_userService.Get(id));
        }

        [HttpPost]
        public IActionResult Editar(ApplicationUser user)
        {
            _userService.Update(user);
            return Redirect();
        }

        [HttpPost]
        public IActionResult Apagar(string id)
        {
            _userService.Delete(id);
            return Redirect();
        }

        public IActionResult List(string id)
        {
            var roles = _userService.Get().Where(r =>
            {
                var identityUserRole = r.Roles.FirstOrDefault(x => x.RoleId == id);
                return identityUserRole != null && identityUserRole.UserId == r.Id;
            });
            return View("Index", roles);
        }

        private IActionResult Redirect()
        {
            return RedirectToAction(nameof(UtilizadoresController.Index));
        }
    }
}
