namespace VRhfo.BL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsSubscribed { get; set; }
        // You can also add properties like subscription expiration date, payment info, etc.
    }

}
