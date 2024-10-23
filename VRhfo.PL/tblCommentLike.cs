using System;
using System.Collections.Generic;

namespace VRhfo.PL;

public partial class tblCommentLike
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public int CommentId { get; set; }

    public bool IsLike { get; set; }

    public DateTime? CreatedAt { get; set; }
}
