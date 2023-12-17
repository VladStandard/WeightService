using System.ComponentModel;
using System.Reflection;

namespace DeviceControl.Utils;

public static class EnumHelper
{
    public static string GetEnumDescription(Enum value)
    {
        FieldInfo? fieldInfo = value.GetType().GetField(value.ToString());
        if (fieldInfo == null) return value.ToString();
        DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
    
    public static T ToEnum<T>(this string value) => (T) Enum.Parse(typeof(T), value, true);
    
    public static T ToEnum<T>(this int value) => Enum.GetName(typeof(T), value)!.ToEnum<T>();
}