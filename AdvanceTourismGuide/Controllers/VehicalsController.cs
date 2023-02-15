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
    public class VehicalsController : Controller
    {
        private ATGSEntities1 db = new ATGSEntities1();

        // GET: Vehicals
        public ActionResult Index()
        {
            return View(db.Vehicals.ToList());
        }

        // GET: Vehicals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehical vehical = db.Vehicals.Find(id);
            if (vehical == null)
            {
                return HttpNotFound();
            }
            return View(vehical);
        }

        // GET: Vehicals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Vehicale_id,Place_Id,Vehical_Name,Vehical_Address,Vehical_Pic,Vehical_dis,Vehical_Contact")] Vehical vehical, HttpPostedFileBase picfile)
        {
            /*  if (ModelState.IsValid)
              {
                  db.Vehicals.Add(vehical);
                  db.SaveChanges();
                  return RedirectToAction("Index");
              }

              return View(vehical);*/

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
            vehical.Vehical_Pic = "~/Content/images/" + picfile.FileName;

            db.Vehicals.Add(vehical);
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        // GET: Vehicals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehical vehical = db.Vehicals.Find(id);
            if (vehical == null)
            {
                return HttpNotFound();
            }
            return View(vehical);
        }

        // POST: Vehicals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Vehicale_id,Place_Id,Vehical_Name,Vehical_Address,Vehical_Pic,Vehical_dis,Vehical_Contact")] Vehical vehical)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehical).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehical);
        }

        // GET: Vehicals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehical vehical = db.Vehicals.Find(id);
            if (vehical == null)
            {
                return HttpNotFound();
            }
            return View(vehical);
        }

        // POST: Vehicals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehical vehical = db.Vehicals.Find(id);
            db.Vehicals.Remove(vehical);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult VehicalHome()
        {

            return View(db.Vehicals.ToList());
        }
          
        [HttpPost]
        public ActionResult vehicalHome(Vehical vehical)
        {
            if (ModelState.IsValid)
            {
                VehicalBooking obj = new VehicalBooking();
                //  Convert.ToInt32(Session["placeidHome"].ToString());
                obj.Place_Id = Convert.ToInt32(Session["placeidHome"].ToString());
                obj.User_Name = Session["username"].ToString();
                obj.Phone = Session["userPhone"].ToString();
                obj.Approve_Status = "pending";
                obj.Coment = "";
                db.VehicalBookings.Add(obj);
                db.SaveChanges();
                ViewBag.message = "success";
            }

           


            return RedirectToAction("VehicalHome", "Vehicals");
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
