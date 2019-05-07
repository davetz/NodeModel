using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;

namespace NodeModel
{
    /// <summary>
    /// Usage:  var model =  NodeModeCore.Create();
    /// </summary>
    public interface INodeModel : INotifyPropertyChanged
    {
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

        /// <summary>
        /// Create a new INodeType instance
        /// </summary>
        /// <returns></returns>
        INodeType CreateNodeType();

        /// <summary>
        /// Delete the given INodeType 
        /// </summary>
        /// <returns></returns>
        bool DeleteNodeType(INodeType nodeType);


        /// <summary>
        /// Current list of all instantiated INodes (can be used to draw all nodes)
        /// </summary>
        ObservableCollection<INode> AllNodes { get; }

        /// <summary>
        /// Current list of all instantiated IEdges (can be used to draw all edges)
        /// </summary>
        ObservableCollection<IEdge> AllEdges { get; }

        /// <summary>
        /// Current list of all instantiated INodeTypes
        /// </summary>
        ObservableCollection<INodeType> AllNodeTypes { get; }
    }
}
