using VRhfo.BL.Models;
using VRhfo.PL;

namespace VRhfo.BL
{
    public static class VideoManager
    {

        public static List<Video> GetSuggestedVideos(int maxSuggestions, string watchedVideoTitle)
        {
            using (VRhfoEntities dc = new VRhfoEntities())
            {
                var random = new Random();
                var unwatchedVideos = dc.tblVideos.Where(v => v.Title != watchedVideoTitle).ToList();
                var shuffledVideos = unwatchedVideos.OrderBy(v => random.Next()).ToList();

                if (shuffledVideos.Count <= maxSuggestions)
                {
                    return shuffledVideos.Select(v => new Video
                    {
                        Id = v.Id,
                        UserId = v.UserId,
                        Title = v.Title,
                        Category = v.Category,
                        ThumbnailUrl = v.ThumbnailUrl,
                        VideoUrl = v.VideoUrl,
                        Description = v.Description,
                        Genre = v.Genre,
                        UploadDate = v.UploadDate,
                        Duration = v.Duration,
                        Views = v.Views,
                        RatingCount = v.RatingCount,
                        IsPublic = v.IsPublic == 1,
                        IsPreview = v.IsPreview == 1,
                        ContentWarning = v.ContentWarning,
                        Likes = v.Likes,
                        Dislikes = v.Dislikes
                    }).ToList();
                }
                else
                {
                    return shuffledVideos.Take(maxSuggestions).Select(v => new Video
                    {
                        Id = v.Id,
                        UserId = v.UserId,
                        Title = v.Title,
                        Category = v.Category,
                        ThumbnailUrl = v.ThumbnailUrl,
                        VideoUrl = v.VideoUrl,
                        Description = v.Description,
                        Genre = v.Genre,
                        UploadDate = v.UploadDate,
                        Duration = v.Duration,
                        Views = v.Views,
                        RatingCount = v.RatingCount,
                        IsPublic = v.IsPublic == 1,
                        IsPreview = v.IsPreview == 1,
                        ContentWarning = v.ContentWarning,
                        Likes = v.Likes,
                        Dislikes = v.Dislikes
                    }).ToList();
                }
            }
        }

        public static Video LoadByTitle(string title)
        {
            using (VRhfoEntities dc = new VRhfoEntities())
            {
                var videoEntity = dc.tblVideos.FirstOrDefault(v => v.Title == title);

                if (videoEntity != null)
                {
                    return new Video
                    {
                        Id = videoEntity.Id,
                        UserId = videoEntity.UserId,
                        Title = videoEntity.Title,
                        Category = videoEntity.Category,
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
        public static List<Video> LoadAll()
        {
            List<Video> list = new List<Video>();

            using (VRhfoEntities dc = new VRhfoEntities())
            {
                (from v in dc.tblVideos
                 select new
                 {
                     v.Id,
                     v.UserId,
                     v.Title,
                     v.Category,
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
                     UserId = video.UserId,
                     Title = video.Title,
                     Category = video.Category,
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
                        UserId = videoEntity.UserId,
                        Title = videoEntity.Title,
                        Category = videoEntity.Category,
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
                    row.UserId = video.UserId;
                    row.Title = video.Title;
                    row.Category = video.Category;
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
