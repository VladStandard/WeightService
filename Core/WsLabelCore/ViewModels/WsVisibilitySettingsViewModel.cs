// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

public sealed class WsVisibilitySettingsViewModel : BaseViewModel
{
	#region Public and private fields and properties

	private string _buttonAbortContent;
	private string _buttonCancelContent;
	private string _buttonCustomContent;
	private string _buttonIgnoreContent;
	private string _buttonNoContent;
	private string _buttonOkContent;
	private string _buttonRetryContent;
	private string _buttonYesContent;
	private Visibility _buttonAbortVisibility;
	private Visibility _buttonCancelVisibility;
	private Visibility _buttonCustomVisibility;
	private Visibility _buttonIgnoreVisibility;
	private Visibility _buttonNoVisibility;
	private Visibility _buttonOkVisibility;
	private Visibility _buttonRetryVisibility;
	private Visibility _buttonYesVisibility;
	public string ButtonAbortContent { get => _buttonAbortContent; private set { _buttonAbortContent = value; OnPropertyChanged(); } }
	public string ButtonCancelContent { get => _buttonCancelContent; private set { _buttonCancelContent = value; OnPropertyChanged(); } }
	public string ButtonCustomContent { get => _buttonCustomContent; set { _buttonCustomContent = value; OnPropertyChanged(); } }
	public string ButtonIgnoreContent { get => _buttonIgnoreContent; private set { _buttonIgnoreContent = value; OnPropertyChanged(); } }
	public string ButtonNoContent { get => _buttonNoContent; private set { _buttonNoContent = value; OnPropertyChanged(); } }
	public string ButtonOkContent { get => _buttonOkContent; private set { _buttonOkContent = value; OnPropertyChanged(); } }
	public string ButtonRetryContent { get => _buttonRetryContent; private set { _buttonRetryContent = value; OnPropertyChanged(); } }
	public string ButtonYesContent { get => _buttonYesContent; private set { _buttonYesContent = value; OnPropertyChanged(); } }
	public Visibility ButtonAbortVisibility { get => _buttonAbortVisibility; private set { _buttonAbortVisibility = value; OnPropertyChanged(); } }
	public Visibility ButtonCancelVisibility { get => _buttonCancelVisibility; private set { _buttonCancelVisibility = value; OnPropertyChanged(); } }
	public Visibility ButtonCustomVisibility { get => _buttonCustomVisibility; set { _buttonCustomVisibility = value; OnPropertyChanged(); } }
	public Visibility ButtonIgnoreVisibility { get => _buttonIgnoreVisibility; private set { _buttonIgnoreVisibility = value; OnPropertyChanged(); } }
	public Visibility ButtonNoVisibility { get => _buttonNoVisibility; set { _buttonNoVisibility = value; OnPropertyChanged(); } }
	public Visibility ButtonOkVisibility { get => _buttonOkVisibility; set { _buttonOkVisibility = value; OnPropertyChanged(); } }
	public Visibility ButtonRetryVisibility { get => _buttonRetryVisibility; private set { _buttonRetryVisibility = value; OnPropertyChanged(); } }
	public Visibility ButtonYesVisibility { get => _buttonYesVisibility; set { _buttonYesVisibility = value; OnPropertyChanged(); } }

	#endregion

	#region Constructor and destructor

	public WsVisibilitySettingsViewModel()
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