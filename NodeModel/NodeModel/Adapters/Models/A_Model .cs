﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace NodeModel
{
    public class A_Model
    {
        protected Item ItemRef;
        protected byte ModelDelta = 222;
        protected Chef ChefRef => ItemRef.GetChef();


        internal A_Model(Item itemRef) { ItemRef = itemRef; }

        protected void SetItemRef(Item item) => ItemRef = item; 

        #region INotifyPropertyChanged  =======================================
        public event PropertyChangedEventHandler PropertyChanged;

        protected void PropertyChange(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }
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
