using Microsoft.AspNetCore.Mvc;
using VRhfo.BL;
using VRhfo.BL.Models;


namespace VRhfo.UI.Controllers
{
    public class UserController : Controller
    {
        public void SetUser(User user)
        {
            if (user != null)
            {
                HttpContext.Session.SetObject("user", user);
                HttpContext.Session.SetString("username", user.Username);
                Console.WriteLine(HttpContext.Session.GetString("username"));
            }
            else
            {
                HttpContext.Session.SetObject("username", string.Empty);

            }
        }

        [HttpGet]
        private ActionResult Login(string returnURL)
        {
            return RedirectToAction("Login");
        }

        [HttpPost]
        private IActionResult Login(User user)
        {
            bool loginWorked = UserManager.Login(user);
            if (HttpContext != null && loginWorked == true) SetUser(user);

            if (TempData?["returnUrl"] != null)
                return Redirect(TempData["returnUrl"]?.ToString());
            else
                return RedirectToAction("Video", "Index");

        }

        // GET: User/Profile
        public ActionResult Profile(int userId)
        {
            User user = UserManager.LoadById(userId);
            return View(user);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index), "Video");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        /*   [HttpPost]
           public ActionResult CreateAccount(User user)
           {
               try
               {
                   int results = UserManager.Insert(user);

                   if (results != 0)
                   {
                       Login(user);
                   }
               }
               catch (Exception)
               {

                   throw;
               }
           } */

    }
}
