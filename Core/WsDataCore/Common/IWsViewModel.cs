// ReSharper disable InconsistentNaming

namespace WsDataCore.Common;

#nullable enable
public interface IWsViewModel : INotifyPropertyChanged
{
    void UpdateCommandsFromActions();
    void SetCommands(ObservableCollection<WsActionCommandModel> commands);
}