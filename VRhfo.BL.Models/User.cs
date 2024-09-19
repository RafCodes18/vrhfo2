using System.ComponentModel.DataAnnotations;

namespace VRhfo.BL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Auth0UserId { get; set; } //This is the unique ID that Auth0 assigns to each user.

        [Required(ErrorMessage = "Username required")]
        public string Username { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
        public char AvatarLetter => string.IsNullOrEmpty(Username) ? ' ' : char.ToUpper(Username[0]);
        


        public DateTime RegistrationDate { get; set; } 
        public DateTime SubscriptionStartDate { get; set; }
        public bool IsSubscribed { get; set; }
        
        

        public DateTime NextRenewalDueDate { get; set; }
       
        public string Subscription_Tier { get; set; }
 


        public int GoonScore { get; set; }
    }
}
