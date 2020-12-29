using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Printers
{
    class Cartridge : Observer
    {
        public int ID { get; set; }
        public int Printer_ID { get; set; }
        public string Name { get; set; }
        public bool IsRGB { get; set; }
        public float Amount { get; set; }

        public Cartridge()
        {

        }

        public Cartridge(int id, int printer_id, string name, bool isRGB, float amount)
        {
            ID = id;
            Printer_ID = printer_id;
            Name = name;
            IsRGB = isRGB;
            Amount = amount;
        }

        private Cartridge(Builder builder)
        {
            Printer_ID = builder.Printer_Id;
            Name = builder.Name;
            IsRGB = builder.IsRGB;
            Amount = builder.Amount;
        }

        public Cartridge(Cartridge cartridge)
            : this(cartridge.ID, cartridge.Printer_ID, cartridge.Name, cartridge.IsRGB, cartridge.Amount)
        {

        }

        public override string ToString()
        {
            return $"Cartridge: ID={ID}, printer_id={Printer_ID}, name=\"{Name}\", isRGB={IsRGB}, amount={Amount}";
        }

        public class Builder
        {
            protected internal string Name;

            internal int Printer_Id = 0;
            internal bool IsRGB = false;
            internal float Amount = 0;

            public Builder(string name)
            {
                Name = name;
            }

            public Builder setRGB(bool isRGB)
            {
                IsRGB = isRGB;
                return this;
            }

            public Builder setAmount(float amount)
            {
                Amount = amount;
                return this;
            }

            public Builder setPrinterID(int printer_id)
            {
                Printer_Id = printer_id;
                return this;
            }

            public Cartridge Build()
            {
                return new Cartridge(this);
            }
        }

        public void Update(int computer_id, int company_id, int printer_id)
        {
            Printer_ID = printer_id;
            Console.WriteLine("Updated " + ToString());
        }

        public Memento SaveState()
        {
            return new Memento(new Cartridge(this));
        }

        public void RestoreState(Memento memento)
        {
            ID = memento.GetState().ID;
            Printer_ID = memento.GetState().Printer_ID;
            Name = memento.GetState().Name;
            IsRGB = memento.GetState().IsRGB;
            Amount = memento.GetState().Amount;
        }
    }
}
