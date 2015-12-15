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
    public class EstadosController : Controller
    {
        private ApplicationDbContext _context;

        public EstadosController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Estados
        public IActionResult Index()
        {
            return View(_context.Estados.ToList());
        }

        // GET: Estados/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Estado estado = _context.Estados.Single(m => m.EstadoId == id);
            if (estado == null)
            {
                return HttpNotFound();
            }

            return View(estado);
        }

        // GET: Estados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Estado estado)
        {
            if (ModelState.IsValid)
            {
                _context.Estados.Add(estado);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estado);
        }

        // GET: Estados/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Estado estado = _context.Estados.Single(m => m.EstadoId == id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            return View(estado);
        }

        // POST: Estados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Estado estado)
        {
            if (ModelState.IsValid)
            {
                _context.Update(estado);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estado);
        }

        // GET: Estados/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Estado estado = _context.Estados.Single(m => m.EstadoId == id);
            if (estado == null)
            {
                return HttpNotFound();
            }

            return View(estado);
        }

        // POST: Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Estado estado = _context.Estados.Single(m => m.EstadoId == id);
            _context.Estados.Remove(estado);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
