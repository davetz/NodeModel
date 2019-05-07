using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    public partial class Chef
    {
        static internal string BlankName = "???"; // indicates blank or missing name
        static internal string InvalidItem = "######"; // indicates invalid reference 

        static internal ValueUnknown ValuesUnknown = new ValueUnknown();
        static internal ValueInvalid ValuesInvalid = new ValueInvalid();

        private PropertyOf<TableX, string> _tableXNameProperty;
        private PropertyOf<TableX, string> _tableXSummaryProperty;

        private PropertyOf<ColumnX, string> _columnXNameProperty;
        private PropertyOf<ColumnX, string> _columnXSummaryProperty;
        private PropertyOf<ColumnX, string> _columnXTypeOfProperty;
        private PropertyOf<ColumnX, bool> _columnXIsChoiceProperty;

        private PropertyOf<RelationX, string> _relationXNameProperty;
        private PropertyOf<RelationX, string> _relationXSummaryProperty;
        private PropertyOf<RelationX, string> _relationXPairingProperty;
        private PropertyOf<RelationX, bool> _relationXIsRequiredProperty;

        private void InitializeProperties()
        {
            var props = new List<Property>();

            #region TableX  ===================================================
            props.Clear();
            {
                var p = _tableXNameProperty = new PropertyOf<TableX, string>(PropertyStore, Trait.TableName_P);
                p.GetValFunc = (item) => p.Cast(item).Name;
                p.SetValFunc = (item, value) => { p.Cast(item).Name = value; return true; };
                p.Value = new StringValue(p);
                props.Add(p);
            }
            {
                var p = _tableXSummaryProperty = new PropertyOf<TableX, string>(PropertyStore, Trait.TableSummary_P);
                p.GetValFunc = (item) => p.Cast(item).Summary;
                p.SetValFunc = (item, value) => { p.Cast(item).Summary = value; return true; };
                p.Value = new StringValue(p);
                props.Add(p);
            }
            Store_Property.SetLink(TableXStore, props);
            #endregion

            #region ColumnX  ==================================================
            props.Clear();
            {
                var p = _columnXNameProperty = new PropertyOf<ColumnX, string>(PropertyStore, Trait.ColumnName_P);
                p.GetValFunc = (item) => p.Cast(item).Name;
                p.SetValFunc = (item, value) => { p.Cast(item).Name = value; return true; };
                p.Value = new StringValue(p);
                props.Add(p);
            }
            {
                var p = _columnXSummaryProperty = new PropertyOf<ColumnX, string>(PropertyStore, Trait.ColumnSummary_P);
                p.GetValFunc = (item) => p.Cast(item).Summary;
                p.SetValFunc = (item, value) => { p.Cast(item).Summary = value; return true; };
                p.Value = new StringValue(p);
                props.Add(p);
            }
            {
                var p = _columnXTypeOfProperty = new PropertyOf<ColumnX, string>(PropertyStore, Trait.ColumnValueType_P, typeof(ValType));
                p.GetValFunc = (item) => p.Cast(item).Value.ValType.ToString();
                p.SetValFunc = (item, value) => SetColumnValueType(p.Cast(item), Enum.TryParse<ValType>(value , out ValType val) ? val : ValType.String);
                p.Value = new StringValue(p);
                props.Add(p);
            }
            {
                var p = _columnXIsChoiceProperty = new PropertyOf<ColumnX, bool>(PropertyStore, Trait.ColumnIsChoice_P);
                p.GetValFunc = (item) => p.Cast(item).IsChoice;
                p.SetValFunc = (item, value) => p.Cast(item).IsChoice = value;
                p.Value = new BoolValue(p);
                props.Add(p);
            }
            Store_Property.SetLink(ColumnXStore, props);
            #endregion

            #region RelationX  ================================================
            props.Clear();
            {
                var p = _relationXNameProperty = new PropertyOf<RelationX, string>(PropertyStore, Trait.RelationName_P);
                p.GetValFunc = (item) => p.Cast(item).Name;
                p.SetValFunc = (item, value) => { p.Cast(item).Name = value; return true; };
                p.Value = new StringValue(p);
                props.Add(p);
            }
            {
                var p = _relationXSummaryProperty = new PropertyOf<RelationX, string>(PropertyStore, Trait.RelationSummary_P);
                p.GetValFunc = (item) => p.Cast(item).Summary;
                p.SetValFunc = (item, value) => { p.Cast(item).Summary = value; return true; };
                p.Value = new StringValue(p);
                props.Add(p);
            }
            {
                var p = _relationXPairingProperty = new PropertyOf<RelationX, string>(PropertyStore, Trait.RelationPairing_P, typeof(Pairing));
                p.GetValFunc = (item) => p.Cast(item).Pairing.ToString();
                p.SetValFunc = (item, value) => p.Cast(item).TrySetPairing(Enum.TryParse<Pairing>(value, out Pairing val) ? val : Pairing.OneToMany);
                p.Value = new StringValue(p);
                props.Add(p);
            }
            {
                var p = _relationXIsRequiredProperty = new PropertyOf<RelationX, bool>(PropertyStore, Trait.RelationIsRequired_P);
                p.GetValFunc = (item) => p.Cast(item).IsRequired;
                p.SetValFunc = (item, value) => { p.Cast(item).IsRequired = value; return true; };
                p.Value = new BoolValue(p);
                props.Add(p);
            }
            Store_Property.SetLink(RelationXStore, props);
            #endregion
        }

        #region LookUpProperty  ===============================================
        static char[] _dotSplit = ".".ToCharArray();
        internal bool TryLookUpProperty(Store store, string name, out Property prop)
        {
            prop = null;

            if (string.IsNullOrWhiteSpace(name)) return false;

            if (store.IsTableX)
            {
                if (TableX_ColumnX.TryGetChildren(store, out IList<ColumnX> ls1))
                {
                    foreach (var col in ls1)
                    {
                        if (string.IsNullOrWhiteSpace(col.Name)) continue;
                        if (string.Compare(col.Name, name, true) == 0) { prop = col; return true; }
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
