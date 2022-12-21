// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BlazorCore.Settings;
using DataCore.Localizations;
using DataCore.Protocols;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace BlazorCore.Razors;

public partial class RazorComponentBase
{
    #region Public and private methods - Run actions

    protected void RunActionsInitialized(List<Action> actionsInitialized) => 
        RunActionsSafe(string.Empty, actionsInitialized);

    protected void RunActionsParametersSet(List<Action> actionsParametersSet)
    {
        SetPropertiesFromParent();
        RunActionsSafe(string.Empty, actionsParametersSet);
    }

    protected void RunActionsSafe(string title, List<Action> actions, [CallerMemberName] string memberName = "")
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
            if (!string.IsNullOrEmpty(title))
                NotificationService?.Notify(NotificationSeverity.Success,
                    $"{LocaleCore.Action.ActionMethod}: {title}" + Environment.NewLine,
                    LocaleCore.Dialog.DialogResultSuccess, BlazorAppSettingsHelper.Delay);
        }
        catch (Exception ex)
        {
            CatchException(ex, memberName, LocaleCore.Dialog.DialogResultFail);
        }
    }

    protected void RunActionsSafe(string title, Action action) => 
	    RunActionsSafe(title, new List<Action> { action });

    protected void CatchException(Exception ex, string title, string fail)
    {
        // Notify log.
        string msg = ex.Message;
        if (!string.IsNullOrEmpty(ex.InnerException?.Message))
            msg += Environment.NewLine + ex.InnerException.Message;
        if (!string.IsNullOrEmpty(fail))
        {
            if (!string.IsNullOrEmpty(msg))
                NotificationService?.Notify(NotificationSeverity.Error, title + Environment.NewLine, fail + Environment.NewLine + msg, BlazorAppSettingsHelper.Delay);
            else
                NotificationService?.Notify(NotificationSeverity.Error, title + Environment.NewLine, fail, BlazorAppSettingsHelper.Delay);
        }
        else
        {
            if (!string.IsNullOrEmpty(msg))
                NotificationService?.Notify(NotificationSeverity.Error, title + Environment.NewLine, msg, BlazorAppSettingsHelper.Delay);
        }

        // SQL log.
        BlazorAppSettings.DataAccess.LogError(ex, NetUtils.GetLocalDeviceName(false), nameof(BlazorCore));
    }

    private void RunActionsWithQeustion(string title, string questionAdd, Action action)
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
                RunActionsSafe(title, action);
            }
        }
        catch (Exception ex)
        {
            CatchException(ex, title, LocaleCore.Dialog.DialogResultFail);
        }
    }

	// https://blazor.radzen.com/dialog
	private async Task ShowDialog(string title, string message)
    {
	    try
	    {
		    if (DialogService is null)
			    throw new ArgumentNullException(nameof(DialogService));
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

    //        await InvokeAsync(async () =>
    //        {
	   //         await Task.Delay(TimeSpan.FromMilliseconds(3000)).ConfigureAwait(false);
				//DialogService.Close();
    //        }).ConfigureAwait(false);

	        await InvokeAsync(async () =>
	        {
                await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
                Task<dynamic>? dialog = DialogService.OpenAsync(title,
                    ds =>
                    {
                        RenderFragment renderFragment = new(builder =>
                        {
	                        int index = 0;
                            builder.OpenComponent<RazorComponentBase>(index++);
							builder.AddAttribute(index++, nameof(title), title);
							//builder.AddComponentReferenceCapture(index++, inst => { RazorComponentBase component = new(); });
							builder.CloseComponent();
                        });
                        return renderFragment;
                    },
                    new DialogOptions() { CloseDialogOnOverlayClick = true, ShowClose = true });
	        }).ConfigureAwait(true);
		}
	    catch (Exception ex)
	    {
		    CatchException(ex, title, LocaleCore.Dialog.DialogResultFail);
	    }
    }

    private static ConfirmOptions GetConfirmOptions() => new()
    {
        OkButtonText = LocaleCore.Dialog.DialogButtonYes,
        CancelButtonText = LocaleCore.Dialog.DialogButtonCancel
    };

    #endregion
}
