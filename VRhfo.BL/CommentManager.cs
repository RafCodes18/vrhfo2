using Microsoft.EntityFrameworkCore;
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
                List<tblComment> tblComments = db.tblComments
                    .Where(c => c.VideoId == videoId)
                    .Include(c => c.User)
                    .OrderByDescending(c => c.DatePosted)
                    .ToList();

                List<Comment> comments = new List<Comment>();

                foreach (var tblComment in tblComments)
                {
                    comments.Add(new Comment
                    {
                        Content = tblComment.Content,
                        DatePosted = tblComment.DatePosted,
                        UserId = tblComment.UserId,
                        Id = tblComment.Id,
                        User = new User
                        {
                            Username = tblComment.User.Username,
                        }
                    });
                }

                return comments;
            }
        }

        public static int InsertComment(Comment newComment)
        {
            try
            {
                int result;
                using(VRhfoEntities dc = new VRhfoEntities())
                {
                    tblComment newRow = new tblComment();
                    newRow.Content = newComment.Content;
                    newRow.DatePosted = newComment.DatePosted;  
                    newRow.UserId = newComment.UserId;
                    newRow.VideoId = newComment.VideoId;
                   /* newRow.DislikesCount = 0;
                    newRow.LikesCount = 0;
*/
                    dc.tblComments.Add(newRow);

                    result = dc.SaveChanges();  
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
