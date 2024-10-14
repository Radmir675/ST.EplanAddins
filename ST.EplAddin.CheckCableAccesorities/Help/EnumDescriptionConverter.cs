using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace ST.EplAddin.CheckCableAccesorities.Help
{
    public class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ProductGroupEnum format)
            {
                return GetString(format);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            foreach (Enum one in Enum.GetValues(typeof(ProductGroupEnum)))
            {
                if (value.ToString() == one.GetDescription())
                    return one;
            }
            return null;
        }

        public string[] Strings => GetStrings();

        public static string GetString(ProductGroupEnum format)
        {
            return GetDescription(format);
        }

        public static string GetDescription(ProductGroupEnum format)
        {
            return format.GetType().GetMember(format.ToString())[0].GetCustomAttribute<DescriptionAttribute>().Description;
        }
        public static string[] GetStrings()
        {
            List<string> list = new List<string>();
            foreach (ProductGroupEnum format in Enum.GetValues(typeof(ProductGroupEnum)))
            {
                list.Add(GetString(format));
            }
            return list.ToArray();
        }
    }
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = (DescriptionAttribute)field.GetCustomAttribute(typeof(DescriptionAttribute));
            var result = attribute != null ? attribute.Description : value.ToString();
            return result;
        }
    }
}
