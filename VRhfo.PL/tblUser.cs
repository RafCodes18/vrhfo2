using System;
using System.Collections.Generic;

namespace VRhfo.PL;

public partial class tblUser
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string Auth0UserId { get; set; } = null!;

    public DateTime FirstVisit { get; set; }

    public bool IsSubscribed { get; set; }

    public DateTime? SubscribedDate { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string SubscriptionTier { get; set; } = null!;

    public DateTime? NextRenewalDueDate { get; set; }

    public int? GoonScore { get; set; }

    public string? NormalizedEmail { get; set; }

    public string? NormalizedUsername { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public bool? LockoutEnabled { get; set; }

    public DateTime? LockoutEnd { get; set; }

    public int? AccessFailedCount { get; set; }

    public string? SecurityStamp { get; set; }

    public string? PasswordResetToken { get; set; }

    public DateTime? PasswordResetTokenExpiration { get; set; }

    public string? IPaddress { get; set; }

    public virtual ICollection<tblVideosLiked> tblVideosLikeds { get; set; } = new List<tblVideosLiked>();
}
