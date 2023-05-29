// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления ПЛУ линии.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsPlusViewModel : WsBaseViewModel, INotifyPropertyChanged
{
    #region Public and private fields, properties, constructor

    public WsSqlPluScaleModel PluLine { get; set; } = new();

    #endregion
}