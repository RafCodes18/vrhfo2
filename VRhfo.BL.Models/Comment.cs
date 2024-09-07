namespace VRhfo.BL.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public int UserId { get; set; } // or public User User { get; set; }
        public int VideoId { get; set; } // or public Video Video { get; set; }
        
        
        //navigation properties (not database fields)
        public User User { get; set; }
        public Video Video { get; set; }



        public string FormattedUploadDate
        {
            get
            {
                var currentDate = DateTime.UtcNow;
                var dateDifference = currentDate - DatePosted;

                if (dateDifference.TotalDays < 1)
                {
                    return "Today";
                }
                else if (dateDifference.TotalDays < 2)
                {
                    return "1 day ago";
                }
                else if (dateDifference.TotalDays < 7)
                {
                    return $"{(int)dateDifference.TotalDays} days ago";
                }
                else if (dateDifference.TotalDays < 14)
                {
                    return "1 wk ago";
                }
                else if (dateDifference.TotalDays < 30)
                {
                    return $"{(int)(dateDifference.TotalDays / 7)} wks ago";
                }
                else if (dateDifference.TotalDays < 365)
                {
                    return $"{(int)(dateDifference.TotalDays / 30)} mths ago";
                }
                else
                {
                    return $"{(int)(dateDifference.TotalDays / 365)} yrs ago";
                }
            }
        }

    }

}
