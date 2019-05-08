using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;

using Windows.Foundation;

namespace NodeModel
{
    public interface INodeType : INotifyPropertyChanged
    {
        // NodeType Properties
        //=====================================================================
        /// <summary>
        /// Name assign to the nodeType
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Optional tooltip text of the nodeType
        /// </summary>
        string ToolTip { get; set; }

        /// <summary>
        /// Optional description of the nodeType
        /// </summary>
        string Description { get; set; }


        // NodeType Graphic Diagram Properties
        //=====================================================================
        // You can create the nodeModel metadate graphicly
        // using pallets, drag/drop, pickerTools, propertyBoxes,.. etc
        /// <summary>
        /// Graphics location of the nodeType on metadata diagram
        /// </summary>
        Vector2 CenterPoint { get; set; }

        /// <summary>
        /// Graphics rectangle bounding the nodeType 
        /// </summary>
        Rect BoundingRect { get; set; }






        // Node PropertyType
        //=====================================================================
        /// <summary>
        /// Create a new propertyType
        /// </summary>
        IEdgeType CreatePropertyType(INodeType destinationNodeType);

        /// <summary>
        /// Delete the given propertyType
        /// </summary>
        IEdgeType DeleteProperyType(IPropertyType properyType);




        // Node Input/Output EdgeTypes
        //=====================================================================
        /// <summary>
        /// Create a new output edgeType from current nodeType to given destination nodeType
        /// </summary>
        IEdgeType CreateOutputEdgeType(INodeType to_NodeType);

        /// <summary>
        /// Create a new input edgeType from current nodeType to given destination nodeType
        /// </summary>
        IEdgeType CreateInputEdgeType(INodeType to_NodeType);

        /// <summary>
        /// Delete this edgeType (assuming it belongs to this nodeType)
        /// </summary>
        bool DeleteEdgeType(IEdgeType edgeType);




        // PropertyType, InputeEdgeType, and OutputEdgeType  Collections
        //=====================================================================
        /// <summary>
        /// Return the current list of propertyType definitions for this nodeType
        /// </summary>
        ObservableCollection<IPropertyType> PropertyTypes { get; }

        /// <summary>
        /// Return the current list of input edgeType definitions for this nodeType
        /// </summary>
        ObservableCollection<IEdgeType> InputEdgeTypes { get; }

        /// <summary>
        /// Return the current list of output edgeType definitions for this nodeType
        /// </summary>
        ObservableCollection<IEdgeType> OutputEdgeTypes { get; }
    }
}
