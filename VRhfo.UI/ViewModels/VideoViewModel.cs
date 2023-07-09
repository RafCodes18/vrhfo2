using VRhfo.BL.Models;

namespace VRhfo.UI.Views.ViewModels
{
    public class VideoViewModel
    {
        public Video video { get; set; }

        public List<Video> suggestedVideos { get; set; }
    }
}
