using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRhfo.BL
{
    public class WatchEntry
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int VideoId { get; set; }
        public DateTime LastDateWatched { get; set; }
        public DateTime FirstViewed { get; set; }
             
        public bool Completed { get; set; }

        public int TimesViewed {  get; set; }

        [Column(TypeName = "bigint")]
        public int WatchDurationTicks { get; set; }
       
    }
}
