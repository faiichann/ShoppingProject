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
            if (Session["UserID"] != null)
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
            else
            {
                return RedirectToAction("Index","Login");
            }
           
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
                int OrderId = 0;
                string OrderNo;

                listCart = Session["cartitem"] as List<CartModel>;
                tbOrder order = new tbOrder()
                {
                    OrderDate = DateTime.Now,
                    OrderNo = string.Format("{0:ddmmyyyyHHmmsss}", DateTime.Now)

                };
                db.tbOrders.Add(order);
                db.SaveChanges();
                OrderId = order.OrderId;
                OrderNo = order.OrderNo.ToString();
                

                foreach (var item in listCart)
                {
                tbBill bill = new tbBill();
                        bill.Bill_Total = item.Total;
                        bill.ItemName = item.ItemName;
                        bill.OrderId = OrderId;
                        bill.Bill_Qty = item.Quantity;
                        bill.Bill_Price = item.UnitPrice;
                        bill.Bill_Date = order.OrderDate;
                        bill.UserName = (string)Session["UserName"];

                        db.tbBills.Add(bill);
                        db.SaveChanges();
                }
                Session["cartitem"] = null;
                Session["CartCounter"] = null;
                Session["OrderId"] = order.OrderId.ToString();
                Session["OrderNo"] = order.OrderNo.ToString();
                Session["OrderDate"] = order.OrderDate.ToString();
           

            IEnumerable<BillModel> listbill = (from b in db.tbBills
                                                join o in db.tbOrders
                                                on b.OrderId equals o.OrderId
                                                select new BillModel()
                                                {
                                                    OrderNo = o.OrderNo,
                                                    ItemName = b.ItemName,  
                                                    Bill_Qty = b.Bill_Qty,
                                                    Bill_Price = b.Bill_Price,
                                                    Bill_Total = b.Bill_Total,
                                                    OrderId = o.OrderId,
                                                }).ToList();

            return View(listbill);
        }
        public ActionResult Search(string search)
        {
            return View(db.tbItems.Where(x => x.It_Name.Contains(search) || search == null).ToList());
        }
        public ActionResult Payment()
        {
            return View();
        }
    }
}