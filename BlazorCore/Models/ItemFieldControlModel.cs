// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Sql.Core;
using DataCore.Sql.Tables;
using Radzen;

namespace BlazorCore.Models;

public class ItemFieldControlModel
{
    #region Public and private methods

    public bool ValidateModel<T>(NotificationService? notificationService, T? item, string field) where T : TableBase, new()
    {
        bool result = item is not null;
        string detailAddition = Environment.NewLine;
        if (result)
        {
	        result = SqlUtils.IsValidation<T>(item, ref detailAddition);
        }
        switch (result)
        {
	        case false:
	        {
		        NotificationMessage msg = new()
		        {
			        Severity = NotificationSeverity.Warning,
			        Summary = LocaleCore.Action.ActionDataControl,
			        Detail = $"{LocaleCore.Action.ActionDataControlField} [{field}]!" +
			                 (Equals(detailAddition, Environment.NewLine) ? string.Empty : detailAddition),
			        Duration = AppSettingsHelper.Delay
		        };
		        notificationService?.Notify(msg);
		        return false;
	        }
	        default:
		        return true;
        }
    }

    #endregion
}
