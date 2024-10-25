using Microsoft.AspNetCore.Mvc;
using VRhfo.BL;
using VRhfo.BL.Models;
using VRhfo.UI.Views.ViewModels;
using X.PagedList;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;


namespace VRhfo.UI.Controllers
{
    public class VideoController : Controller
    {
        private User GetCurrentUser()
        {
            return HttpContext.Session.GetObject<User>("user");
        }

        [HttpPost]
        public ActionResult CheckIfLiked(Guid userId, int postId)
        {
            bool isLiked = LikedVideosManager.CheckIfLiked(userId, postId);
            return Json(new { isLiked });
        }


        [HttpPost]
        public ActionResult Like(int id, VideosLiked vidsLiked)
        {
            
            var user = HttpContext.Session.GetObject<User>("user");
            vidsLiked.VideoID = id;
            vidsLiked.UserID = user.Id;
            vidsLiked.LikedDate = DateTime.Now;
            LikedVideosManager.Insert(vidsLiked);
            return Json(new { PostId = vidsLiked.VideoID, isLiked = true });
        }

        [HttpPost]
        public ActionResult Unlike(int id, VideosLiked vidsLiked)
        {
            var user = HttpContext.Session.GetObject<User>("user");
            vidsLiked.VideoID = id;
            vidsLiked.UserID = user.Id;

            LikedVideosManager.Delete(vidsLiked.UserID, vidsLiked.VideoID);
            return Json(new {PostId = vidsLiked.VideoID, isLiked = false});

        }

        // GET: VideoController
        public ActionResult Index(string? category, int? page)
        {

            //grab all videos in DB
            List<Video> videos = VideoManager.LoadAll();

            //check if needs to load by category
            // Filtering and Sorting
            if (!string.IsNullOrEmpty(category))
            {
                if (category == "Latest")
                {
                    videos = videos.OrderByDescending(v => v.UploadDate).ToList();
                }
                else
                {
                    videos = videos.Where(v => v.Category == category).ToList();
                }
            }

            // 2. Set up paging parameters
            int pageSize = 12; // Videos per page
            int pageNumber = (page ?? 1); // Default to page 1

            ViewData["PageTitle"] = category; // Set the category as the page title


            return View(videos.ToPagedList(pageNumber, pageSize));
        }


        // GET: VideoController/Watch/how-to-build-a-tube-site (example URL)
        public ActionResult Watch(string title)
        {
            // Revert '-' (or '+') back to spaces
            string cleanTitle = title.Replace("-", " ");

            //grab user
            User currentUser = HttpContext.Session.GetObject<User>("user");
            ViewBag.CurrentUser = currentUser;
            
            VideoViewModel videoViewModel = new VideoViewModel();

            if(currentUser != null)
            {
                videoViewModel.LoggedInUserId = currentUser.Id;
            }

            //Load video 
            videoViewModel.video = VideoManager.LoadByTitle(cleanTitle);

            //load user
            if (videoViewModel.video.UserId != Guid.Empty)
            {
                videoViewModel.video.user = UserManager.LoadById(videoViewModel.video.UserId);
            }
           
            
            //load suggested videos
            List<Video> list = VideoManager.GetSuggestedVideos(12, title);
            videoViewModel.suggestedVideos = list;

            if(currentUser != null)
            {
                videoViewModel.likeState = VideoManager.checkIfCurrentVideoLiked(videoViewModel.video, currentUser);
            }
            else
            {
                videoViewModel.likeState = "like-noL";
            }
            

            videoViewModel.video.Comments = CommentManager.GetCommentsByVideoId(videoViewModel.video.Id);

            foreach(Comment c in videoViewModel.video.Comments)
            {
                c.LikesCount = CommentManager.LoadLikeCount(c.Id);
                c.DislikesCount = CommentManager.LoadDislikeCount(c.Id);
            }
 
            for(int i = 0; i < videoViewModel.video.Comments.Count; i++)
            {
                videoViewModel.video.Comments[i].Replies = CommentManager.LoadListOfReplies(videoViewModel.video.Comments[i].Id);
            }
            foreach (var comment in videoViewModel.video.Comments)
            {
                // Ensure Replies are not null
                if (comment.Replies != null)
                {
                    foreach (var reply in comment.Replies)
                    {
                        // Load user that posted each reply
                        reply.User = UserManager.LoadById(reply.UserId);
                    }
                }
            }
            if (currentUser != null)
            {
                var watchEntry = new WatchEntry
                {         
                    UserId = currentUser.Id,
                    VideoId = videoViewModel.video.Id,
                    WatchDurationTicks = 0,
                    LastDateWatched = DateTime.Now,
                    FirstViewed = DateTime.Now,
                    Completed = false
                };

                //only insert a watch entry if the video hasnt already been watched
                if (!VideoManager.CheckIfWatchEntry(watchEntry))
                {
                    VideoManager.InsertWatchEntry(watchEntry);
                }                
            }

            return View(videoViewModel);
        }

