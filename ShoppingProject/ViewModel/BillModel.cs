using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingProject.ViewModel
{
    public class BillModel
    {
        public int Bill_Id { get; set; }
        public Nullable<int> OrderId { get; set; }
        public string ItemName { get; set; }
        public Nullable<decimal> Bill_Qty { get; set; }
        public decimal Bill_Price { get; set; }
        public Nullable<decimal> Bill_Total { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> Bill_Date { get; set; }
        public string OrderNo { get; set; }
    }
}