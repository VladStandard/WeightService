// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

#nullable enable
public sealed class WsMessageBoxViewModel : WsWpfBaseViewModel
{
	#region Public and private fields and properties

	public string Message { get; set; }
    public Visibility MessageVisibility => !string.IsNullOrEmpty(Message) ? Visibility.Visible : Visibility.Hidden;
    public double FontSizeCaption => 30;
    public double FontSizeMessage => 26;
    public double FontSizeButton => 24;
    public WsButtonVisibilityModel ButtonVisibility { get; }

    public WsMessageBoxViewModel()
	{
		Message = string.Empty;
		ButtonVisibility = new();
	}

    #endregion

    #region Public and private methods

    public void Setup(string message, WsButtonVisibilityModel buttonVisibility,
        Action actionOk, Action actionCancel, Action actionDefault)
    {
        Message = message;
        
        ButtonVisibility.ButtonAbortVisibility = buttonVisibility.ButtonAbortVisibility;
        ButtonVisibility.ButtonCancelVisibility = buttonVisibility.ButtonCancelVisibility;
        ButtonVisibility.ButtonCustomVisibility = buttonVisibility.ButtonCustomVisibility;
        ButtonVisibility.ButtonIgnoreVisibility = buttonVisibility.ButtonIgnoreVisibility;
        ButtonVisibility.ButtonNoVisibility = buttonVisibility.ButtonNoVisibility;
        ButtonVisibility.ButtonOkVisibility = buttonVisibility.ButtonOkVisibility;
        ButtonVisibility.ButtonRetryVisibility = buttonVisibility.ButtonRetryVisibility;
        ButtonVisibility.ButtonYesVisibility = buttonVisibility.ButtonYesVisibility;
        ButtonVisibility.Localization();

        ActionReturnOk = actionOk;
        ActionReturnOk += actionDefault;
        ActionReturnCancel = actionCancel;
        ActionReturnCancel += actionDefault;
    }

    #endregion
}