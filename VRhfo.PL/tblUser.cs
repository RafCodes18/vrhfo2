using System;
using System.Collections.Generic;

namespace VRhfo.PL;

public partial class tblUser
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Auth0UserId { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public byte IsSubscribed { get; set; }

    public DateTime SubscribedDate { get; set; }
}
