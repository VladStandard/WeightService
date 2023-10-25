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