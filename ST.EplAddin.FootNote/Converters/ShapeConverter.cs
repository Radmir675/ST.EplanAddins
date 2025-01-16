using System;
using System.Globalization;
using System.Windows.Data;
using Shape = ST.EplAddin.FootNote.Models.Shape;

namespace ST.EplAddin.FootNote.Converters
{
    internal class ShapeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Shape shape) return null;
            return (int)shape;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as int?) == 0 ? Shape.Circle : Shape.Arrow;
        }
    }
}
