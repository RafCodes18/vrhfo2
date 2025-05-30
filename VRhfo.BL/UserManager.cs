using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public static async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            try
            {
                // Generate a secure random token
                byte[] tokenBytes = new byte[32];
                using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
                {
                    rng.GetBytes(tokenBytes);
                }
                string token = Convert.ToBase64String(tokenBytes);

                // Set expiration (1 hour from now)
                DateTime expiration = DateTime.UtcNow.AddHours(1);

                // Update the user in the database with the token and expiration
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    var tblUser = await dc.tblUsers.FirstOrDefaultAsync(u => u.Id == user.Id);
                    if (tblUser == null)
                        throw new Exception("User not found.");

                    tblUser.PasswordResetToken = token;
                    tblUser.PasswordResetTokenExpiration = expiration;
                    await dc.SaveChangesAsync();
                }

                return token;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<User> FindByEmailAsync(ResetPassword rpModel)
        {
            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    tblUser row = await dc.tblUsers.FirstOrDefaultAsync(u => u.Email == rpModel.Email);
                    if (row == null)
                        return null;

                    return new User
                    {
                        Username = row.Username,
                        Id = row.Id,
                        Password = row.PasswordHash,
                        Email = row.Email,
                        FirstVisit = row.FirstVisit,
                        SubscriptionStartDate = (DateTime)row.SubscribedDate,
                        Auth0UserId = row.Auth0UserId,
                        Subscription_Tier = row.SubscriptionTier,
                        NextRenewalDueDate = (DateTime)row.NextRenewalDueDate,
                        GoonScore = (int)row.GoonScore
                    };
                }
            }
            catch (Exception)
            {
                throw;

            }
        }

        public static async Task<User> LoadByIdAsync(Guid id)
        {
            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    tblUser tblUser = await dc.tblUsers.FirstOrDefaultAsync(x => x.Id == id);
                    if (tblUser == null)
                        return null;

                    User user = new User
                    {
                        Id = id,
                        SubscriptionStartDate = (DateTime)tblUser.SubscribedDate,
                        Username = tblUser.Username,
                        Email = tblUser.Email,
                        Auth0UserId = tblUser.Auth0UserId,
                        IsSubscribed = tblUser.IsSubscribed,
                        FirstVisit = tblUser.FirstVisit,
                        Password = tblUser.PasswordHash,
                        Subscription_Tier = tblUser.SubscriptionTier,
                        GoonScore = (int)tblUser.GoonScore
                    };

                    return user;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<int> InsertAsync(User user, bool rollback = false)
        {
            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    tblUser tb = new tblUser
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
                        NextRenewalDueDate = user.NextRenewalDueDate,
                        GoonScore = user.GoonScore
                    };

                    dc.tblUsers.Add(tb);
                    return await dc.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<bool> LoginAsync(User user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.Username) && !string.IsNullOrEmpty(user.Password))
                {
                    using (VRhfoEntities db = new VRhfoEntities())
                    {
                        tblUser row = await db.tblUsers.FirstOrDefaultAsync(u => u.Username == user.Username);
                        if (row == null)
                            return false;

                        // Check password
                        if (row.PasswordHash == GetHash(user.Password) || row.PasswordHash == user.Password)
                        {
                            user.SubscriptionStartDate = (DateTime)row.SubscribedDate;
                            user.Subscription_Tier = row.SubscriptionTier;
                            user.NextRenewalDueDate = (DateTime)row.NextRenewalDueDate;
                            user.FirstVisit = row.FirstVisit;
                            user.Username = row.Username;
                            user.IsSubscribed = row.IsSubscribed;
                            user.Email = row.Email;
                            user.Password = row.PasswordHash == user.Password ? user.Password : GetHash(user.Password);
                            user.GoonScore = (int)row.GoonScore;
                            return true;
                        }
                        return false;
                    }
                }
                return false;
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

        public static async Task<User> LoadByUsernameAsync(string username)
        {
            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    tblUser row = await dc.tblUsers.FirstOrDefaultAsync(u => u.Username == username);
                    if (row == null)
                        return null;

                    return new User
                    {
                        Username = row.Username,
                        Id = row.Id,
                        Password = row.PasswordHash,
                        Email = row.Email,
                        FirstVisit = row.FirstVisit,
                        SubscriptionStartDate = (DateTime)row.SubscribedDate,
                        Auth0UserId = row.Auth0UserId,
                        Subscription_Tier = row.SubscriptionTier,
                        NextRenewalDueDate = (DateTime)row.NextRenewalDueDate,
                        GoonScore = (int)row.GoonScore
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public static async Task<int> LoadTodaysWatchTimeAsync(Guid userId)
        {
            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    var today = DateTime.UtcNow.Date;
                    var todaysWatchTime = await dc.tblWatchEntries
                        .Where(w => w.UserId == userId && w.LastDateWatched >= today)
                        .SumAsync(w => (int?)w.WatchDurationTicks) ?? 0;

                    return todaysWatchTime;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<int> LoadWeeklyWatchTimeAsync(Guid userId)
        {
            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    var lastWeek = DateTime.UtcNow.Date.AddDays(-7);
                    var weeklyWatchTime = await dc.tblWatchEntries
                        .Where(w => w.UserId == userId && w.LastDateWatched >= lastWeek)
                        .SumAsync(w => (int?)w.WatchDurationTicks) ?? 0;

                    return weeklyWatchTime;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<int> LoadLifetimeWatchTimeAsync(Guid userId)
        {
            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    var lifetimeWatchTime = await dc.tblWatchEntries
                        .Where(w => w.UserId == userId)
                        .SumAsync(w => (int?)w.WatchDurationTicks) ?? 0;

                    return lifetimeWatchTime;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<int> LoadMonthlyWatchTimeAsync(Guid userId)
        {
            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    var lastMonth = DateTime.UtcNow.Date.AddDays(-30);
                    var monthlyWatchTime = await dc.tblWatchEntries
                        .Where(w => w.UserId == userId && w.LastDateWatched >= lastMonth)
                        .SumAsync(w => (int?)w.WatchDurationTicks) ?? 0;

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
                    tblUser freeUser = new tblUser
                    {
                        Id = Guid.Parse(guid),
                        Auth0UserId = "free-user",
                        Username = "Free User",
                        Email = "freeuser@example.com",
                        PasswordHash = "FreeUserPassword123",
                        FirstVisit = DateTime.UtcNow,
                        SubscribedDate = DateTime.UtcNow,
                        IsSubscribed = false,
                        NextRenewalDueDate = DateTime.UtcNow.AddYears(1),
                        SubscriptionTier = "Free",
                        GoonScore = 0
                    };

                    dc.tblUsers.Add(freeUser);
                    return await dc.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<User> LoadWatchedVideosAsync(string username)
        {
            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    var user = await dc.tblUsers.FirstOrDefaultAsync(u => u.Username == username);
                    if (user == null)
                        return null;

                    var today = DateTime.Today;
                    var weekAgo = today.AddDays(-7);

                    var watchEntries = await dc.tblWatchEntries
                        .Where(we => we.UserId == user.Id)
                        .Select(we => new
                        {
                            we.VideoId,
                            we.LastDateWatched
                        })
                        .ToListAsync();

                    var videosWatchedToday = watchEntries
                        .Where(we => we.LastDateWatched >= today)
                        .Select(we => we.VideoId)
                        .Distinct()
                        .ToList();

                    var videosWatchedPastWeek = watchEntries
                        .Where(we => we.LastDateWatched >= weekAgo && we.LastDateWatched < today)
                        .Select(we => we.VideoId)
                        .Distinct()
                        .ToList();

                    var restOfVideosWatched = watchEntries
                        .Where(we => we.LastDateWatched < weekAgo)
                        .Select(we => we.VideoId)
                        .Distinct()
                        .ToList();

                    var videosToday = await dc.tblVideos
                        .Where(v => videosWatchedToday.Contains(v.Id))
                        .Select(v => new Video
                        {
                            Id = v.Id,
                            Title = v.Title,
                            ThumbnailUrl = v.ThumbnailUrl,
                            Duration = v.Duration,
                            VideoUrl = v.VideoUrl
                        })
                        .ToListAsync();

                    var videosPastWeek = await dc.tblVideos
                        .Where(v => videosWatchedPastWeek.Contains(v.Id))
                        .Select(v => new Video
                        {
                            Id = v.Id,
                            Title = v.Title,
                            ThumbnailUrl = v.ThumbnailUrl,
                            Duration = v.Duration,
                            VideoUrl = v.VideoUrl
                        })
                        .ToListAsync();

                    var videosRest = await dc.tblVideos
                        .Where(v => restOfVideosWatched.Contains(v.Id))
                        .Select(v => new Video
                        {
                            Id = v.Id,
                            Title = v.Title,
                            ThumbnailUrl = v.ThumbnailUrl,
                            Duration = v.Duration,
                            VideoUrl = v.VideoUrl
                        })
                        .ToListAsync();

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
                throw new ApplicationException("An error occurred while loading watched videos.", ex);
            }
        }
    }
}