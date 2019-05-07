using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    internal class ValueInvalid : ValueEmpty
    {
        internal ValueInvalid()
        {
            _idString = "######";
            _valueType = ValType.IsInvalid;
        }
    }
}
