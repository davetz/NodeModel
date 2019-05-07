using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;

using Windows.Foundation;

namespace NodeModel
{
    public interface INode : INotifyPropertyChanged
    {
        /// <summary>
        /// The nodes center point x,y position
        /// </summary>
        Vector2 CenterPoint { get; set; }

        /// <summary>
        /// The rectangle enclosing the node 
        /// </summary>
        Rect BoundingRect { get; }

        /// <summary>
        /// The current list of input connections
        /// </summary>
        ObservableCollection<IEdge> InputEdges { get; }

        /// <summary>
        /// The current list of output connections
        /// </summary>
        ObservableCollection<IEdge> OutputEdges { get; }

        /// <summary>
        /// All posible input edge types
        /// </summary>
        ObservableCollection<IEdgeType> AllInputEdgeTypes { get; }

        /// <summary>
        /// All posible output edge types
        /// </summary>
        ObservableCollection<IEdgeType> AllOutputEdgeTypes { get; }

        /// <summary>
        /// The current list of unused input edge types
        /// </summary>
        ObservableCollection<IEdgeType> UnusedInputEdgeTypes { get; }

        /// <summary>
        /// The current list of unused input edge types
        /// </summary>
        ObservableCollection<IEdgeType> UnusedOutputEdgeTypes { get; }

        /// <summary>
        /// The nodeType that owns this node
        /// </summary>
        INodeType NodeType { get; }

        /// <summary>
        /// Remove (unlink) the giveng edge
        /// </summary>
        bool RemoveEdge(IEdge edge);
    }
}
