// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MemberCanBeProtected.Global

namespace WsDataCore.Common;

/// <summary>
/// Базовый класс модели представления MVVM INotifyPropertyChanged.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public class WsViewModelBase : ObservableObject
{
    #region Public and private fields, properties, constructor

    public Guid Uid { get; set; }

    public WsViewModelBase()
    {
        Uid = Guid.NewGuid();
    }

    /// Не менять модификатор доступа!
    public WsViewModelBase(WsViewModelBase item)
    {
        Uid = item.Uid;
    }

    #endregion

    #region Public and private methods

    public override string ToString() => Uid.ToString();

    #endregion
}