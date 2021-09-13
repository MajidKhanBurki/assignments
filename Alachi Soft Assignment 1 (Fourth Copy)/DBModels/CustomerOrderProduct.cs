using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace DBModels
{
   public class CustomerOrderProduct
    {
        public List <Customer> customers__ { get; set; }

        public List <Orders> orders__ { get; set; }

        public List <Products> products__ { get; set; }
        

    }
}
