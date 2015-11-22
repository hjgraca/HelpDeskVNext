using System.Linq;
using HelpDeskVNext.Data.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;

namespace HelpDeskVNext.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AcessosController : Controller
    {
        private readonly IService<IdentityRole, string> _roleService;

        public AcessosController(IService<IdentityRole, string> roleService)
        {
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            return View(_roleService.Get());
        }

        public IActionResult Editar(string id)
        {
            return View(_roleService.Get(id));
        }

        [HttpPost]
        public IActionResult Editar(IdentityRole role)
        {
            _roleService.Update(role);
            return Redirect();
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(IdentityRole role)
        {
            _roleService.Create(role);
            return Redirect();
        }

        [HttpPost]
        public IActionResult Apagar(string id)
        {
            _roleService.Delete(id);
            return Redirect();
        }

        public IActionResult List(string id)
        {
            var roles = _roleService.Get().Where(r =>
            {
                var identityUserRole = r.Users.FirstOrDefault(x => x.UserId == id);
                return identityUserRole != null && identityUserRole.RoleId == r.Id;
            });
            return View("Index", roles);
        }

        private IActionResult Redirect()
        {
            return RedirectToAction(nameof(AcessosController.Index));
        }
    }
}
