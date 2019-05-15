using NodeModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NodeModelCanvas
{
    public sealed partial class ModelCanvas
    {
        void ShowRegionGrid()
        {
            UpdateRegionGrid();
            RegionGrid.Visibility = Visibility.Visible;           
        }

        void HideRegionGrid() => RegionGrid.Visibility = Visibility.Collapsed;

        void UpdateRegionGrid()
        {
            var x1 = _selector.GridPoint1.X;
            var y1 = _selector.GridPoint1.Y;
            var x2 = _selector.GridPoint2.X;
            var y2 = _selector.GridPoint2.Y;

            var dx = x2 - x1;
            var dy = y2 - y1;

            RegionGrid.Width = dx > 0 ? dx : -dx;
            RegionGrid.Height = dy > 0 ? dy : -dy;

            var top  = y1 < y2 ? y1 : y2;
            var left = x1 < x2 ? x1 : x2;

            Canvas.SetTop(RegionGrid, top);
            Canvas.SetLeft(RegionGrid, left);
        }
    }
}
