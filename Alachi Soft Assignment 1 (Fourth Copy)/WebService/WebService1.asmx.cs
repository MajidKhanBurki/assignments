using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using DBHandlers;
using DBModels;
namespace WebService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        CustomerDBHandle cdb = new CustomerDBHandle();
        OrderDBHandle odb = new OrderDBHandle();
        ProductDBHandle pdb = new ProductDBHandle();
       
        [WebMethod]
        public string GetCustomers()
        {
            return JsonConvert.SerializeObject(cdb.getCustomers());
        }
        [WebMethod]
        public string GetOrders(string customerId)
        {
            var orders = odb.getOrders(customerId);
            var products = pdb.getProductNames();
            CustomerOrderProduct model = new CustomerOrderProduct();
            model.orders__ = orders;
            model.products__ = products;
            return (JsonConvert.SerializeObject(model));
        }

        [WebMethod]
        public string CreateCustomer(string CustomerId, string CompanyName, string ContactName)
        {

            return JsonConvert.SerializeObject(cdb.AddCustomer(CustomerId, CompanyName, ContactName));




        }
        [WebMethod]
        public string UpdateCustomer(string CustomerId, string CompanyName, string ContactName)
        {
            return JsonConvert.SerializeObject(cdb.UpdateCustomer(CustomerId, CompanyName, ContactName));
        }
        [WebMethod]

        public string DeleteCustomer(string id)
        {
            return JsonConvert.SerializeObject(cdb.DeleteCustomer(id));
        }
        [WebMethod]
        public string PlaceOrder(int ProductId, string CustomerId)
        {
            return JsonConvert.SerializeObject(cdb.PlaceOrder(ProductId, CustomerId));
        }
    }
}
