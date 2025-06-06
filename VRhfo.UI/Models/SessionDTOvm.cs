namespace VRhfo.UI.Models
{
    public class SessionDTOvm
    {
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
        public int SessionDurationSeconds { get; set; }
        public int WatchTimeSeconds { get; set; }
    }
}
