using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    public partial class Chef
    {
        #region SetColumnValueType  ===========================================
        internal bool SetColumnValueType(ColumnX col, ValType valType)
        {
            if (col.Value.ValType == valType) return true;

            var newGroup = Value.GetValGroup(valType);
            var preGroup = Value.GetValGroup(col.Value.ValType);

            if (!TableX_ColumnX.TryGetParent(col, out TableX tbl)) return false;

            var N = tbl.Count;

            if (N == 0)
            {
                col.Value = Value.Create(valType);
                return true;
            }

            if ((newGroup & ValGroup.ScalarGroup) != 0 && (preGroup & ValGroup.ScalarGroup) != 0)
            {
                var rows = tbl.Items;
                var value = Value.Create(valType, N);

                switch (newGroup)
                {
                    case ValGroup.Bool:
                        for (int i = 0; i < N; i++)
                        {
                            var key = rows[i];
                            col.Value.GetValue(key, out bool v);
                            if (!value.SetValue(key, v)) return false;
                        }
                        break;
                    case ValGroup.Long:
                        for (int i = 0; i < N; i++)
                        {
                            var key = rows[i];
                            col.Value.GetValue(key, out Int64 v);
                            if (!value.SetValue(key, v)) return false;
                        }
                        break;
                    case ValGroup.String:
                        for (int i = 0; i < N; i++)
                        {
                            var key = rows[i];
                            col.Value.GetValue(key, out string v);
                            if (!value.SetValue(key, v)) return false;
                        }
                        break;
                    case ValGroup.Double:
                        for (int i = 0; i < N; i++)
                        {
                            var key = rows[i];
                            col.Value.GetValue(key, out double v);
                            if (!value.SetValue(key, v)) return false;
                        }
                        break;
                    default:
                        return false;
                }
                col.Value = value;
                return true;
            }
            return false;
        }
        #endregion
    }
}
