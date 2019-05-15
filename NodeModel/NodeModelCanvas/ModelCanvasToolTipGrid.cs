﻿using NodeModel;
using Windows.UI.Xaml.Controls;

namespace NodeModelCanvas
{
    public sealed partial class ModelCanvas
    {
        private bool _isToolTipVisible;

        private void ShowTooltip()
        {
            var name = _selector.ToolTip_Text1;
            var text = _selector.ToolTip_Text2;
            if (string.IsNullOrWhiteSpace(name)) HideTootlip();

            ItemName.Text = name;
            ItemToolTip.Text = text;

            var ds = ItemToolTip.Text.Length * 4;
            var x = _selector.GridPoint2.X - ds;
            var y = _selector.GridPoint2.Y - 60;

            Canvas.SetTop(ToolTipBorder, y);
            Canvas.SetLeft(ToolTipBorder, x);
            ToolTipBorder.Visibility = Windows.UI.Xaml.Visibility.Visible;
            _isToolTipVisible = true;
        }
        private void HideTootlip()
        {
            if (_isToolTipVisible)
            {
                _isToolTipVisible = false;
                ToolTipBorder.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                //_model.GraphicController.HideLocator(this);
            }
        }
    }
}
