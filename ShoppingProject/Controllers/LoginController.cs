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
    public class LoginController : Controller
    {
        ShopDBEntities2 db = new ShopDBEntities2();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveData(tbUser user)
        {
            user.U_Isvalid = false;
            db.tbUsers.Add(user);
            db.SaveChanges();

            return Json(" Register Successfull" , JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckValidUser(tbUser user)
        {
            string result = "Fail";

            var UserLogin = db.tbUsers.Where(x => x.U_Name == user.U_Name && x.U_Password == user.U_Password).SingleOrDefault();
            if (UserLogin != null)
            {
                Session["UserID"] = UserLogin.U_Id.ToString();
                Session["UserName"] = UserLogin.U_Name.ToString();
                Session["UserEmail"] = UserLogin.U_Email.ToString();
                Session["UserAddress"] = UserLogin.U_Address.ToString();
                Session["UserPhone"] = UserLogin.U_Phone.ToString();
                result = "Success";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login(tbUser user)
        {
            if (Session["UserID"] != null)
            {
                if (user.U_Isvalid != true)
                {
                    return View();
                }
                else
                {
                    return View("Admin");
                }
                
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Admin()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
        //public ActionResult Data()
        //{

        //    if (Session["UserID"] != null)
        //    {
        //        List<DataPoint> dataPoints = new List<DataPoint>{
        //        new DataPoint(10, 22),
        //        new DataPoint(20, 36),
        //        new DataPoint(30, 42),
        //        new DataPoint(40, 51),
        //        new DataPoint(50, 46),
        //    };

        //        ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}


    }
}