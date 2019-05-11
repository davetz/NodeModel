using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace NodeModel
{
    public class RowX : Item
    {
        internal string Name;
        internal string ToolTip;

        internal (float X, float Y) Center;

        TableX TableXRef => Owner as TableX; 

        #region Constructors  =================================================
        internal RowX(TableX owner)
        {
            Owner = owner;
            Trait = Trait.RowX;
            AutoExpandRight = true;

            owner.Add(this);
        }
        #endregion

        #region Properies/Methods  ============================================
        internal TableX TableX => (Owner as TableX);
        #endregion
    }
}
