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
    public class DepartamentosController : Controller
    {
        private ApplicationDbContext _context;

        public DepartamentosController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Departamentoes
        public IActionResult Index()
        {
            return View(_context.Departamentos.ToList());
        }

        // GET: Departamentoes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Departamento departamento = _context.Departamentos.Single(m => m.DepartamentoId == id);
            if (departamento == null)
            {
                return HttpNotFound();
            }

            return View(departamento);
        }

        // GET: Departamentoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departamentoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _context.Departamentos.Add(departamento);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departamento);
        }

        // GET: Departamentoes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Departamento departamento = _context.Departamentos.Single(m => m.DepartamentoId == id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // POST: Departamentoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _context.Update(departamento);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departamento);
        }

        // GET: Departamentoes/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Departamento departamento = _context.Departamentos.Single(m => m.DepartamentoId == id);
            if (departamento == null)
            {
                return HttpNotFound();
            }

            return View(departamento);
        }

        // POST: Departamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Departamento departamento = _context.Departamentos.Single(m => m.DepartamentoId == id);
            _context.Departamentos.Remove(departamento);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
