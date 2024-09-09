using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRhfo.BL.Models;
using VRhfo.PL;

namespace VRhfo.BL
{
    public class LikedVideosManager
    {
        public static bool CheckIfLiked(Guid userId, int postId)
        {
            try
            {
                using (VRhfoEntities dc = new VRhfoEntities())
                {
                    // Check if a row exists in the database for the given userId and postId
                    bool isLiked = dc.tblVideosLikes.Any(v => v.UserID == userId && v.VideoID == postId);
                    return isLiked;
                }
            }
            catch (Exception)
            {
                // Handle any exceptions that occur during the check
                throw;
            }
        }

        public static int Delete(Guid userId, int postId)
        {
            try
            {
                int result = 0;
                using(VRhfoEntities dc = new VRhfoEntities())
                {
                    tblVideosLiked tb = new tblVideosLiked();
                    tb.UserID = userId;
                    tb.VideoID = postId;
                    dc.tblVideosLikes.Remove(tb);

                    result = dc.SaveChanges();
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Insert(VideosLiked vidLiked)
        {
			try
			{
				int result = 0;
				using(VRhfoEntities dc = new VRhfoEntities())
				{
					tblVideosLiked newVidLiked = new tblVideosLiked();
					newVidLiked.UserID = vidLiked.UserID;
					newVidLiked.VideoID = vidLiked.VideoID;
					newVidLiked.LikedDate = vidLiked.LikedDate;

					dc.tblVideosLikes.Add(newVidLiked);
					result = dc.SaveChanges();
				}
				return result; 

			}
            catch (Exception)
            {
                // Handle other exceptions
                throw;
            }
        }

        private static void Remove(Guid userID, int videoID)
        {
            try
            {
                int result = 0;
                using(VRhfoEntities dc = new VRhfoEntities())
                {
                    tblVideosLiked recordToRemove = dc.tblVideosLikes.FirstOrDefault(v => v.UserID == userID && v.VideoID == videoID);

                    if (recordToRemove != null)
                    {
                        // Remove the record from the database context
                        dc.tblVideosLikes.Remove(recordToRemove);
                        // Save changes to apply the removal
                        dc.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
