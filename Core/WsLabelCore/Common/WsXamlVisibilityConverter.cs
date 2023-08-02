// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Common;

/// <summary>
/// Конвертер типов Visibility/WsEnumVisibility.
/// </summary>
public class WsXamlVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is WsEnumVisibility visibility) return (Visibility)visibility;
        return Visibility.Hidden;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility) return (WsEnumVisibility)visibility;
        return WsEnumVisibility.Hidden;
    }
}