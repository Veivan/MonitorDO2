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

        public IActionResult Index(DateTime rddate, SortState sortOrder = SortState.AwbNumberAsc)
        {
            if (rddate == DateTime.MinValue) rddate = DateTime.Today;
            //if (rddate == DateTime.MinValue) rddate = new DateTime(2021, 4, 22);

            ViewData["RdDate"] = rddate.ToString(System.Globalization.CultureInfo.InvariantCulture);
            ViewData["AwbNumberSort"] = sortOrder == SortState.AwbNumberAsc ? SortState.AwbNumberDesc : SortState.AwbNumberAsc;
            ViewData["AwbTechSort"] = sortOrder == SortState.AwbTechAsc ? SortState.AwbTechDesc : SortState.AwbTechAsc;

            var viewModel = new Do2ViewModel
            {
                RdDate = rddate,
                RdWoDo2s = repo.GetDo2s(rddate, sortOrder)
            };
            return View(viewModel);
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
