// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления сообщений.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsMessageBoxViewModel : WsViewModelBase, INotifyPropertyChanged
{
    #region Public and private fields and properties

    public string Message { get; set; } = "";
    public Visibility MessageVisibility => !string.IsNullOrEmpty(Message) ? Visibility.Visible : Visibility.Hidden;

    #endregion

    #region Public and private methods

    public override string ToString() => string.IsNullOrEmpty(Message) ? $"{base.ToString()}" : $"{base.ToString()} | {Message}";

    public void Setup(string message,
        WsActionCommandModel cmdAbort, WsActionCommandModel cmdCancel, WsActionCommandModel cmdCustom, 
        WsActionCommandModel cmdIgnore, WsActionCommandModel cmdNo, WsActionCommandModel cmdOk, 
        WsActionCommandModel cmdRetry, WsActionCommandModel cmdYes, int controlWidth)
    {
        Message = message;
        SetupCommands(cmdAbort, cmdCancel, cmdCustom, cmdIgnore, cmdNo, cmdOk, cmdRetry, cmdYes);
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Настройка действий.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="actionAbort"></param>
    /// <param name="actionCancel"></param>
    /// <param name="actionCustom"></param>
    /// <param name="actionIgnore"></param>
    /// <param name="actionNo"></param>
    /// <param name="actionOk"></param>
    /// <param name="actionRetry"></param>
    /// <param name="actionYes"></param>
    /// <param name="actionBack"></param>
    /// <param name="controlWidth"></param>
    public void SetupActions(string message, 
        Action actionAbort, Action actionCancel, Action actionCustom, Action actionIgnore, Action actionNo,
        Action actionOk, Action actionRetry, Action actionYes, Action actionBack, int controlWidth)
    {
        Message = message;
        SetupActions(actionAbort, actionCancel, actionCustom, actionIgnore, actionNo, actionOk, actionRetry, actionYes, 
            actionBack, controlWidth);
    }

    public void SetupButtonsNoYes(string message, Action actionNo, Action actionYes, Action actionBack, int controlWidth)
    {
        Message = message;
        SetupButtonsNoYes(actionNo, actionYes, actionBack, controlWidth);
    }

    public void SetupButtonsOk(string message, Action actionOk, int controlWidth)
    {
        Message = message;
        SetupButtonsOk(actionOk, controlWidth);
    }

    #endregion
}