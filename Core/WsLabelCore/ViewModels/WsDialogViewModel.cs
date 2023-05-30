// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления диалога.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsDialogViewModel : WsXamlBaseViewModel, INotifyPropertyChanged
{
    #region Public and private fields, properties, constructor

    public WsDialogViewModel()
    {
        //
    }

    #endregion
}