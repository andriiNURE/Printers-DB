using System;
using System.Collections.Generic;
using System.Text;

namespace Printers
{
        class Company : Observable
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
            public string Name { get; set; }

            public Company()
            {
            }

            public Company(int id, string name)
            {
                this.id = id;
                Name = name;
            }

            public Company(object[] company)
            {
                this.id = (int)company[0];
                Name = (string)company[1];
            }

            public override string ToString()
            {
                return $"Company: ID={ID}, name=\"{Name}\"";
            }

            public class Builder
            {
                private Company newcompany;

                public Builder()
                {
                    newcompany = new Company();
                }

                public Builder AddName(string name)
                {
                    newcompany.Name = name;
                    return this;
                }

                public Company Build()
                {
                    return newcompany;
                }
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
                foreach (var observer in observers)
                {
                    observer.Update(0, ID, 0);
                }
            }
        }

}
