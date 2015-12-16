using HelpDeskVNext.Data.Models;
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
        private readonly ApplicationDbContext _context;

        public UtilizadoresController(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
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
            }).ToList();

            var model = _userManager.Users.Include(x => x.Departamento).FirstOrDefault(x => x.Id == id);
            model.RoleNames = await _userManager.GetRolesAsync(model);

            ViewBag.Departamentos = new SelectList(_context.Departamentos.ToList(), "DepartamentoId", "Nome", model.DepartamentoId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ApplicationUser editarUser)
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                user.DepartamentoId = editarUser.DepartamentoId;
                user.Nome = editarUser.Nome;
                user.Email = editarUser.Email;
                user.PhoneNumber = editarUser.PhoneNumber;

                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
                await _userManager.AddToRolesAsync(user, editarUser.RoleNames);
                await _userManager.UpdateAsync(user);                
            }
            
            return Redirect();
        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
        }

        [ActionName("Apagar")]
        public async Task<IActionResult> Apagar(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
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
