// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Protocols;
using DataCore.Sql.Core;
using Radzen;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Environment = System.Environment;

namespace BlazorCore.Models;

public partial class RazorPageBase
{
    #region Public and private methods - Run actions

    protected void RunActionsInitialized(List<Action> actionsInitialized)
    {
        IsActionsInitializedFinished = false;
        RunActionsSafe(string.Empty, string.Empty, LocaleCore.Dialog.DialogResultFail, actionsInitialized);
        IsActionsInitializedFinished = true;
    }

    protected void RunActionsParametersSet(List<Action> actionsParametersSet)
    {
        IsActionsParametersSetFinished = false;
        SetPropertiesFromParent();
        RunActionsSafe(string.Empty, string.Empty, LocaleCore.Dialog.DialogResultFail, actionsParametersSet);
        IsActionsParametersSetFinished = true;
    }

    private void RunActionsSafe(string title, string success, string fail, List<Action> actions, [CallerMemberName] string memberName = "")
    {
        try
        {
            if (actions.Any())
            {
                foreach (Action action in actions)
                {
                    action.Invoke();
                }
            }
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(success))
                NotificationService?.Notify(NotificationSeverity.Success,
                    $"{LocaleCore.Action.ActionMethod}: {title}" + Environment.NewLine, success, AppSettingsHelper.Delay);
        }
        catch (Exception ex)
        {
            CatchException(ex, memberName, fail);
        }
    }

    private void RunActionsSafe(string title, string success, string fail, Action action) => 
	    RunActionsSafe(title, success, fail, new List<Action> { action });

    protected void CatchException(Exception ex, string title, string fail)
    {
        // Notify log.
        string msg = ex.Message;
        if (!string.IsNullOrEmpty(ex.InnerException?.Message))
            msg += Environment.NewLine + ex.InnerException.Message;
        if (!string.IsNullOrEmpty(fail))
        {
            if (!string.IsNullOrEmpty(msg))
                NotificationService?.Notify(NotificationSeverity.Error, title + Environment.NewLine, fail + Environment.NewLine + msg, AppSettingsHelper.Delay);
            else
                NotificationService?.Notify(NotificationSeverity.Error, title + Environment.NewLine, fail, AppSettingsHelper.Delay);
        }
        else
        {
            if (!string.IsNullOrEmpty(msg))
                NotificationService?.Notify(NotificationSeverity.Error, title + Environment.NewLine, msg, AppSettingsHelper.Delay);
        }

        // SQL log.
        AppSettings.DataAccess.LogError(ex, NetUtils.GetLocalHostName(false), nameof(BlazorCore));
    }

    private void RunActionsWithQeustion(string title, string success, string fail, string questionAdd, Action action)
    {
        try
        {
	        if (DialogService is null)
		        throw new ArgumentNullException(nameof(DialogService));

            string question = string.IsNullOrEmpty(questionAdd) ? LocaleCore.Dialog.DialogQuestion : questionAdd;
			Task<bool?> dialog = DialogService.Confirm(question, title, GetConfirmOptions());
			bool? result = dialog.Result;
			if (result == true)
            {
                RunActionsSafe(title, success, fail, action);
            }
        }
        catch (Exception ex)
        {
            CatchException(ex, title, fail);
        }
    }

    private static ConfirmOptions GetConfirmOptions() => new()
    {
        OkButtonText = LocaleCore.Dialog.DialogButtonYes,
        CancelButtonText = LocaleCore.Dialog.DialogButtonCancel
    };

    #endregion
}
