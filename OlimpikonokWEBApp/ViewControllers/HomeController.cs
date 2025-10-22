using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OlimpikonokWEBApp.Models;

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

        public IActionResult ModositSportolo(int id)
        {
            Sportolo sportolo = new SportoloController().GetSportoloById(id);
            TempData["sportoloData"] = sportolo;
            return View();
        }

        public IActionResult ModositasEredmenye(Sportolo modositott)
        {
            if (modositott == null)
            {
                return View("ModositasEredmenye", "Hiba, nincs módosítandó adat");
            }
            modositott.Id = int.Parse(Request.Form["id"].ToString());
            modositott.Nev = Request.Form["nev"].ToString();
            modositott.Neme = Request.Form["neme"].ToString() == "1" ? true : false;
            modositott.SzulDatum = Request.Form["szuldatum"].ToString() != "" ? DateTime.Parse(Request.Form["szulDatum"].ToString()): DateTime.Now;
            modositott.Ermek = int.Parse(Request.Form["ermekszama"].ToString());
            return View("ModositasEredmenye", $"{modositott.ToString()}" + new SportoloController().PutSportolo(modositott) );
        }

        public IActionResult TorolSportolo(int id)
        {
            return View();
        }
        public IActionResult Sportolok()
        {
            return View(new SportoloController().GetSportolok());
        }

        public IActionResult NagyKep(int id)
        {
            return View(new SportoloController().GetSportoloDTOById(id));
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
