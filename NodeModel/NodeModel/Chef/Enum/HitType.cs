using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    [Flags]
    internal enum HitType
    {
        None = 0,
        Pin = 0x01,
        Node = 0x02,
        Region = 0x04,
    }
}
