using System;

using NodeModelTestApp.ViewModels;

using Windows.UI.Xaml.Controls;

namespace NodeModelTestApp.Views
{
    public sealed partial class ModelingPage : Page
    {
        public ModelingViewModel ViewModel { get; } = new ModelingViewModel();

        public ModelingPage()
        {
            InitializeComponent();
        }
    }
}
