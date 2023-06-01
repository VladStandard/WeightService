// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления диалога.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsXamlDialogViewModel : WsXamlBaseViewModel, INotifyPropertyChanged
{
    #region Public and private fields, properties, constructor

    public WsXamlDialogViewModel()
    {
        FormUserControl = WsEnumFormUserControl.Dialog;
    }

    #endregion
}