using Microsoft.AspNetCore.Mvc;
using VRhfo.BL;
using VRhfo.BL.Models;
using VRhfo.UI.Views.ViewModels;
using X.PagedList;

namespace VRhfo.UI.Controllers
{
    public class VideoController : Controller
    {
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
            int pageSize = 10; // Videos per page
            int pageNumber = (page ?? 1); // Default to page 1

            ViewData["PageTitle"] = category; // Set the category as the page title


            return View(videos.ToPagedList(pageNumber, pageSize));
        }


        // GET: VideoController/Watch/how-to-build-a-tube-site (example URL)
        public ActionResult Watch(string title)
        {
            VideoViewModel videoViewModel = new VideoViewModel();

            // Important change: Look up the video by title (slug) now       

            videoViewModel.video = VideoManager.LoadByTitle(title);
            videoViewModel.video.user = UserManager.LoadById(videoViewModel.video.UserId);
            // ... Rest of your logic remains the same
            List<Video> list = VideoManager.GetSuggestedVideos(8, title);
            videoViewModel.suggestedVideos = list;

            videoViewModel.video.Comments = CommentManager.GetCommentsByVideoId(videoViewModel.video.Id);
            return View(videoViewModel);
        }

        // GET: VideoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VideoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
