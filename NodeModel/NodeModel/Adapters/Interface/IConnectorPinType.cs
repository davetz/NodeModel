using System.ComponentModel;
using System.Numerics;

using Windows.Foundation;

namespace NodeModel
{
    public interface IConnectorPinType : INotifyPropertyChanged
    {
        /// <summary>
        /// Name assign to the PinType
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Optional tooltip text of the PinType
        /// </summary>
        string ToolTip { get; set; }

        /// <summary>
        /// Optional description of the PinType
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Optional size (Width, Height) of the PinType
        /// </summary>
        Size Size { get; set; }

        /// <summary>
        /// EdgeType owner
        /// </summary>
        IEdgeType EdgeType { get; }


        bool IsInputPin { get; }

        bool IsOutputPin { get; }


        bool CanSetStartingPoint(Vector2 location);
        bool SetStartingPoint(Vector2 location);


        bool CanSetEndingPoint(Vector2 location);
        bool SetEndingPoint(Vector2 location);


        bool CanMoveTo(Vector2 location);
        bool MoveTo(Vector2 location);



        Vector2 CenterPoint { get; }
        Size TargetSize { get; set; }
    }
}
