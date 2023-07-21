using VRhfo.BL.Models;
using VRhfo.PL;

namespace VRhfo.BL
{
    public static class VideoManager
    {
        public static List<Video> LoadAll()
        {
            List<Video> list = new List<Video>();

            using (VRhfoEntities dc = new VRhfoEntities())
            {
                (from v in dc.tblVideos
                 select new
                 {
                     v.Id,
                     v.Title,
                     v.Studio,
                     v.ThumbnailUrl,
                     v.VideoUrl,
                     v.Description,
                     v.Genre,
                     v.UploadDate,
                     v.Duration,
                     v.Views,
                     v.RatingCount,
                     v.IsPublic,
                     v.IsPreview,
                     v.ContentWarning,
                     v.Likes,
                     v.Dislikes
                 }).ToList().ForEach(video => list.Add(new Video
                 {
                     Id = video.Id,
                     Title = video.Title,
                     Studio = video.Studio,
                     ThumbnailUrl = video.ThumbnailUrl,
                     VideoUrl = video.VideoUrl,
                     Description = video.Description,
                     Genre = video.Genre,
                     UploadDate = video.UploadDate,
                     Duration = video.Duration,
                     Views = video.Views,
                     RatingCount = video.RatingCount,
                     IsPublic = video.IsPublic == 1,
                     IsPreview = video.IsPreview == 1,
                     ContentWarning = video.ContentWarning,
                     Likes = video.Likes,
                     Dislikes = video.Dislikes
                 }));

                return list;
            }
        }
    }
}
