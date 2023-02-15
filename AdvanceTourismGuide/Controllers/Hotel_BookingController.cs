using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdvanceTourismGuide.Models;

namespace AdvanceTourismGuide.Controllers
{
    public class Hotel_BookingController : Controller
    {
        private ATGSEntities1 db = new ATGSEntities1();

        // GET: Hotel_Booking
        public ActionResult Index()
        {
            return View(db.Hotel_Booking.ToList());
        }

        // GET: Hotel_Booking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel_Booking hotel_Booking = db.Hotel_Booking.Find(id);
            if (hotel_Booking == null)
            {
                return HttpNotFound();
            }
            return View(hotel_Booking);
        }

        // GET: Hotel_Booking/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hotel_Booking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HB_Id,Place_Id,User_Name,Phone,Approve_Status,Coment")] Hotel_Booking hotel_Booking)
        {
            if (ModelState.IsValid)
            {
                db.Hotel_Booking.Add(hotel_Booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotel_Booking);
        }

        // GET: Hotel_Booking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel_Booking hotel_Booking = db.Hotel_Booking.Find(id);
            if (hotel_Booking == null)
            {
                return HttpNotFound();
            }
            return View(hotel_Booking);
        }

        // POST: Hotel_Booking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HB_Id,Place_Id,User_Name,Phone,Approve_Status,Coment")] Hotel_Booking hotel_Booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel_Booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel_Booking);
        }

        // GET: Hotel_Booking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel_Booking hotel_Booking = db.Hotel_Booking.Find(id);
            if (hotel_Booking == null)
            {
                return HttpNotFound();
            }
            return View(hotel_Booking);
        }

        // POST: Hotel_Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hotel_Booking hotel_Booking = db.Hotel_Booking.Find(id);
            db.Hotel_Booking.Remove(hotel_Booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UserIndex()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("login", "UserInfoes");
            }
            return View(db.Hotel_Booking.ToList());
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
