using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    public class RelationX : RelationOf<RowX, RowX>
    {
        internal string Name;
        internal string Summary;
        internal string Description;

        internal (byte DX, byte DY) Offset1;
        internal (byte DX, byte DY) Offset2;
        internal (byte W, byte H) Size1;
        internal (byte W, byte H) Size2;

        #region Constructors  =================================================
        internal RelationX(StoreOf<RelationX> owner)
        {
            Owner = owner;
            Trait = Trait.RelationX;
            Pairing = Pairing.OneToMany;
            owner.Add(this);
        }
        #endregion
    }
}
