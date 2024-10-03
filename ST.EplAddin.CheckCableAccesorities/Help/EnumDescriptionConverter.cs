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
            if (value is ProductGroupType format)
            {
                return GetString(format);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                return Enum.Parse(typeof(ProductGroupType), s.Substring(0, s.IndexOf(':')));
            }
            return null;
        }

        public string[] Strings => GetStrings();

        public static string GetString(ProductGroupType format)
        {
            return format.ToString() + ": " + GetDescription(format);
        }

        public static string GetDescription(ProductGroupType format)
        {
            return format.GetType().GetMember(format.ToString())[0].GetCustomAttribute<DescriptionAttribute>().Description;

        }
        public static string[] GetStrings()
        {
            List<string> list = new List<string>();
            foreach (ProductGroupType format in Enum.GetValues(typeof(ProductGroupType)))
            {
                list.Add(GetString(format));
            }

            return list.ToArray();
        }
        //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        if (value is Enum enumValue)
        //        {
        //            var result = enumValue.GetDescription();
        //            return result;
        //        }
        //        return value;
        //    }

        //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        {
        //            if (value == null) return null;
        //            foreach (Enum one in Enum.GetValues(parameter as Type))
        //            {
        //                if (value.ToString() == one.GetDescription())
        //                    return one;
        //            }
        //            return null;
        //        }
        //    }
        //}
        //public static class EnumExtensions
        //{
        //    public static string GetDescription(this Enum value)
        //    {
        //        FieldInfo field = value.GetType().GetField(value.ToString());
        //        DescriptionAttribute attribute = (DescriptionAttribute)field.GetCustomAttribute(typeof(DescriptionAttribute));
        //        var result = attribute != null ? attribute.Description : value.ToString();
        //        return result;
        //    }
    }
}
