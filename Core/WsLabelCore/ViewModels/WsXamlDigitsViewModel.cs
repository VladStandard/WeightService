// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

/// <summary>
/// XAML модель представления ввода цифр.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public class WsXamlDigitsViewModel : WsXamlBaseViewModel, IWsViewModel
{
    #region Public and private fields, properties, constructor

    public WsXamlDigitsViewModel()
    {
        FormUserControl = WsEnumNavigationPage.PinCode;
    }

    #endregion
}