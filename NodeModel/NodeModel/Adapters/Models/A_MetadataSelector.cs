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


        #region HitTest  ======================================================
        public override void HidePropertyPanel() => SetTableX(null);
        public override void ShowPropertyPanel() => SetTableX(HitTableX);

        override public bool HitTest()
        {
            if (ItemRef is null) return false;
            return ChefRef.MetadataHitTest(this);
        }
        override public bool HitVerify() => ChefRef.MetadataHitVerify(this);
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
