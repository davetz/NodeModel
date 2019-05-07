using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NodeModel
{
    public class PropertyTypeModel : IPropertyType
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ToolTip { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        string IPropertyType.ValueType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IList<string> ValueTypes => throw new NotImplementedException();

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
