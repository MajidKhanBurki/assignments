using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DBModels
{
    public class Products
    {
        [Display(Name = "ProductID")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product Name is Required ")]
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public float UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public int ReOrderLevel { get; set; }
        public byte Discontinued { get; set; }
    }
}
