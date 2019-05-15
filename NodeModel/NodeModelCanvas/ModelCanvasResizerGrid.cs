using NodeModel;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace NodeModelCanvas
{
    public sealed partial class ModelCanvas
    {

        void ShowResizerGrid()
        {
            UpdateResizerGrid();
            ResizerGrid.Visibility = Visibility.Visible;
        }

        void HideResizerGrid() => ResizerGrid.Visibility = Visibility.Collapsed;

        void UpdateResizerGrid()
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

            ResizerGrid.Width = width + margin.Right;
            ResizerGrid.Height = height;

            var top = y1 < y2 ? y1 : y2;
            var left = x1 < x2 ? x1 : x2;

            Canvas.SetTop(ResizerGrid, top);
            Canvas.SetLeft(ResizerGrid, left);
        }

        #region Resizer_PointerEvents  =========================================
        private void SizerTopLeft_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            TrySetNewCursor(CoreCursorType.SizeNorthwestSoutheast);
        }

        private void SizerTopLeft_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            RestorePointerCursor();
        }

        private void SizerTopLeft_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
        }

        private void SizerTopLeft_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
        }

        private void SizerTopLeft_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
        }




        private void SizerTopCenter_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            TrySetNewCursor(CoreCursorType.SizeNorthSouth);
        }

        private void SizerTopCenter_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            RestorePointerCursor();
        }

        private void SizerTopCenter_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
        }

        private void SizerTopCenter_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
        }

        private void SizerTopCenter_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
        }




        private void SizerTopRight_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            TrySetNewCursor(CoreCursorType.SizeNortheastSouthwest);
        }

        private void SizerTopRight_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            RestorePointerCursor();
        }

        private void SizerTopRight_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
        }

        private void SizerTopRight_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
        }

        private void SizerTopRight_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
        }




        private void SizerBottomLeft_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            TrySetNewCursor(CoreCursorType.SizeNortheastSouthwest);
        }

        private void SizerBottomLeft_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            RestorePointerCursor();
        }

        private void SizerBottomLeft_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
        }

        private void SizerBottomLeft_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
        }

        private void SizerBottomLeft_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
        }




        private void SizerBottomCenter_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            TrySetNewCursor(CoreCursorType.SizeNorthSouth);
        }

        private void SizerBottomCenter_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            RestorePointerCursor();
        }

        private void SizerBottomCenter_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
        }

        private void SizerBottomCenter_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
        }

        private void SizerBottomCenter_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
        }




        private void SizerBottomRight_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            TrySetNewCursor(CoreCursorType.SizeNorthwestSoutheast);
        }

        private void SizerBottomRight_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            RestorePointerCursor();
        }

        private void SizerBottomRight_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
        }

        private void SizerBottomRight_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
        }

        private void SizerBottomRight_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
        }




        private void SizerCenterLeft_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            TrySetNewCursor(CoreCursorType.SizeWestEast);
        }

        private void SizerCenterLeft_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            RestorePointerCursor();
        }

        private void SizerCenterLeft_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
        }

        private void SizerCenterLeft_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
        }

        private void SizerCenterLeft_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
        }




        private void SizerCenterRight_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            TrySetNewCursor(CoreCursorType.SizeWestEast);
        }

        private void SizerCenterRight_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            RestorePointerCursor();
        }

        private void SizerCenterRight_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
        }

        private void SizerCenterRight_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
        }

        private void SizerCenterRight_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
        }
        #endregion



        #region PointerCursor  ================================================
        private void RestorePointerCursor() => TrySetNewCursor(CoreCursorType.Arrow);
        private void TrySetNewCursor(CoreCursorType cursorType)
        {
            if (_currentCusorType == cursorType) return;
            if (_cursors.TryGetValue(cursorType, out CoreCursor newCursor))
            {
                _currentCusorType = cursorType;
                Window.Current.CoreWindow.PointerCursor = newCursor;
            }
        }
        private CoreCursorType _currentCusorType;
        readonly Dictionary<CoreCursorType, CoreCursor> _cursors = new Dictionary<CoreCursorType, CoreCursor>()
        {
            [CoreCursorType.Pin] = new CoreCursor(CoreCursorType.Pin, 0),
            [CoreCursorType.Hand] = new CoreCursor(CoreCursorType.Hand, 0),
            [CoreCursorType.Wait] = new CoreCursor(CoreCursorType.Wait, 0),
            [CoreCursorType.Help] = new CoreCursor(CoreCursorType.Help, 0),
            [CoreCursorType.Arrow] = new CoreCursor(CoreCursorType.Arrow, 0),
            [CoreCursorType.IBeam] = new CoreCursor(CoreCursorType.IBeam, 0),
            [CoreCursorType.Cross] = new CoreCursor(CoreCursorType.Cross, 0),
            [CoreCursorType.Person] = new CoreCursor(CoreCursorType.Person, 0),
            [CoreCursorType.UpArrow] = new CoreCursor(CoreCursorType.UpArrow, 0),
            [CoreCursorType.SizeAll] = new CoreCursor(CoreCursorType.SizeAll, 0),
            [CoreCursorType.UniversalNo] = new CoreCursor(CoreCursorType.UniversalNo, 0),
            [CoreCursorType.SizeWestEast] = new CoreCursor(CoreCursorType.SizeWestEast, 0),
            [CoreCursorType.SizeNorthSouth] = new CoreCursor(CoreCursorType.SizeNorthSouth, 0),
            [CoreCursorType.SizeNortheastSouthwest] = new CoreCursor(CoreCursorType.SizeNortheastSouthwest, 0),
            [CoreCursorType.SizeNorthwestSoutheast] = new CoreCursor(CoreCursorType.SizeNorthwestSoutheast, 0),
        };
        #endregion
    }
}
