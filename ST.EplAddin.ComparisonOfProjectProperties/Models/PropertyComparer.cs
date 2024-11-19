using System.Collections.Generic;

namespace ST.EplAddin.ComparisonOfProjectProperties.Models
{
    public class PropertyComparer : IEqualityComparer<KeyValuePair<PropertyKey, Property>>
    {
        public bool Equals(KeyValuePair<PropertyKey, Property> x, KeyValuePair<PropertyKey, Property> y)
        {
            return x.Key.Id == y.Key.Id && x.Key.Index == y.Key.Index;
        }

        public int GetHashCode(KeyValuePair<PropertyKey, Property> obj)
        {
            unchecked
            {
                return 0; /*obj.Key.GetHashCode();*/
            }
        }

    }
}
