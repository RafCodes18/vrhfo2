using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VRhfo.BL;
using VRhfo.UI.Models;
using VRhfo.UI.ViewModels;

namespace VRhfo.UI.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult JoinNow()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LearnHow()
        {
            return View();
        }

        public ActionResult liked()
        {
            ProfileViewModel pvm = new ProfileViewModel();
            pvm.Videos = VideoManager.LoadAll();
            return View(pvm);
        }
    }
}