using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    public partial class Chef
    {
        const int _ds = 4; // hit tollerence
        internal TableX TableXHitTest(A_MetadataSelector selector, TableX txPrev)
        {
            var px = selector.DrawPoint2.X;
            var py = selector.DrawPoint2.Y;

            if (IsTableXHit(txPrev, px, py)) return txPrev;

            foreach (var tx in TableXStore.Items)
            {
                if (IsTableXHit(tx, px, py)) return tx;
            }
            return null;
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
    }
}
