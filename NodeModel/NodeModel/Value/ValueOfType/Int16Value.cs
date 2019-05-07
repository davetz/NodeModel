﻿using System;
using System.Collections.Generic;

namespace NodeModel
{
    internal class Int16Value : ValueOfType<short>
    {
        internal Int16Value(IValueStore<short> store) { _valueStore = store; }
        internal override ValType ValType => ValType.Int16;

        internal ValueDictionary<short> ValueDictionary => _valueStore as ValueDictionary<short>;
        internal override bool IsSpecific(Item key) => _valueStore.IsSpecific(key);

        #region GetValue  =====================================================
        internal override bool GetValue(Item key, out bool value)
        {
            var b = GetVal(key, out short v);
            value = (v != 0);
            return b;
        }

        internal override bool GetValue(Item key, out int value)
        {
            var b = GetVal(key, out short v);
            value = v;
            return b;
        }

        internal override bool GetValue(Item key, out Int64 value)
        {
            var b = GetVal(key, out short v);
            value = v;
            return b;
        }

        internal override bool GetValue(Item key, out double value)
        {
            var b = GetVal(key, out short v);
            value = v;
            return b;
        }

        internal override bool GetValue(Item key, out string value)
        {
            var b = GetVal(key, out short v);
            value = ValueFormat(v, Format);
            return b;
        }
        #endregion

        #region PseudoArrayValue  =============================================
        internal override bool GetValue(Item key, out bool[] value)
        {
            var b = GetValue(key, out bool v);
            value = new bool[] { v };
            return b;
        }

        internal override bool GetValue(Item key, out int[] value)
        {
            var b = GetValue(key, out int v);
            value = new int[] { v };
            return b;
        }

        internal override bool GetValue(Item key, out Int64[] value)
        {
            var b = GetValue(key, out Int64 v);
            value = new Int64[] { v };
            return b;
        }

        internal override bool GetValue(Item key, out double[] value)
        {
            var b = GetValue(key, out double v);
            value = new double[] { v };
            return b;
        }

        internal override bool GetValue(Item key, out string[] value)
        {
            var b = GetValue(key, out string v);
            value = new string[] { v };
            return b;
        }
        internal override bool GetValue(Item key, out DateTime[] value)
        {
            var b = GetValue(key, out DateTime v);
            value = new DateTime[] { v };
            return b;
        }
        #endregion

        #region SetValue ======================================================
        internal override bool SetValue(Item key, bool value) => SetVal(key, (short)(value ? 1 : 0));

        internal override bool SetValue(Item key, int value) => (value < short.MinValue || value > short.MaxValue) ? false : SetVal(key, (short)value);

        internal override bool SetValue(Item key, Int64 value) => (value < short.MinValue || value > short.MaxValue) ? false : SetVal(key, (short)value);

        internal override bool SetValue(Item key, double value) => (value < short.MinValue || value > short.MaxValue) ? false : SetVal(key, (short)value);

        internal override bool SetValue(Item key, string value)
        {
            var (ok, val) = Int16Parse(value);
            return (ok) ? SetVal(key, val) : false;
        }
        #endregion
    }
}
