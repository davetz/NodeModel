using System.ComponentModel;
using System.Diagnostics;
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
        internal (byte X, byte Y) Radius = (MIN, MIN); // minimum x,y radius
        internal const byte MIN = 8;

        int Cnvt1(byte v) => v < MIN ? MIN : v * 2;
        byte Cnvt2(int v) => (byte)(v < 8 ? 4 : v > 510 ? 255  : v / 2);

        #region Constructors  =================================================
        internal TableX(StoreOf<TableX> owner)
        {
            Owner = owner;
            Trait = Trait.TableX;
            owner.Add(this);
        }
        #endregion

        #region Resize  =======================================================
        byte Limit(float v) => (byte)(v < MIN ? MIN : v > byte.MaxValue ? byte.MaxValue : v);

        internal void ResizeTop(Vector2 p)
        {
            var (x, y) = Center;
            var (w, h) = Radius;

            var dy = p.Y - y;
            if (dy < -2 || dy > 2)
            {
                var zz = dy;
            }

            var yL1 = y - 255;
            var yL2 = y - MIN;

            var x1 = x - w;
            var x2 = x + w;

            var y1 = p.Y < yL1 ? yL1 : p.Y > yL2 ? yL2 : p.Y;
            var y2 = y + h;

            SetCenterRadius(x1, y1, x2, y2);
        }
        internal void ResizeBottom(Vector2 delta, (float, float) centerRef)
        {
            var r = Limit(Radius.Y + delta.Y);

            var x1 = Center.X - Radius.X;
            var y1 = Center.Y - Radius.Y;

            var x2 = Center.X + Radius.X;
            var y2 = Center.Y + r;

            SetCenterRadius(x1, y1, x2, y2);
        }
        internal void ResizeLeft(Vector2 delta, (float, float) centerRef)
        {
            var r = Limit(Radius.X - delta.X);

            var x1 = Center.X - r;
            var y1 = Center.Y - Radius.Y;

            var x2 = Center.X + Radius.X;
            var y2 = Center.Y + Radius.Y;

            SetCenterRadius(x1, y1, x2, y2);
        }
        internal void ResizeRight(Vector2 delta, (float, float) centerRef)
        {
            var r = Limit(Radius.X + delta.X);

            var x1 = Center.X - Radius.X;
            var y1 = Center.Y - Radius.Y;

            var x2 = Center.X + r;
            var y2 = Center.Y + Radius.Y;

            SetCenterRadius(x1, y1, x2, y2);
        }
        internal void ResizeTopLeft(Vector2 delta, (float, float) centerRef)
        {
            //ResizeTop(delta);
            //ResizeLeft(delta);
        }
        internal void ResizeTopRight(Vector2 delta, (float, float) centerRef)
        {
            //ResizeTop(delta);
            //ResizeRight(delta);
        }
        internal void ResizeBottomLeft(Vector2 delta, (float, float) centerRef)
        {
            //ResizeBottom(delta);
            //ResizeLeft(delta);
        }
        internal void ResizeBottomRight(Vector2 delta, (float, float) centerRef)
        {
            //ResizeBottom(delta);
            //ResizeRight(delta);
        }

        void SetCenterRadius(float x1, float y1, float x2, float y2)
        {
            var x = (x1 + x2) / 2;
            var y = (y1 + y2) / 2;
            var w = (x2 - x1) / 2;
            var h = (y2 - y1) / 2;
            Center = (x, y);
            Radius = ((byte)w, (byte)h);
        }
        #endregion
    }
}
