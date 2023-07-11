using VRhfo.BL.Models;

public class Video
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Studio { get; set; }
    public string ThumbnailUrl { get; set; }
    public string VideoUrl { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public DateTime UploadDate { get; set; }
    public TimeSpan Duration { get; set; }
    public List<string> Tags { get; set; }
    public int Views { get; set; }
    public int RatingCount { get; set; }
    public bool IsPublic { get; set; }  //True = 5 min preview vids, false = Subscription full length
    public bool IsPreview { get; set; }// better clarity


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
