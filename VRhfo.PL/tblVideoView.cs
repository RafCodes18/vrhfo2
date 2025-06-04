using System;
using System.Collections.Generic;

namespace VRhfo.PL;

public partial class tblVideoView
{
    public int Id { get; set; }

    public int VideoId { get; set; }

    public Guid? UserId { get; set; }

    public string? IPAdress { get; set; }

    public DateTime ViewTime { get; set; }
}
