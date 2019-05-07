using System;

using NodeModelTestApp.ViewModels;

using Windows.UI.Xaml.Controls;

namespace NodeModelTestApp.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
