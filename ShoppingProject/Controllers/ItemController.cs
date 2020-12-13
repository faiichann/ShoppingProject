using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingProject.Models;
using ShoppingProject.ViewModel;
using System.IO;
using System.Dynamic;
using Newtonsoft.Json;

namespace ShoppingProject.Controllers
{
    public class ItemController : Controller
    {
        ShopDBEntities2 db = new ShopDBEntities2();
        // GET: Item
        public ActionResult Index()
        {
            return View();
        }
    }
}