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


        public static Video LoadById(int id)
        {
            using (VRhfoEntities dc = new VRhfoEntities())
            {
                var videoEntity = dc.tblVideos.FirstOrDefault(v => v.Id == id);

                if (videoEntity != null)
                {
                    return new Video
                    {
                        Id = videoEntity.Id,
                        Title = videoEntity.Title,
                        Studio = videoEntity.Studio,
                        ThumbnailUrl = videoEntity.ThumbnailUrl,
                        VideoUrl = videoEntity.VideoUrl,
                        Description = videoEntity.Description,
                        Genre = videoEntity.Genre,
                        UploadDate = videoEntity.UploadDate,
                        Duration = videoEntity.Duration,
                        Views = videoEntity.Views,
                        RatingCount = videoEntity.RatingCount,
                        IsPublic = videoEntity.IsPublic == 1,
                        IsPreview = videoEntity.IsPreview == 1,
                        ContentWarning = videoEntity.ContentWarning,
                        Likes = videoEntity.Likes,
                        Dislikes = videoEntity.Dislikes
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        public static int Insert(Video video, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    tblVideo row = new tblVideo();
                    row.Id = video.Id;
                    row.Title = video.Title;
                    row.Studio = video.Studio;
                    row.ThumbnailUrl = video.ThumbnailUrl;
                    row.VideoUrl = video.VideoUrl;
                    row.ContentWarning = video.ContentWarning;

                    //Todo
                    row.Likes = video.Likes;
                }
                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Delete(Video video, bool rollback = false)
        {
            try
            {
                int results = 0;
                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
