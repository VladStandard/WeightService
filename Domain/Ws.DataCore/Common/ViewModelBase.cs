namespace Ws.DataCore.Common;

/// <summary>
/// Базовый класс модели представления MVVM INotifyPropertyChanged.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class ViewModelBase : ObservableObject
{
    #region Public and private fields, properties, constructor

    public Guid Uid { get; set; }

    public ViewModelBase()
    {
        Uid = Guid.NewGuid();
    }

    /// Не менять модификатор доступа!
    public ViewModelBase(ViewModelBase item)
    {
        Uid = item.Uid;
    }

    #endregion

    #region Public and private methods

    public override string ToString() => Uid.ToString();

    #endregion
}