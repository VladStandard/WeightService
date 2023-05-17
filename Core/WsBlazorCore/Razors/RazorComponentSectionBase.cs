// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using WsBlazorCore.Settings;
using WsBlazorCore.Utils;
using WsStorageCore.Models;
using WsStorageCore.Tables;
using WsStorageCore.Utils;

namespace WsBlazorCore.Razors;

public class RazorComponentSectionBase<TItem> : RazorComponentBase where TItem : WsSqlTableBase, new()
{
    #region Public and private fields, properties, constructor

    #region Parameters
    
    [Inject] protected ContextMenuService? ContextMenuService { get; set; }
    [Parameter] public WsSqlCrudConfigModel SqlCrudConfigSection { get; set; }
    [Parameter] public ButtonSettingsModel? ButtonSettings { get; set; }

    protected bool IsLoading = true;
    
    #endregion

    public IList<TItem>? SelectedRow { get; set; }

    protected List<TItem> SqlSectionCast { get; set; }

    protected List<TItem> SqlSectionSave { get; set; }

    public RazorComponentSectionBase()
    {
        SqlSectionCast = new List<TItem>();
        SelectedRow = new List<TItem>();
        SqlSectionSave = new List<TItem>();
        SqlCrudConfigSection = WsSqlCrudConfigUtils.GetCrudConfigSection(false);
        SqlCrudConfigSection.IsGuiShowItemsCount = true;
        SqlCrudConfigSection.IsGuiShowFilterMarked = true;
        SqlCrudConfigSection.IsGuiShowFilterOnlyTop = false;

        ButtonSettings = new(true, true, true, true, true, false, false);
    }

    #endregion
    
    
    protected async Task SqlItemEditAsync()
    {
        if (User?.IsInRole(UserAccessStr.Read) == false) return;
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        RunActionsSafe(string.Empty, () => { SetRouteItemNavigate(SqlItem); });
    }

    protected void SqlItemSet(TItem item)
    {
        SelectedRow = new List<TItem>() { item };
        SqlItem = SelectedRow.Last();
    }

    //TODO: insert into DataCore
	protected void OnCellContextMenu(DataGridCellMouseEventArgs<TItem> args)
	{
		LocaleContextMenu locale = LocaleCore.ContextMenu;

		SelectedRow = new List<TItem>() { args.Data };
		SqlItem = args.Data;
		List<ContextMenuItem> contextMenuItems = new()
		{
			new() { Text = locale.Open, Value = ContextMenuAction.Open },
			new() { Text = locale.OpenNewTab, Value = ContextMenuAction.OpenNewTab },
		};
        if (User?.IsInRole(UserAccessStr.Write) == true)
        {
            if (ButtonSettings?.IsShowMark == true)
                contextMenuItems.Add(new() { Text = locale.Mark, Value = ContextMenuAction.Mark });
            if (ButtonSettings?.IsShowDelete == true)
                contextMenuItems.Add(new() { Text = locale.Delete, Value = ContextMenuAction.Delete });
        }
        ContextMenuService?.Open(args, contextMenuItems, (e) => ParseContextMenuActions(e, args));
	}
	
	protected void ParseContextMenuActions(MenuItemEventArgs e, DataGridCellMouseEventArgs<TItem> args) 
	{
		InvokeAsync(async () =>
		{
			switch ((ContextMenuAction)e.Value)
			{
				case ContextMenuAction.OpenNewTab:
					if (JsRuntime != null)
						await JsRuntime.InvokeAsync<object>("open", 
							GetRouteItemPathForLink(args.Data), "_blank");
					break;
				case ContextMenuAction.Open:
					await SqlItemEditAsync();
					break;
				case ContextMenuAction.Mark:
					await SqlItemMarkAsync();
					break;
				case ContextMenuAction.Delete:
					await SqlItemDeleteAsync();
					break;
			}
			ContextMenuService?.Close();
		});
	}
    
    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender)
        {
            base.OnAfterRender(firstRender);
            return;
        }
        GetSectionData();
    }
    
    protected void GetSectionData()
    {
        RunActionsSafe(string.Empty, SetSqlSectionCast);
        SqlCrudConfigSection.IsGuiShowFilterOnlyTop = SqlSectionCast.Count >= ContextManager.JsonSettings.Local.SelectTopRowsCount;
        IsLoading = false;
        StateHasChanged();
    }

    protected virtual void SetSqlSectionCast()
    {
        SqlSectionCast = ContextManager.AccessManager.AccessList.GetListNotNullable<TItem>(SqlCrudConfigSection);
    }

    protected void SetSqlSectionSave(TItem model)
    {
        if (!SqlSectionSave.Any(item => Equals(item.IdentityValueUid, model.IdentityValueUid)))
                SqlSectionSave.Add(model);
    }

    protected virtual async Task OnSqlSectionSaveAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        RunActionsWithQeustion(LocaleCore.Table.TableSave, GetQuestionAdd(), () =>
        {
            foreach (TItem item in SqlSectionSave)
                ContextManager.AccessManager.AccessItem.Update(item);
            SqlSectionSave.Clear();
        });
    }
    
    protected async Task SqlItemCopyAsync()
    {
        if (User?.IsInRole(UserAccessStr.Write) == false) return;
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (SqlItem is null)
        {
            await ShowDialog(LocaleCore.Sql.SqlItemIsNotSelect, LocaleCore.Sql.SqlItemDoSelect).ConfigureAwait(true);
            return;
        }

        RunActionsWithQeustion(LocaleCore.Table.TableCopy, GetQuestionAdd(), () =>
        {
            SqlItem = SqlItem.CloneCast();
            SetRouteItemNavigate(SqlItem);
        });
    }

    protected async Task SqlItemMarkAsync()
    {
        if (User?.IsInRole(UserAccessStr.Write) == false) return;
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (SqlItem is null)
        {
            await ShowDialog(LocaleCore.Sql.SqlItemIsNotSelect, LocaleCore.Sql.SqlItemDoSelect).ConfigureAwait(true);
            return;
        }
		
        RunActionsWithQeustion(LocaleCore.Table.TableMark, GetQuestionAdd(), () =>
        {
            ContextManager.AccessManager.AccessItem.Mark(SqlItem); ;
        });
    }

    protected async Task SqlItemDeleteAsync()
    {
        if (User?.IsInRole(UserAccessStr.Write) == false) return;
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (SqlItem is null)
        {
            await ShowDialog(LocaleCore.Sql.SqlItemIsNotSelect, LocaleCore.Sql.SqlItemDoSelect).ConfigureAwait(true);
            return;
        }
		
        RunActionsWithQeustion(LocaleCore.Table.TableDelete, GetQuestionAdd(), () =>
        {
            ContextManager.AccessManager.AccessItem.Delete(SqlItem);
        });
    }
    
    protected async Task SqlItemNewAsync<TItem>() where TItem : WsSqlTableBase, new()
    {
        if (User?.IsInRole(UserAccessStr.Write) == false) return;
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        RunActionsWithQeustion(LocaleCore.Table.TableNew, GetQuestionAdd(), () =>
        {
            SqlItem = SqlItemNew<TItem>();
            SetRouteItemNavigate(SqlItem);
        });
    }
    
    #region Public and private methods

    #endregion
}