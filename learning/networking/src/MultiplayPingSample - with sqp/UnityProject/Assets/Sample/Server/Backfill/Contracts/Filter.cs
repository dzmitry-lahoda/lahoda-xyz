using System;

namespace DefaultNamespace
{
    /// <summary>
    /// A configuration contract for applying general-purpose filters against range indexes against a pool
    /// </summary>
    public class Filter
    {
        public Filter()
        {

        }

        public Filter(string attributeName, double min, double max)
        {
            Attribute = attributeName;
            Min = min;
            Max = max;
        }
        /// <summary>
        /// The index to query against
        /// </summary>
        public string Attribute { get; set; }

        // The maximum of the range to be considered (inclusive)
        public double Max { get; set; }

        // The minimum of the range to be considered (exclusive)
        public double Min { get; set; }
    }
}
