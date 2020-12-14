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
    public class AdminController : Controller
    {
        ShopDBEntities db = new ShopDBEntities();
        // GET: Admin

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            ItemModel tbItem = new ItemModel();
            tbItem.TypeListItem = (from e in db.tbTypes
                                   select new SelectListItem()
                                   {
                                       Text = e.Type_Name,
                                       Value = e.Type_Id.ToString(),
                                       Selected = true
                                   });
            ViewBag.Type = tbItem.TypeListItem;
            return View(tbItem);
        }
        [HttpPost]
        public JsonResult Add(ItemModel item)
        {
            string NewImage = Guid.NewGuid() + Path.GetExtension(item.It_Img.FileName);
            item.It_Img.SaveAs(filename: Server.MapPath("~/Imgdb/Item/" + NewImage));

            tbItem dbItem = new tbItem();

            dbItem.It_Id = Guid.NewGuid();
            dbItem.It_Img = "~/Imgdb/Item/" + NewImage;
            dbItem.Type_Id = item.Type_Id;
            dbItem.It_Code = item.It_Code;
            dbItem.It_Name = item.It_Name;
            dbItem.It_Des = item.It_Des;
            dbItem.It_Price = item.It_Price;
            dbItem.It_Unit = item.It_Unit;

            db.tbItems.Add(dbItem);
            db.SaveChangesAsync();

            return Json(data: new { Success = true, Message = " Add Successfully" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edititem(int? id)
        {
            return View();
        }
        public ActionResult Deleteitem()
        {
            return View();
        }
    }
}