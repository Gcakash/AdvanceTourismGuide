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
    public class Cost_EstimationController : Controller
    {
        private ATGSEntities1 db = new ATGSEntities1();

        // GET: Cost_Estimation
        public ActionResult Index()
        {
            return View(db.Cost_Estimation.ToList());
        }

        // GET: Cost_Estimation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cost_Estimation cost_Estimation = db.Cost_Estimation.Find(id);
            if (cost_Estimation == null)
            {
                return HttpNotFound();
            }
            return View(cost_Estimation);
        }

        // GET: Cost_Estimation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cost_Estimation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cost_Id,Place_Id,Avrage_Cost")] Cost_Estimation cost_Estimation)
        {
            if (ModelState.IsValid)
            {
                db.Cost_Estimation.Add(cost_Estimation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cost_Estimation);
        }

        // GET: Cost_Estimation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cost_Estimation cost_Estimation = db.Cost_Estimation.Find(id);
            if (cost_Estimation == null)
            {
                return HttpNotFound();
            }
            return View(cost_Estimation);
        }

        // POST: Cost_Estimation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cost_Id,Place_Id,Avrage_Cost")] Cost_Estimation cost_Estimation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cost_Estimation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cost_Estimation);
        }

        // GET: Cost_Estimation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cost_Estimation cost_Estimation = db.Cost_Estimation.Find(id);
            if (cost_Estimation == null)
            {
                return HttpNotFound();
            }
            return View(cost_Estimation);
        }

        // POST: Cost_Estimation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cost_Estimation cost_Estimation = db.Cost_Estimation.Find(id);
            db.Cost_Estimation.Remove(cost_Estimation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CostHome()
        {
            return View(db.Cost_Estimation.ToList());
        }
        public ActionResult CostIndex()
        {
            return View(db.Cost_Estimation.ToList());
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
