using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    internal class ValueUnknown : ValueEmpty
    {
        internal ValueUnknown()
        {
            _idString = "??????";
            _valueType = ValType.IsUnknown;
        }
    }
}
