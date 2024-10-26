using System;
using System.Collections.Generic;

namespace VRhfo.PL;

public partial class tblComment
{
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime DatePosted { get; set; }

    public Guid UserId { get; set; }

    public int VideoId { get; set; }

    public int LikesCount { get; set; }

    public int DislikesCount { get; set; }
    public virtual tblUser User { get; set; }

}
