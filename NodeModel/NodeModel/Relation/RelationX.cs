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

        internal ConnectorPin InputPin = new ConnectorPin();
        internal ConnectorPin OutputPin = new ConnectorPin();
        internal struct ConnectorPin
        {
            internal string Name;
            internal string Summary;
            internal string Description;

            internal (byte DX, byte DY) Offset; //delta from center of nodeType
            internal (byte W, byte H) Size;     //used to calculate boundingRect of pin
        }

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
