using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

using System.Windows.Forms;

namespace IceCreamShopCSharp
{
    class Database
    {
        private OleDbConnection conn = new OleDbConnection();
        private OleDbCommand command = new OleDbCommand();
        private OleDbDataReader reader;

        public string query { get; set; }

        private const string DATABASE_NAME = "icecream.mdb";
        private const string PROVIDER = "Microsoft.Jet.OLEDB.4.0";
        private const string DATA_SOURCE = "|DataDirectory|";

        public void Connected()
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                conn.ConnectionString = @"Provider=" + PROVIDER + ";Data Source=" + DATA_SOURCE + @"\" + DATABASE_NAME + ";";
                conn.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Database Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public OleDbDataReader CommandReader()
        {
            Connected();
            command.CommandText = query;
            command.Connection = conn;
            reader = command.ExecuteReader();
            return reader;
        }

        public void CommandExecute()
        {
            Connected();
            command.CommandText = query;
            command.Connection = conn;
            command.ExecuteNonQuery();
        
        }
    }
}
