using HelpDeskVNext.Data.Entitidades;
using HelpDeskVNext.Data.Models;
using HelpDeskVNext.Data.Models.Roles;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Mvc.Rendering;

namespace HelpDeskVNext.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UtilizadoresController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UtilizadoresController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_userManager.Users.Include(x => x.Roles).Include(x => x.Departamento));
        }

        public async Task<IActionResult> Editar(string id)
        {
            ViewBag.Roles = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name
            });

            var model = _userManager.Users.Include(x => x.Departamento).FirstOrDefault(x => x.Id == id);
            model.RoleNames = await _userManager.GetRolesAsync(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ApplicationUser editarUser)
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                user.DepartamentoId = editarUser.Departamento.DepartamentoId;
                user.Nome = editarUser.Nome;
                user.Email = editarUser.Email;
                user.PhoneNumber = editarUser.PhoneNumber;
                var roles = await _userManager.GetRolesAsync(user);

                var t1 = _userManager.RemoveFromRolesAsync(user, roles);
                var t2 = _userManager.AddToRolesAsync(user, editarUser.RoleNames);
                var t3 = _userManager.UpdateAsync(user);

                await Task.WhenAll(t1, t2, t3);
            }
            
            return Redirect();
        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
        }

        [HttpPost]
        public async Task<IActionResult> Apagar(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return Redirect();
        }

        public async Task<IActionResult> List(string id)
        {
            var users = await _userManager.GetUsersInRoleAsync(id);
            var t = _userManager.Users.Where(x => users.Select(r => r.Id).Contains(x.Id))
                .Include(x => x.Roles).Include(x => x.Departamento);
            
            return View("Index", t);
        }

        private IActionResult Redirect()
        {
            return RedirectToAction(nameof(UtilizadoresController.Index));
        }
    }
}
