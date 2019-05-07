using System;
using System.ComponentModel;
using NodeModelTestApp.Services;
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
            DataContext = NodeModelService.Current.CurentModel;
        }

        private void NewModel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NodeModelService.Current.NewModel();
        }

        private void OpenModel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NodeModelService.Current.OpenModel();
        }

        private void SaveModel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NodeModelService.Current.SaveModel();
        }

        private void ReloadModel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void SaveAsModel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NodeModelService.Current.SaveAsModel();
        }
    }
}
