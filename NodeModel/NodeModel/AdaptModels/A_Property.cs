using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace NodeModel
{
    public class A_Property : A_Model, IProperty
    {
        ColumnX ColumnXRef => ItemRef as ColumnX;

        internal A_Property(ColumnX itemRef) : base(itemRef) { }

        public string Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string ValueType => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public string ToolTip => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public INode Node => throw new NotImplementedException();

        public bool BoolValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool[] BoolArray { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int IntValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int[] IntArray { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public long LongValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public long[] LongArray { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double DoubleValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double[] DoubleArray { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string StringValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string[] StringArray { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DateTimeValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime[] DateTimeArray { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
