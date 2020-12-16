using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ShoppingProject.ViewModel
{
	[DataContract]
	public class DataModel
    {
		public DataModel(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "x")]
		public Nullable<double> X = null;

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "y")]
		public Nullable<double> Y = null;
	}
}