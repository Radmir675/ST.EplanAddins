using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ST.EplAddin.FootNote.Converters
{
    internal static class EnumExtension
    {
        public static string GetDescriptionFromEnumValue(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = (DescriptionAttribute)field.GetCustomAttribute(typeof(DescriptionAttribute));
            var result = attribute != null ? attribute.Description : value.ToString();
            return result;
        }
        public static List<T> GetEnumList<T>()
        {
            T[] array = (T[])Enum.GetValues(typeof(T));
            List<T> list = new List<T>(array);
            return list;
        }

        /// <summary>
        /// Enumerates all enum values
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>IEnumerable containing all enum values</returns>
        /// <see cref="http://stackoverflow.com/questions/972307/can-you-loop-through-all-enum-values"/>
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
