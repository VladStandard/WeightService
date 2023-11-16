namespace WsLabelCore.Common;

/// <summary>
/// Конвертер типов Visibility/EnumVisibility.
/// </summary>
public class XamlVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is EnumVisibility visibility) return (Visibility)visibility;
        return Visibility.Hidden;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility) return (EnumVisibility)visibility;
        return EnumVisibility.Hidden;
    }
}