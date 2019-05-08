using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using Windows.Foundation;

namespace NodeModel
{
    public interface IConnectorPin : INotifyPropertyChanged
    {
        /// <summary>
        /// Name assign to the pin
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Optional tooltip text of the pin
        /// </summary>
        string ToolTip { get; set; }

        /// <summary>
        /// Optional description of the pin
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Graphics center point location the pin 
        /// </summary>
        Vector2 CenterPoint { get; }

        /// <summary>
        /// Graphics rectangle bounding the pin 
        /// </summary>
        Rect BoundingRect { get; }
    }
}
