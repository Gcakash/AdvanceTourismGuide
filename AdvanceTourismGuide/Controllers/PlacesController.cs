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
using PagedList;
using PagedList.Mvc;

namespace AdvanceTourismGuide.Controllers
{
    public class PlacesController : Controller
    {
        private ATGSEntities1 db = new ATGSEntities1();
        CombineModel combineModel = new CombineModel();//added
        // GET: Places
        public ActionResult Index( int? i)
        {
            return View(db.Places.ToList().ToPagedList(i?? 1,4));
        }

        // GET: Places/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // GET: Places/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Places/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Place_Id,City_Id,Place_Name,Place_Image,Discription,Place_Tag,Cost_Per_Day")] Place place , HttpPostedFileBase placepicfile)
        {
           /* if (ModelState.IsValid)
            {
                db.Places.Add(place);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(place);*/
            //this is added part
            var path = "";
            if (placepicfile != null)
            {
                if (placepicfile.ContentLength > 0)
                {
                    ViewBag.contentlent = true;//this is for acuret length

                    if (Path.GetExtension(placepicfile.FileName).ToLower() == ".jpg" ||
                      Path.GetExtension(placepicfile.FileName).ToLower() == ".png" ||
                      Path.GetExtension(placepicfile.FileName).ToLower() == ".gif" ||
                      Path.GetExtension(placepicfile.FileName).ToLower() == ".jpeg")
                    {
                        ViewBag.fileextention = true;//thi is to veryfy correct extention
                        path = Path.Combine(Server.MapPath("~/Content/images"), placepicfile.FileName);
                        placepicfile.SaveAs(path);

                        ViewBag.UploadSuccess = true;

                    }

                }
            }
            place.Place_Image = "~/Content/images/" + placepicfile.FileName;

            db.Places.Add(place);
            db.SaveChanges();
            return RedirectToAction("Index");
            //added part end
        }

        // GET: Places/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Place_Id,City_Id,Place_Name,Place_Image,Discription,Place_Tag,Cost_Per_Day")] Place place)
        {
            if (ModelState.IsValid)
            {
                db.Entry(place).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(place);
        }

        // GET: Places/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Place place = db.Places.Find(id);
            db.Places.Remove(place);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PlaceHome(int? i,string searchstring = "")
        {
            if (searchstring != "")
            {
                ViewBag.ResultsFor = searchstring;
                combineModel.Places = db.Places.Where(x=> x.Place_Name.Contains(searchstring)||x.Place_Tag.Contains(searchstring)).ToList();
               

                return View(combineModel);
            }
            else
            {
                ViewBag.ResultsFor = "All Places";
                combineModel.Places = db.Places.ToList();
                combineModel.Cities = db.Cities.ToList();//this is for if we use content of table city in the view
                return View(combineModel);
            }
            
           


        }
        public ActionResult Visit(string placeid="")
        {
            int placeId = Convert.ToInt32(placeid);
            List<Map> map = db.Maps.Where(x => x.Place_Id == placeId).ToList();
            Session["placeid"] = placeId;
            return View(map);
        }
     /*  [HttpPost]
        public ActionResult visit([Bind(Include = "Place_Id,Place_Name")] Place place)
        {

            Session["Placeid"] = place.Place_Name;

            return RedirectToAction("Visit", "Places");
           
        }

    */
        public ActionResult UserPlace(int? i,string searchstring ="")
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("login", "UserInfoes");
            }
            if (searchstring != "")
            {
                ViewBag.ResultsFor = searchstring;

                return View(db.Places.Where(x => x.Place_Name.Contains(searchstring) || x.Place_Tag.Contains(searchstring)).ToList().ToPagedList(i?? 1,1));
            }
            else
            {
                ViewBag.ResultsFor = "All Places";
                //this is for if we use content of table city in the view
                return View(db.Places.ToList().ToPagedList(i ?? 1, 4));
            }
        }



        public ActionResult PlaceMap()
        {
            return View();
        }

        public ActionResult VisitNow(int? i, string City_Id)

        {
           /* var districtList = db.Districts.ToList();
            SelectList list1 = new SelectList(districtList, "District_Id", "District_Name");
            ViewBag.districtList = list1;*/

            var districtList = db.Cities.ToList();
            SelectList list1 = new SelectList(districtList, "City_Id", "City_Name");
            ViewBag.districtList = list1;


            /* if (Session["username"] == null)
             {
                 return RedirectToAction("login", "UserInfoes");
             }*/
            int id = Convert.ToInt32(City_Id);

            if (City_Id != "")
            {
                ViewBag.ResultsFor = City_Id;

                return View(db.Places.Where(x => x.City_Id == id).ToList().ToPagedList(i ?? 1, 1));
            }
            else
            {
                ViewBag.ResultsFor = "All Places";
                //this is for if we use content of table city in the view
                return View(db.Places.ToList().ToPagedList(i ?? 1, 4));
            }

           
        }
        public ActionResult VisitNowHome(string placeid = "")
        {

            int placeId = Convert.ToInt32(placeid);
            List<Place> place = db.Places.Where(x => x.Place_Id == placeId).ToList();
            Session["placeidHome"] = placeId;
            return View(place);


           
            
        }
        public ActionResult VisitNowHome2()
        {
            return View(db.Places.ToList());
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
