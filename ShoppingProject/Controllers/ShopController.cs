using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingProject.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View();
        }
        public ActionResult Bill()
        {
            return View();
        }
        public ActionResult Payment()
        {
            return View();
        }
    }
}