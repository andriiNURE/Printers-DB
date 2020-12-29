using System;
using System.Collections.Generic;

namespace Printers
{
    class Program
    {
        static void Main(string[] args)
        {
            Proxy proxy = new Proxy("admin", "admin");

            DAOFactory dAOFactory = new DAOFactory();

            Printer printer = new Printer.Builder("anotherOne", 3).computerId(3).Built();

            dAOFactory.GetPrinterDAO().Create(printer, proxy);
        }
    }
}
