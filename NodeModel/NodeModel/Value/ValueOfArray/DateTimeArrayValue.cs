﻿using System;
using System.Collections.Generic;

namespace NodeModel
{
    internal class DateTimeArrayValue : ValueOfArray<DateTime>
    {
        internal DateTimeArrayValue(IValueStore<DateTime[]> store) { _valueStore = store; }
        internal override ValType ValType => ValType.DateTimeArray;

        internal ValueDictionary<DateTime[]> ValueDictionary => _valueStore as ValueDictionary<DateTime[]>;
        internal override bool IsSpecific(Item key) => _valueStore.IsSpecific(key);

        #region Required  =====================================================
        internal override bool GetValue(Item key, out string value)
        {
            var b = (GetVal(key, out DateTime[] v));
            value = ArrayFormat(v, (i) => ValueFormat(v[i], Format));
            return b;
        }
        internal override bool SetValue(Item key, string value)
        {
            (var ok, DateTime[] v) = ArrayParse(value, (s) => DateTimeParse(s));
            return ok ? SetVal(key, v) : false;
        }
        #endregion

        #region GetValueAt  ===================================================
        internal override bool GetValueAt(Item key, out DateTime value, int index) => GetValAt(key, out value, index);

        internal override bool GetValueAt(Item key, out string value, int index)
        {
            var b = GetValAt(key, out DateTime v, index);
            value = ValueFormat(v, Format);
            return b;
        }
        #endregion

        #region GetLength  ====================================================
        internal override bool GetLength(Item key, out int value)
        {
            if (GetVal(key, out DateTime[] v))
            {
                value = v.Length;
                return true;
            }
            value = 0;
            return false;
        }
        #endregion

        #region GetValue (array)  =============================================
        internal override bool GetValue(Item key, out DateTime[] value) => GetVal(key, out value);

        internal override bool GetValue(Item key, out string[] value)
        {
            var b = GetVal(key, out DateTime[] v);
            var c = ValueArray(v, out value, (i) => ValueFormat(v[i], Format));
            return b && c;
        }

        #endregion

        #region SetValue (array) ==============================================
        internal override bool SetValue(Item key, DateTime[] value) => SetVal(key, value);

        internal override bool SetValue(Item key, string[] value)
        {
            var c = ValueArray(value, out DateTime[] v, (i) => DateTimeParse(value[i]));
            var b = SetVal(key, v);
            return b && c;
        }
        #endregion
    }
}
