using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ClientApp_PointOfSell.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplaySellPage()
        {
            return View();
        }


        public ActionResult Practice()
        {
            return View();
        }
    }
}
