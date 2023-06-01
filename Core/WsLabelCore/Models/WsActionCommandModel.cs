// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://stackoverflow.com/questions/19517292/adding-dynamic-buttons-in-wpf

using MvvmHelpers.Commands;

namespace WsLabelCore.Models;

/// <summary>
/// Модель действия/команды.
/// </summary>
[DebuggerDisplay("{ToString()}")]
#nullable enable
public sealed partial class WsActionCommandModel : WsBaseMvvm
{
    #region Public and private fields, properties, constructor

    private string Name { get; }
    public Action? Action { get; private set; }
    public ICommand Cmd => new Command(Action ?? (() => { }));
    public string Content { get; private set; }
    public Visibility Visibility { get; set; }
    
    public WsActionCommandModel(string name, string content, Visibility visibility)
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
    /// <param name="actions"></param>
    public void AddActions(List<Action> actions)
    {
        Action = null;
        if (!actions.Any()) return;
        actions.ForEach(AddAction);
    }

    /// <summary>
    /// Добавить действие.
    /// </summary>
    /// <param name="action"></param>
    public void AddAction(Action action)
    {
        if (Action is null || (Action is not null && Action.GetInvocationList().Length == 0))
        {
            Action = action;
            return;
        }
        if (Action is not null && Action.GetInvocationList().Length > 0)
        {
            IEnumerable<string> names = Action.GetInvocationList().Select(item => item.Method.Name);
            if (!names.Contains(action.Method.Name)) 
                Action += action;
        }
    }

    #endregion
}