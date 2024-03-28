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


        private void Login(User user)
        {
            throw new NotImplementedException();
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
                return RedirectToAction(nameof(Index));
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
