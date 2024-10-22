using System;
using System.Collections.Generic;

namespace VRhfo.PL;

public partial class tblUser
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Auth0UserId { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public byte IsSubscribed { get; set; }

    public DateTime SubscribedDate { get; set; }

    public string Password { get; set; } = null!;

    public string SubscriptionTier { get; set; } = null!;

    public DateTime NextRenewalDueDate { get; set; }

    public int GoonScore { get; set; }

    public virtual ICollection<tblVideosLiked> tblVideosLikeds { get; set; } = new List<tblVideosLiked>();
}
