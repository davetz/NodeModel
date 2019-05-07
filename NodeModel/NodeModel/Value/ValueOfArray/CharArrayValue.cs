using System;
using System.Collections.Generic;

namespace NodeModel
{
    internal class CharArrayValue : ValueOfArray<char>
    {
        internal CharArrayValue(IValueStore<char[]> store) { _valueStore = store; }
        internal override ValType ValType => ValType.CharArray;

        internal ValueDictionary<char[]> ValueDictionary => _valueStore as ValueDictionary<char[]>;
        internal override bool IsSpecific(Item key) => _valueStore.IsSpecific(key);

        #region Required  =====================================================
        internal override bool GetValue(Item key, out string value)
        {
            var b = (GetVal(key, out char[] v));
            value = ArrayFormat(v, (i) => v[i].ToString());
            return b;
        }
        internal override bool SetValue(Item key, string value)
        {
            return SetVal(key, value.ToCharArray());
        }
        #endregion

        #region GetValueAt  ===================================================
        internal override bool GetValueAt(Item key, out bool value, int index)
        {
            var b = GetValAt(key, out char v, index);
            value = (v != 0);
            return b;
        }

        internal override bool GetValueAt(Item key, out int value, int index)
        {
            var b = GetValAt(key, out char v, index);
            value = v;
            return b;
        }

        internal override bool GetValueAt(Item key, out Int64 value, int index)
        {
            var b = GetValAt(key, out char v, index);
            value = v;
            return b;
        }

        internal override bool GetValueAt(Item key, out double value, int index)
        {
            var b = GetValAt(key, out char v, index);
            value = v;
            return b;
        }

        internal override bool GetValueAt(Item key, out string value, int index)
        {
            var b = GetValAt(key, out char v, index);
            value = v.ToString();
            return b;
        }
        #endregion

        #region GetLength  ====================================================
        internal override bool GetLength(Item key, out int value)
        {
            if (GetVal(key, out char[] v))
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
            var b = GetVal(key, out char[] v);
            var c = ValueArray(v, out value, (i) => (true, v[i] != 0));
            return b && c;
        }

        internal override bool GetValue(Item key, out int[] value)
        {
            var b = GetVal(key, out char[] v);
            var c = ValueArray(v, out value, (i) => (true, v[i]));
            return b && c;
        }

        internal override bool GetValue(Item key, out Int64[] value)
        {
            var b = GetVal(key, out char[] v);
            var c = ValueArray(v, out value, (i) => (true, v[i]));
            return b && c;
        }

        internal override bool GetValue(Item key, out double[] value)
        {
            var b = GetVal(key, out char[] v);
            var c = ValueArray(v, out value, (i) => (true, v[i]));
            return b && c;
        }

        internal override bool GetValue(Item key, out string[] value)
        {
            var b = GetVal(key, out char[] v);
            var c = ValueArray(v, out value, (i) => v[i].ToString());
            return b && c;
        }

        #endregion

        #region SetValue (array) ==============================================
        internal override bool SetValue(Item key, bool[] value)
        {
            var c = ValueArray(value, out char[] v, (i) => (true, (char)(value[i] ? 1 : 0)));
            var b = SetVal(key, v);
            return b && c;
        }

        internal override bool SetValue(Item key, int[] value)
        {
            var c = ValueArray(value, out char[] v, (i) => (!(value[i] < char.MinValue || value[i] > char.MaxValue), (char)value[i]));
            var b = SetVal(key, v);
            return b && c;
        }

        internal override bool SetValue(Item key, Int64[] value)
        {
            var c = ValueArray(value, out char[] v, (i) => (!(value[i] < char.MinValue || value[i] > char.MaxValue), (char)value[i]));
            var b = SetVal(key, v);
            return b && c;
        }

        internal override bool SetValue(Item key, double[] value)
        {
            var c = ValueArray(value, out char[] v, (i) => (!(value[i] < char.MinValue || value[i] > char.MaxValue), (char)value[i]));
            var b = SetVal(key, v);
            return b && c;
        }
        internal override bool SetValue(Item key, string[] value)
        {
            var c = ValueArray(value, out char[] v, (i) => ((value[i] != null && value[i].Length > 0), (value[i] != null && value[i].Length > 0) ? value[i][0] : default(char)));
            var b = SetVal(key, v);
            return b && c;
        }
        #endregion
    }
}
