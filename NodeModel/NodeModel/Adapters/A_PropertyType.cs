using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NodeModel
{
    public class A_PropertyType : A_Model, IPropertyType
    {
        ColumnX ColumnXRef => ItemRef as ColumnX;

        internal A_PropertyType(ColumnX itemRef) : base(itemRef) { }

        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ToolTip { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        string IPropertyType.ValueType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IList<string> ValueTypes => throw new NotImplementedException();
    }
}
