using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace IceCreamShopCSharp
{
    class Connection
    {
        protected OleDbConnection connection = new OleDbConnection();
        protected OleDbCommand command = new OleDbCommand();
        protected OleDbDataReader reader;
        protected string query { get; set; }

        private string database = "icecream.mdb";

        public void Connected()
        {

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\" + database + ";";
            connection.Open();

        }

        public OleDbDataReader CommandReader(string sql)
        {
            Connected();
            command.CommandText = sql;
            command.Connection = connection;
            reader = command.ExecuteReader();
            return reader;
        }
    }
}
