using System;
using System.Collections.Generic;

namespace Printers
{
    interface IPrinterDAO
    {
        void Update(Printer printer, string newName, Proxy proxy);
        void Delete(Printer printer, Proxy proxy);
        Printer Create(string name, int computer_id, int company_id, Proxy proxy);
        Printer Create(Printer printerToCopy, Proxy proxy);
        Printer GetById(int id);
        Printer GetByName(string name);
        List<Printer> GetByComputer(int computer_id);
        List<Printer> GetByCompany(int company_id);
        List<Printer> GetAll();
    }

    interface ICompanyDAO
    {
        void Update(Company company, string newName, Proxy proxy);
        void Delete(Company company, Proxy proxy);
        Company Create(string name, Proxy proxy);
        Company GetByName(string companyname);
        Company GetById(int id);
        List<Company> GetAll();

    }

    interface IComputerDAO
    {
        void Update(Computer computer, string newName, Proxy proxy);
        void Delete(Computer computer, Proxy proxy);
        Computer Create(string name, Proxy proxy);
        Computer GetByName(string name);
        Computer GetById(int id);
        List<Computer> GetAll();
    }

    interface ICartridgeDAO
    {
        Cartridge Update(Cartridge cartridge, string newName, Proxy proxy);
        Cartridge UpdatePrinterID(Cartridge cartridge, int newPrinter, Proxy proxy);
        Cartridge Update(Cartridge cartridge, float newPain, Proxy proxyt);
        void Delete(Cartridge cartridge, Proxy proxy);
        Cartridge Create(int printer_id, string name, bool isRGB, float amount, Proxy proxy);
        Cartridge Create(Cartridge cartridge, Proxy proxy);
        Cartridge GetByName(string name);
        Cartridge GetById(int id);
        List<Cartridge> GetByPrinter(Printer printer);
        List<Cartridge> GetAll();
        Cartridge Restore(Cartridge cartridge, Memento memento, Proxy proxy);
    }
}