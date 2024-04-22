using VRhfo.BL.Models;
using VRhfo.PL;

namespace VRhfo.BL
{
    public static class UserManager
    {
        public static User LoadById(int id)
        {
            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    tblUser tblUser = dc.tblUsers.FirstOrDefault(x => x.Id == id);
                    User user = new User();
                    user.Id = id;
                    user.SubscribedDate = tblUser.SubscribedDate;
                    user.Username = tblUser.Username;
                    user.Email = tblUser.Email;
                    user.Auth0UserId = tblUser.Auth0UserId;
                    user.IsSubscribed = tblUser.IsSubscribed == 1 ? true : false;
                    user.RegistrationDate = tblUser.RegistrationDate;

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
                        Email = user.Email,
                        Auth0UserId = user.Auth0UserId,
                        IsSubscribed = (byte)(user.IsSubscribed == true ? 1 : 0),
                        RegistrationDate = user.RegistrationDate,
                        Username = user.Username,
                        SubscribedDate = user.SubscribedDate,
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

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return true;
        }

    }
}
