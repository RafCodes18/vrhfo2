using System.Net.NetworkInformation;

namespace VRhfo.BL.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public Guid UserId { get; set; } // or public User User { get; set; }
        public int VideoId { get; set; } // or public Video Video { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }


        //navigation properties (not database fields)
        public User User { get; set; }
        public Video Video { get; set; }

        public List<Reply> Replies { get; set; }

        public string FormattedUploadDate
        {
            get
            {
                var currentDate = DateTime.UtcNow;
                var dateDifference = currentDate - DatePosted;

                if (dateDifference.TotalMinutes < 1) { 
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
                    if(dateDifference.TotalDays / 365 < 2)
                    {
                        return $"{(int)(dateDifference.TotalDays / 365)} year ago";
                    }
                    return $"{(int)(dateDifference.TotalDays / 365)} years ago";
                }
            }
        }

    }

}
