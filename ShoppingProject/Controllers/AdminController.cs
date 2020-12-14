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
        public ActionResult Edititem()
        {
            return View();
        }
        public ActionResult Deleteitem()
        {
            return View();
        }
    }
}