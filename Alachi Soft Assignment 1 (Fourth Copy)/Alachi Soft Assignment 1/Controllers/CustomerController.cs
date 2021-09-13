using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBHandlers;
using DBModels;
using Newtonsoft.Json;

namespace Alachi_Soft_Assignment_1.Controllers
{
    public class CustomerController : Controller
    {
        ServiceReference1.WebService1SoapClient proxy = new ServiceReference1.WebService1SoapClient();
        // GET: Customer
        public ActionResult Index()
        {
            List<Customer> getCustomers = JsonConvert.DeserializeObject<List<Customer>> (proxy.GetCustomers());
            ModelState.Clear();

            return View(getCustomers);
        }

        // GET: Customer/Orders/5
        public ActionResult Orders(String id)
        {
            string orders = proxy.GetOrders(id);
            CustomerOrderProduct productsOrders = JsonConvert.DeserializeObject<CustomerOrderProduct>(orders);
            ModelState.Clear();
            return View(productsOrders); 
        }
     

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(string CustomerId,string CompanyName, string ContactName)
        {
            bool createCustomer = JsonConvert.DeserializeObject <bool>(proxy.CreateCustomer(CustomerId, CompanyName, ContactName));
            
            try
            {
                if (ModelState.IsValid)
                {
                    if (createCustomer)
                    {
                        return RedirectToAction("Index");
                    }
                }
                // TODO: Add insert logic here

                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(string id)
        {
            CustomerDBHandle cdb = new CustomerDBHandle();
            return View(cdb.getCustomers().Find(cs => cs.CustomerId == id));
        }

        //POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(string CustomerId, string CompanyName, string ContactName)
        {
            try
            {
                bool updateCustomer = JsonConvert.DeserializeObject<bool>(proxy.UpdateCustomer(CustomerId,CompanyName,ContactName));
                // TODO: Add update logic here
                if (updateCustomer)
                {
                    return RedirectToAction("Index");
                }
                return (ViewBag.AlertMsg("Error"));
            }
            catch (Exception e)
            {

                return View();
            }
        }

        // Customer/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                bool deleteCustomer = JsonConvert.DeserializeObject<bool>(proxy.DeleteCustomer(id));
           
                if (deleteCustomer)
                {
                    ViewBag.AlertMsg = "Customer Deleted Successfully";
                }

                return RedirectToAction("Index");
            }
            catch(Exception )
            {
                
                return View();
            }
        }

        
        [HttpPost]
        public ActionResult PlaceOrder(int ProductId, string CustomerId)
        {
            try
            {
                bool placeOrder = JsonConvert.DeserializeObject<bool>(proxy.PlaceOrder(ProductId, CustomerId));
          
                if (placeOrder)
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            catch (Exception e)
            {
                return ViewBag.AlertMsg("Error");
            }
            return View();
        }
    }
}
