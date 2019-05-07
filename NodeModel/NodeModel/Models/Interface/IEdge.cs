using System.ComponentModel;
using System.Numerics;

namespace NodeModel
{
    public interface IEdge : INotifyPropertyChanged
    {
        // Draw the edge from XYPin1 to XYPin2
        //=======================================

        /// <summary>
        /// The x,y graphcs location of Node1 connector pin
        /// </summary>
        Vector2 XYPin1 { get; }

        /// <summary>
        /// The x,y graphcs location of Node2 connector pin
        /// </summary>
        Vector2 XYPin2 { get; }

        //=====================================================================
        // IEdge is built on the fly from the perspective of the reference Node.
        //
        // It presents a diretional path (up the parent tree) or (down the child tree)
        // You can travers the graph by first getting the list of output edges at a
        // given node, and then from each of those edges travers to the next node
        // given by edge.OtherNode.
        //
        // use node.OutputEdges to traverse the child tree 
        // use node.InputEdges to traverse the parent tree
        //=================================================================

        /// <summary>
        /// This is the reference node (independant of parent/child context)
        /// </summary>
        INode RefNode { get; }

        /// <summary>
        /// This is the node at the other end of this Edge (independant of parent/child context)
        /// </summary>
        INode OtherNode { get; }
    }
}
