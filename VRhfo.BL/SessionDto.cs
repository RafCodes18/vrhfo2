using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRhfo.BL
{
    public class SessionDto
    {
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
        public int SessionDurationSeconds { get; set; }
        public int WatchTimeSeconds { get; set; }
    }
}
