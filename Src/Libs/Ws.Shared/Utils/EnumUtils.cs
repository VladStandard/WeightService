using System.ComponentModel;
using System.Reflection;

namespace Ws.Shared.Utils;

public static class EnumUtils
{
    public static string GetEnumDescription(Enum value)
    {
        FieldInfo? fieldInfo = value.GetType().GetField(value.ToString());
        if (fieldInfo == null) return value.ToString();
        DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }

    public static T GetValueFromDescription<T>(string description) where T : Enum
    {
        foreach (FieldInfo field in typeof(T).GetFields())
        {
            DescriptionAttribute? attribute =
                Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if ((attribute != null && attribute.Description == description) || field.Name == description)
                return (T)field.GetValue(null)!;
        }
        return default!;
    }

    public static T ToEnum<T>(this string value) => (T)Enum.Parse(typeof(T), value, true);

    public static T ToEnum<T>(this int value) => Enum.GetName(typeof(T), value)!.ToEnum<T>();
}