using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using Windows.UI.Xaml;

namespace NodeModel
{
    /// <summary>
    /// Usage:  var model =  NodeModeCore.Create();
    /// </summary>
    public interface INodeModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The models name
        /// </summary>
        string ModelName { get; }

        /// <summary>
        /// The models full path name
        /// </summary>
        string ModelFullName { get; }

        /// <summary>
        /// Refresh all the bindings
        /// </summary>
        void Refresh();


        /// <summary>
        /// Load from a repository
        /// </summary>
        bool Load(IRepository repository);

        /// <summary>
        /// Save to a new a repository
        /// </summary>
        bool SaveAs(IRepository repository);

        /// <summary>
        /// Save to current repository
        /// </summary>
        bool Save();

        /// <summary>
        /// Reload from current repository
        /// </summary>
        bool Reload();

        ISelector GetMetadataSelector();
        ISelector GetModelingSelector();
    }
}
