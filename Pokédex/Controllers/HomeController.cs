using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokédex.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Pokédex.Services;

namespace Pokédex.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        PokemonServices services = new PokemonServices();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult BugTypePage()
        {
            var pageModel = services.GetPageModel("bug", 12);
            return View("Index", pageModel);
        }

        public IActionResult ElectricTypePage()
        {
            var pageModel = services.GetPageModel("electric", 9);
            return View("Index", pageModel);
        }

        public IActionResult FightingTypePage()
        {
            var pageModel = services.GetPageModel("fighting", 8);
            return View("Index", pageModel);
        }
        public IActionResult FireTypePage()
        {
            var pageModel = services.GetPageModel("fire", 12);
            return View("Index", pageModel);
        }

        public IActionResult GrassTypePage()
        {
            var pageModel = services.GetPageModel("grass", 12);
            return View("Index", pageModel);
        }

        public IActionResult GroundTypePage()
        {
            var pageModel = services.GetPageModel("ground", 8);
            return View("Index", pageModel);
        }

        public IActionResult NormalTypePage()
        {
            var pageModel = services.GetPageModel("normal", 22);
            return View("Index", pageModel);
        }

        public IActionResult PoisonTypePage()
        {
            var pageModel = services.GetPageModel("poison", 14);
            return View("Index", pageModel);
        }

        public IActionResult PsychicTypePage()
        {
            var pageModel = services.GetPageModel("psychic", 8);
            return View("Index", pageModel);
        }

        public IActionResult RockTypePage()
        {
            var pageModel = services.GetPageModel("rock", 9);
            return View("Index", pageModel);
        }

        public IActionResult SpecialTypePage()
        {
            var pageModel = services.GetSpecialPageModel();
            return View("Index", pageModel);
        }

        public IActionResult WaterTypePage()
        {
            var pageModel = services.GetPageModel("water", 28);
            return View("Index", pageModel);
        }

        public ActionResult GetLeftPanel(PanelModel model)
        {
            PanelServices services = new PanelServices();
            var newSlate = services.GetPanelForLeft(model);
            return Json(new { slate = newSlate });
        }

        public ActionResult GetRightPanel(PanelModel model)
        {
            PanelServices services = new PanelServices();
            var newSlate = services.GetPanelForRight(model);
            return Json(new { slate = newSlate });
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
