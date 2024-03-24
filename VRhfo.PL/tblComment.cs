using System;
using System.Collections.Generic;

namespace VRhfo.PL;

public partial class tblComment
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime DatePosted { get; set; }

    public int UserId { get; set; }

    public int VideoId { get; set; }
}
