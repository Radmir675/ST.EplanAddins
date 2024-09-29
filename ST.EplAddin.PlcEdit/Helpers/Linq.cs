using System.Collections.Generic;
using System.Linq;

namespace ST.EplAddin.PlcEdit.Helpers
{
    public static class Linq
    {
        public static IEnumerable<CsvFileDataModelView> SkipLast(this IEnumerable<CsvFileDataModelView> items, int count)
        {
            var itemsArray = items.ToArray();
            var result = new List<CsvFileDataModelView>();

            if (count >= itemsArray.Length)
            {
                return result;
            }

            for (int i = 0; i < itemsArray.Length - count; i++)
            {
                result.Add(itemsArray[i]);
            }
            return result;
        }
    }
}
