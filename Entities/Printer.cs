using System;
using System.Collections.Generic;

namespace Printers
{
    class Printer : Observable, Observer
    {
        private List<Observer> observers =
            new List<Observer>();

        private int id;
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                NotifyObservers();
            } 
        }
        public int Computer_ID { get; set; }
        public int Company_ID { get; set; }
        public string Name { get; set; }

        public Printer(int id, string name, int computer_id, int company_id)
        {
            this.id = id;
            Name = name;
            Computer_ID = computer_id;
            Company_ID = company_id;
        }

        public override string ToString()
        {
            return $"Printer: ID={ID}, name=\"{Name}\", computer_id={Computer_ID}, company_id={Company_ID}";
        }

        public class Builder
        {
            protected internal string Name;
            protected internal int Company_id;

            internal int Computer_id = 1;

            public Builder(string name, int company_id)
            {
                Name = name;
                Company_id = company_id;
            }

            public Builder computerId(int computer_id)
            {
                Computer_id = computer_id;
                return this;
            }

            public Printer Built()
            {
                return new Printer(this);
            }
        }

        private Printer(Builder builder)
        {
            Name = builder.Name;
            Computer_ID = builder.Computer_id;
            Company_ID = builder.Company_id;
        }

        public void RegisterObserver(Observer observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(Observer observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach(var observer in observers)
            {
                observer.Update(0,0,ID);
            }
        }

        public void Update(int computer_id, int company_id, int printer_id)
        {
            if (Convert.ToBoolean(computer_id))
                Computer_ID = computer_id;

            if (Convert.ToBoolean(company_id))
                Company_ID = company_id;

            Console.WriteLine("Updated " + ToString());
        }
    }
}
