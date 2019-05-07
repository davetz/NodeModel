using System;

using NodeModelTestApp.ViewModels;

using Windows.UI.Xaml.Controls;

namespace NodeModelTestApp.Views
{
    public sealed partial class MetadataPage : Page
    {
        public MetadataViewModel ViewModel { get; } = new MetadataViewModel();

        public MetadataPage()
        {
            InitializeComponent();
        }
    }
}
