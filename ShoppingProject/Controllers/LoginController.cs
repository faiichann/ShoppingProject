using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Dynamic;
using ShoppingProject.Models;

namespace ShoppingProject.Controllers
{
    public class LoginController : Controller
    {
        ShopDBEntities db = new ShopDBEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveData(tbUser user)
        {
            user.U_Isvalid= false;
            db.tbUsers.Add(user);
            db.SaveChanges();
            return Json("Register Successfull", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckValidUser(tbUser user)
        {
            string result = "Fail";
            var DataItem = db.tbUsers.Where(x => x.U_Name == user.U_Name && x.U_Password == user.U_Password).SingleOrDefault();
            if (DataItem != null)
            {
                Session["UserID"] = DataItem.U_Id.ToString();
                Session["UserFName"] = DataItem.U_Fname.ToString();
                Session["UserName"] = DataItem.U_Name.ToString();
                Session["UserEmail"] = DataItem.U_Email.ToString();
                Session["UserPass"] = DataItem.U_Password.ToString();
                Session["UserAddress"] = DataItem.U_Address.ToString();
                Session["UserPhone"] = DataItem.U_Phone.ToString();
                result = "Success";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
        public ActionResult Home()
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
        public ActionResult Contact()
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
        public ActionResult Aboutus()
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
        public ActionResult Profile()
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
        public ActionResult EditProfile()
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
        public ActionResult History()
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
    }
}