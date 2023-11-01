namespace WsDataCore.Common;

public interface IWsViewModel : INotifyPropertyChanged
{
    void UpdateCommandsFromActions();
    void SetCommands(ObservableCollection<WsActionCommandModel> commands);
}