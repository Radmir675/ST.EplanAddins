using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Eplan.EplApi.DataModel.PropertyDefinition;

namespace ST.EplAddin.ReportSorting.Comparators
{

    class SortAscendingHelper : IComparer<StorableObject>
    {
        List<string> order = new List<string>();

        public void SetOrder(List<string> props)
        {
            order = props;
        }

        public int Compare(StorableObject x, StorableObject y)
        {
            foreach (string p in order)
            {

                if (p == string.Empty) continue;

                string pattern = @"\b[D|B]"; //char type
                string input = p;// "21101";
                Match m = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
                char propType = (m.Success) ? m.Value.First() : '\0';

                string pattern2 = @"-?\d+\b"; //digint
                Match m2 = Regex.Match(input, pattern2, RegexOptions.IgnoreCase);
                int propID = (m2.Success) ? int.Parse(m2.Value) : 0;
                bool ascending = propID >= 0;
                propID = Math.Abs(propID);

                string pattern3 = @"(?<=:)[0-9]+"; //index
                Match m3 = Regex.Match(input, pattern3, RegexOptions.IgnoreCase);
                int propIDindex = (m3.Success) ? int.Parse(m3.Value) : 0;

                int result = 0;
                
                if (x == null || !x.Properties.Exists(propID) || x.Properties[propID, propIDindex].IsEmpty)
                    result = -1;
                else
                if (y == null || !x.Properties.Exists(propID) || y.Properties[propID, propIDindex].IsEmpty)
                    result = 1;
                else
                    result = CompareBy(x, y, propID, propIDindex, propType);

                if (!ascending) result *= -1;

                if (result != 0)
                    return result;
            }

            return 0;
        }

        public int CompareBy(StorableObject x, StorableObject y, int propID, int propIDindex, char type = '\0')
        {
            try
            {
                var s1 = x.Properties[propID, propIDindex];
                var s2 = y.Properties[propID, propIDindex];

                PropertyType propType;
                switch (type)
                {
                    case 'B': propType = PropertyType.Bool; break;
                    case 'C': propType = PropertyType.Coord; break;
                    case 'D': propType = PropertyType.Double; break;
                    case 'L': propType = PropertyType.Long; break;
                    case 'M': propType = PropertyType.MultilangString; break;
                    case 'P': propType = PropertyType.Point; break;
                    case 'S': propType = PropertyType.String; break;
                    case 'T': propType = PropertyType.Time; break;
                    case 'V': propType = PropertyType.Variable; break;
                    default: propType = s1.Definition.Type; break;
                }

                int result = 0;
                switch (propType)
                {
                    case PropertyDefinition.PropertyType.Double:
                        {
                            double p1, p2 = 0;
                            if (s1.Definition.Type == PropertyType.String)
                            {
                                s1 = s1.ToString().Replace(',', '.');
                                double.TryParse(s1, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out p1);
                            }
                            else
                            {
                                p1 = s1.ToDouble();
                            }

                            if (s2.Definition.Type == PropertyType.String)
                            {

                                s2 = s2.ToString().Replace(',', '.');
                                double.TryParse(s2, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out p2);
                            }
                            else
                            {
                                p2 = s2.ToDouble();
                            }

                            result = (p1 > p2) ? 1 : (p1 < p2) ? -1 : 0;
                            break;
                        }
                    default:
                    case PropertyDefinition.PropertyType.String:
                        {
                            AlphanumComparator.AlphanumComparator ac = new AlphanumComparator.AlphanumComparator();
                            result = ac.Compare(s1.ToString(), s2.ToString());
                        }
                        break;
                }

                return result;
            }

            catch (BaseException e)
            {
                System.Diagnostics.Trace.WriteLine(e.ToString());
            }

            return 0;
        }
    }
}
