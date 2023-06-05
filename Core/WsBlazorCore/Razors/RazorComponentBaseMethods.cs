// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Radzen;
using WsBlazorCore.Settings;
using WsStorageCore.Models;
using WsStorageCore.Utils;

namespace WsBlazorCore.Razors;

public partial class RazorComponentBase
{
    #region Public and private methods

    protected string GetQuestionAdd()
    {
        return LocaleCore.Dialog.DialogQuestion + Environment.NewLine;
    }

    #endregion

    #region Public and private methods - Actions

    private bool SqlItemValidate<T>(NotificationService? notificationService, T? item) where T : WsSqlTableBase, new()
    {
        bool result = item is not null;
        string detailAddition = Environment.NewLine;
        if (result)
        {
            result = WsSqlValidationUtils.IsValidation(item, ref detailAddition);
        }
        switch (result)
        {
            case false:
                {
                    NotificationMessage msg = new()
                    {
                        Severity = NotificationSeverity.Warning,
                        Summary = LocaleCore.Action.ActionDataControl,
                        Detail = $"{LocaleCore.Action.ActionDataControlField}!" + Environment.NewLine + detailAddition,
                        Duration = BlazorAppSettingsHelper.Delay
                    };
                    notificationService?.Notify(msg);
                    return false;
                }
            default:
                return true;
        }
    }

    protected TItem SqlItemNew<TItem>() where TItem : WsSqlTableBase, new()
    {
        TItem item = new();
        item.FillProperties();
        return item;
    }

    protected TItem SqlItemNewEmpty<TItem>() where TItem : WsSqlTableBase, new()
    {
        TItem item = ContextManager.AccessManager.AccessItem.GetItemNewEmpty<TItem>();
        item.FillProperties();
        return item;
    }

    protected void SqlItemSave<T>(T? item) where T : WsSqlTableBase, new()
    {
        if (item is null) return;
        if (item.IsNew)
        {
            ContextManager.AccessManager.AccessItem.Save(item);
        }
        else
        {
            if (!SqlItemValidate(NotificationService, item)) return;
            ContextManager.AccessManager.AccessItem.Update(item);
        }
    }

    protected void SqlItemsSave<T>(List<T>? items) where T : WsSqlTableBase, new()
    {
        if (items is null) return;

        foreach (T item in items)
            SqlItemSave(item);
    }

    #endregion
}