// https://stackoverflow.com/questions/19517292/adding-dynamic-buttons-in-wpf

using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers.Commands;

namespace WsDataCore.Common;

[DebuggerDisplay("{ToString()}")]
public sealed partial class ActionCommandModel : ViewModelBase
{
    #region Public and private fields, properties, constructor

    private string Name { get; }
    public Action? Action { get; private set; }
    public ICommand Cmd => new Command(Action ?? (() => { }));
    public string Content { get; private set; }
    public EnumVisibility Visibility { get; set; }
    
    public ActionCommandModel(string name, string content, EnumVisibility visibility)
    {
        Name = name;
        Action = null;
        Content = content;
        Visibility = visibility;
    }

    #endregion

    #region Public and private methods

    public override string ToString() => 
        $"{Name} | {Visibility} | {(Action is null ? "<Empty action>" : $"[{Action.GetInvocationList().Length}] actions")}";

    /// <summary>
    /// Прервать.
    /// </summary>
    [RelayCommand]
    public void Relay() => Action?.Invoke();

    /// <summary>
    /// Настройка действий.
    /// </summary>
    public void AddActions(List<Action> actions)
    {
        Action = null;
        if (!actions.Any()) return;
        actions.ForEach(AddAction);
    }

    /// <summary>
    /// Добавить действие.
    /// </summary>
    public void AddAction(Action action)
    {
        if (Action is null || (Action is not null && Action.GetInvocationList().Length == 0))
        {
            Action = action;
            return;
        }
        if (Action is not null && Action.GetInvocationList().Length > 0)
        {
            IEnumerable<string> namesExists = Action.GetInvocationList().Select(item => item.Method.Name);
            if (!namesExists.Contains(action.Method.Name))
                Action += action;
        }
    }

    #endregion
}