using Microsoft.AspNetCore.Mvc;
using VRhfo.BL;
using VRhfo.BL.Models;
using VRhfo.UI.Views.ViewModels;

namespace VRhfo.UI.Controllers
{
    public class VideoController : Controller
    {
        // GET: VideoController
        public ActionResult Index()
        {
            List<Video> videoList = VideoManager.LoadAll();

            return View(videoList);
        }

        // GET: VideoController/Details/5
        public ActionResult Details(int id)
        {
            VideoViewModel videoViewModel = new VideoViewModel();

            Video vid = VideoManager.LoadById(id);
            videoViewModel.video = vid;

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
