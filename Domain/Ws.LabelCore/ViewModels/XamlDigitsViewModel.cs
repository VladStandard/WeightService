using Ws.LabelCore.Common;
namespace Ws.LabelCore.ViewModels;

/// <summary>
/// XAML модель представления ввода цифр.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public class XamlDigitsViewModel : XamlBaseViewModel, IViewModel
{
    #region Public and private fields, properties, constructor

    public XamlDigitsViewModel()
    {
        FormUserControl = EnumNavigationPage.Digit;
    }

    #endregion
}