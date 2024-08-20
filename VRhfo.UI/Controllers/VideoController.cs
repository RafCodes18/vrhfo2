using Microsoft.AspNetCore.Mvc;
using VRhfo.BL;
using VRhfo.BL.Models;
using VRhfo.UI.Views.ViewModels;
using X.PagedList;
using Microsoft.AspNetCore.Http.Extensions;


namespace VRhfo.UI.Controllers
{
    public class VideoController : Controller
    {
        [HttpPost]
        public ActionResult CheckIfLiked(int userId, int postId)
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
            int pageSize = 8; // Videos per page
            int pageNumber = (page ?? 1); // Default to page 1

            ViewData["PageTitle"] = category; // Set the category as the page title


            return View(videos.ToPagedList(pageNumber, pageSize));
        }


        // GET: VideoController/Watch/how-to-build-a-tube-site (example URL)
        public ActionResult Watch(string title)
        {
            // Revert '-' (or '+') back to spaces
            string cleanTitle = title.Replace("-", " ");

            User currentUser = HttpContext.Session.GetObject<User>("user");
            ViewBag.CurrentUser = currentUser;
            
            VideoViewModel videoViewModel = new VideoViewModel();

            // Important change: Look up the video by title (slug) now       

            videoViewModel.video = VideoManager.LoadByTitle(cleanTitle);
            videoViewModel.video.user = UserManager.LoadById(videoViewModel.video.UserId);
            // 
            List<Video> list = VideoManager.GetSuggestedVideos(8, title);
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
            return View(videoViewModel);
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
    }
}
