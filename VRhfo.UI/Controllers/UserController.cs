using Microsoft.AspNetCore.Mvc;
using VRhfo.BL;
using VRhfo.BL.Models;
using VRhfo.UI.ViewModels;


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

        /*  [HttpGet]
          private ActionResult Login(string returnURL)
          {
              return View();
          }*/

        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                bool loginWorked = UserManager.Login(user);
                if (HttpContext != null && loginWorked == true) SetUser(user);

                if (TempData?["returnUrl"] != null)
                    return Redirect(TempData["returnUrl"]?.ToString());
                else
                    return RedirectToAction("Index", "Video");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Login", "User");
            }

        }

        // GET: User/Profile
        public ActionResult Profile(int userId)
        {
            ProfileViewModel pVM = new ProfileViewModel();
            pVM.User = UserManager.LoadById(userId);
            pVM.Videos = VideoManager.LoadByUserId(userId);
            return View(pVM);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                user.SubscribedDate = DateTime.Now;
                user.IsSubscribed = false;
                user.Auth0UserId = "";
                user.RegistrationDate = DateTime.Now;

                if (UserManager.Insert(user) > 0)
                {
                    SetUser(user);
                    return RedirectToAction(nameof(Index), "Video");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("JoinNow", "Home");
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

        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            User nullUser = new User();
            nullUser.Username = "";
            SetUser(nullUser);
            return RedirectToAction("Index", "Video");
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
