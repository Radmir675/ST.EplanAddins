using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace ST.EplAddin.FootNote.Converters
{
    internal class AlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var elementName = (value as RadioButton).Name;
            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
