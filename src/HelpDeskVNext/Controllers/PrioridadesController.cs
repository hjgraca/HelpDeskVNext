using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using HelpDeskVNext.Data.Entitidades;
using HelpDeskVNext.Data.Models;
using Microsoft.AspNet.Authorization;

namespace HelpDeskVNext.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class PrioridadesController : Controller
    {
        private ApplicationDbContext _context;

        public PrioridadesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Prioridades1
        public IActionResult Index()
        {
            return View(_context.Prioridades.ToList());
        }

        // GET: Prioridades1/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Prioridade prioridade = _context.Prioridades.Single(m => m.PrioridadeId == id);
            if (prioridade == null)
            {
                return HttpNotFound();
            }

            return View(prioridade);
        }

        // GET: Prioridades1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prioridades1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Prioridade prioridade)
        {
            if (ModelState.IsValid)
            {
                _context.Prioridades.Add(prioridade);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prioridade);
        }

        // GET: Prioridades1/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Prioridade prioridade = _context.Prioridades.Single(m => m.PrioridadeId == id);
            if (prioridade == null)
            {
                return HttpNotFound();
            }
            return View(prioridade);
        }

        // POST: Prioridades1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Prioridade prioridade)
        {
            if (ModelState.IsValid)
            {
                _context.Update(prioridade);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prioridade);
        }

        // GET: Prioridades1/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Prioridade prioridade = _context.Prioridades.Single(m => m.PrioridadeId == id);
            if (prioridade == null)
            {
                return HttpNotFound();
            }

            return View(prioridade);
        }

        // POST: Prioridades1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Prioridade prioridade = _context.Prioridades.Single(m => m.PrioridadeId == id);
            _context.Prioridades.Remove(prioridade);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
