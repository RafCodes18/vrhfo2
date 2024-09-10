using System;
using System.Collections.Generic;

namespace VRhfo.PL;

public partial class tblWatchEntry
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public int VideoId { get; set; }

    public DateTime LastDateWatched { get; set; }

    public DateTime FirstViewed { get; set; }

    public long WatchDurationTicks { get; set; }

    public int TimesViewed { get; set; }

    public bool Completed { get; set; }
}
