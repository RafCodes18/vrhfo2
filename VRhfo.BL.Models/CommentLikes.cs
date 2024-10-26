using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRhfo.BL.Models
{
    public class CommentLikes
    {
        public int Id { get; set; } 
        public Guid UserId { get; set; }
        public Guid CommentId { get; set; }  
        public bool IsLike { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
