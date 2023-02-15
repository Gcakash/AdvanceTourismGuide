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
    public class AdminInfoesController : Controller
    {
        private ATGSEntities1 db = new ATGSEntities1();

        // GET: AdminInfoes
        public ActionResult Index()
        {
            if (Session["Adminname"]==null)
            {
                
                    return RedirectToAction("AdminLogin", "AdminInfoes");
                
            }

            return View(db.AdminInfoes.ToList());
        }

        // GET: AdminInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminInfo adminInfo = db.AdminInfoes.Find(id);
            if (adminInfo == null)
            {
                return HttpNotFound();
            }
            return View(adminInfo);
        }

        // GET: AdminInfoes/Create
        public ActionResult Create()
        {
            if (Session["Adminname"] == null)
            {

                return RedirectToAction("AdminLogin", "AdminInfoes");

            }
            return View();
        }

        // POST: AdminInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminId,AdminName,Password,FullName,Email,Address,Phone")] AdminInfo adminInfo)
        {
            if (ModelState.IsValid)
            {
                db.AdminInfoes.Add(adminInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminInfo);
        }

        // GET: AdminInfoes/Edit/5
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
            AdminInfo adminInfo = db.AdminInfoes.Find(id);
            if (adminInfo == null)
            {
                return HttpNotFound();
            }
            return View(adminInfo);
        }

        // POST: AdminInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminId,AdminName,Password,FullName,Email,Address,Phone")] AdminInfo adminInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminInfo);
        }

        // GET: AdminInfoes/Delete/5
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
            AdminInfo adminInfo = db.AdminInfoes.Find(id);
            if (adminInfo == null)
            {
                return HttpNotFound();
            }
            return View(adminInfo);
        }

        // POST: AdminInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminInfo adminInfo = db.AdminInfoes.Find(id);
            db.AdminInfoes.Remove(adminInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult AdminHome()
        {
            if (Session["Adminname"] == null)
            {

                return RedirectToAction("AdminLogin", "AdminInfoes");

            }
            return View();
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]

        public ActionResult AdminLogin([Bind(Include = "AdminName,Password")] AdminInfo admin)
        {
            var AdminLoggedIn = db.AdminInfoes.SingleOrDefault(x => x.AdminName == admin.AdminName && x.Password == admin.Password);
            if (AdminLoggedIn != null)
            {
                ViewBag.message = "loggedin";
                ViewBag.triedOnce = "yes";
                //this is sesssion
                 Session["Adminname"] = admin.AdminName;


                return RedirectToAction("Index", "AdminInfoes", new {Adminname = admin.AdminName });
            }
            else
            {
                ViewBag.triedOnce = "yes";
                return View();
            }
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
