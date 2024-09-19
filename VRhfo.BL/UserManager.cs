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
                        NextRenewalDueDate = user.NextRenewalDueDate 
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
                    NextRenewalDueDate = row.NextRenewalDueDate
            };
            }
        }
    }
}
