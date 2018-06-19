using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IceCreamShopCSharp
{
    class Database
    {
  
        public static string GetConnectionString()
        {
            return @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\icecreamdb.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
           //return @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\icecream.mdb;";
        }

        public static IDbConnection Connection()
        {
            return new SqlConnection(Database.GetConnectionString());
            //return new OleDbConnection(Database.GetConnectionString());
        }
    }
}
