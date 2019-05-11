using System;
using NodeModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using NodeModelRepository;
using System.Collections.Generic;

namespace NodeModelTestApp.Services
{
    internal class NodeModelService
    {
        public static NodeModelService Current => _current ?? (_current = new NodeModelService());
        private static NodeModelService _current;

        private CoreDispatcher _dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;

        public INodeModel CurentModel => _currentModel ?? (_currentModel = NodeModelCreator.Create());
        private INodeModel _currentModel;

        public async Task<bool> NewModel()
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                _currentModel.Load(null);
            });
            return true;
        }

        public async Task<bool> OpenModel()
        {
            var openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            openPicker.FileTypeFilter.Add(".nmdf");
            StorageFile file = await openPicker.PickSingleFileAsync(); //PickSingleFileAsync();
            if (file != null)
            {
                await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    var repository = new Repository(file);
                    _currentModel.Load(repository);
                });
                _currentModel.Refresh();
            }
            return true;
        }
        public async Task<bool> SaveModel()
        {
            if (_currentModel is null) return false;

            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                _currentModel.Save();
            });
            return true;
        }

        public async Task<bool> SaveAsModel()
        {
            if (_currentModel is null) return false;

            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = string.Empty
            };
            savePicker.FileTypeChoices.Add("DataFile", new List<string>() { ".nmdf" });
            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    var repository = new Repository(file);
                    _currentModel.SaveAs(repository);
                });
                _currentModel.Refresh();
            }
            return true;
        }

        public async Task<bool> ReloadModel()
        {
            if (_currentModel is null) return false;

            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                _currentModel.Reload();
            });
            return true;
        }
    }
}
