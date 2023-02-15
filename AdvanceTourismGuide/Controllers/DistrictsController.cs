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
    public class DistrictsController : Controller
    {
        private ATGSEntities1 db = new ATGSEntities1();

        // GET: Districts
        public ActionResult Index()
        {
            if (Session["Adminname"] == null)
            {

                return RedirectToAction("AdminLogin", "AdminInfoes");

            }
            return View(db.Districts.ToList());
        }

        // GET: Districts/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Adminname"] == null)
            {

                return RedirectToAction("AdminLogin", "AdminInfoes");

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            District district = db.Districts.Find(id);
            if (district == null)
            {
                return HttpNotFound();
            }
            return View(district);
        }

        // GET: Districts/Create
        public ActionResult Create()
        {
            if (Session["Adminname"] == null)
            {

                return RedirectToAction("AdminLogin", "AdminInfoes");

            }
            return View();
        }

        // POST: Districts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "District_Id,District_Name,Province_Id")] District district)
        {
            if (ModelState.IsValid)
            {
                db.Districts.Add(district);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(district);
        }

        // GET: Districts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Adminname"] == null)
            {

                return RedirectToAction("AdminLogin", "AdminInfoes");

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            District district = db.Districts.Find(id);
            if (district == null)
            {
                return HttpNotFound();
            }
            return View(district);
        }

        // POST: Districts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "District_Id,District_Name,Province_Id")] District district)
        {
            if (ModelState.IsValid)
            {
                db.Entry(district).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(district);
        }

        // GET: Districts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Adminname"] == null)
            {

                return RedirectToAction("AdminLogin", "AdminInfoes");

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            District district = db.Districts.Find(id);
            if (district == null)
            {
                return HttpNotFound();
            }
            return View(district);
        }

        // POST: Districts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            District district = db.Districts.Find(id);
            db.Districts.Remove(district);
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
    }
}
