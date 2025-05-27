using System.Security.Policy;
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
                    user.SubscriptionStartDate = (DateTime)tblUser.SubscribedDate;
                    user.Username = tblUser.Username;
                    user.Email = tblUser.Email;
                    user.Auth0UserId = tblUser.Auth0UserId;
                    user.IsSubscribed = tblUser.IsSubscribed;
                    user.FirstVisit = tblUser.FirstVisit;
                    user.Password = tblUser.PasswordHash;
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
                        IsSubscribed = user.IsSubscribed,
                        FirstVisit = user.FirstVisit,
                        Username = user.Username,
                        SubscribedDate = user.SubscriptionStartDate,
                        PasswordHash = user.Password,
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
                                if (row.PasswordHash == GetHash(user.Password))
                                {
                                    //login successful with correct hashed or unhashed password

                                    user.SubscriptionStartDate = (DateTime)row.SubscribedDate;
                                    user.NextRenewalDueDate = row.NextRenewalDueDate;
                                    user.Subscription_Tier = row.SubscriptionTier;
                                    user.FirstVisit = row.FirstVisit;
                                    user.Username = row.Username;
                                    user.IsSubscribed = row.IsSubscribed;
                                    user.Email = row.Email;
                                    user.Password = GetHash(user.Password);
                                    user.GoonScore = row.GoonScore;
                                    return true;
                                }
                                else if (row.PasswordHash == user.Password)
                                {
                                    user.SubscriptionStartDate = (DateTime)row.SubscribedDate;
                                    user.Subscription_Tier = row.SubscriptionTier;
                                                                        user.NextRenewalDueDate = row.NextRenewalDueDate;

                                    user.FirstVisit = row.FirstVisit;
                                    user.Username = row.Username;
                                    user.IsSubscribed = row.IsSubscribed;
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
                    Password = row.PasswordHash,
                    Email = row.Email,
                    FirstVisit = row.FirstVisit,
                    SubscriptionStartDate = (DateTime)row.SubscribedDate,
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
                        PasswordHash = "FreeUserPassword123", // Placeholder password
                        FirstVisit = DateTime.UtcNow, // Current date as registration date
                        SubscribedDate = DateTime.UtcNow, // Start date for subscription
                        IsSubscribed = false, // Not subscribed
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

        //Put the watchedLastWeek video properties in the User model instead of creating a VM
        //One method loads all 3 watch histories 
        public static User LoadWatchedVideos(string username)
        {

            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    // Find the user by username
                    var user = dc.tblUsers.FirstOrDefault(u => u.Username == username);
                    if (user == null)
                    {
                        return null; // or handle user not found
                    }

                    var today = DateTime.Today;
                    var weekAgo = today.AddDays(-7);

                    // Query the watch history for the user by their userId
                    var watchEntries = dc.tblWatchEntries
                                        .Where(we => we.UserId == user.Id)
                                        .Select(we => new
                                        {
                                            we.VideoId,
                                            we.LastDateWatched
                                        })
                                        .ToList();

                    // Videos watched today
                    var videosWatchedToday = watchEntries
                                             .Where(we => we.LastDateWatched >= today)
                                             .Select(we => we.VideoId)
                                             .Distinct()
                                             .ToList();

                    // Videos watched in the past week (excluding today)
                    var videosWatchedPastWeek = watchEntries
                                                .Where(we => we.LastDateWatched >= weekAgo && we.LastDateWatched < today)
                                                .Select(we => we.VideoId)
                                                .Distinct()
                                                .ToList();

                    // Videos watched more than a week ago
                    var restOfVideosWatched = watchEntries
                                              .Where(we => we.LastDateWatched < weekAgo)
                                              .Select(we => we.VideoId)
                                              .Distinct()
                                              .ToList();

                    // Fetch the video details for each category
                    var videosToday = dc.tblVideos
                                        .Where(v => videosWatchedToday.Contains(v.Id))
                                        .Select(v => new Video
                                        {
                                            Id = v.Id,
                                            Title = v.Title,
                                            ThumbnailUrl = v.ThumbnailUrl,
                                            Duration = v.Duration,
                                            VideoUrl = v.VideoUrl
                                        })
                                        .ToList();

                    var videosPastWeek = dc.tblVideos
                                           .Where(v => videosWatchedPastWeek.Contains(v.Id))
                                           .Select(v => new Video
                                           {
                                               Id = v.Id,
                                               Title = v.Title,
                                               ThumbnailUrl = v.ThumbnailUrl,
                                               Duration = v.Duration,
                                               VideoUrl = v.VideoUrl
                                           })
                                           .ToList();

                    var videosRest = dc.tblVideos
                                       .Where(v => restOfVideosWatched.Contains(v.Id))
                                       .Select(v => new Video
                                       {
                                           Id = v.Id,
                                           Title = v.Title,
                                           ThumbnailUrl = v.ThumbnailUrl,
                                           Duration = v.Duration,
                                           VideoUrl = v.VideoUrl
                                       })
                                       .ToList();

                    // Populate the UserModel with categorized watch history
                    return new User
                    {
                        VideosWatchedToday = videosToday,
                        VideosWatchedPastWeek = videosPastWeek,
                        RestOfVideosWatched = videosRest
                    };
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new ApplicationException("An error occurred while loading watched videos.", ex);
            }
        }
    }
}
