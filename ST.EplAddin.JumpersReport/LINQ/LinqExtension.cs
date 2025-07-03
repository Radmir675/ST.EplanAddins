using System.Collections.Generic;
using System.Linq;

namespace ST.EplAddin.JumpersReport.LINQ;

internal class LinqExtension
{
    public static IEnumerable<IEnumerable<JumperConnection>> GroupBy(List<JumperConnection> connections)
    {
        var groups = new List<List<JumperConnection>>();

        var enumerator = connections.GetEnumerator();
        var list = new List<JumperConnection>();
        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;
            if (!list.Any())
            {
                list.Add(current);
                continue;
            }

            if (list.Contains(current, new Comparator()))
            {
                list.Add(current);
                continue;
            }

            else
            {
                groups.Add(list);
                list = new();
                list.Add(current);
                continue;
            }
        }
        groups.Add(list);
        return groups;
    }
}