using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreCMS3.Areas.Blog.Models;
using TheatreCMS3.Models;

namespace TheatreCMS3.Areas.Blog.Controllers
{
    public class BlogPhotosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Blog/BlogPhotoes
        public ActionResult Index()
        {
            return View(db.BlogPhotos.ToList());
        }

        // GET: Blog/BlogPhotoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPhoto blogPhoto = db.BlogPhotos.Find(id);
            if (blogPhoto == null)
            {
                return HttpNotFound();
            }
            return View(blogPhoto);
        }

        // GET: Blog/BlogPhotoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog/BlogPhotoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlogPhotoId,Title,Photo")] BlogPhoto blogPhoto)
        {
            if (ModelState.IsValid)
            {
                db.BlogPhotos.Add(blogPhoto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogPhoto);
        }

        // GET: Blog/BlogPhotoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPhoto blogPhoto = db.BlogPhotos.Find(id);
            if (blogPhoto == null)
            {
                return HttpNotFound();
            }
            return View(blogPhoto);
        }

        // POST: Blog/BlogPhotoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogPhotoId,Title,Photo")] BlogPhoto blogPhoto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogPhoto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogPhoto);
        }

        // GET: Blog/BlogPhotoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPhoto blogPhoto = db.BlogPhotos.Find(id);
            if (blogPhoto == null)
            {
                return HttpNotFound();
            }
            return View(blogPhoto);
        }

        // POST: Blog/BlogPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPhoto blogPhoto = db.BlogPhotos.Find(id);
            db.BlogPhotos.Remove(blogPhoto);
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








        [HttpGet]
        public ActionResult CreateBlogPhoto()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBlogPhoto([Bind(Include = "BlogPhotoId,Title,Photo")] BlogPhoto blogPhoto, HttpPostedFileBase photoInput)
        {
            //Ensure the input form isn't empty.
            if (photoInput != null && photoInput.ContentLength > 0)
            {
                string extension = Path.GetExtension(photoInput.FileName);
                //Ensure the file loaded is an image file. Remove or adjust this to allow other uploads.
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    var photobyte = imgToByteArray(photoInput);

                    if (ModelState.IsValid)
                    {
                        blogPhoto.Photo = photobyte;
                        db.BlogPhotos.Add(blogPhoto);
                        db.SaveChanges();
                        Response.Write("<script>alert('Image was uploaded successfully.'); </script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg, jpeg, or png formats are acceptable. Please select an image of that type.'); </script>");
                }
                return View("Create");
            }
            else
            {
                Response.Write("<script>alert('Please select a file to upload.'); </script>");
                return View("Create");
            }
        }

        //convert image to byte array
        public byte[] imgToByteArray(HttpPostedFileBase photoInput)
        {
            byte[] byteArray;
            using (BinaryReader reader = new BinaryReader(photoInput.InputStream))
            {
                byteArray = reader.ReadBytes(photoInput.ContentLength);
            }
            return byteArray;
        }

        //Convert byte array to image
        public ActionResult byteToImg(int Id)
        {
            BlogPhoto image = db.BlogPhotos.Find(Id);
            byte[] byteArray = image.Photo;
            return File(byteArray, "image/jpeg");
        }
    }
}
