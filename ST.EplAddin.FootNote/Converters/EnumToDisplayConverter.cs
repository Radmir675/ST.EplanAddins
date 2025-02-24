using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ST.EplAddin.FootNote.Converters
{
    public class EnumToDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            var enumValue = value as Enum;
            if (enumValue != null)
            {
                var s = enumValue.GetDescriptionFromEnumValue();
                return s;
            }
            return enumValue == null ? DependencyProperty.UnsetValue : enumValue.GetDescriptionFromEnumValue();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "123";
        }
    }
}
