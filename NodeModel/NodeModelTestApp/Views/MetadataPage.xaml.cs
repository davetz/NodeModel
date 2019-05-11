using NodeModel;
using NodeModelTestApp.Services;
using NodeModelTestApp.ViewModels;
using Windows.UI.Xaml.Controls;

namespace NodeModelTestApp.Views
{
    public sealed partial class MetadataPage : Page
    {
        private INodeModel _model;
        private ISelector _selector;

        public MetadataViewModel ViewModel { get; } = new MetadataViewModel();

        public MetadataPage()
        {
            InitializeComponent();

            _model = NodeModelService.Current.CurentModel;
            _selector = _model.GetMetadataSelector();
            ModelCanvas.Initialize(_model, _selector);
            DataContext = _selector;
        }
    }
}
