using ST.EplAddin.ComparisonOfProjectProperties.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ST.EplAddin.ComparisonOfProjectProperties.Converters
{
    internal class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ChangesRecord changesRecord = new ChangesRecord();
            var recordList = changesRecord.GetChangesList();
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
