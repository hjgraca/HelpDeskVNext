using System.Linq;
using System.Security.Claims;
using HelpDeskVNext.Data.Entitidades;
using HelpDeskVNext.Data.Models;
using Microsoft.AspNet.Mvc;

namespace HelpDeskVNext.Controllers
{
    public class TicketController : Controller
    {
        private readonly IService<Ticket, int> _ticketService;

        public TicketController(IService<Ticket, int> ticketService)
        {
            _ticketService = ticketService;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Administrador"))
            {
                ViewBag.Titulo = "Gerir Tickets";
                return View(_ticketService.Get());
            }

            ViewBag.Titulo = "Os meus Tickets";
            if (User.IsInRole("Tecnico"))
            {
                return View(_ticketService.Get().Where(x => x.TecnicoId == User.GetUserId()));
            }

            return View(_ticketService.Get().Where(x => x.CreatedByUtilizadorId == User.GetUserId()));
        }

        public IActionResult Editar(int id)
        {
            return View(_ticketService.Get(id));
        }

        [HttpPost]
        public IActionResult Editar(Ticket ticket)
        {
            _ticketService.Update(ticket);
            return Redirect();
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(Ticket ticket)
        {
            _ticketService.Create(ticket);
            return Redirect();
        }

        private IActionResult Redirect()
        {
            return RedirectToAction(nameof(TicketController.Index), "Ticket");
        }
    }
}
