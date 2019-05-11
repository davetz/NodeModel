using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace NodeModel
{
    public class TableX : StoreOf<RowX>
    {
        internal string Name;
        internal string ToolTip;
        internal string Description;

        internal (float X, float Y) Center;
        internal (byte X, byte Y) Radius;

        #region Constructors  =================================================
        internal TableX(StoreOf<TableX> owner)
        {
            Owner = owner;
            Trait = Trait.TableX;
            owner.Add(this);
        }
        #endregion
    }
}
