using System.Linq;
using System.Threading.Tasks;
using HelpDeskVNext.Data.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;

namespace HelpDeskVNext.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AcessosController : Controller
    {
        private readonly IService<IdentityRole, string> _roleService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AcessosController(IService<IdentityRole, string> roleService, RoleManager<IdentityRole> roleManager)
        {
            _roleService = roleService;
            _roleManager = roleManager;
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
        public async Task<IActionResult> Editar(IdentityRole role)
        {
            var r = await _roleManager.FindByIdAsync(role.Id);
            r.Name = role.Name;
            await _roleManager.UpdateAsync(r);
            return Redirect();
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(IdentityRole role)
        {
            await _roleManager.CreateAsync(role);
            return Redirect();
        }

        [HttpPost]
        public async Task<IActionResult> Apagar(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);
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
