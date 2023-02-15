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
    public class GuideBookingsController : Controller
    {
        private ATGSEntities1 db = new ATGSEntities1();

        // GET: GuideBookings
        public ActionResult Index()
        {
            return View(db.GuideBookings.ToList());
        }

        // GET: GuideBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuideBooking guideBooking = db.GuideBookings.Find(id);
            if (guideBooking == null)
            {
                return HttpNotFound();
            }
            return View(guideBooking);
        }

        // GET: GuideBookings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GuideBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GB_Id,Place_Id,User_name,Phone,Approve_Status,GB_Comment")] GuideBooking guideBooking)
        {
            if (ModelState.IsValid)
            {
                db.GuideBookings.Add(guideBooking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(guideBooking);
        }

        // GET: GuideBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuideBooking guideBooking = db.GuideBookings.Find(id);
            if (guideBooking == null)
            {
                return HttpNotFound();
            }
            return View(guideBooking);
        }

        // POST: GuideBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GB_Id,Place_Id,User_name,Phone,Approve_Status,GB_Comment")] GuideBooking guideBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guideBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guideBooking);
        }

        // GET: GuideBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuideBooking guideBooking = db.GuideBookings.Find(id);
            if (guideBooking == null)
            {
                return HttpNotFound();
            }
            return View(guideBooking);
        }

        // POST: GuideBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GuideBooking guideBooking = db.GuideBookings.Find(id);
            db.GuideBookings.Remove(guideBooking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UserIndex()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("login", "UserInfoes");
            }
            return View(db.GuideBookings.ToList());
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
