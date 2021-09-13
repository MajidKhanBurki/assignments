using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModels;
using System.Configuration;
using System.Data.SqlClient;

namespace DBHandlers
{
    public class ProductDBHandle
    {
        Connection connection = new Connection();

        public List<Products> getProductNames()
        {
            connection.Connect();
            List<Products> li = new List<Products>();
            string getProductNamesQuery = "SELECT ProductID, ProductName FROM Products";

            try
            {
                SqlCommand cmd = new SqlCommand(getProductNamesQuery, connection.con);
                connection.con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Products product = new Products();
                    product.ProductId =Convert.ToInt32( rdr.GetValue(0).ToString());
                    product.ProductName = rdr.GetValue(1).ToString();
                    li.Add(product);
                }
                connection.con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return li;
        }
    }
}
