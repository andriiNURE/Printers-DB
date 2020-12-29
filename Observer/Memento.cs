using System;
using System.Collections.Generic;
using System.Text;

namespace Printers
{
    class Memento
    {
        private Cartridge state;

        public Memento(Cartridge state)
        {
            this.state = state;
        }

        public Cartridge GetState()
        {
            return state;
        }
    }

    class Caretaker
    {
        Stack<Memento> mementos =
            new Stack<Memento>();

        public Memento GetMemento()
        {
            return mementos.Pop();
        }

        public void SetMemento(Memento memento)
        {
            mementos.Push(memento);
        }

        public List<Memento> GetStates()
        {
            return new List<Memento>(mementos);
        }
    }
}
