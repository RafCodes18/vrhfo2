namespace VRhfo.BL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Auth0UserId { get; set; } // This is the unique ID that Auth0 assigns to each user.
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime SubscribedDate { get; set; }
        public bool IsSubscribed { get; set; }
        public char AvatarLetter => string.IsNullOrEmpty(Username) ? ' ' : char.ToUpper(Username[0]);

    }
}
