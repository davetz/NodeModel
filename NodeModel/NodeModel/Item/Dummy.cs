using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    public class Dummy : Item
    {
        internal readonly Guid Guid;
        internal Dummy(Chef owner)
        {
            Owner = owner;
            Trait = Trait.Dummy;
            Guid = new Guid("BB4B121E-9BE4-441B-AEBB-7136889F0143");
        }
    }
}
