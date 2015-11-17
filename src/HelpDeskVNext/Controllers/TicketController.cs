using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDeskVNext.Services.ProjectManager;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelpDeskVNext.Controllers
{
    public class TicketController : Controller
    {
        private readonly IProjectManager _projectManager;

        public TicketController(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            _projectManager.AddTask();

            return View();
        }
    }
}
