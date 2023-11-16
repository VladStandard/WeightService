namespace WsDataCore.Common;

public interface IViewModel : INotifyPropertyChanged
{
    void UpdateCommandsFromActions();
    void SetCommands(ObservableCollection<ActionCommandModel> commands);
}