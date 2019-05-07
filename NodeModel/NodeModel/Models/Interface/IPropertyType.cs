using System.Collections.Generic;
using System.ComponentModel;

namespace NodeModel
{
    public interface IPropertyType : INotifyPropertyChanged
    {
        /// <summary>
        /// Name assign to the propertyType
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Optional tooltip text of the propertyType
        /// </summary>
        string ToolTip { get; set; }

        /// <summary>
        /// Optional description of the propertyType
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Data type of property value
        /// </summary>
        string ValueType { get; set; }

        /// <summary>
        /// List of available value types 
        /// </summary>
        IList<string> ValueTypes { get; }
    }
}
