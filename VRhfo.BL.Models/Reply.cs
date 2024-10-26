using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRhfo.BL.Models
{
    public class Reply
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public Guid UserId { get; set; }  // User who posted the reply
        public Guid CommentId { get; set; }  // The comment this reply belongs to
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }

        // Navigation properties

        public User User { get; set; }
        public string UserUsername { get; set; }
        public Comment Comment { get; set; }  // The parent comment

        public string FormattedUploadDate
        {
            get
            {
                var currentDate = DateTime.UtcNow;
                var dateDifference = currentDate - DatePosted;

                if (dateDifference.TotalMinutes < 1)
                {
                    return $"{(int)dateDifference.TotalSeconds} seconds ago";
                }
                else if (dateDifference.TotalSeconds >= 60 && dateDifference.TotalSeconds < 120)
                {
                    return $"{(int)dateDifference.TotalMinutes} minute ago";

                }
                else if (dateDifference.TotalMinutes < 60)
                {
                    return $"{(int)dateDifference.TotalMinutes} minutes ago";
                }
                else if (dateDifference.TotalHours < 24)
                {
                    return $"{(int)dateDifference.TotalHours} hours ago";
                }
                else if (dateDifference.TotalDays < 2)
                {
                    return "Yesterday";
                }
                else if (dateDifference.TotalDays < 7)
                {
                    return $"{(int)dateDifference.TotalDays} days ago";
                }
                else if (dateDifference.TotalDays < 14)
                {
                    return "1 week ago";
                }
                else if (dateDifference.TotalDays < 30)
                {
                    return $"{(int)(dateDifference.TotalDays / 7)} weeks ago";
                }
                else if (dateDifference.TotalDays < 365)
                {
                    return $"{(int)(dateDifference.TotalDays / 30)} months ago";
                }
                else
                {
                    if (dateDifference.TotalDays / 365 < 2)
                    {
                        return $"{(int)(dateDifference.TotalDays / 365)} year ago";
                    }
                    return $"{(int)(dateDifference.TotalDays / 365)} years ago";
                }
            }
        }
    }
}
