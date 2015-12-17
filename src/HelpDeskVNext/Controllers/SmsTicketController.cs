using System;
using System.Linq;
using HelpDeskVNext.Data.Entitidades;
using HelpDeskVNext.Data.Models;
using Microsoft.AspNet.Mvc;

namespace HelpDeskVNext.Controllers
{
    [Route("api/[controller]")]
    public class SmsTicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SmsTicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST api/ticket
        [HttpPost]
        [Produces("text/plain")]
        public IActionResult Post(string from, string body)
        {
            if (string.IsNullOrWhiteSpace(body))
            {
                return Ok("Tem de fornecer pelo menos o titulo do ticket");
            }

            var user = _context.Users.FirstOrDefault(x => x.PhoneNumber == from);
            if (user == null)
            {
                return Ok("Utilizador nao registado na aplicao. Por favor faca o registo online: http://helpdesk20151214120334.azurewebsites.net/account/register");
            }

            var values = body.Split(':');
            var ticket = new Ticket
            {
                Titulo = values[0],
                CreatedByUtilizador = user,
                EstadoId = 1,
                DataInsercao = DateTime.Now,
                Descricao = values[0]
            };

            if (values.Length > 1)
            {
                int prioridadeId;
                if (int.TryParse(values[1], out prioridadeId))
                {
                    ticket.PrioridadeId = prioridadeId;
                }
                if (values.Length > 2)
                {
                    int departamentoId;
                    if (int.TryParse(values[2], out departamentoId))
                    {
                        ticket.DepartamentoId = departamentoId;
                    }
                }
            }

            if(ticket.DepartamentoId == null)
                ticket.DepartamentoId = 1;
            if (ticket.PrioridadeId == 0)
                ticket.PrioridadeId = 1;

            GetRandomTec(ticket);

            try
            {
                _context.Tickets.Add(ticket);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

            return Ok($"Ola {ticket.CreatedByUtilizador.Nome}, o ticket #{ticket.TicketId} foi criado com sucesso." +
                      $" O tecnico {ticket.Tecnico.Nome} foi assignado ao mesmo. Pode ver o detalhe online: http://helpdesk20151214120334.azurewebsites.net/Tickets/Details/{ticket.TicketId}");
        }

        private void GetRandomTec(Ticket ticket)
        {
            var rnd = new Random();
            // so assigna tickets a user do departamento
            var users = _context.Users.Where(x => x.DepartamentoId == ticket.DepartamentoId).ToList();
            var index = rnd.Next(users.Count);
            ticket.Tecnico = users[index];
        }
    }
}
