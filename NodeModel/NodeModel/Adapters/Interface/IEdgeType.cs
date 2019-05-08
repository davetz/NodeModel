using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;

namespace NodeModel
{
    public interface IEdgeType : INotifyPropertyChanged
    {
        /// <summary>
        /// Name assign to the edgeType
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Optional tooltip text of the edgeType
        /// </summary>
        string ToolTip { get; set; }

        /// <summary>
        /// Optional description of the edgeType
        /// </summary>
        string Description { get; set; }

        bool CanCreateInputPin(Vector2 location);
        IEdgeType CreateInputPin(Vector2 location);

        bool CanCreateOutputPin(Vector2 location);
        bool CreateOutputPin(Vector2 location);


        IConnectorPinType InputPinType { get; }

        IConnectorPinType OutputPinType { get; }




        //======================== Relationship type =======================================
        // The relational tree can be traversed parrent-to-child as well as child-to-parent
        // In a ManyToMany relationship a node can have multiple children of the same node type
        // as well as multiple parrents of the same node type
        // You can define many relationships and only actually use just a few depending
        // on the use case, just because the relationship is posible it doesn't has to exist

        /// <summary>
        /// The type of relationship: OneToOne, OneToMany, ManyToMany 
        /// </summary>
        string Piaring { get; set; }

        /// <summary>
        /// List of available pairing types
        /// </summary>
        IList<string> PairingTypes { get; }


        /// <summary>
        /// The relations childNodeType
        /// </summary>
        INodeType ChildNodeType { get; set; } 

        /// <summary>
        /// The relations parentNodeType
        /// </summary>
        INodeType ParentNodeType { get; set; }

    }
}
