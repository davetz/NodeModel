using System;
using NodeModelTestApp.Services;
using NodeModelTestApp.ViewModels;
using Microsoft.Graphics.Canvas.UI.Xaml;

using Windows.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.UI;

namespace NodeModelTestApp.Views
{
    public sealed partial class ModelingPage : Page
    {
        public ModelingViewModel ViewModel { get; } = new ModelingViewModel();

        public ModelingPage()
        {
            InitializeComponent();
            DataContext = NodeModelService.Current.CurentModel;
            
        }

        #region EditorCanvas_Draw  ============================================
        private void EditorCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            var ds = args.DrawingSession;
            Rect rect = new Rect(50, 50, 100, 60);
            var color2 = Color.FromArgb(0xff, 0xff, 0xff, 0x80);
            ds.DrawRoundedRectangle(rect, 5, 5, color2, 3);
        }

        #endregion

        #region EditorCanavas_Loaded  =======================================
        private void EditorCanvas_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            EditorCanvas.Invalidate();
        }

        #endregion

        #region Page_Unloaded  =============================================
        private void Page_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (EditorCanvas != null)
            {
                EditorCanvas.RemoveFromVisualTree();
                EditorCanvas = null;
            }
        }
        #endregion

    }
}
