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
    }

}
