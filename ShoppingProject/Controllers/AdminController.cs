using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingProject.Models;
using ShoppingProject.ViewModel;
using System.IO;
using Newtonsoft.Json;

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
            Session["Product"] = listStore;
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
        public ActionResult Edit(Guid id)
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
            Session["Product"] = listStore;
            var DataItem = db.tbItems.Where(x => x.It_Id == id ).SingleOrDefault();
            var store = listStore.Where(s => s.Li_Id == id).SingleOrDefault();
            if(store != null)
            {
                Session["Id"] = store.Li_Id.ToString();
                Session["Img"] = store.Li_Img.ToString();
                Session["Type"] = store.Type.ToString();
                Session["Code"] = store.li_Code.ToString();
                Session["Name"] = store.Li_Name.ToString();
                Session["Des"] = store.Li_Des.ToString();
                Session["Price"] = store.Li_Price.ToString();
                Session["Unit"] = store.Li_Unit.ToString();
            }
            return View(listStore);

        }
        public ActionResult Data()
        {
            List<DataModel> dataPoints = new List<DataModel>{
                new  DataModel(10, 22),
                new  DataModel(20, 36),
                new  DataModel(30, 42),
                new  DataModel(40, 51),
                new  DataModel(50, 46),
            };

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            List<ListModel> cart = (List<ListModel>)Session["product"];
            var del = cart.Find(m => m.Li_Id == id);
            cart.Remove(del);
            Session["product"] = cart;
            db.SaveChanges();

            return View("Index", cart);
            //if ( id !=null)
            //{
            //    var del = (from c in db.tbItems
            //               where c.It_Id == id
            //               select c).FirstOrDefault();
            //    List<ListModel> cart = (List<ListModel>)Session["Product"];
            //    cart.Find(m => m.Li_Id == id);
            //    cart.Remove(del);
            //    Session["Product"] = cart;
            //    db.tbItems.Remove(del);
            //    db.SaveChanges();
                
            //}
            //return View("Index");
        }
        public ActionResult Search(string search)
        {
            return View(db.tbItems.Where(x => x.It_Name.Contains(search) || search == null).ToList());
        }
    }
}