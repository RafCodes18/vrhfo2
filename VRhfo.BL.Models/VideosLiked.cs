using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRhfo.BL.Models
{
    public class VideosLiked
    {
        public Guid UserID { get; set; }

        public int VideoID { get; set; }

        public DateTime LikedDate { get; set; }
    }
}
