using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheatreCMS3.Models;

namespace TheatreCMS3.Areas.Blog.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public ApplicationUser Author { get; set; }
        public string Message { get; set; }
        public DateTime CommentDate { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int CommentRef { get; set; }

        //Class Constructor: set the CommentDate to the current time
        public Comment() 
        {
            CommentDate = DateTime.Now;
            //Define parameter values for any int types because they cannot be null:
            Likes = 0;
            Dislikes = 0;
            CommentRef = 0;
        }

        //Method to return the PERCENTAGE of Likes to Total Votes:
        public double LikeRatio()
        {
            double LikeRatio = Convert.ToDouble(Likes)/(Likes + Dislikes)*100;
            return LikeRatio;
        }
    }
}