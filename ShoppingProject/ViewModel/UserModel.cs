using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingProject.ViewModel
{
    public class UserModel
    {
        public int U_Id { get; set; }
        public string U_Name { get; set; }
        public string U_Email { get; set; }
        public string U_Password { get; set; }
        public string U_Address { get; set; }
        public string U_Phone { get; set; }
        public Nullable<bool> U_Isvalid { get; set; }
    }
}