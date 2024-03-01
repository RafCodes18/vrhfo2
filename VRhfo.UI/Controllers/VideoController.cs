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
        public ActionResult Index(int? page)
        {
            //grab all videos in DB
            List<Video> videos = VideoManager.LoadAll();

            // 2. Set up paging parameters
            int pageSize = 2; // Videos per page
            int pageNumber = (page ?? 1); // Default to page 1

            return View(videos.ToPagedList(pageNumber, pageSize));
        }
        
        
        // GET: VideoController/Watch/how-to-build-a-tube-site (example URL)
        public ActionResult Watch(string title)
        {
            VideoViewModel videoViewModel = new VideoViewModel();

            // Important change: Look up the video by title (slug) now       

            videoViewModel.video = VideoManager.LoadByTitle(title);
            // ... Rest of your logic remains the same
            List<Video> list = VideoManager.LoadAll();
            videoViewModel.suggestedVideos = list;

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
