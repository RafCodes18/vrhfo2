using System;
using System.Collections.Generic;

namespace VRhfo.PL;

public partial class tblReply
{
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime DatePosted { get; set; }

    public Guid UserId { get; set; }

    public int CommentId { get; set; }

    public int LikesCount { get; set; }

    public int DislikesCount { get; set; }
}
