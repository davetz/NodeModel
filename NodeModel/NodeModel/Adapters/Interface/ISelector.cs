using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;

using Windows.Foundation;
using Windows.UI.Xaml;

namespace NodeModel
{
    public interface ISelector
    {
        Vector2 DrawPoint1 { get; set; }
        Vector2 DrawPoint2 { get; set; }

        Rect RegionRect { get; }

        bool IsAnyHit { get; }
        bool IsHitPin { get; }
        bool IsHitNode { get; }
        bool IsHitRegion { get; }

        bool HitTest();

        Visibility NodePanel_Visible { get; }
        string Node_Name { get; set; }
        string Node_ToolTip { get; set; }
        string Node_Description { get; }

        Visibility NodeTypePanel_Visible { get; }
        string NodeType_Name { get; set; }
        string NodeType_ToolTip { get; set; }
        string NodeType_Description { get; set; }

        IList<(Rect Rect, byte Width, (byte A, byte R, byte G, byte B) Color)> DrawRects { get; }
        IList<(Vector2[] Points, bool IsDotted, byte Width, (byte A, byte R, byte G, byte B) Color)> DrawSplines { get; }
        IList<(Vector2 TopLeft, string Text, (byte A, byte R, byte G, byte B) Color)> DrawText { get; }
    }
}
