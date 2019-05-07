using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace NodeModel
{
    public class PropertyModel : IProperty
    {
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

        #region INotifyPropertyChanged  =======================================
        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
