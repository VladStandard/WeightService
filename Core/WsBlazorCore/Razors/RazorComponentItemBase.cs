// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WsBlazorCore.Settings;
using WsStorageCore.Tables;
using WsStorageCore.TableScaleFkModels.DeviceScalesFks;
using WsStorageCore.TableScaleFkModels.DeviceTypesFks;
using WsStorageCore.TableScaleFkModels.PlusBundlesFks;
using WsStorageCore.TableScaleFkModels.PlusNestingFks;
using WsStorageCore.TableScaleFkModels.PlusTemplatesFks;
using WsStorageCore.TableScaleModels.Boxes;
using WsStorageCore.TableScaleModels.Bundles;
using WsStorageCore.TableScaleModels.Devices;
using WsStorageCore.TableScaleModels.DeviceTypes;
using WsStorageCore.TableScaleModels.Plus;
using WsStorageCore.TableScaleModels.Scales;
using WsStorageCore.TableScaleModels.Templates;

namespace WsBlazorCore.Razors;

public class RazorComponentItemBase<TItem> : RazorComponentBase where TItem : WsSqlTableBase, new()
{
	#region Public and private fields, properties, constructor

	protected TItem SqlItemCast
	{
		get => SqlItem is null ? new() : (TItem)SqlItem;
		set => SqlItem = value;
	}
    
    [Parameter] public ButtonSettingsModel? ButtonSettings { get; set; }
    [Parameter] public CssStyleTableHeadModel CssTableStyleHead { get; set; }
    
    public RazorComponentItemBase()
	{
        CssTableStyleHead = new();
        ButtonSettings = new(false, false, false, false, false, true, true);
    }

	#endregion

	#region Public and private methods
    protected async void CopyToClipboard(string textToCopy)
    {
        if (JsRuntime != null)
            await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", textToCopy);
    }
    
    protected async Task SqlItemSaveAsync()
	{ 
        // TODO: fix this
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		if (SqlItem is null)
		{
			await ShowDialog(LocaleCore.Sql.SqlItemIsNotSelect, LocaleCore.Sql.SqlItemDoSelect).ConfigureAwait(true);
			return;
		}

        
        RunActionsWithQeustion(LocaleCore.Table.TableSave, GetQuestionAdd(), () =>
        {
            switch (SqlItem)
            {
                case WsSqlDeviceModel device:
                    SqlItemSave(SqlItem);
                    SqlItemSaveDevice(device);
                    break;
                case WsSqlPluModel plu:
                    SqlItemSave(SqlItem);
                    SqlItemSavePlu(plu);
                    break;
                case WsSqlPluBundleFkModel pluBundleFk:
                    // Don't do it!
                    //SqlItemSave(SqlItem);
                    SqlItemSavePluBundleFk(pluBundleFk);
                    break;
                case WsSqlPluNestingFkModel pluNestingFk:
                    // Don't do it!
                    //SqlItemSave(SqlItem);
                    SqlItemSavePluNestingFk(pluNestingFk);
                    break;
                case WsSqlScaleModel scale:
                    SqlItemSave(SqlItem);
                    SqlItemSaveScale(scale);
                    break;
                default:
                    SqlItemSave(SqlItem);
                    break;
            }
            SetRouteSectionNavigate();
        });
    }

    private void SqlItemSaveScale(WsSqlScaleModel scale)
    {
        if (SqlLinkedItems is null || !SqlLinkedItems.Any()) return;
        foreach (WsSqlTableBase item in SqlLinkedItems)
        {
            if (item is WsSqlDeviceModel device)
            {
                WsSqlDeviceScaleFkModel? deviceScaleFk = ContextManager.ContextItem.GetItemDeviceScaleFkNullable(scale);
                if (device.IsNotNew)
                {
                    if (deviceScaleFk is null)
                        deviceScaleFk = new() { Device = device, Scale = scale };
                    else
                        deviceScaleFk.Device = device;
                    SqlItemSave(deviceScaleFk);
                }
                else
                {
                    if (deviceScaleFk is not null)
                        ContextManager.AccessManager.AccessItem.Delete(deviceScaleFk);
                }
            }
        }
    }

    private void SqlItemSavePlu(WsSqlPluModel plu)
    {
        if (SqlLinkedItems is not null && SqlLinkedItems.Any())
        {
            foreach (WsSqlTableBase item in SqlLinkedItems)
            {
                if (item is WsSqlTemplateModel template)
                {
                    WsSqlPluTemplateFkModel? pluTemplateFk = ContextManager.ContextItem.GetItemPluTemplateFkNullable(plu);
                    if (template.IsNotNew)
                    {
                        if (pluTemplateFk is null)
                            pluTemplateFk = new() { Plu = plu, Template = template };
                        else
                            pluTemplateFk.Template = template;
                        SqlItemSave(pluTemplateFk);
                    }
                    else
                    {
                        if (pluTemplateFk is not null)
                            ContextManager.AccessManager.AccessItem.Delete(pluTemplateFk);
                    }
                }
            }
        }
    }

    private void SqlItemSavePluBundleFk(WsSqlPluBundleFkModel pluBundleFk)
    {
        if (SqlLinkedItems is null || !SqlLinkedItems.Any()) return;
        WsSqlPluModel? plu = SqlLinkedItems.First(x => x is WsSqlPluModel) as WsSqlPluModel;
        WsSqlBundleModel? bundle = SqlLinkedItems.First(x => x is WsSqlBundleModel) as WsSqlBundleModel;
		if (plu is null || bundle is null) return;
        pluBundleFk.Plu = plu;
        pluBundleFk.Bundle = bundle;
        SqlItemSave(pluBundleFk);
    }
    
    private void SqlItemSavePluNestingFk(WsSqlPluNestingFkModel pluNestingFk)
    {
        if (SqlLinkedItems is null || !SqlLinkedItems.Any()) return;
        WsSqlPluBundleFkModel? pluBundleFk = SqlLinkedItems.First(x => x is WsSqlPluBundleFkModel) as WsSqlPluBundleFkModel;
        WsSqlBoxModel? box = SqlLinkedItems.First(x => x is WsSqlBoxModel) as WsSqlBoxModel;
        if (pluBundleFk is null) return;
        if (box is null) return;
        pluNestingFk.PluBundle = pluBundleFk;
        pluNestingFk.Box = box;
        SqlItemSave(pluNestingFk);
    }

    private void SqlItemSaveDevice(WsSqlDeviceModel device)
    {
        if (SqlLinkedItems is null || !SqlLinkedItems.Any()) return;
        foreach (WsSqlTableBase item in SqlLinkedItems)
        {
            if (item is WsSqlDeviceTypeModel deviceType)
            {
                WsSqlDeviceTypeFkModel? deviceTypeFk = ContextManager.ContextItem.GetItemDeviceTypeFkNullable(device);
                if (deviceType.IsNotNew)
                {
                    if (deviceTypeFk is null)
                        deviceTypeFk = new() { Type = deviceType, Device = device };
                    else
                        deviceTypeFk.Type = deviceType;
                    SqlItemSave(deviceTypeFk);
                }
                else
                {
                    if (deviceTypeFk is not null)
                        ContextManager.AccessManager.AccessItem.Delete(deviceTypeFk);
                }
            }
        }
    }

    
	#endregion
}
