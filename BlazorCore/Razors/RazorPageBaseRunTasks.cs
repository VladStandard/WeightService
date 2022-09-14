// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorCore.Razors;

public partial class RazorPageBase
{
	#region Public and private methods - Tun tasks

	//public async Task GetDataAsync(Task task, bool continueOnCapturedContext)
	//{
	//    await RunTasksAsync(LocaleCore.Table.TableRead, "", LocaleCore.Dialog.DialogResultFail, "",
	//        new() { task }, continueOnCapturedContext).ConfigureAwait(false);
	//}

	//public override Task SetParametersAsync(ParameterView parameters)
	//{
	//    //int code = parameters.GetHashCode();
	//    //if (code == 0)
	//    //    return Task.CompletedTask;
	//    //parameters.SetParameterProperties(this);
	//    //Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

	//    //AppSettings.FontSize = parameters.TryGetValue("FontSize", out int fontSize) ? fontSize : 14;
	//    //AppSettings.FontSizeHeader = parameters.TryGetValue("FontSizeHeader", out int fontSizeHeader) ? fontSizeHeader : 20;

	//    //if (Identity.Id is null && ParentRazor?.Identity.Id is not null)
	//    //    Identity.Id = ParentRazor.Identity.Id;
	//    //if (IdentityUid is null && ParentRazor?.Identity.Uid is not null)
	//    //    IdentityUid = ParentRazor.Identity.Uid;
	//    //if (string.IsNullOrEmpty(Table.Name))
	//    //{
	//    //    if (ParentRazor is not null)
	//    //    {
	//    //        Table = ParentRazor.Table;
	//    //    }
	//    //}
	//    //if (TableAction == DbTableAction.Default && ParentRazor is not null)
	//    //{
	//    //    if (ParentRazor.TableAction != DbTableAction.Default)
	//    //        TableAction = ParentRazor.TableAction;
	//    //}
	//    //switch (Table)
	//    //{
	//    //    case TableScaleEntity:
	//    //        SetParametersForTableScale(parameters, GetTableScale(Table.Name));
	//    //        break;
	//    //}

	//    return base.SetParametersAsync(ParameterView.Empty);
	//}

	//public async Task RunTasksAsync(string title, string success, string fail, string cancel, List<Task> tasks,
	//    bool continueOnCapturedContext,
	//    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	//{
	//    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

	//    RunActionsSilent(title, success, fail, cancel, tasks, continueOnCapturedContext, filePath, lineNumber, memberName);
	//}

	//public void RunTasks(string title, string success, string fail, string cancel, List<Task> tasks, bool continueOnCapturedContext,
	//       [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	//   {
	//       try
	//       {
	//           RunTasksCore(title, success, cancel, tasks, continueOnCapturedContext);
	//       }
	//       catch (Exception ex)
	//       {
	//           RunTasksCatch(ex, title, fail, filePath, lineNumber, memberName);
	//       }
	//   }

	//public void RunTasks(string title, string success, string fail, string cancel, Task task, bool continueOnCapturedContext,
	//    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	//{
	//    RunTasks(title, success, fail, cancel, new List<Task> { task }, continueOnCapturedContext, filePath, lineNumber, memberName);
	//}

	//private void RunTasksCore(string title, string success, string cancel, List<Task> tasks, bool continueOnCapturedContext)
	//{
	//    if (tasks.Any())
	//    {
	//        foreach (Task task in tasks)
	//        {
	//            task.ConfigureAwait(continueOnCapturedContext);
	//            task.Start();
	//        }
	//    }
	//    if (!string.IsNullOrEmpty(success))
	//        NotificationService?.Notify(NotificationSeverity.Success, title + Environment.NewLine, success, AppSettingsHelper.Delay);
	//    else
	//    {
	//        if (!string.IsNullOrEmpty(cancel))
	//            NotificationService?.Notify(NotificationSeverity.Info, title + Environment.NewLine, cancel, AppSettingsHelper.Delay);
	//    }
	//}

	#endregion
}
