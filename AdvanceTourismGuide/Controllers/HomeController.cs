using AdvanceTourismGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdvanceTourismGuide.Controllers
{
    public class HomeController : Controller
    {
        private ATGSEntities1 db = new ATGSEntities1();
        CombineModel combineModel = new CombineModel(); //this not work
        public ActionResult Index()
        {
            combineModel.Places = db.Places.ToList();//added
            return View(combineModel);//added combinemodel
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult UserHome()
        {
            return View();
        }


        public ActionResult HomeIndex()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }


    }
}