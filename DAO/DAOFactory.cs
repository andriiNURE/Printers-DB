using System;
using System.Collections.Generic;
using System.Text;

namespace Printers
{
    interface IDAOFactory
    {
        IPrinterDAO GetPrinterDAO();
        ICompanyDAO GetCompanyDAO();
        IComputerDAO GetComputerDAO();
        ICartridgeDAO GetCartridgeDAO();
    }

    class DAOFactory : IDAOFactory
    {
        public IPrinterDAO GetPrinterDAO()
        {
            return new PrinterDAOImpl();
        }

        public ICompanyDAO GetCompanyDAO()
        {
            return new CompanyDAOImpl();
        }

        public IComputerDAO GetComputerDAO()
        {
            return new ComputerDAOImpl();
        }

        public ICartridgeDAO GetCartridgeDAO()
        {
            return new CartridgeDAOImpl();
        }
    }
}
