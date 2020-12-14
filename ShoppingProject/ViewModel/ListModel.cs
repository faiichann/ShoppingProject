using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingProject.ViewModel
{
    public class ListModel
    {
        public Guid Li_Id { get; set; }
        public string Type { get; set; }
        public string li_Code { get; set; }
        public string Li_Name { get; set; }
        public string Li_Des { get; set; }
        public string Li_Img { get; set; }
        public decimal Li_Price { get; set; }
        public decimal Li_Unit { get; set; }

    }
}