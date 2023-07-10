using Microsoft.AspNetCore.Mvc;
using VRhfo.BL.Models;
using VRhfo.UI.Views.ViewModels;

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
            video.Title = "Lana Rhoades";
            video.Id = 81;
            video.VideoUrl = "ffsjof";
            video.UploadDate = DateTime.Now;
            video.ThumbnailUrl = "lana.jpg";
            video.Studio = "BangBros";
            list.Add(video);
            Video video2 = new Video();
            video2.Description = "Description";
            video2.Title = "Hot Anal";
            video2.Id = 71;
            video2.VideoUrl = "2nd";
            video2.UploadDate = DateTime.Now;
            video2.ThumbnailUrl = "vrpic.jpg";
            video2.Studio = "XXX";
            list.Add(video2);
            Video video3 = new Video();
            video3.Description = "Shes on fire.";
            video3.Title = "Nicole Aniston";
            video3.Id = 16;
            video3.VideoUrl = "ffsjof";
            video3.UploadDate = DateTime.Now;
            video3.ThumbnailUrl = "nicole.jpg";
            video3.Studio = "Pornhub";
            list.Add(video3);
            Video video4 = new Video();
            video4.Description = "Description";
            video4.Title = "Fat ass riding";
            video4.Id = 15;
            video4.VideoUrl = "ffsjof";
            video4.UploadDate = DateTime.Now;
            video4.ThumbnailUrl = "azz.jpg";
            video4.Studio = "Naughty America";
            list.Add(video4);
            Video video5 = new Video();
            video5.Description = "Description";
            video5.Title = "Reily Reed 3 Some";
            video5.Id = 14;
            video5.VideoUrl = "ffsjof";
            video5.UploadDate = DateTime.Now;
            video5.ThumbnailUrl = "reily.jpg";
            video5.Studio = "BangBros";
            list.Add(video5);
            Video video6 = new Video();
            video6.Description = "Description";
            video6.Title = "Angela White TitFucki";
            video6.Id = 11;
            video6.VideoUrl = "ffsjof";
            video6.UploadDate = DateTime.Now;
            video6.ThumbnailUrl = "ang.jpg";
            video6.Studio = "NaughtyAmerica";
            list.Add(video6);

            Video video7 = new Video();
            video7.Description = "Description";
            video7.Title = "Squat";
            video7.Id = 12;
            video7.VideoUrl = "ffsjof";
            video7.UploadDate = DateTime.Now;
            video7.ThumbnailUrl = "squat.jpg";
            video7.Studio = "NaughtyAmerica";
            list.Add(video7);

            Video video8 = new Video();
            video8.Description = "Description";
            video8.Title = "Puss";
            video8.Id = 21;
            video8.VideoUrl = "ffsjof";
            video8.UploadDate = DateTime.Now;
            video8.ThumbnailUrl = "puss.jpg";
            video8.Studio = "NaughtyAmerica";
            list.Add(video8);

            Video video9 = new Video();
            video9.Description = "Description";
            video9.Title = "Squat";
            video9.Id = 20;
            video9.VideoUrl = "ffsjof";
            video9.UploadDate = DateTime.Now;
            video9.ThumbnailUrl = "squat.jpg";
            video9.Studio = "NaughtyAmerica";
            list.Add(video9);

            Video video10 = new Video();
            video10.Description = "Description";
            video10.Title = "Puss";
            video10.Id = 10;
            video10.VideoUrl = "ffsjof";
            video10.UploadDate = DateTime.Now;
            video10.ThumbnailUrl = "puss.jpg";
            video10.Studio = "NaughtyAmerica";
            list.Add(video10);

            return View(list);
        }

        // GET: VideoController/Details/5
        public ActionResult Details(int id)
        {
            VideoViewModel videoViewModel = new VideoViewModel();

            Video vid = new Video();
            vid.Description = "Description";
            vid.Title = "Title";
            vid.Id = id;
            vid.VideoUrl = "ass.mp4";
            vid.UploadDate = DateTime.Now;
            vid.ThumbnailUrl = "puss.jpg";
            vid.Studio = "dg";
            videoViewModel.video = vid;

            List<Video> list = new List<Video>();
            Video video = new Video();
            video.Description = "Description";
            video.Title = "Lana Rhoades";
            video.Id = 1;
            video.VideoUrl = "ffsjof";
            video.UploadDate = DateTime.Now;
            video.ThumbnailUrl = "lana.jpg";
            video.Studio = "BangBros";
            list.Add(video);
            Video video2 = new Video();
            video2.Description = "Description";
            video2.Title = "Hot Anal";
            video2.Id = 1;
            video2.VideoUrl = "2nd";
            video2.UploadDate = DateTime.Now;
            video2.ThumbnailUrl = "vrpic.jpg";
            video2.Studio = "XXX";
            list.Add(video2);
            Video video3 = new Video();
            video3.Description = "Shes on fire.";
            video3.Title = "Nicole Aniston";
            video3.Id = 1;
            video3.VideoUrl = "ffsjof";
            video3.UploadDate = DateTime.Now;
            video3.ThumbnailUrl = "nicole.jpg";
            video3.Studio = "Pornhub";
            list.Add(video3);
            Video video4 = new Video();
            video4.Description = "Description";
            video4.Title = "Fat ass riding";
            video4.Id = 1;
            video4.VideoUrl = "ffsjof";
            video4.UploadDate = DateTime.Now;
            video4.ThumbnailUrl = "azz.jpg";
            video4.Studio = "Naughty America";
            list.Add(video4);
            Video video5 = new Video();
            video5.Description = "Description";
            video5.Title = "Reily Reed 3 Some";
            video5.Id = 1;
            video5.VideoUrl = "ffsjof";
            video5.UploadDate = DateTime.Now;
            video5.ThumbnailUrl = "reily.jpg";
            video5.Studio = "BangBros";
            list.Add(video5);
            Video video6 = new Video();
            video6.Description = "Description";
            video6.Title = "Angela White TitFucki";
            video6.Id = 1;
            video6.VideoUrl = "ffsjof";
            video6.UploadDate = DateTime.Now;
            video6.ThumbnailUrl = "ang.jpg";
            video6.Studio = "NaughtyAmerica";
            list.Add(video6);

            Video video7 = new Video();
            video7.Description = "Description";
            video7.Title = "Squat";
            video7.Id = 1;
            video7.VideoUrl = "ffsjof";
            video7.UploadDate = DateTime.Now;
            video7.ThumbnailUrl = "squat.jpg";
            video7.Studio = "NaughtyAmerica";
            list.Add(video7);

            Video video8 = new Video();
            video8.Description = "Description";
            video8.Title = "Puss";
            video8.Id = 1;
            video8.VideoUrl = "ffsjof";
            video8.UploadDate = DateTime.Now;
            video8.ThumbnailUrl = "puss.jpg";
            video8.Studio = "NaughtyAmerica";
            list.Add(video8);

            Video video9 = new Video();
            video9.Description = "Description";
            video9.Title = "Squat";
            video9.Id = 1;
            video9.VideoUrl = "ffsjof";
            video9.UploadDate = DateTime.Now;
            video9.ThumbnailUrl = "squat.jpg";
            video9.Studio = "NaughtyAmerica";
            list.Add(video9);

            Video video10 = new Video();
            video10.Description = "Description";
            video10.Title = "Puss";
            video10.Id = 1;
            video10.VideoUrl = "ffsjof";
            video10.UploadDate = DateTime.Now;
            video10.ThumbnailUrl = "puss.jpg";
            video10.Studio = "NaughtyAmerica";
            list.Add(video10);
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
