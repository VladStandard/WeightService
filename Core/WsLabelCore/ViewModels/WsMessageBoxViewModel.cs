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
    public double FontSizeMessage => 26;
    public double FontSizeButton => 24;

    public WsMessageBoxViewModel()
    {
        //
    }

    public WsMessageBoxViewModel(string message,
        WsActionCommandModel cmdAbort, WsActionCommandModel cmdCancel, WsActionCommandModel cmdCustom,
        WsActionCommandModel cmdIgnore, WsActionCommandModel cmdNo, WsActionCommandModel cmdOk,
        WsActionCommandModel cmdRetry, WsActionCommandModel cmdYes)
    {
        Setup(message, cmdAbort, cmdCancel, cmdCustom, cmdIgnore, cmdNo, cmdOk, cmdRetry, cmdYes);
    }

    public WsMessageBoxViewModel(string message,
        Action actionAbort, Action actionCancel, Action actionCustom, Action actionIgnore, Action actionNo,
        Action actionOk, Action actionRetry, Action actionYes, Action actionBack)
    {
        Setup(message, actionAbort, actionCancel, actionCustom, actionIgnore, actionNo, actionOk, actionRetry, actionYes, actionBack);
    }

    #endregion

    #region Public and private methods

    public override string ToString() => string.IsNullOrEmpty(Message) ? $"{base.ToString()}" : $"{base.ToString()} | {Message}";

    public void Setup(string message,
        WsActionCommandModel cmdAbort, WsActionCommandModel cmdCancel, WsActionCommandModel cmdCustom, 
        WsActionCommandModel cmdIgnore, WsActionCommandModel cmdNo, WsActionCommandModel cmdOk, 
        WsActionCommandModel cmdRetry, WsActionCommandModel cmdYes)
    {
        Message = message;
        Setup(cmdAbort, cmdCancel, cmdCustom, cmdIgnore, cmdNo, cmdOk, cmdRetry, cmdYes);
    }

    public void Setup(string message, 
        Action actionAbort, Action actionCancel, Action actionCustom, Action actionIgnore, Action actionNo,
        Action actionOk, Action actionRetry, Action actionYes, Action actionBack)
    {
        Message = message;
        actionAbort += actionBack;
        actionCancel += actionBack;
        actionCustom += actionBack;
        actionIgnore += actionBack;
        actionNo += actionBack;
        actionOk += actionBack;
        actionRetry += actionBack;
        actionYes += actionBack;
        Setup(actionAbort, actionCancel, actionCustom, actionIgnore, actionNo, actionOk, actionRetry, actionYes);
    }

    public void SetupNoYes(string message, Action actionNo, Action actionYes, Action actionBack)
    {
        Message = message;
        actionNo += actionBack;
        actionYes += actionBack;
        SetupNoYes(actionNo, actionYes);
    }

    public void SetupOk(string message, Action actionOk)
    {
        Message = message;
        SetupOk(actionOk);
    }

    #endregion
}