using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRhfo.BL.Models
{
    public class Reply
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public Guid UserId { get; set; }  // User who posted the reply
        public int CommentId { get; set; }  // The comment this reply belongs to
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Comment Comment { get; set; }  // The parent comment
    }
}
