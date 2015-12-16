using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using HelpDeskVNext.Data.Models;

namespace HelpDeskVNext.Controllers
{
    public class BoardController : Controller
    {
        private ApplicationDbContext _context;

        public BoardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public IActionResult Index()
        {
            ViewBag.Departamentos = new SelectList(_context.Departamentos, "DepartamentoId", "Nome");
            ViewBag.Prioridades = new SelectList(_context.Prioridades, "PrioridadeId", "Designacao");
            ViewBag.Estados = new SelectList(_context.Estados, "EstadoId", "Designacao");

            var role = (from r in _context.Roles where r.Name == "Tecnico" select r).FirstOrDefault();
            var users = _context.Users.Include(x => x.Roles).ToList();

            ViewBag.Tecnicos = new SelectList(users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)), "Id",
                "Nome");

            return View();
        }
    }
}
