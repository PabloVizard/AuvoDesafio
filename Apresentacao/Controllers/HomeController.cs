using Apresentacao.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Apresentacao.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            ViewBag.Nome = "Pablo Ferreira";
            ViewBag.CargoPretendido = "Desenvolvedor .Net";
            ViewBag.PretensaoSalarial = "R$ 4.000,00";
            ViewBag.DataTeste = "10/04/2023";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}