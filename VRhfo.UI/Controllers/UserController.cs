using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
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
            string returnUrl = json["returnUrl"]?.ToString();  


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
                return Json(new { success = true, message = "Login successful",  returnUrl = returnUrl });
            }
            else
            {
                return Json(new { success = false, message = "Invalid credentials" });
            }
        }

        // GET: User/Profile
        public ActionResult Profile(Guid userId)
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

        [HttpGet]
        public ActionResult PaymentSuccess()
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
                user.SubscriptionStartDate = DateTime.Now; //what happ be after reg
                user.IsSubscribed = true;
                user.Auth0UserId = "xxx";
                user.FirstVisit = DateTime.Now; 
                user.Id = Guid.NewGuid();
                user.Username = user.Email.Substring(0, user.Email.IndexOf('@'));
                user.NextRenewalDueDate = DateTime.Now.AddDays(30);
                user.GoonScore = 15; //the default 15 points for joining, will go down if first week watch time does not surpass 1 hour
                if (UserManager.Insert(user) > 0)
                {
                    SetUser(user);
                    return RedirectToAction(nameof(PaymentSuccess), "User");
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
            //CHANGE USERNAME to DELETED to keep comments, comments will show as (Deleted) and still show content
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

            if (authUser == null || authUser.Username == "")
            {
                return RedirectToAction(nameof(Login), "Home");
            }

            //watch stats are grabbed all at once and stored in an empty user so I can place them in actual user
            User userHistory = new User();
            userHistory = UserManager.LoadWatchedVideos(username);
            user.VideosWatchedToday = userHistory.VideosWatchedToday;
            user.VideosWatchedPastWeek = userHistory.VideosWatchedPastWeek;
            user.RestOfVideosWatched = userHistory.RestOfVideosWatched;


            return View(user);
        }




        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");

            return RedirectToAction("Index", "Video");
        }

        public ActionResult ManageAccount(string username)
        {
            User user = UserManager.LoadByUsername(username);

            return View(user);
        }

        [HttpGet]
        public IActionResult GetWatchTime(Guid userId)
        {

            //HERE only grabbing the seconds. 
            //Daily will query based on watch entries LastDateWatched value being today, then total the durations
            //Weekly will query videos first watched 
            int dailySeconds = UserManager.LoadTodaysWatchTime(userId);
            int weeklySeconds = UserManager.LoadWeeklyWatchTime(userId);
            int monthlySeconds = UserManager.LoadMonthlyWatchTime(userId);
            int lifetimeSeconds = UserManager.LoadLifetimeWatchTime(userId);

            return Json(new
            {
                daily = dailySeconds,
                weekly = weeklySeconds,
                monthly = monthlySeconds,
                lifetime = lifetimeSeconds
            });
        }


        // Define a model class for the incoming data
        public class CreateAccountRequest
        {
            public string Guid { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFreeAccount()
        {
            try
            {
                // Read the incoming JSON data from the request body asynchronously
                using (var reader = new StreamReader(Request.Body))
                {
                    var body = await reader.ReadToEndAsync();
                    var request = JsonConvert.DeserializeObject<CreateAccountRequest>(body);

                    // Check if the GUID is valid
                    if (request == null || string.IsNullOrEmpty(request.Guid))
                    {
                        return BadRequest(new { message = "GUID is required." });
                    }

                    // Perform account creation logic here using request.Guid
                    // Assuming UserManager.InsertFreeAccountGUID is made async
                    int result = await UserManager.InsertFreeAccountGUIDAsync(request.Guid);
                    if (result > 0)
                    {
                        // If account creation is successful
                        return Ok(new { message = "Account created successfully." });
                    }
                    return BadRequest(new { message = "Failed to create account." });
                }
            }
            catch (JsonException ex)
            {
                // Handle JSON deserialization errors
                return BadRequest(new { message = "Invalid data format.", details = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, new { message = "An error occurred while creating the account.", details = ex.Message });
            }
        }
    }
}
