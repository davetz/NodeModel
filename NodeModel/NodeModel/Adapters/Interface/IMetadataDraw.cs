using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;

using Windows.Foundation;

namespace NodeModel
{
    /// <summary>
    /// Win2D wants to draw everything in one Invalidate event
    /// </summary>
    public interface IMetadataDraw : INotifyPropertyChanged
    {
        /// <summary>
        /// A notify on property change can be hooked up to Canvas.Invalidate(); 
        /// </summary>
        bool Invalidate { get; }

        /// <summary>
        /// All rectangles on the metadata diagram
        /// </summary>
        IList<Rect> MetadataRects { get; }
 
        /// <summary>
        /// All splines on the metadata diagram
        /// </summary>
        IList<(Vector2[] Points, bool IsDotted, byte Width, (byte A, byte R, byte G, byte B) Color)> MetadataSplines { get; }

        /// <summary>
        /// All text on the metadata diagram
        /// </summary>
        IList<(Vector2 TopLeft, string Text, (byte A, byte R, byte G, byte B) Color)> MetadataText { get; }
    }
}
