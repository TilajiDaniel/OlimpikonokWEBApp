using Microsoft.AspNetCore.Mvc;
using OlimpikonokWEBApp.Models;
using System.Diagnostics;

namespace OlimpikonokWEBApp.Controllers
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
            return View();
        }
        public IActionResult UjSportolo()
        {
            return View();
        }
        public IActionResult ModositasEredmenye(Sportolo modositott)
        {
            if (modositott == null)
            {
                return View( "ModositasEredmenye", "Hiba, nincs modositando adat"   );
            }
            modositott.Nev = Request.Form["nev"].ToString();
            modositott.Neme = Request.Form["neme"].ToString() =="1" ? true: false;
            modositott.SzulDatum = Request.Form
        }
        public IActionResult ModositSporolo(int id)
        {
            return View(new SportoloController().GetSportoloById(id));
        }
        public IActionResult TorolSportolo(int id)
        {
            return View();
        }
        public IActionResult Orszagok()
        {
            return View(new OrszagokController().GetOrszagok());
        }
        public IActionResult Sportolok()
        {
            return View(new SportoloController().GetSportolok());
        }
        public IActionResult KepMegjelenites(int id)
        {
            return View(new SportoloController().GetSportoloById(id));
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
