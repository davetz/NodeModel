using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    public enum Pairing : byte
    {
        OneToOne = 0,
        OneToMany = 1,
        ManyToMany = 2,
    }
}
