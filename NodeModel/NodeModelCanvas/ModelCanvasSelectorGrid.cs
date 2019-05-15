using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NodeModelCanvas
{
    public sealed partial class ModelCanvas
    {
        void ShowSelectorGrid()
        {
            SelectorRegionGrid();
            SelectorGrid.Visibility = Visibility.Visible;
        }

        void HideSelectorGrid() => SelectorGrid.Visibility = Visibility.Collapsed;

        void SelectorRegionGrid()
        {
            var x1 = _selector.GridPoint1.X;
            var y1 = _selector.GridPoint1.Y;
            var x2 = _selector.GridPoint2.X;
            var y2 = _selector.GridPoint2.Y;

            var dx = x2 - x1;
            var dy = y2 - y1;

            var width = dx > 0 ? dx : -dx;
            var height = dy > 0 ? dy : -dy;
            var margin = SelectorBorder.Margin;

            SelectorGrid.Width = width + margin.Right;
            SelectorGrid.Height = height;

            var top = y1 < y2 ? y1 : y2;
            var left = x1 < x2 ? x1 : x2;

            Canvas.SetTop(SelectorGrid, top);
            Canvas.SetLeft(SelectorGrid, left);
        }

    }
}
