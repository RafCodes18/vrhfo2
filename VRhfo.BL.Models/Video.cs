namespace VRhfo.BL.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Studio { get; set; }
        public string ThumbnailUrl { get; set; }
        public string VideoUrl { get; set; }
        public DateTime UploadDate { get; set; }
        public string Description { get; set; }
        // You can also add properties like duration, tags, view count, etc.

    }
}
