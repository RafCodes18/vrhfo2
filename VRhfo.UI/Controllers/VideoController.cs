using Microsoft.AspNetCore.Mvc;
using VRhfo.BL.Models;

namespace VRhfo.UI.Controllers
{
    public class VideoController : Controller
    {
        // GET: VideoController
        public ActionResult Index()
        {
            List<Video> list = new List<Video>();
            Video video = new Video();
            video.Description = "Description";
            video.Title = "Title";
            video.Id = 1;
            video.VideoUrl = "ffsjof";
            video.UploadDate = DateTime.Now;
            video.ThumbnailUrl = "sf";
            video.Studio = "d";
            list.Add(video);
            Video video2 = new Video();
            video2.Description = "Description";
            video2.Title = "Title";
            video2.Id = 1;
            video2.VideoUrl = "vrpic.jpg";
            video2.UploadDate = DateTime.Now;
            video2.ThumbnailUrl = "sf";
            video2.Studio = "d";
            list.Add(video2);
            Video video3 = new Video();
            video3.Description = "Description";
            video3.Title = "Title";
            video3.Id = 1;
            video3.VideoUrl = "ffsjof";
            video3.UploadDate = DateTime.Now;
            video3.ThumbnailUrl = "sf";
            video3.Studio = "d";
            list.Add(video3);
            Video video4 = new Video();
            video4.Description = "Description";
            video4.Title = "Title";
            video4.Id = 1;
            video4.VideoUrl = "ffsjof";
            video4.UploadDate = DateTime.Now;
            video4.ThumbnailUrl = "sf";
            video4.Studio = "d";
            list.Add(video4);
            Video video5 = new Video();
            video5.Description = "Description";
            video5.Title = "Title";
            video5.Id = 1;
            video5.VideoUrl = "ffsjof";
            video5.UploadDate = DateTime.Now;
            video5.ThumbnailUrl = "sf";
            video5.Studio = "d";
            list.Add(video5);
            Video video6 = new Video();
            video6.Description = "Description";
            video6.Title = "Title";
            video6.Id = 1;
            video6.VideoUrl = "ffsjof";
            video2.UploadDate = DateTime.Now;
            video2.ThumbnailUrl = "sf";
            video2.Studio = "d";
            list.Add(video6);

            return View(list);
        }

        // GET: VideoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
