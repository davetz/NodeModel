using System.ComponentModel;
using System.Numerics;

using Windows.Foundation;

namespace NodeModel
{
    public interface IConnectorPinType : INotifyPropertyChanged
    {
        /// <summary>
        /// The name of this connector pin
        /// </summary>
        string Name { get; set; }

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
