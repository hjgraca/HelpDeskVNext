using HelpDeskVNext.Data.Entitidades;
using HelpDeskVNext.Data.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace HelpDeskVNext.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class DepartamentosController : Controller
    {
        private readonly IService<Departamento, int> _departamentoService;

        public DepartamentosController(IService<Departamento, int> departamentoService)
        {
            _departamentoService = departamentoService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_departamentoService.Get());
        }

        public IActionResult Editar(int id)
        {
            return View(_departamentoService.Get(id));
        }

        [HttpPost]
        public IActionResult Editar(Departamento departamento)
        {
            _departamentoService.Update(departamento);
            return Redirect();
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(Departamento departamento)
        {
            _departamentoService.Create(departamento);
            return Redirect();
        }

        [HttpPost]
        public IActionResult Apagar(int id)
        {
            _departamentoService.Delete(id);
            return Redirect();
        }

        private IActionResult Redirect()
        {
            return RedirectToAction(nameof(DepartamentosController.Index), "Departamentos");
        }
    }
}
