using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace NodeModel
{
    public class A_MetadataSelector : A_Selector
    {
        internal A_MetadataSelector(A_NodeModel model, Item itemRef) : base(model, itemRef)
        {
            RefreshCanvasDraw();
        }

        #region Resize  =======================================================

        #region ResizeInitialize  =============================================
        void ResizeInitialize(TableX tx)
        {
            var (x, y) = tx.Center;
            var (w, h) = tx.Radius;

            NodePoint1 = new Vector2(x - w, y - h);
            NodePoint2 = new Vector2(x + w, y + h);
        }
        #endregion

        #region ResizePropagate  ==============================================
        override public void ResizePropagate()
        {
            var tx = TapHitTableX;
            if (tx is null) return;

            var x1 = NodePoint1.X;
            var y1 = NodePoint1.Y;
            var x2 = NodePoint2.X;
            var y2 = NodePoint2.Y;

            var x = (x2 + x1) / 2;
            var y = (y2 + y1) / 2;
            var w = (byte)((x2 - x1) / 2);
            var h = (byte)((y2 - y1) / 2);

            tx.Center = (x, y);
            tx.Radius = (w, h);

        }
        #endregion

        #region ResizeTop  ====================================================
        override public void ResizeTop()
        {
            if (TapHitTableX is null) return;

            var (x, y) = TapHitTableX.Center;
            var (w, h) = TapHitTableX.Radius;
            var p = DrawPoint2;

            var yL1 = y + h - 510;
            var yL2 = y + h - 2 * TableX.MIN;

            var x1 = x - w;
            var x2 = x + w;

            var y1 = p.Y < yL1 ? yL1 : p.Y > yL2 ? yL2 : p.Y;
            var y2 = y + h;

            NodePoint1 = new Vector2(x1, y1);
            NodePoint2 = new Vector2(x2, y2);
        }
        #endregion

        #region ResizeLeft  ===================================================
        override public void ResizeLeft()
        {
            if (TapHitTableX is null) return;

            var (x, y) = TapHitTableX.Center;
            var (w, h) = TapHitTableX.Radius;
            var p = DrawPoint2;

            var xL1 = x + w - 510;
            var xL2 = x + w - 2 * TableX.MIN;

            var x1 = p.X < xL1 ? xL1 : p.X > xL2 ? xL2 : p.X;
            var x2 = x + w;

            var y1 = y - h;
            var y2 = y + h;

            NodePoint1 = new Vector2(x1, y1);
            NodePoint2 = new Vector2(x2, y2);
        }
        #endregion

        #region ResizeRight  ==================================================
        override public void ResizeRight()
        {
            if (TapHitTableX is null) return;

            var (x, y) = TapHitTableX.Center;
            var (w, h) = TapHitTableX.Radius;
            var p = DrawPoint2;

            var xL1 = x - w + 2 * TableX.MIN;
            var xL2 = x - w + 510;

            var x1 = x - w;
            var x2 = p.X < xL1 ? xL1 : p.X > xL2 ? xL2 : p.X;

            var y1 = y - h;
            var y2 = y + h;

            NodePoint1 = new Vector2(x1, y1);
            NodePoint2 = new Vector2(x2, y2);
        }
        #endregion

        #region ResizeBottom  =================================================
        override public void ResizeBottom()
        {
            if (TapHitTableX is null) return;

            var (x, y) = TapHitTableX.Center;
            var (w, h) = TapHitTableX.Radius;
            var p = DrawPoint2;

            var yL1 = y - h + 2 * TableX.MIN;
            var yL2 = y - h + 510;

            var x1 = x - w;
            var x2 = x + w;

            var y1 = y - h;
            var y2 = p.Y < yL1 ? yL1 : p.Y > yL2 ? yL2 : p.Y;

            NodePoint1 = new Vector2(x1, y1);
            NodePoint2 = new Vector2(x2, y2);
        }
        #endregion

        #region ResizeTopLeft  ================================================
        override public void ResizeTopLeft()
        {
            if (TapHitTableX is null) return;

            var (x, y) = TapHitTableX.Center;
            var (w, h) = TapHitTableX.Radius;
            var p = DrawPoint2;

            var xL1 = x + w - 510;
            var xL2 = x + w - 2 * TableX.MIN;

            var yL1 = y + h - 510;
            var yL2 = y + h - 2 * TableX.MIN;

            var x1 = p.X < xL1 ? xL1 : p.X > xL2 ? xL2 : p.X;
            var x2 = x + w;

            var y1 = p.Y < yL1 ? yL1 : p.Y > yL2 ? yL2 : p.Y;
            var y2 = y + h;

            NodePoint1 = new Vector2(x1, y1);
            NodePoint2 = new Vector2(x2, y2);
        }
        #endregion

        #region ResizeTopRight  ===============================================
        override public void ResizeTopRight()
        {
            if (TapHitTableX is null) return;

            var (x, y) = TapHitTableX.Center;
            var (w, h) = TapHitTableX.Radius;
            var p = DrawPoint2;

            var xL1 = x - w + 2 * TableX.MIN;
            var xL2 = x - w + 510;

            var yL1 = y + h - 510;
            var yL2 = y + h - 2 * TableX.MIN;

            var x1 = x - w;
            var x2 = p.X < xL1 ? xL1 : p.X > xL2 ? xL2 : p.X;

            var y1 = p.Y < yL1 ? yL1 : p.Y > yL2 ? yL2 : p.Y;
            var y2 = y + h;

            NodePoint1 = new Vector2(x1, y1);
            NodePoint2 = new Vector2(x2, y2);
        }
        #endregion

        #region ResizeBottomLeft  =============================================
        override public void ResizeBottomLeft()
        {
            if (TapHitTableX is null) return;

            var (x, y) = TapHitTableX.Center;
            var (w, h) = TapHitTableX.Radius;
            var p = DrawPoint2;

            var xL1 = x + w - 510;
            var xL2 = x + w - 2 * TableX.MIN;

            var yL1 = y - h + 2 * TableX.MIN;
            var yL2 = y - h + 510;

            var x1 = p.X < xL1 ? xL1 : p.X > xL2 ? xL2 : p.X;
            var x2 = x + w;

            var y1 = y - h;
            var y2 = p.Y < yL1 ? yL1 : p.Y > yL2 ? yL2 : p.Y;

            NodePoint1 = new Vector2(x1, y1);
            NodePoint2 = new Vector2(x2, y2);
        }
        #endregion

        #region ResizeBottomRight  ============================================
        override public void ResizeBottomRight()
        {
            if (TapHitTableX is null) return;

            var (x, y) = TapHitTableX.Center;
            var (w, h) = TapHitTableX.Radius;
            var p = DrawPoint2;

            var xL1 = x - w + 2 * TableX.MIN;
            var xL2 = x - w + 510;

            var yL1 = y - h + 2 * TableX.MIN;
            var yL2 = y - h + 510;

            var x1 = x - w;
            var x2 = p.X < xL1 ? xL1 : p.X > xL2 ? xL2 : p.X;

            var y1 = y - h;
            var y2 = p.Y < yL1 ? yL1 : p.Y > yL2 ? yL2 : p.Y;

            NodePoint1 = new Vector2(x1, y1);
            NodePoint2 = new Vector2(x2, y2);
        }
        #endregion

        #endregion

        #region HitTest  ======================================================
        internal TableX TapHitTableX;
        internal TableX EndHitTableX;
        internal TableX SkimHitTableX;
        internal TableX DragHitTableX;

        internal RelationX HitRelationX;
        internal bool HitEdgeFarEnd;
        internal (float X, float Y) ResizeCenter;

        public override void HidePropertyPanel() => SetTableX(null);
        public override void ShowPropertyPanel() => SetTableX(TapHitTableX);


        override public bool TapHitTest()
        {
            Hit = HitType.None;
            if (ItemRef is null) return false;

            TapHitTableX = ChefRef.TableXHitTest(this, TapHitTableX);
            if (TapHitTableX is null) return false;
            Hit = HitType.Node;
            ResizeCenter = TapHitTableX.Center;
            ResizeInitialize(TapHitTableX);
            return true;
        }
        override public bool EndHitTest()
        {
            Hit = HitType.None;
            if (ItemRef is null) return false;

            EndHitTableX = ChefRef.TableXHitTest(this, EndHitTableX);
            if (EndHitTableX is null) return false;

            Hit = HitType.Node;
            return true;
        }
        override public bool SkimHitTest()
        {
            Hit = HitType.None;
            if (ItemRef is null) return false;

            SkimHitTableX = ChefRef.TableXHitTest(this, SkimHitTableX);
            if (SkimHitTableX is null) return false;

            Hit = HitType.Node;
            ToolTip_Text1 = SkimHitTableX.Name;
            ToolTip_Text2 = SkimHitTableX.ToolTip;
            return true;
        }
        override public bool DragHitTest()
        {
            Hit = HitType.None;
            if (ItemRef is null) return false;

            EndHitTableX = DragHitTableX = ChefRef.TableXHitTest(this, DragHitTableX);
            if (DragHitTableX is null) return false;

            Hit = HitType.Node;
            return true;
        }
        #endregion

        #region CreateNode  ===================================================
        override public bool CreateNode()
        {
            if (ItemRef is null) return false;

            var tx = ChefRef.CreateTableX();
            tx.Center = (DrawPoint1.X, DrawPoint1.Y);
            RefreshCanvasDraw();
            SetTableX(tx);

            return true;
        }
        #endregion

        #region RefreshMetadataCanvas  ========================================
        override public void RefreshCanvasDrawData() => RefreshCanvasDraw();

        internal void RefreshCanvasDraw()
        {
            if (ItemRef is null) return;

            _drawRects.Clear();
            _drawSplines.Clear();
            _drawText.Clear();
            byte thick = 3;
            //#CDDFFF

            (byte, byte, byte, byte) color = (0xFF, 0xCD, 0xDF, 0xFF);
            foreach (var tx in ChefRef.TableXStore.Items)
            {
                var (x, y) = tx.Center;
                var (dx, dy) = tx.Radius;
                if (dx < 4) dx = 4;
                if (dy < 4) dy = 4;
                _drawRects.Add((new Rect(x - dx, y - dy, 2 * dx, 2 * dy), thick, color));
            }
        }
        #endregion
    }
}
