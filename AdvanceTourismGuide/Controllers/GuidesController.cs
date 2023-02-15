using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdvanceTourismGuide.Models;

namespace AdvanceTourismGuide.Controllers
{
    public class GuidesController : Controller
    {
        private ATGSEntities1 db = new ATGSEntities1();

        // GET: Guides
        public ActionResult Index()
        {
            if (Session["Adminname"] == null)
            {

                return RedirectToAction("AdminLogin", "AdminInfoes");

            }
            return View(db.Guides.ToList());
        }

        // GET: Guides/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guide guide = db.Guides.Find(id);
            if (guide == null)
            {
                return HttpNotFound();
            }
            return View(guide);
        }

        // GET: Guides/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Guide_Id,Place_Id,Guide_Name,Guide_Address,Guide_Pic,Guide_Contac")] Guide guide, HttpPostedFileBase picfile)
        {
          /*  if (ModelState.IsValid)
            {
                db.Guides.Add(guide);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(guide);

    */


            var path = "";
            if (picfile != null)
            {
                if (picfile.ContentLength > 0)
                {
                    ViewBag.contentlent = true;//this is for acuret length

                    if (Path.GetExtension(picfile.FileName).ToLower() == ".jpg" ||
                      Path.GetExtension(picfile.FileName).ToLower() == ".png" ||
                      Path.GetExtension(picfile.FileName).ToLower() == ".gif" ||
                      Path.GetExtension(picfile.FileName).ToLower() == ".jpeg")
                    {
                        ViewBag.fileextention = true;//thi is to veryfy correct extention
                        path = Path.Combine(Server.MapPath("~/Content/images"), picfile.FileName);
                        picfile.SaveAs(path);

                        ViewBag.UploadSuccess = true;

                    }

                }
            }
            guide.Guide_Pic = "~/Content/images/" + picfile.FileName;

            db.Guides.Add(guide);
            db.SaveChanges();
            return RedirectToAction("Index");




        }

        // GET: Guides/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guide guide = db.Guides.Find(id);
            if (guide == null)
            {
                return HttpNotFound();
            }
            return View(guide);
        }

        // POST: Guides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Guide_Id,Place_Id,Guide_Name,Guide_Address,Guide_Pic,Guide_Contac")] Guide guide)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guide).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guide);
        }

        // GET: Guides/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guide guide = db.Guides.Find(id);
            if (guide == null)
            {
                return HttpNotFound();
            }
            return View(guide);
        }

        // POST: Guides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guide guide = db.Guides.Find(id);
            db.Guides.Remove(guide);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult GuideHome()
        {
            return View(db.Guides.ToList());
        }

      

        [HttpPost]
        public ActionResult guideHome()
        {
            if (ModelState.IsValid)
            {
                Hotel_Booking obj = new Hotel_Booking();
                //  Convert.ToInt32(Session["placeidHome"].ToString());
                obj.Place_Id = Convert.ToInt32(Session["placeidHome"].ToString());
                obj.User_Name = Session["username"].ToString();
                obj.Phone = Session["userPhone"].ToString();
                obj.Approve_Status = "Pending";
                obj.Coment = "";
                db.Hotel_Booking.Add(obj);
                db.SaveChanges();
                ViewBag.message = "success";
            }




            return RedirectToAction("GuideHome", "Guides");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
