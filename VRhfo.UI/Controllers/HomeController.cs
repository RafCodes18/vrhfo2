using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VRhfo.BL;
using VRhfo.BL.Models;
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

        public IActionResult Captions()
        {
            return View();
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

        public IActionResult Login(string? returnUrl)
        {
            TempData["returnUrl"] = returnUrl;

            return View();
        }

        public IActionResult LearnHow()
        {
            return View();
        }

        public ActionResult Liked()
        {
            User currentUser = HttpContext.Session.GetObject<User>("user");

            ProfileViewModel pvm = new ProfileViewModel();
            pvm.Videos = VideoManager.GetUsersLikedVideos(currentUser.Id);
            return View(pvm);
        }

        public ActionResult Live()
        {
            return View();
        }
    }
}