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
    public class VehicalBookingsController : Controller
    {
        private ATGSEntities1 db = new ATGSEntities1();

        // GET: VehicalBookings
        public ActionResult Index()
        {
            return View(db.VehicalBookings.ToList());
        }

        // GET: VehicalBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicalBooking vehicalBooking = db.VehicalBookings.Find(id);
            if (vehicalBooking == null)
            {
                return HttpNotFound();
            }
            return View(vehicalBooking);
        }

        // GET: VehicalBookings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicalBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VB_Id,Place_Id,User_Name,Phone,Approve_Status,Coment")] VehicalBooking vehicalBooking)
        {
            if (ModelState.IsValid)
            {
                db.VehicalBookings.Add(vehicalBooking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicalBooking);
        }

        // GET: VehicalBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicalBooking vehicalBooking = db.VehicalBookings.Find(id);
            if (vehicalBooking == null)
            {
                return HttpNotFound();
            }
            return View(vehicalBooking);
        }

        // POST: VehicalBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VB_Id,Place_Id,User_Name,Phone,Approve_Status,Coment")] VehicalBooking vehicalBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicalBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicalBooking);
        }

        // GET: VehicalBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicalBooking vehicalBooking = db.VehicalBookings.Find(id);
            if (vehicalBooking == null)
            {
                return HttpNotFound();
            }
            return View(vehicalBooking);
        }

        // POST: VehicalBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicalBooking vehicalBooking = db.VehicalBookings.Find(id);
            db.VehicalBookings.Remove(vehicalBooking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UserIndex()
        {
            
                if (Session["username"] == null)
                {
                    return RedirectToAction("login", "UserInfoes");
                }
                return View(db.VehicalBookings.ToList());
           
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
