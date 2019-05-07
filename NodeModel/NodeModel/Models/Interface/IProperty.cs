using System;

namespace NodeModel
{
    public interface IProperty
    {
        /// <summary>
        /// Name of this property, from propertyType
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Optional tooltip text, from propertyType
        /// </summary>
        string ToolTip { get; }

        /// <summary>
        /// Optional property descripton, from propertyType
        /// </summary>
        string Description { get; }

        
        /// <summary>
        /// String representation of the property's value
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// String representation of the property's type
        /// </summary>
        string ValueType { get; }


        /// <summary>
        /// The Node that owns this property
        /// </summary>
        INode Node { get; }

        //==================================================================
        // Optionally you can work with the actual data values.
        // However, please be consistant with specified ValueType
        // for example don't try to shove a DateTime value into a bool
        // Although most resaonable conversions do work, for example
        // If ValueType is Byte setting its DoubleValue to 254.49 is ok
        //==================================================================

        /// <summary>
        /// Property as a boolean value (if that makes sense)
        /// </summary>
        bool BoolValue { get; set; }

        /// <summary>
        /// Property as a boolean array (if that makes sense)
        /// </summary>
        bool[] BoolArray { get; set; }

        /// <summary>
        /// Property as an integer value (if that makes sense)
        /// </summary>
        int IntValue { get; set; }

        /// <summary>
        /// Property as an integer array (if that makes sense)
        /// </summary>
        int[] IntArray { get; set; }

        /// <summary>
        /// Property as an long value (if that makes sense)
        /// </summary>
        long LongValue { get; set; }

        /// <summary>
        /// Property as an long array (if that makes sense)
        /// </summary>
        long[] LongArray { get; set; }

        /// <summary>
        /// Property as an double value (if that makes sense)
        /// </summary>
        double DoubleValue { get; set; }

        /// <summary>
        /// Property as an double array (if that makes sense)
        /// </summary>
        double[] DoubleArray { get; set; }

        /// <summary>
        /// Property as a string value (if that makes sense)
        /// </summary>
        string StringValue { get; set; }

        /// <summary>
        /// Property as a string array (if that makes sense)
        /// </summary>
        string[] StringArray { get; set; }

        /// <summary>
        /// Property as a DateTime value (if that makes sense)
        /// </summary>
        DateTime DateTimeValue { get; set; }

        /// <summary>
        /// Property as a DateTime array (if that makes sense)
        /// </summary>
        DateTime[] DateTimeArray { get; set; }
    }
}