        [HttpPost]
        public ActionResult UpdateWatchProgress([FromBody] dynamic body)
        {          
            var watchEntry = new WatchEntry()
            {
                UserId = Guid.Parse(body.GetProperty("userId").GetString()),  // userId is a string (GUID)
                VideoId = int.Parse(body.GetProperty("videoId").GetString()), 
                LastDateWatched = DateTime.UtcNow,  
                WatchDurationTicks = body.GetProperty("watchDuration").GetInt32(), // parse the timespan string
                Completed = body.GetProperty("completed").GetBoolean()  
            };

            try
            {              
                    if (VideoManager.UpdateWatchEntry(watchEntry))
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Failed to update watch entry" });
                    }                       
            }
            catch (Exception ex)
            {                
                return Json(new { success = false, message = "An error occurred while updating watch entry" });
            }

        }

        // Helper method to parse watch duration from string format
        private long ParseTimeSpan(string timeSpanString)
        {
            // Assuming the format is hh:mm:ss or similar
            TimeSpan timeSpan = TimeSpan.Parse(timeSpanString);
            return timeSpan.Ticks;
        }
        // GET: VideoController/Upload
        public ActionResult Upload()
        {
            return View();
        }

        // POST: VideoController/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VideoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VideoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VideoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VideoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult AddComment([FromBody] dynamic commentDto)
        {
            User currentUser = GetCurrentUser();

            if(currentUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            // Validate commentDto and save the comment in the database
            var newComment = new Comment
            {
                Content = commentDto.GetProperty("commentText").GetString(),
                DatePosted = DateTime.UtcNow,
                UserId = currentUser.Id,
                VideoId = int.Parse(commentDto.GetProperty("videoId").GetString())
            };

            if (CommentManager.InsertComment(newComment) > 0)
            {
                newComment.User = UserManager.LoadById(newComment.UserId);

                return Json(new { success = true, comment = newComment });              
            }
            else
            {
                return Json(new { success = false, comment = newComment });

            }
        }

        [HttpPost]
        public JsonResult LikeComment([FromBody] dynamic likeData)
        {
            var commentId = int.Parse(likeData.GetProperty("commentId").GetString());
            var isLike = likeData.GetProperty("isLike").GetBoolean();
            
            if(GetCurrentUser() == null || GetCurrentUser().Id == Guid.Empty)
            {
                return Json(new { success = false, message = "An error occurred while processing your request." });
            }
            var userId = GetCurrentUser().Id;
         
            var existingLike = CommentManager.CheckForExistingLikeEntry(commentId, userId);

            if (existingLike != null)
            {
                // Update the existing like/dislike if the user already interacted with the comment
                existingLike.IsLike = isLike;
                CommentManager.UpdateLikeDislikeEntry(existingLike);
            }
            else
            {
                // Create a new like/dislike
                var newLike = new CommentLikes
                {
                    UserId = userId,
                    CommentId = commentId,
                    IsLike = isLike,
                    CreatedAt = DateTime.UtcNow
                };
                CommentManager.InsertLikeDislikeEntry(newLike);
            }
            // Return the updated like/dislike counts
            var likeCount = CommentManager.LoadLikeCount(commentId);
            var dislikeCount = CommentManager.LoadDislikeCount(commentId);

            return Json(new {success = true, likeCount, dislikeCount });

        }

    }
}
