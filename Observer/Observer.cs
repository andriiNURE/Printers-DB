using System;
using System.Collections.Generic;
using System.Text;

namespace Printers
{
    interface Observer
    {
        void Update(int computer_id, int company_id, int printer_id);
    }

    interface Observable
    {
        void RegisterObserver(Observer observer);
        void RemoveObserver(Observer observer);
        void NotifyObservers();
    }
}
