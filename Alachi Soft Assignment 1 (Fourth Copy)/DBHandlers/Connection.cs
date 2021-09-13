using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace DBHandlers
{
    class Connection
    {
        public SqlConnection con;
        public void Connect()
        {
            string constring = ConfigurationManager.ConnectionStrings["NorthwindQA3"].ToString();
             con = new SqlConnection(constring);
            
        }
    }
}
