namespace VRhfo.BL.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string ThumbnailUrl { get; set; }
        public string VideoUrl { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public DateTime UploadDate { get; set; }
        public TimeSpan Duration { get; set; }
        public List<string> Tags { get; set; }
        public int Views { get; set; }
        public int RatingCount { get; set; }
        public bool IsPublic { get; set; }  //For full length vids that are free, but not previews
        public bool IsPreview { get; set; }// If True, returns 5 min preview, if false must be paid.

        public string FormattedDuration
        {
            get
            {
                if (Duration.TotalHours >= 1)
                {
                    return $"{Duration.Hours}h {Duration.Minutes}m";
                }
                else
                {
                    return $"{Duration.Minutes}m";
                }
            }
        }

        public string FormattedLikes
        {
            get
            {
                if (Likes < 1000)
                {
                    return Likes.ToString();
                }
                else if (Likes < 1000000)
                {
                    return (Likes / 1000.0).ToString("F1") + "k";
                }
                else
                {
                    return (Likes / 1000000.0).ToString("F1") + "m";
                }
            }
        }

        //Think IceCube, "EXPLICIT CONTENT" sold more NWA albums. Viewers of my site are degenerates, like me, adding "WARNING: STRONG HYPNOSIS", or "WARNING: STRONG HYPNOTIC CONTENT"  adds to the experience.
        public string ContentWarning { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public double AverageRating
        {
            get
            {
                int totalVotes = Likes + Dislikes;
                return totalVotes > 0 ? (double)Likes / totalVotes * 100 : 0;
            }
        }

        public virtual ICollection<Comment> Comments { get; set; }



    }
}