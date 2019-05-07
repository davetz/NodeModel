using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace NodeModel
{
    public class Model_X
    {
        protected Item ItemRef;
        protected byte ModelDelta = 222;
        protected Chef ChefRef => ItemRef.GetChef();


        internal Model_X(Item itemRef) { ItemRef = itemRef; }

        #region INotifyPropertyChanged  =======================================
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
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
