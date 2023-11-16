namespace Ws.DataCore.Common;

public interface IViewModel : INotifyPropertyChanged
{
    void UpdateCommandsFromActions();
    void SetCommands(ObservableCollection<ActionCommandModel> commands);
}