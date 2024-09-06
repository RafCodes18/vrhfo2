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

        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                bool loginWorked = UserManager.Login(user);
                User user1 = UserManager.LoadByUsername(user.Username);
                
                if (HttpContext != null && loginWorked == true) SetUser(user1);

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

        [HttpPost]
        public ActionResult LoginModal([FromBody] dynamic loginData)
        {
            // Validate credentials and perform login logic
            // Return appropriate JSON response
            // Parse the JSON data
            var json = Newtonsoft.Json.Linq.JObject.Parse(loginData.ToString());

            // Extract username and password
            string username = json["username"]?.ToString();
            string password = json["password"]?.ToString();

            User user = new User()
            {
                Username = username,
                Password = password
            };
            bool loginWorked = UserManager.Login(user);
            User user1 = UserManager.LoadByUsername(user.Username);
            if (loginWorked)
            {
                SetUser(user1);
                return Json(new { success = true, message = "Login successful" });
            }
            else
            {
                return Json(new { success = false, message = "Invalid credentials" });
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

        public ActionResult Index(string username)
        {

            User user = UserManager.LoadByUsername(username);

            var authUser = HttpContext.Session.GetObject<User>("user");

            if (authUser == null)
            {
                return RedirectToAction(nameof(Login), "Home");
            }
            return View(user);
        }

        public IActionResult Logout()
        {
            User nullUser = new User();
            nullUser.Username = "";
            SetUser(nullUser);
            return RedirectToAction("Index", "Video");
        }

        public ActionResult ManageAccount(string username)
        {
            User user = UserManager.LoadByUsername(username);

            return View(user);
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
