using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using HelpDeskVNext.Data.Entitidades;
using HelpDeskVNext.Data.Models;

namespace HelpDeskVNext.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Tickets
        public IActionResult Index()
        {
            var applicationDbContext = _context.Tickets.Include(t => t.CreatedByUtilizador).Include(t => t.Departamento).Include(t => t.Estado).Include(t => t.Prioridade).Include(t => t.Tecnico);
            //return View(applicationDbContext.ToList());
            ViewBag.Departamentos = new SelectList(_context.Departamentos, "DepartamentoId", "Nome");
            ViewBag.Prioridades = new SelectList(_context.Prioridades, "PrioridadeId", "Designacao");

            return View("Board");
        }

        // GET: Tickets/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Ticket ticket = _context.Tickets.Single(m => m.TicketId == id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewBag.Departamentos = new SelectList(_context.Departamentos, "DepartamentoId", "Nome");
            ViewBag.Prioridades = new SelectList(_context.Prioridades, "PrioridadeId", "Designacao");
            return View();
        }

        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.CreatedByUtilizadorId = User.GetUserId();
                ticket.DataInsercao = DateTime.Now;
                ticket.EstadoId = 1;

                _context.Tickets.Add(ticket);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["CreatedByUtilizadorId"] = new SelectList(_context.Users, "Id", "CreatedByUtilizador", ticket.CreatedByUtilizadorId);
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "DepartamentoId", "Departamento", ticket.DepartamentoId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadoId", "Estado", ticket.EstadoId);
            ViewData["PrioridadeId"] = new SelectList(_context.Prioridades, "PrioridadeId", "Prioridade", ticket.PrioridadeId);
            ViewData["TecnicoId"] = new SelectList(_context.Users, "Id", "Tecnico", ticket.TecnicoId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Ticket ticket = _context.Tickets.Single(m => m.TicketId == id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewData["CreatedByUtilizadorId"] = new SelectList(_context.Users, "Id", "CreatedByUtilizador", ticket.CreatedByUtilizadorId);
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "DepartamentoId", "Departamento", ticket.DepartamentoId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadoId", "Estado", ticket.EstadoId);
            ViewData["PrioridadeId"] = new SelectList(_context.Prioridades, "PrioridadeId", "Prioridade", ticket.PrioridadeId);
            ViewData["TecnicoId"] = new SelectList(_context.Users, "Id", "Tecnico", ticket.TecnicoId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Update(ticket);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["CreatedByUtilizadorId"] = new SelectList(_context.Users, "Id", "CreatedByUtilizador", ticket.CreatedByUtilizadorId);
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "DepartamentoId", "Departamento", ticket.DepartamentoId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadoId", "Estado", ticket.EstadoId);
            ViewData["PrioridadeId"] = new SelectList(_context.Prioridades, "PrioridadeId", "Prioridade", ticket.PrioridadeId);
            ViewData["TecnicoId"] = new SelectList(_context.Users, "Id", "Tecnico", ticket.TecnicoId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Ticket ticket = _context.Tickets.Single(m => m.TicketId == id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = _context.Tickets.Single(m => m.TicketId == id);
            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
