using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KitchanismoDInjector;
using MiddleLayer;
using DataAccessLayer;

namespace IceCreamShopCSharp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RegisterDepedency();
            Application.Run(new MainForm());
        }
        
        static void RegisterDepedency()
        {
            Container.Register<IProduct, Product>();
            Container.Register<IProductService, ProductService>();
            Container.Register<ISales, Sales>();
            Container.Register<ISalesService, SalesService>();
        }
    }
}
