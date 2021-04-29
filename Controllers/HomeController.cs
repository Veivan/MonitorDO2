using MonitorDO2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorDO2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IDoRepository repo;

        public HomeController(ILogger<HomeController> logger, IDoRepository r)
        {
            _logger = logger;
            repo = r;
        }

        public IActionResult Index()
        {
            var viewModel = new Do2ViewModel
            {
                RdWoDo2s = repo.GetDo2s()
            };
            return View(viewModel);
            //return View(repo.GetDo2s());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
