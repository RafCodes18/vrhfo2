using VRhfo.BL.Models;

namespace VRhfo.UI.Models
{
    public class Authenticate
    {
        public static bool IsAuthenticated(HttpContext context)
        {
            return context == null ? true : context.Session.GetObject<User>("user") != null;
        }
    }
}
