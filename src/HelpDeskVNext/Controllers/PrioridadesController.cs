using HelpDeskVNext.Data.Entitidades;
using HelpDeskVNext.Data.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace HelpDeskVNext.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class PrioridadesController : Controller
    {
        private readonly IService<Prioridade, int> _prioridadeService;

        public PrioridadesController(IService<Prioridade, int> prioridadeService)
        {
            _prioridadeService = prioridadeService;
        }

        public IActionResult Index()
        {
            return View(_prioridadeService.Get());
        }

        public IActionResult Editar(int id)
        {
            return View(_prioridadeService.Get(id));
        }

        [HttpPost]
        public IActionResult Editar(Prioridade prioridade)
        {
            _prioridadeService.Update(prioridade);
            return Redirect();
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(Prioridade prioridade)
        {
            _prioridadeService.Create(prioridade);
            return Redirect();
        }

        [HttpPost]
        public IActionResult Apagar(int id)
        {
            _prioridadeService.Delete(id);
            return Redirect();
        }

        private IActionResult Redirect()
        {
            return RedirectToAction(nameof(PrioridadesController.Index), "Prioridades");
        }
    }
}