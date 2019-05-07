using System;

namespace NodeModel
{
    public class ColumnX : Property
    {
        internal string Name;
        internal string Summary;
        internal string Description;

        #region Constructors  =================================================
        internal ColumnX(StoreOf<ColumnX> owner)
        {
            Owner = owner;
            Trait = Trait.ColumnX;
            AutoExpandRight = true;

            Value = Value.Create(ValType.String);

            owner.Add(this);
        }
        #endregion

        #region Property  =====================================================

        internal void Initialize(ValType type, string defaultVal, int rowCount)
        {
            Value = Value.Create(type, rowCount, defaultVal);
        }
        #endregion
    }
}
