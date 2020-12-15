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
        private List<CartModel> listCart = new List<CartModel>();
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
        [HttpPost]
        public JsonResult Index(string ItemID)
        {
            CartModel cartModel = new CartModel();
            tbItem dbItem = db.tbItems.Single(model => model.It_Id.ToString() == ItemID);
            if (Session["CartCounter"] != null)
            {
                listCart = Session["cartitem"] as List<CartModel>;
            }
            if (listCart.Any(Model => Model.ItemId == ItemID))
            {
                cartModel = listCart.Single(model => model.ItemId == ItemID);
                cartModel.Quantity = cartModel.Quantity + 1;
                cartModel.Total = cartModel.Quantity * cartModel.UnitPrice;
            }
            else
            {
                cartModel.ItemId = ItemID;
                cartModel.Image = dbItem.It_Img;
                cartModel.ItemName = dbItem.It_Name;
                cartModel.Quantity = 1;
                cartModel.Total = (decimal)dbItem.It_Price;
                cartModel.UnitPrice = (decimal)dbItem.It_Price;
                listCart.Add(cartModel);

            }
            Session["CartCounter"] = listCart.Count;
            Session["cartitem"] = listCart;

            return Json(data: new { Success = true, Counter = listCart.Count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Order()
        {
            listCart = Session["cartitem"] as List<CartModel>;

            return View(listCart);
        }
        public ActionResult Delete(string id)
        {
            List<CartModel> cart = (List<CartModel>)Session["cartitem"];
            var del = cart.Find(m => m.ItemId == id);
            cart.Remove(del);
            Session["CartCounter"] = cart.Count;
            Session["cartitem"] = cart;

            return View("Order", cart);
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