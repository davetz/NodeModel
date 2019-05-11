using NodeModelTestApp.Services;
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
            DataContext = NodeModelService.Current.CurentModel;            
        }
    }
}
