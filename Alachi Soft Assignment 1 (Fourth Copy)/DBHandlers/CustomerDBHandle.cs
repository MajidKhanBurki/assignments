
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
//using System.Web.UI.WebControls;
using System.Runtime.Remoting.Messaging;
using DBModels;
using System.Runtime.InteropServices;

namespace DBHandlers
{



    public class CustomerDBHandle
    {
        Connection connection = new Connection();
       
        // Adding a new Customer //
        public bool AddCustomer(string CustomerId, string CompanyName, string ContactName)
        {
            string createCustomerQuery = "INSERT INTO Customers(CustomerID,CompanyName,ContactName) VALUES (@CustomerID, @CompanyName, @ContactName)";
            connection.Connect();
            SqlCommand cmd = new SqlCommand(createCustomerQuery, connection.con);
            cmd.Parameters.AddWithValue("@CustomerID", CustomerId);
            cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
            cmd.Parameters.AddWithValue("@ContactName", ContactName);
            connection.con.Open();
            int i = cmd.ExecuteNonQuery();
            connection.con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        
        // View Customers //
        public List<Customer> getCustomers()
        {
            connection.Connect();
            List<Customer> li = new List<Customer>();
            string GetCustomerDetails = "SELECT CustomerID,CompanyName,ContactName FROM Customers";

            try
            {
                SqlCommand cmd = new SqlCommand(GetCustomerDetails, connection.con);
                connection.con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Customer cs = new Customer();
                    cs.CustomerId = rdr.GetValue(0).ToString();
                    cs.CompanyName = rdr.GetValue(1).ToString();
                    cs.ContactName = rdr.GetValue(2).ToString();
                    li.Add(cs);
                }
                connection.con.Close();
            }

            catch (Exception)
            {
                throw;
            }
            return li;
        }
        // Update Customer //
        public bool UpdateCustomer(string CustomerId, string CompanyName, string ContactName)
        {
            connection.Connect();
            string UpdateCustomerQuery = "UPDATE Customers SET CompanyName = @CompanyName, ContactName = @ContactName WHERE CustomerID = @CustomerID ";
            SqlCommand cmd = new SqlCommand(UpdateCustomerQuery, connection.con);
            cmd.Parameters.AddWithValue("@CustomerID", CustomerId);
            cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
            cmd.Parameters.AddWithValue("@ContactName", ContactName);
            connection.con.Open();
            int i = cmd.ExecuteNonQuery();
            connection.con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
        // Delete Customer //
        public bool DeleteCustomer(string id)
        {
            connection.Connect();
            string deleteOrderDetails = "DELETE FROM [Order Details] WHERE OrderID IN( SELECT OrderID FROM Orders WHERE CustomerID = @CustomerID)";
            string deleteOrders = "DELETE FROM Orders WHERE CustomerID = @CustomerID";
            string customerDeleteQuery = "DELETE FROM CUSTOMERS WHERE CustomerID =@CustomerID";
            SqlCommand cmd1 = new SqlCommand(deleteOrderDetails, connection.con);
            SqlCommand cmd2 = new SqlCommand(deleteOrders, connection.con);
            SqlCommand cmd3 = new SqlCommand(customerDeleteQuery, connection.con);
            cmd1.Parameters.AddWithValue("@CustomerID", id);
            cmd2.Parameters.AddWithValue("@CustomerID", id);
            cmd3.Parameters.AddWithValue("@CustomerID", id);
            connection.con.Open();
            int i = cmd1.ExecuteNonQuery();
            int j = cmd2.ExecuteNonQuery();
            int k = cmd3.ExecuteNonQuery();
            connection.con.Close();

            if (i >= 1 && j >= 1 && k >= 1)
                return true;
            else
                return false;
        }

        public bool PlaceOrder(int productId, string customerId)
        {
            DateTime currentDate = DateTime.Now;
            int OrderId = 0;
            connection.Connect();
            string addProduct = "INSERT INTO [Order Details] VALUES (@OrderID, @ProductID, @UnitPrice, @Quantity, @Discount)";
            string addCustomerID = "INSERT INTO Orders(CustomerID,EmployeeID,OrderDate) VALUES (@CustomerID,@EmployeeID,@OrderDate)";
            string OrderID = "SELECT max(OrderID) from Orders";
            SqlCommand cmd1 = new SqlCommand(addCustomerID, connection.con);
            cmd1.Parameters.AddWithValue("@CustomerID", customerId);
            cmd1.Parameters.AddWithValue("@EmployeeID", 4);
            cmd1.Parameters.AddWithValue("@OrderDate", currentDate);
            SqlCommand cmd2 = new SqlCommand(OrderID, connection.con);
            connection.con.Open();
            int i = cmd1.ExecuteNonQuery();
            //OrderId = (int)cmd2.ExecuteNonQuery();
            SqlDataReader rdr = cmd2.ExecuteReader();
            while (rdr.Read())
            {
                OrderId = Convert.ToInt32(rdr.GetValue(0).ToString());
            }
           connection.con.Close();
            SqlCommand cmd3 = new SqlCommand(addProduct, connection.con);
            cmd3.Parameters.AddWithValue("@OrderID", OrderId);
            cmd3.Parameters.AddWithValue("@ProductID", productId);
            cmd3.Parameters.AddWithValue("@UnitPrice", 23.33);
            cmd3.Parameters.AddWithValue("@Quantity", 2);
            cmd3.Parameters.AddWithValue("@Discount", 0);

            connection.con.Open();
            int j = cmd3.ExecuteNonQuery();
            connection.con.Close();

            if (i >= 1 && j >= 1)
            {
                return true;
            }
            else
                return false;


        }

    }
}
