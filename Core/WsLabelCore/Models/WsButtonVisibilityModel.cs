// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmHelpers.Commands;

namespace WsLabelCore.Models;

public sealed class WsButtonVisibilityModel : BaseViewModel, INotifyPropertyChanged
{
    #region Public and private fields and properties

    public string ButtonAbortContent => LocaleCore.Buttons.Abort;
    public string ButtonCancelContent => LocaleCore.Buttons.Cancel;
    public string ButtonCustomContent => LocaleCore.Buttons.Custom;
    public string ButtonIgnoreContent => LocaleCore.Buttons.Ignore;
    public string ButtonNoContent => LocaleCore.Buttons.No;
    public string ButtonOkContent => LocaleCore.Buttons.Ok;
    public string ButtonRetryContent => LocaleCore.Buttons.Retry;
    public string ButtonYesContent => LocaleCore.Buttons.Yes;
    public Visibility ButtonAbortVisibility { get; set; }
    public Visibility ButtonIgnoreVisibility { get; set; }
    public Visibility ButtonCancelVisibility { get; set; }
    public Visibility ButtonCustomVisibility { get; set; }
    public Visibility ButtonNoVisibility { get; set; }
    public Visibility ButtonOkVisibility { get; set; }
    public Visibility ButtonRetryVisibility { get; set; }
    public Visibility ButtonYesVisibility { get; set; }
    public ObservableCollection<WsButtonCommandModel> Commands { get; }

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
        Commands = new();
    }

    #endregion

    #region Public and private methods

    public void Setup(WsButtonVisibilityModel buttonVisibility, Action actionOk, Action actionCancel)
    {
        ButtonAbortVisibility = buttonVisibility.ButtonAbortVisibility;
        ButtonCancelVisibility = buttonVisibility.ButtonCancelVisibility;
        ButtonCustomVisibility = buttonVisibility.ButtonCustomVisibility;
        ButtonIgnoreVisibility = buttonVisibility.ButtonIgnoreVisibility;
        ButtonNoVisibility = buttonVisibility.ButtonNoVisibility;
        ButtonOkVisibility = buttonVisibility.ButtonOkVisibility;
        ButtonRetryVisibility = buttonVisibility.ButtonRetryVisibility;
        ButtonYesVisibility = buttonVisibility.ButtonYesVisibility;

        Commands.Clear();
        //foreach (WsButtonCommandModel command in commands) Commands.Add(command);
        if (ButtonAbortVisibility.Equals(Visibility.Visible))
            Commands.Add(new(ButtonAbortContent, new Command(actionOk)));
        if (ButtonCancelVisibility.Equals(Visibility.Visible))
            Commands.Add(new(ButtonCancelContent, new Command(actionCancel)));
        if (ButtonCustomVisibility.Equals(Visibility.Visible))
            Commands.Add(new(ButtonCustomContent, new Command(actionOk)));
        if (ButtonIgnoreVisibility.Equals(Visibility.Visible))
            Commands.Add(new(ButtonIgnoreContent, new Command(actionOk)));
        if (ButtonNoVisibility.Equals(Visibility.Visible))
            Commands.Add(new(ButtonNoContent, new Command(actionOk)));
        if (ButtonOkVisibility.Equals(Visibility.Visible))
            Commands.Add(new(ButtonOkContent, new Command(actionOk)));
        if (ButtonRetryVisibility.Equals(Visibility.Visible))
            Commands.Add(new(ButtonRetryContent, new Command(actionOk)));
        if (ButtonYesVisibility.Equals(Visibility.Visible))
            Commands.Add(new(ButtonYesContent, new Command(actionOk)));
    }

    #endregion
}