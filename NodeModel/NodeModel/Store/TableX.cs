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
        internal (byte X, byte Y) Radius = (8, 8); // minimum x,y radius

        internal int MinWidth { get => Cnvt1(Radius.X); set => Radius.X = Cnvt2(value); }
        internal int MinHeight { get => Cnvt1(Radius.Y); set => Radius.Y = Cnvt2(value); }

        int Cnvt1(byte v) => v < 8 ? 8 : v * 2;
        byte Cnvt2(int v) => (byte)(v < 8 ? 4 : v > 510 ? 255  : v / 2);

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
