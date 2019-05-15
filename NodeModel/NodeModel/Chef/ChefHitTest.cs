using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    public partial class Chef
    {
        const int _ds = 4; // hit tollerence
        internal bool MetadataHitTest(A_Selector selector)
        {
            selector.Hit = HitType.None;
            selector.HitRowX = null;
            selector.HitTableX = null;
            selector.HitRelationX = null;

            var px = selector.DrawPoint2.X;
            var py = selector.DrawPoint2.Y;

            foreach (var tx in TableXStore.Items)
            {
                if (IsTableXHit(tx, px, py))
                {
                    selector.Hit = HitType.Node;
                    selector.HitTableX = tx;
                    selector.ToolTip_Text1 = tx.Name;
                    selector.ToolTip_Text2 = tx.ToolTip;
                    return true;
                }
            }
            return false;
        }
        bool IsTableXHit(TableX tx, float px, float py)
        {
            if (tx is null) return false;
            var (x, y) = tx.Center;
            var (w, h) = tx.Radius;
            if (px < (x - w - _ds)) return false;
            if (py < (y - h - _ds)) return false;
            if (px > (x + w + _ds)) return false;
            if (py > (y + h + _ds)) return false;
            return true;
        }
        internal bool MetadataHitVerify(A_Selector selector)
        {
            var px = selector.DrawPoint2.X;
            var py = selector.DrawPoint2.Y;

            if (selector.IsHitNode)
            {
                return (IsTableXHit(selector.HitTableX, px, py));
            }
            return false;
        }

    }
}
