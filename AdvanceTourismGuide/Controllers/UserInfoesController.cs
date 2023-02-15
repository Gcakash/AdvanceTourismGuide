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
    public class UserInfoesController : Controller
    {
        private ATGSEntities db = new ATGSEntities();

        // GET: UserInfoes
        public ActionResult Index()
        {
            return View(db.UserInfoes.ToList());
        }

        // GET: UserInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfoes.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // GET: UserInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,Password,FullName,Email,Phone,Address")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                db.UserInfoes.Add(userInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userInfo);
        }

        // GET: UserInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfoes.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,Password,FullName,Email,Phone,Address")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userInfo);
        }

        // GET: UserInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfoes.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserInfo userInfo = db.UserInfoes.Find(id);
            db.UserInfoes.Remove(userInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //log in part start--------
        public ActionResult Login()
        {
            if (Session["username"] != null)
            {
                return RedirectToAction("Index", "Home", new { username = Session["username"].ToString() });

            }
            else
            {
                return View();
            }
        }
        [HttpPost]

        public ActionResult login([Bind(Include = "UserName,Password,Phone")] UserInfo user)
        {
            var userLoggedIn = db.UserInfoes.SingleOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            if (userLoggedIn != null)
            {
                ViewBag.message = "loggedin";
                ViewBag.triedOnce = "yes";
                //this is sesssion
                Session["username"] = user.UserName;
                Session["userPhone"] = db.UserInfoes.SingleOrDefault(x => x.UserName == user.UserName).Phone;

                return RedirectToAction("UserPlace", "Places", new { username = user.UserName });
            }
            else
            {
                ViewBag.triedOnce = "yes";
                return View();
            }
        }
        //signup part started
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup([Bind(Include = "UserId,UserName,Password,FullName,Email,Phone,Address")] UserInfo userInfo)
        {
            var userSignup = db.UserInfoes.SingleOrDefault(x => x.UserName == userInfo.UserName );
           
                
            if (ModelState.IsValid)
                {
                if (userSignup == null)
                {

                    db.UserInfoes.Add(userInfo);
                    db.SaveChanges();
                   
                    return RedirectToAction("Login");
                    
                }
                else
                {
                    ViewBag.message = "sameuser";
                    
                }

            }
           
            return View(userInfo);
            
        }
        //this is for user home page
        
       public ActionResult UserHome()
        {
            return View();
        }
        public ActionResult UserProfile()
        {
          
                if (Session["username"] == null)
                {
                    return RedirectToAction("login", "UserInfoes");
                }
               
            
            return View(db.UserInfoes.ToList());
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
