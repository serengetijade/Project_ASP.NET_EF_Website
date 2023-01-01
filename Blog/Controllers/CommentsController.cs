using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreCMS3.Areas.Blog.Models;
using TheatreCMS3.Models;

namespace TheatreCMS3.Areas.Blog.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Blog/Comments
        public ActionResult Index()
        {
            return View(db.Comments.OrderByDescending(Comment => Comment.CommentDate).ThenBy(Comment => Comment.CommentDate).ToList());
        }

        // GET: Blog/Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Blog/Comments/Create
        [ModAuthorize(Roles = "CommentModerator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog/Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [ModAuthorize(Roles = "CommentModerator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,Message,CommentDate,Likes,Dislikes")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comment);
        }

        // GET: Blog/Comments/Edit/5
        [ModAuthorize(Roles = "CommentModerator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Blog/Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [ModAuthorize(Roles = "CommentModerator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,Message,CommentDate,Likes,Dislikes")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: Blog/Comments/Delete/5
        [ModAuthorize(Roles = "CommentModerator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Blog/Comments/Delete/5
        [Authorize(Roles = "CommentModerator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Methods to add likes:
        [HttpPost]
        public JsonResult IncrementLike(int Id)
        {
            var Comment = db.Comments.Find(Id);
            Comment.Likes+=1;
            db.Entry(Comment).State = EntityState.Modified;
            db.SaveChanges();
            var result = new JsonResult();
            result.Data = new List<double>() { Comment.Likes, Comment.LikeRatio() };
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet; //Change the default setting of this behavior to ALLOW Get requests.
            return Json(result);
        }
 
        //Method to add dislikes: 
        [HttpPost]
        public JsonResult IncrementDislike(int Id)
        {
            var Comment = db.Comments.Find(Id);
            Comment.Dislikes+=1;
            db.Entry(Comment).State = EntityState.Modified;
            db.SaveChanges();
            var result = new JsonResult();
            result.Data = new List<double>() { Comment.Dislikes, Comment.LikeRatio() };
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet; //Change the default setting of this behavior to ALLOW Get requests.
            return Json(result);
        }

        //Method to delete comments
        [HttpPost, ActionName("DeleteCommentConfirmed")]
        public ActionResult DeleteCommentConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");       
        }

        //Create a new (asynchronous) comment- part 2
        [HttpPost, ActionName("CreateComment")]
        public JsonResult CreateComment(string message, int id)
        {
            var Comment = new Comment();
            Comment.Message = message;
            Comment.CommentRef = id;
            db.Comments.Add(Comment);
            db.SaveChanges();
            var result = new JsonResult();
            result.Data = Comment;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet; //Change the default setting of this behavior to ALLOW Get requests.
            return Json(result);
        }

        // GET: Blog/Comments/AccessDenied
        public ActionResult AccessDenied()
        {                       
            return View();    
            //return View(empty) direct users to whichever view that has the same name as the method,
            //i.e. "AccessDenied" view in the Comments folder (because this is in the Comments Controller).
        }
    }
}
