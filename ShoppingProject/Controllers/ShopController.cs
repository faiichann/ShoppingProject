using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingProject.Models;
using ShoppingProject.ViewModel;
using System.IO;

namespace ShoppingProject.Controllers
{

    public class ShopController : Controller
    {
        ShopDBEntities db = new ShopDBEntities();
        // GET: Shop
        public ActionResult Index()
        {
            IEnumerable<ListModel> listStore = (from e in db.tbItems
                                                join s in db.tbTypes
                                                on e.Type_Id equals s.Type_Id
                                                select new ListModel()
                                                {
                                                    Li_Id = e.It_Id,
                                                    Li_Img = e.It_Img,
                                                    Type = s.Type_Name,
                                                    li_Code = e.It_Code,
                                                    Li_Name = e.It_Name,
                                                    Li_Des = e.It_Des,
                                                    Li_Price = (decimal)e.It_Price,
                                                    Li_Unit = (int)e.It_Unit
                                                }).ToList();

            return View(listStore);
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