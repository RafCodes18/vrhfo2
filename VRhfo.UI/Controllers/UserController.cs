using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using VRhfo.BL;
using VRhfo.BL.Models;
using VRhfo.PL;
using VRhfo.UI.Models;
using VRhfo.UI.Services;
using VRhfo.UI.ViewModels;


namespace VRhfo.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly EmailClient _emailClient;
        private readonly VRhfoEntities _dbContext;

        public UserController(EmailClient emailClient, VRhfoEntities dbContext)
        {
            _emailClient = emailClient;
            _dbContext = dbContext;
        }
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
        private User GetCurrentUser()
        {
            return HttpContext.Session.GetObject<User>("user");
        }

        [HttpPost]
        public ActionResult SaveSessionData([FromBody] SessionDto data)
        {
            // Example: data contains SessionStart, SessionEnd, SessionDurationSeconds, WatchTimeSeconds
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();
            var userId = GetCurrentUser().Id; // Your logic here

            if(UserManager.SaveSessionData(userId, ip, data))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }


        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel rpModel)
        {

            if (ModelState.IsValid)
            {
                Guid userId = new Guid(rpModel.UserId);
                User user = await UserManager.LoadByIdAsync(userId);
                string newPassword = rpModel.ConfirmPassword;
                if (user != null && user.PasswordResetTokenExpiration != null)
                {
                    // Check if the token is expired
                    if (DateTime.UtcNow <= user.PasswordResetTokenExpiration.Value)
                    {
                        // Proceed with password reset logic
                        int success = await UserManager.UpdatePassword(user, newPassword);
                        // Example: user.Password = Hash(rpModel.NewPassword); etc.
                        if(success >= 1)
                        {
                            return RedirectToAction("ResetPasswordConfirmation");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The password reset link has expired. Please request a new one.");
                        return View(rpModel);
                    }
                }

            }
            return View();
        }

        [HttpGet]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ResetPassword rpModel)
        {
            if (ModelState.IsValid)
            {                 
                var user = await UserManager.FindByEmailAsync(rpModel);
                if (user != null)
                {
                    var token = await UserManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Action("ResetPassword", "User", new { userId = user.Id, token }, protocol: Request.Scheme);
                    await _emailClient.SendEmailAsync(rpModel.Email, "Reset Password - PornWorship", callbackUrl);
                    return RedirectToAction("ForgotPasswordConfirmation", rpModel);

                }
                //for security, dont let them know if email exists, whether it does or not show same message
                else
                {
                    return RedirectToAction("ForgotPasswordConfirmation", rpModel);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult ForgotPasswordConfirmation(ResetPassword rpModel)
        {
            return View(rpModel);
        }

        [HttpGet]
        public IActionResult ResetPassword(Guid userId, string token)
        {
            if (userId == Guid.Empty || string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Home");
            var model = new ResetPasswordViewModel { UserId = userId.ToString(), Token = token };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                bool loginWorked = await UserManager.LoginAsync(user);
                User user1 = await UserManager.LoadByUsernameAsync(user.Username);
                
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
        public async Task<IActionResult> LoginModal([FromBody] dynamic loginData)
        {
            // Validate credentials and perform login logic
            // Return appropriate JSON response
            // Parse the JSON data
            var json = Newtonsoft.Json.Linq.JObject.Parse(loginData.ToString());

            // Extract username and password
            string email = json["email"]?.ToString();
            string password = json["password"]?.ToString();
            string returnUrl = json["returnUrl"]?.ToString();  


            User user = new User()
            {
                Email = email,
                Password = password,                
            };
            bool loginWorked = await UserManager.LoginAsync(user);
            User user1 = await UserManager.LoadByUsernameAsync(user.Username);
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
        public async Task<IActionResult> Profile(Guid userId)
        {
            ProfileViewModel pVM = new ProfileViewModel();
            pVM.User =  await UserManager.LoadByIdAsync(userId);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> JoinNow(User user)
        {
            try
            {
                user.SubscriptionStartDate = DateTime.Now;
                user.IsSubscribed = true;
                user.Auth0UserId = "xxx";
                user.FirstVisit = DateTime.Now;
                user.Id = Guid.NewGuid();
                user.Username = user.Email.Substring(0, user.Email.IndexOf('@'));
                user.NextRenewalDueDate = DateTime.Now.AddDays(30);
                user.GoonScore = 15;

                if (await UserManager.InsertAsync(user) > 0)
                {
                    SetUser(user);
                    return Json(new { success = true, message = "Registration successful!", redirectUrl = Url.Action("PaymentSuccess", "User") });
                }
                else
                {
                    return Json(new { success = false, message = "Registration failed." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred during registration." });
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

        
        public async Task<IActionResult> Index(string username)
        {

            User user = await UserManager.LoadByUsernameAsync(username);

            var authUser = HttpContext.Session.GetObject<User>("user");

            if (authUser == null || authUser.Username == "")
            {
                return RedirectToAction(nameof(Login), "Home");
            }

            //watch stats are grabbed all at once and stored in an empty user so I can place them in actual user
            User userHistory = new User();
            userHistory = await UserManager.LoadWatchedVideosAsync(username);
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

        public async Task<IActionResult> ManageAccount(string username)
        {
            User user = await UserManager.LoadByUsernameAsync(username);

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetWatchTime(Guid userId)
        {

            //HERE only grabbing the seconds. 
            //Daily will query based on watch entries LastDateWatched value being today, then total the durations
            //Weekly will query videos first watched 
            int dailySeconds = await UserManager.LoadTodaysWatchTimeAsync(userId);
            int weeklySeconds = await UserManager.LoadWeeklyWatchTimeAsync(userId);
            int monthlySeconds = await UserManager.LoadMonthlyWatchTimeAsync(userId);
            int lifetimeSeconds = await UserManager.LoadLifetimeWatchTimeAsync(userId);

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
