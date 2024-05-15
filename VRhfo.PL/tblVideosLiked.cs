using System;
using System.Collections.Generic;

namespace VRhfo.PL;

public partial class tblVideosLiked
{
    public int UserID { get; set; }

    public int VideoID { get; set; }

    public DateTime LikedDate { get; set; }

    public virtual tblUser User { get; set; } = null!;

    public virtual tblVideo Video { get; set; } = null!;
}
