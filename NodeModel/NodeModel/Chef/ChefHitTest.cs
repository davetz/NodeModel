using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace NodeModel
{
    public partial class Chef
    {
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
            if (px < (x - w)) return false;
            if (py < (y - h)) return false;
            if (px > (x + w)) return false;
            if (py > (y + h)) return false;
            return true;
        }

        internal bool RegionTableXHitTest(Vector2 p1, Vector2 p2, List<TableX> list)
        {
            var min = Vector2.Min(p1, p2);
            var max = Vector2.Max(p1, p2);
            list.Clear();

            foreach (var tx in TableXStore.Items)
            {
                var (x, y) = tx.Center;
                if (x < min.X) continue;
                if (y < min.Y) continue;
                if (x > max.X) continue;
                if (y > max.Y) continue;
                list.Add(tx);
            }

            return list.Count > 0;
        }
    }
}
