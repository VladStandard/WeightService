// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления пин кода.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public class WsXamlPinCodeViewModel : WsXamlBaseViewModel, INotifyPropertyChanged
{
    #region Public and private fields, properties, constructor

    public WsXamlPinCodeViewModel()
    {
        FormUserControl = WsEnumFormUserControl.PinCode;
    }

    #endregion
}