using ST.EplAddin.FootNote.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ST.EplAddin.FootNote.Converters
{
    internal class AlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Alignment.Left)
            {
                return true;

            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
