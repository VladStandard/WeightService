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
public sealed partial class WsActionCommandModel : WsMvvmBase
{
    #region Public and private fields, properties, constructor

    public string Name { get; private set; } = "";
    public Action? Action { get; private set; }
    public ICommand Cmd => new Command(Action ?? (() => { }));
    public string Content { get; private set; } = "";
    public Visibility Visibility { get; private set; } = Visibility.Hidden;
    
    public WsActionCommandModel(string name, string content, Visibility visibility)
    {
        SetupEmpty(name, content, visibility);
    }

    public WsActionCommandModel(string name, Action action, string content, Visibility visibility)
    {
        Setup(name, action, content, visibility);
    }

    #endregion

    #region Public and private methods

    public override string ToString() => $"{Name} | {Content} | {Visibility} | {(Action is null ? "<Empty action>" : Action)}";

    /// <summary>
    /// Прервать.
    /// </summary>
    [RelayCommand]
    public void Relay() => Action?.Invoke();

    /// <summary>
    /// Настройка.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    /// <param name="content"></param>
    /// <param name="visibility"></param>
    public void Setup(string name, Action action, string content, Visibility visibility)
    {
        Name = name;
        Action = action;
        Content = content;
        Visibility = visibility;
    }

    /// <summary>
    /// Настройка.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="content"></param>
    /// <param name="visibility"></param>
    public void SetupEmpty(string name, string content, Visibility visibility)
    {
        Name = name;
        Action = null;
        Content = content;
        Visibility = visibility;
    }

    /// <summary>
    /// Настройка действий.
    /// </summary>
    /// <param name="actions"></param>
    public void Setup(List<Action> actions)
    {
        Action = null;
        if (!actions.Any()) return;
        actions.ForEach(action => Action += action);
    }

    /// <summary>
    /// Настройка первого действия.
    /// </summary>
    /// <param name="action"></param>
    public void Setup1Action(Action action)
    {
        Action = null;
        Action = action;
    }

    /// <summary>
    /// Добавить действие.
    /// </summary>
    /// <param name="action"></param>
    public void AddAction(Action action) => Action += action;

    #endregion
}