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
    public class HotelsController : Controller
    {
        private ATGSEntities1 db = new ATGSEntities1();

        // GET: Hotels
        public ActionResult Index()
        {
            return View(db.Hotels.ToList());
        }

        // GET: Hotels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // GET: Hotels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Hotel_Id,Place_Id,Hotel_Name,Hotel_Address,Hotel_Pic,Hotel_Dis,Hotel_Contact")] Hotel hotel, HttpPostedFileBase picfile)
        {
            /* if (ModelState.IsValid)
             {
                 db.Hotels.Add(hotel);
                 db.SaveChanges();
                 return RedirectToAction("Index");


             return View(hotel);}*/

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
            hotel.Hotel_Pic = "~/Content/images/" + picfile.FileName;

            db.Hotels.Add(hotel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Hotels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Hotel_Id,Place_Id,Hotel_Name,Hotel_Address,Hotel_Pic,Hotel_Dis,Hotel_Contact")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            db.Hotels.Remove(hotel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult HotelHome()
        {
            return View(db.Hotels.ToList());
        }


        [HttpPost]
        public ActionResult hotelHome()
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




            return RedirectToAction("HotelHome", "Hotels");
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
