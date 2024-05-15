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

				throw;
			}
        }
    }
}
