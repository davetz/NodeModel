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
            selector.Hit = HitType.Node;

            var px = selector.DrawPoint2.X;
            var py = selector.DrawPoint2.Y;

            foreach (var tx in TableXStore.Items)
            {
                var (x, y) = tx.Center;
                var (w, h) = tx.Radius;
                if (px < x - w - _ds) continue;
                if (px < y - h - _ds) continue;
                if (px > x + w + _ds) continue;
                if (px < y + h + _ds) continue;
                selector.Hit |= HitType.NodeType;
                return true;
            }
            return false;
        }
    }
}
