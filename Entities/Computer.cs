using System;
using System.Collections.Generic;
using System.Text;

namespace Printers
{
    class Computer : Observable
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

        public Computer()
        {

        }

        public Computer(object[] computer)
        {
            this.id = (int)computer[0];
            Name = (string)computer[1];
        }

        public Computer(int id, string name)
        {
            this.id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"Computer: ID={ID}, name=\"{Name}\"";
        }

        public class Builder
        {
            private Computer computer;

            public Builder()
            {
                computer = new Computer();
            }

            public Builder AddName(string name)
            {
                computer.Name = name;
                return this;
            }

            public Computer Build()
            {
                return computer;
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
                observer.Update(ID, 0, 0);
            }
        }
    }
}
