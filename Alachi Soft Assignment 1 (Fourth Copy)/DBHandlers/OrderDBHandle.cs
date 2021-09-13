using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DBModels;

namespace DBHandlers
{
    public class OrderDBHandle
    {
        Connection connection = new Connection();
      

        // Show Record
        public List<Orders> getOrders(String id)
        {
            connection.Connect();
            List<Orders> li = new List<Orders>();
            string getOrderDetails = "SELECT * FROM Orders WHERE CustomerID =@CustomerID";
            try
            {

                SqlCommand cmd = new SqlCommand(getOrderDetails, connection.con);
                cmd.Parameters.AddWithValue("@CustomerID", id);
                connection.con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Orders order = new Orders();
                    order.OrderId = Convert.ToInt32(rdr.GetValue(0).ToString());
                    order.CustomerId = rdr.GetValue(1).ToString();
                    order.EmployeeId = rdr.GetValue(2).ToString();
                    order.OrderDate = Convert.ToDateTime(rdr.GetValue(3).ToString());
                    li.Add(order);


                }
            }
            catch (Exception)
            {
                throw;
            }
            return li;
        }
    }
}
