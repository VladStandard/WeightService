// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Models;

public sealed class WsButtonVisibilityModel : BaseViewModel, INotifyPropertyChanged
{
	#region Public and private fields and properties

	public string ButtonAbortContent { get; private set; }
	public string ButtonCancelContent { get; private set; }
    public string ButtonCustomContent { get; private set; }
    public string ButtonIgnoreContent { get; private set; }
	public string ButtonNoContent { get; private set; }
	public string ButtonOkContent { get; private set; }
	public string ButtonRetryContent { get; private set; }
	public string ButtonYesContent { get; private set; }
	public Visibility ButtonAbortVisibility { get; private set; }
	public Visibility ButtonCancelVisibility { get; private set; }
	public Visibility ButtonCustomVisibility { get; private set; }
    public Visibility ButtonIgnoreVisibility { get; private set; }
	public Visibility ButtonNoVisibility { get; init; }
	public Visibility ButtonOkVisibility { get; set; }
	public Visibility ButtonRetryVisibility { get; private set; }
	public Visibility ButtonYesVisibility { get; init; }

	#endregion

	#region Constructor and destructor

	public WsButtonVisibilityModel()
	{
		ButtonAbortVisibility = Visibility.Hidden;
		ButtonCancelVisibility = Visibility.Hidden;
		ButtonCustomVisibility = Visibility.Hidden;
		ButtonIgnoreVisibility = Visibility.Hidden;
		ButtonNoVisibility = Visibility.Hidden;
		ButtonOkVisibility = Visibility.Hidden;
		ButtonRetryVisibility = Visibility.Hidden;
		ButtonYesVisibility = Visibility.Hidden;

		Localization();
	}

	public void Localization()
	{
		ButtonAbortContent = LocaleCore.Buttons.Abort;
		ButtonCancelContent = LocaleCore.Buttons.Cancel;
		ButtonCustomContent = LocaleCore.Buttons.Custom;
		ButtonIgnoreContent = LocaleCore.Buttons.Ignore;
		ButtonNoContent = LocaleCore.Buttons.No;
		ButtonOkContent = LocaleCore.Buttons.Ok;
		ButtonRetryContent = LocaleCore.Buttons.Retry;
		ButtonYesContent = LocaleCore.Buttons.Yes;
	}

	#endregion
}