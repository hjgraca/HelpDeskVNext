using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using HelpDeskVNext.Data.Entitidades;
using HelpDeskVNext.Data.Models;
using HelpDeskVNext.ViewModels.Tickets;
using System.Security.Claims;

namespace HelpDeskVNext.Controllers
{
    [Produces("application/json")]
    [Route("api/BoardWebApi")]
    public class BoardWebApiController : Controller
    {
        private ApplicationDbContext _context;

        public BoardWebApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BoardWebApi
        [HttpGet]
        public IEnumerable<BoardColumn> GetBoard()
        {
            foreach (var estado in _context.Estados.ToList())
            {
                yield return new BoardColumn
                {
                    Id = estado.EstadoId,
                    Nome = estado.Designacao,
                    Tickets = _context.Tickets.Where(x => x.EstadoId == estado.EstadoId)
                    .OrderBy(x => x.Posicao)
                };
            }
        }

        // GET: api/BoardWebApi/5
        [HttpGet("{id}", Name = "GetTicket")]
        public IActionResult GetTicket([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Ticket ticket = _context.Tickets.Single(m => m.TicketId == id);

            if (ticket == null)
            {
                return HttpNotFound();
            }

            return Ok(ticket);
        }

        // PUT: api/BoardWebApi/5
        [HttpPut("{id}")]
        public IActionResult PutTicket(int id, [FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != ticket.TicketId)
            {
                return HttpBadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return HttpNotFound();
                }
                else
                {
                    throw;
                }
            }

            return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
        }

        [HttpPost]
        [Route("api/BoardWebApi/UpdatePosicoes")]
        public IActionResult UpdatePosicoes([FromBody]int[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                var ticket = _context.Tickets.FirstOrDefault(x => x.TicketId == ids[i]);
                if (ticket != null)
                {
                    ticket.Posicao = i + 1;
                    _context.Entry(ticket).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }

            return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
        }

        // POST: api/BoardWebApi
        [HttpPost]
        public IActionResult PostTicket([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            ticket.CreatedByUtilizadorId = User.GetUserId();
            ticket.EstadoId = 1;
            ticket.DataInsercao = DateTime.Now;
            _context.Tickets.Add(ticket);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TicketExists(ticket.TicketId))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetTicket", new { id = ticket.TicketId }, ticket);
        }

        // DELETE: api/BoardWebApi/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTicket(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Ticket ticket = _context.Tickets.Single(m => m.TicketId == id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            _context.Tickets.Remove(ticket);
            _context.SaveChanges();

            return Ok(ticket);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Count(e => e.TicketId == id) > 0;
        }
    }
}