using VRhfo.BL.Models;
using VRhfo.PL;

namespace VRhfo.BL
{
    public class CommentManager
    {
        public static List<Comment> GetCommentsByVideoId(int videoId)
        {
            using (VRhfoEntities db = new VRhfoEntities())
            {
                List<tblComment> tblComments = db.tblComments.Where(v => v.VideoId == videoId).ToList();
                List<Comment> comments = new List<Comment>();
                foreach (var tblComment in tblComments)
                {
                    comments.Add(new Comment
                    {
                        Content = tblComment.Content,
                        DatePosted = tblComment.DatePosted,
                        UserId = tblComment.UserId,
                        Id = tblComment.Id,
                    });
                }
                return comments;
            }
        }
    }
}
