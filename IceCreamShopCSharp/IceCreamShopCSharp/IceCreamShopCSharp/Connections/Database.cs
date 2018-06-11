using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.OleDb;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IceCreamShopCSharp
{
    class Database<T> 
    {

        private IDbConnection _conn = new OleDbConnection();
        private IDbCommand _cmd = new OleDbCommand();
        private IDataReader _reader;

        private const string DATABASE_NAME = "icecream.mdb";
        private const string PROVIDER = "Microsoft.Jet.OLEDB.4.0";
        private const string DATA_SOURCE = "|DataDirectory|";

        public string query { get; set; }

        public void Connected()
        {
            try
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }

                _conn.ConnectionString = @"Provider=" + PROVIDER + ";Data Source=" + DATA_SOURCE + @"\" + DATABASE_NAME + ";";
                _conn.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Database Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public T CommandReader()
        {
            Connected();
            _cmd.CommandText = query;
            _cmd.Connection = _conn;

            _reader = _cmd.ExecuteReader();
            return (T)_reader;
        }

        public void CommandExecute()
        {
           
            Connected();
            _cmd.CommandText = query;
            _cmd.Connection = _conn;
            _cmd.ExecuteNonQuery();
          
        }
    }
}
