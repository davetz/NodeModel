using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    public abstract class Store : Item
    {
        internal abstract void Add(Item item);
        internal abstract void Move(Item item, int index);
        internal abstract void Insert(Item item, int index);
        public abstract void Remove(Item item);
        internal abstract void RemoveAll();
        internal abstract int IndexOf(Item item);
        internal abstract List<Item> GetItems();
        internal abstract int Count { get; }
    }
}
