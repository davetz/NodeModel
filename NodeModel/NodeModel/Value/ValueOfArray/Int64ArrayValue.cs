﻿using System;
using System.Collections.Generic;

namespace NodeModel
{
    internal class Int64ArrayValue : ValueOfArray<Int64>
    {
        internal Int64ArrayValue(IValueStore<Int64[]> store) { _valueStore = store; }
        internal override ValType ValType => ValType.Int64Array;

        internal ValueDictionary<Int64[]> ValueDictionary => _valueStore as ValueDictionary<Int64[]>;
        internal override bool IsSpecific(Item key) => _valueStore.IsSpecific(key);

        #region Required  =====================================================
        internal override bool GetValue(Item key, out string value)
        {
            var b = (GetVal(key, out Int64[] v));
            value = ArrayFormat(v, (i) => ValueFormat(v[i], Format));
            return b;
        }
        internal override bool SetValue(Item key, string value)
        {
            (var ok, Int64[] v) = ArrayParse(value, (s) => Int64Parse(s));
            return ok ? SetVal(key, v) : false;
        }
        #endregion

        #region GetValueAt  ===================================================
        internal override bool GetValueAt(Item key, out bool value, int index)
        {
            var b = GetValAt(key, out Int64 v, index);
            value = (v != 0);
            return b;
        }

        internal override bool GetValueAt(Item key, out int value, int index)
        {
            var b = (GetValAt(key, out Int64 v, index) && !(v < int.MinValue || v > int.MaxValue));
            value = (int)v;
            return b;
        }

        internal override bool GetValueAt(Item key, out Int64 value, int index) => GetValAt(key, out value, index);

        internal override bool GetValueAt(Item key, out double value, int index)
        {
            var b = GetValAt(key, out Int64 v, index);
            value = v;
            return b;
        }

        internal override bool GetValueAt(Item key, out string value, int index)
        {
            var b = GetValAt(key, out Int64 v, index);
            value = ValueFormat(v, Format);
            return b;
        }
        #endregion

        #region GetLength  ====================================================
        internal override bool GetLength(Item key, out int value)
        {
            if (GetVal(key, out Int64[] v))
            {
                value = v.Length;
                return true;
            }
            value = 0;
            return false;
        }
        #endregion

        #region GetValue (array)  =============================================
        internal override bool GetValue(Item key, out bool[] value)
        {
            var b = GetVal(key, out Int64[] v);
            var c = ValueArray(v, out value, (i) => (true, v[i] != 0));
            return b && c;
        }

        internal override bool GetValue(Item key, out int[] value)
        {
            var b = GetVal(key, out Int64[] v);
            var c = ValueArray(v, out value, (i) => (!(v[i] < int.MinValue || v[i] > int.MaxValue), (int)v[i]));
            return b && c;
        }

        internal override bool GetValue(Item key, out Int64[] value) => GetVal(key, out value);

        internal override bool GetValue(Item key, out double[] value)
        {
            var b = GetVal(key, out Int64[] v);
            var c = ValueArray(v, out value, (i) => (true, v[i]));
            return b && c;
        }

        internal override bool GetValue(Item key, out string[] value)
        {
            var b = GetVal(key, out Int64[] v);
            var c = ValueArray(v, out value, (i) => ValueFormat(v[i], Format));
            return b && c;
        }

        #endregion

        #region SetValue (array) ==============================================
        internal override bool SetValue(Item key, bool[] value)
        {
            var c = ValueArray(value, out Int64[] v, (i) => (true, (value[i] ? 1 : 0)));
            var b = SetVal(key, v);
            return b && c;
        }

        internal override bool SetValue(Item key, int[] value)
        {
            var c = ValueArray(value, out Int64[] v, (i) => (true, value[i]));
            var b = SetVal(key, v);
            return b && c;
        }


        internal override bool SetValue(Item key, Int64[] value) => SetVal(key, value);

        internal override bool SetValue(Item key, double[] value)
        {
            var c = ValueArray(value, out Int64[] v, (i) => (!(value[i] < Int64.MinValue || value[i] > Int64.MaxValue), (Int64)value[i]));
            var b = SetVal(key, v);
            return b && c;
        }

        internal override bool SetValue(Item key, string[] value)
        {
            var c = ValueArray(value, out Int64[] v, (i) => Int64Parse(value[i]));
            var b = SetVal(key, v);
            return b && c;
        }
        #endregion
    }
}
