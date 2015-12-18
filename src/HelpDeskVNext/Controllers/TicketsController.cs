using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using HelpDeskVNext.Data.Entitidades;
using HelpDeskVNext.Data.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNet.Authorization;

namespace HelpDeskVNext.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private ApplicationDbContext _context;
        private readonly TelemetryClient _telemetry;

        public TicketsController(ApplicationDbContext context, TelemetryClient telemetry)
        {
            _context = context;
            _telemetry = telemetry;
        }

        // GET: Tickets1
        public IActionResult Index()
        {
            var applicationDbContext = _context.Tickets.Where(x => x.CreatedByUtilizadorId == User.GetUserId()).Include(t => t.CreatedByUtilizador).Include(t => t.Departamento).Include(t => t.Estado).Include(t => t.Prioridade).Include(t => t.Tecnico);
            return View(applicationDbContext.ToList());
        }

        [AllowAnonymous]
        // GET: Tickets1/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Ticket ticket = _context.Tickets.Include(t => t.Departamento).Include(t => t.Estado).Include(t => t.Prioridade).Include(t => t.Tecnico).Single(m => m.TicketId == id);

            if (ticket == null)
            {
                return HttpNotFound();
            }

            return View(ticket);
        }

        // GET: Tickets1/Create
        public IActionResult Create()
        {
            ViewBag.Departamentos = new SelectList(_context.Departamentos, "DepartamentoId", "Nome");
            ViewBag.Prioridades = new SelectList(_context.Prioridades, "PrioridadeId", "Designacao");

            return View();
        }

        // POST: Tickets1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.CreatedByUtilizadorId = User.GetUserId();
                ticket.EstadoId = 1;
                ticket.DataInsercao = DateTime.Now;

                GetRandomTec(ticket);

                _context.Tickets.Add(ticket);
                _context.SaveChanges();

                _telemetry.TrackEvent("ticketcriado");

                return RedirectToAction("Index");
            }
            
            ViewBag.Departamentos = new SelectList(_context.Departamentos, "DepartamentoId", "Nome", ticket.DepartamentoId);
            ViewBag.Prioridades = new SelectList(_context.Prioridades, "PrioridadeId", "Designacao", ticket.PrioridadeId);

            return View(ticket);
        }

        private void GetRandomTec(Ticket ticket)
        {
            var rnd = new Random();
            // so assigna tickets a user do departamento
            var users = _context.Users.Where(x => x.DepartamentoId == ticket.DepartamentoId).ToList();
            var index = rnd.Next(users.Count);
            ticket.TecnicoId = users[index].Id;
        }

        // GET: Tickets1/Edit/5
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
            ViewBag.Departamentos = new SelectList(_context.Departamentos, "DepartamentoId", "Nome", ticket.DepartamentoId);
            ViewBag.Prioridades = new SelectList(_context.Prioridades, "PrioridadeId", "Designacao", ticket.PrioridadeId);

            return View(ticket);
        }

        // POST: Tickets1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.DataActualizacao = DateTime.Now;
                _context.Update(ticket);
                _context.SaveChanges();

                _telemetry.TrackEvent("ticketeditado");

                return RedirectToAction("Index");
            }

            ViewBag.Departamentos = new SelectList(_context.Departamentos, "DepartamentoId", "Nome", ticket.DepartamentoId);
            ViewBag.Prioridades = new SelectList(_context.Prioridades, "PrioridadeId", "Designacao", ticket.PrioridadeId);
            return View(ticket);
        }

        // GET: Tickets1/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            Ticket ticket = _context.Tickets.Single(m => m.TicketId == id);
            _context.Tickets.Remove(ticket);
            _context.SaveChanges();

            _telemetry.TrackEvent("ticketapagado");

            return RedirectToAction("Index");
        }
    }
}
