using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Panelak.DataTable.WebTest.Models;
using System.Diagnostics;

namespace Panelak.DataTable.WebTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataProtector dp;

        public HomeController(ILogger<HomeController> logger, IDataProtectionProvider dpp)
        {
            _logger = logger;
            this.dp = dpp.CreateProtector("TEST");
        }

        public IActionResult Index()
        {
            string prot = dp.Protect("text");
            string unprot = dp.Unprotect(prot);
            return View();
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
