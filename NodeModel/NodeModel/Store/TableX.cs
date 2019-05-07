using System.Numerics;

using Windows.Foundation;

namespace NodeModel
{
    public class TableX : StoreOf<RowX>
    {
        internal (float X, float Y) Center;
        internal (byte X, byte Y) Radius;

        #region CalculatedValues  =============================================
        const int MinRadius = 2;

        internal Vector2 CenterPoint { get { return GetCenter(); } set { SetCenter(value); } }
        internal Rect BoundingRect { get { return GetRect(); } set { SetRect(value); } }

        //=====================================================================
        private Vector2 GetCenter() => new Vector2(Center.X, Center.Y);
        private void SetCenter(Vector2 val) => Center = (val.X, val.Y);

        //=====================================================================
        private Rect GetRect()
        {
            var (x, y) = TopLeft();
            var w = 2 * (Radius.X + MinRadius);
            var h = 2 * (Radius.Y + MinRadius);
            return new Rect(x, y, w, h);
        }
        internal Rect GetRect((float X, float Y) p)
        {
            var (x, y) = TopLeft(p);
            var w = 2 * (Radius.X + MinRadius);
            var h = 2 * (Radius.Y + MinRadius);
            return new Rect(x, y, w, h);
        }
        private void SetRect(Rect v)
        {
            var x = (float)((v.Left + v.Right) / 2);
            var y = (float)((v.Top + v.Bottom) / 2);
            Center = (x, y);

            var rx = Cnvt((v.Width / 2) - MinRadius);
            var ry = Cnvt((v.Height / 2) - MinRadius);
            Center = (rx, ry);
        }

        //=====================================================================
        private byte Cnvt(double v) => (byte)(v < 0 ? 0 : v > 255 ? 255 : v);
        private (float X, float Y) TopLeft()
        {
            var x = Center.X - Radius.X - MinRadius;
            var y = Center.Y - Radius.Y - MinRadius;
            return (x, y);
        }
        internal (float X, float Y) TopLeft((float X, float Y) p)
        {
            var x = p.X - Radius.X - MinRadius;
            var y = p.Y - Radius.Y - MinRadius;
            return (x, y);
        }
        #endregion

        internal string Name;
        internal string Summary;
        internal string Description;

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
