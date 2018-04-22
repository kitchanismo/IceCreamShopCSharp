﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;    
using kitchanismo;
using System.Windows.Forms;

namespace IceCreamShopCSharp
{
    class Products : Connection
    {

        public int productQuantity { get; set; }
        public string productCode { get; set; }
        public string productCategory { get; set; }
        public string productName { get; set; }
        public double productPrice { get; set; }
        public int productStock { get; set; }
        public DateTime productPurchased { get; set; }

        protected OleDbDataReader readProducts(ProductAction product)
        {  
            switch (product)
            {
                case ProductAction.Load:
                    query = loadQuery();
                    break;
                case ProductAction.Search:
                    query = searchQuery();
                    break;
                case ProductAction.Insert:
                    query = insertQuery();
                    break;
            }
            return CommandReader(query);
        }

        private string searchQuery()
        {
            var key = "'%" + productName.ToLower() + "%'";
            return "select * from tblproducts where productName like " + key + " order by id";
        }

        private string loadQuery()
        {
            return "select * from tblproducts order by id";
        }

        private string insertQuery()
        {
            var _query = "insert into tblproducts (productCode,productCategory,productName,productPrice,productStock,datePurchased)";
            var _values = " values ('" + productCode + "','" + productCategory + "','" + productName + "'," + productPrice + "," + productStock + "," + productPurchased + ")";
            return _query + _values;
        }

        public int getStock(string _prodCode)
        {
           query = "select productStock from tblproducts where productCode = '" + _prodCode +"'";
           var reader = CommandReader(query);
            reader.Read();
            if (reader.HasRows)
            {
                return  int.Parse(reader[0].ToString());
            }
            else
            {
                return 0;
            }
        }
 
    }
    enum ProductAction
    {
        Load,
        Search,
        Insert
    }  
}