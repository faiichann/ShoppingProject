using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingProject.ViewModel
{
    public class ItemModel
    {
        public Guid It_Id { get; set; }
        public Nullable<int> Type_Id { get; set; }
        public string It_Code { get; set; }
        public string It_Name { get; set; }
        public string It_Des { get; set; }
        public Nullable<decimal> It_Price { get; set; }
        public HttpPostedFileBase It_Img { get; set; }
        public Nullable<int> It_Unit { get; set; }
        public Nullable<bool> It_Delete { get; set; }
        public IEnumerable<SelectListItem> TypeListItem { get; set; }
    }
}