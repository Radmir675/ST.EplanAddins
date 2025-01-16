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
            if (value is Shape shape)
                return (int)shape;
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            switch (value as int?)
            {
                case 0: return Shape.Circle;
                default: return Shape.Arrow;
            }

        }
    }
}
