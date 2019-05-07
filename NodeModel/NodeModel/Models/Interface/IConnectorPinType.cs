using System.ComponentModel;
using System.Numerics;

using Windows.Foundation;

namespace NodeModel
{
    /// <summary>
    /// Graphicaly place/move an edgeType's connector pins
    /// </summary>
    public interface IConnectorPinType : INotifyPropertyChanged
    {
        /// <summary>
        /// EdgeType owner
        /// </summary>
        IEdgeType EdgeType { get; }


        bool IsInputPin { get; }

        bool IsOutputPin { get; }


        bool CanSetStartintPoint(Vector2 location);
        bool SetStartingPoint(Vector2 location);


        bool CanSetEndingPoint(Vector2 location);
        bool SetEndingPoint(Vector2 location);


        bool CanMoveTo(Vector2 location);
        bool MoveTo(Vector2 location);



        Vector2 CenterPoint { get; }
        Size TargetSize { get; set; }
    }
}
