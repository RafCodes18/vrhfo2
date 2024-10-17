using VRhfo.BL.Models;
using VRhfo.PL;
using static VRhfo.BL.Models.User;

namespace VRhfo.BL
{
    public static class UserManager
    {
        public class LoginFailureException : Exception
        {
            public LoginFailureException() : base("Invalid Username or Password.")
            {

            }

            public LoginFailureException(string message) : base(message)
            {

            }
        }

        public static User LoadById(Guid id)
        {
            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    tblUser tblUser = dc.tblUsers.FirstOrDefault(x => x.Id == id);
                    User user = new User();
                    user.Id = id;
                    user.SubscriptionStartDate = tblUser.SubscribedDate;
                    user.Username = tblUser.Username;
                    user.Email = tblUser.Email;
                    user.Auth0UserId = tblUser.Auth0UserId;
                    user.IsSubscribed = tblUser.IsSubscribed == 1 ? true : false;
                    user.RegistrationDate = tblUser.RegistrationDate;
                    user.Password = tblUser.Password;
                    user.Subscription_Tier = tblUser.SubscriptionTier;
                    user.GoonScore = tblUser.GoonScore;

                    return user;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Insert(User user, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    tblUser tb = new tblUser()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Auth0UserId = user.Auth0UserId,
                        IsSubscribed = (byte)(user.IsSubscribed == true ? 1 : 0),
                        RegistrationDate = user.RegistrationDate,
                        Username = user.Username,
                        SubscribedDate = user.SubscriptionStartDate,
                        Password = user.Password,
                        SubscriptionTier = user.Subscription_Tier,
                        NextRenewalDueDate = user.NextRenewalDueDate ,
                        GoonScore = user.GoonScore
                    };

                    dc.tblUsers.Add(tb);

                    // Save changes to the database
                    results = dc.SaveChanges();
                };

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static bool Login(User user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.Username))
                {
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        using (VRhfoEntities db = new VRhfoEntities())
                        {
                            tblUser row = db.tblUsers.FirstOrDefault(u => u.Username == user.Username);

                            if (row != null)
                            {
                                //check password 
                                if (row.Password == GetHash(user.Password))
                                {
                                    //login successful with correct hashed or unhashed password

                                    user.SubscriptionStartDate = row.SubscribedDate;
                                    user.NextRenewalDueDate = row.NextRenewalDueDate;
                                    user.Subscription_Tier = row.SubscriptionTier;
                                    user.RegistrationDate = row.RegistrationDate;
                                    user.Username = row.Username;
                                    user.IsSubscribed = row.IsSubscribed != 0;
                                    user.Email = row.Email;
                                    user.Password = GetHash(user.Password);
                                    user.GoonScore = row.GoonScore;
                                    return true;
                                }
                                else if (row.Password == user.Password)
                                {
                                    user.SubscriptionStartDate = row.SubscribedDate;
                                    user.Subscription_Tier = row.SubscriptionTier;
                                                                        user.NextRenewalDueDate = row.NextRenewalDueDate;

                                    user.RegistrationDate = row.RegistrationDate;
                                    user.Username = row.Username;
                                    user.IsSubscribed = row.IsSubscribed != 0;
                                    user.Email = row.Email;
                                    user.Password = user.Password;
                                    user.GoonScore = row.GoonScore;
                                    return true;
                                }
                                else
                                {
                                     
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }



        private static string GetHash(string password)
        {
            using (var hasher = new System.Security.Cryptography.SHA1Managed())
            {
                var hashbytes = System.Text.Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }

        public static User LoadByUsername(string username)
        {
            using (VRhfoEntities dc = new VRhfoEntities())
            {
                // Correct the query to retrieve the user
                tblUser row = dc.tblUsers.FirstOrDefault(u => u.Username == username);

                if (row == null)
                {
                    return null; // or handle it as per your requirement, e.g., throw an exception
                }

                // Map tblUser to User
                return new User()
                {
                    Username = row.Username,
                    Id = row.Id,
                    Password = row.Password,
                    Email = row.Email,
                    RegistrationDate = row.RegistrationDate,
                    SubscriptionStartDate = row.SubscribedDate,
                    Auth0UserId = row.Auth0UserId,
                    Subscription_Tier = row.SubscriptionTier,
                    NextRenewalDueDate = row.NextRenewalDueDate,
                    GoonScore = row.GoonScore
            };
            }
        }        

        public static int LoadTodaysWatchTime(Guid userId)
        {
            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    var today = DateTime.UtcNow.Date;

                    var todaysWatchTime = dc.tblWatchEntries
                        .Where(w => w.UserId == userId && w.LastDateWatched >= today)
                        .Sum(w => (int?)w.WatchDurationTicks) ?? 0;

                    return todaysWatchTime;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int LoadWeeklyWatchTime(Guid userId)
        {
            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    var lastWeek = DateTime.UtcNow.Date.AddDays(-7);

                    var weeklyWatchTime = dc.tblWatchEntries
                        .Where(w => w.UserId == userId && w.LastDateWatched >= lastWeek)
                        .Sum(w => (int?)w.WatchDurationTicks) ?? 0;

                    return weeklyWatchTime;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int LoadLifetimeWatchTime(Guid userId)
        {
            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    var lifetimeWatchTime = dc.tblWatchEntries
                        .Where(w => w.UserId == userId)
                        .Sum(w => (int?)w.WatchDurationTicks) ?? 0;

                    return lifetimeWatchTime;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int LoadMonthlyWatchTime(Guid userId)
        {
            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    var lastMonth = DateTime.UtcNow.Date.AddDays(-30);

                    var monthlyWatchTime = dc.tblWatchEntries
                        .Where(w => w.UserId == userId && w.LastDateWatched >= lastMonth)
                        .Sum(w => (int?)w.WatchDurationTicks) ?? 0;

                    return monthlyWatchTime;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<int> InsertFreeAccountGUIDAsync(string guid)
        {
            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    tblUser freeUser = new tblUser()
                    {

                        Id = Guid.Parse(guid),
                        Auth0UserId = "free-user", // Dummy Auth0 user ID
                        Username = "Free User", // Placeholder username
                        Email = "freeuser@example.com", // Dummy email
                        Password = "FreeUserPassword123", // Placeholder password
                        RegistrationDate = DateTime.UtcNow, // Current date as registration date
                        SubscribedDate = DateTime.UtcNow, // Start date for subscription
                        IsSubscribed = 0, // Not subscribed
                        NextRenewalDueDate = DateTime.UtcNow.AddYears(1), // Dummy renewal date, 1 year ahead
                        SubscriptionTier = "Free", // Subscription tier for free users
                        GoonScore = 0,// Placeholder
                    };

                    // Save the user to the database
                    dc.tblUsers.Add(freeUser);
                    return  dc.SaveChanges(); // Return the number of state entries written to the database
                }
            
        

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
