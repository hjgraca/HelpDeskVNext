using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using HelpDeskVNext.Data.Entitidades;
using HelpDeskVNext.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;

namespace HelpDeskVNext.Controllers
{
    public class TicketController : Controller
    {
        private readonly IService<Ticket, int> _ticketService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketController(IService<Ticket, int> ticketService, UserManager<ApplicationUser> userManager)
        {
            _ticketService = ticketService;
            _userManager = userManager;
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
            ViewBag.Utilizadores = new List<SelectListItem>
            {
                new SelectListItem()
            }.Union(_userManager.Users.Select(x => new SelectListItem
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            }));

            return View(new Ticket());
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
