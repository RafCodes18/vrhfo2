using System;
using System.Collections.Generic;

namespace VRhfo.PL;

public partial class tblVideo
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string ThumbnailUrl { get; set; } = null!;

    public string VideoUrl { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public DateTime UploadDate { get; set; }

    public TimeSpan Duration { get; set; }

    public int Views { get; set; }

    public int RatingCount { get; set; }

    public byte IsPublic { get; set; }

    public byte IsPreview { get; set; }

    public string ContentWarning { get; set; } = null!;

    public int Likes { get; set; }

    public int Dislikes { get; set; }
}
