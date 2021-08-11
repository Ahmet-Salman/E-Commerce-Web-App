using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVC_Without_EF.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}