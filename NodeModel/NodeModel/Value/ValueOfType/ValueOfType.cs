﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    internal abstract class ValueOfType<T> : Value
    {
        protected IValueStore<T> _valueStore;

        internal override int Count => _valueStore.Count;
        internal override void Clear() => _valueStore.Clear();
        internal override void Remove(Item key) => _valueStore.Remove(key);
        protected bool GetVal(Item key, out T val) => _valueStore.GetVal(key, out val);
        protected bool SetVal(Item key, T value) => _valueStore.SetVal(key, value);
    }
}
