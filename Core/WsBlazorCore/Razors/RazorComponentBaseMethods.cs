// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net.Sockets;
using Microsoft.JSInterop;
using Radzen;
using WsBlazorCore.Settings;
using WsBlazorCore.Utils;
using WsDataCore.Protocols;
using WsStorageCore.Models;
using WsStorageCore.TableDiagModels.Logs;
using WsStorageCore.TableDiagModels.ScalesScreenshots;
using WsStorageCore.Tables;
using WsStorageCore.TableScaleFkModels.DeviceScalesFks;
using WsStorageCore.TableScaleFkModels.DeviceTypesFks;
using WsStorageCore.TableScaleFkModels.PlusBundlesFks;
using WsStorageCore.TableScaleFkModels.PlusLabels;
using WsStorageCore.TableScaleFkModels.PlusNestingFks;
using WsStorageCore.TableScaleFkModels.PlusTemplatesFks;
using WsStorageCore.TableScaleFkModels.PlusWeighingsFks;
using WsStorageCore.TableScaleFkModels.PrintersResourcesFks;
using WsStorageCore.TableScaleModels.Access;
using WsStorageCore.TableScaleModels.BarCodes;
using WsStorageCore.TableScaleModels.Boxes;
using WsStorageCore.TableScaleModels.Bundles;
using WsStorageCore.TableScaleModels.Contragents;
using WsStorageCore.TableScaleModels.Devices;
using WsStorageCore.TableScaleModels.DeviceTypes;
using WsStorageCore.TableScaleModels.Orders;
using WsStorageCore.TableScaleModels.OrdersWeighings;
using WsStorageCore.TableScaleModels.Organizations;
using WsStorageCore.TableScaleModels.Plus;
using WsStorageCore.TableScaleModels.PlusGroups;
using WsStorageCore.TableScaleModels.PlusScales;
using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.PrintersTypes;
using WsStorageCore.TableScaleModels.ProductionFacilities;
using WsStorageCore.TableScaleModels.ProductSeries;
using WsStorageCore.TableScaleModels.Scales;
using WsStorageCore.TableScaleModels.Templates;
using WsStorageCore.TableScaleModels.TemplatesResources;
using WsStorageCore.TableScaleModels.WorkShops;
using WsStorageCore.Utils;

namespace WsBlazorCore.Razors;

public partial class RazorComponentBase
{
	#region Public and private methods

	protected string GetQuestionAdd()
    {
        return LocaleCore.Dialog.DialogQuestion + Environment.NewLine;
    }

	protected string GetItemTitle(WsSqlTableBase? item) => item switch
		{
			WsSqlAccessModel => LocaleCore.Strings.ItemAccess,
			WsSqlBarCodeModel => LocaleCore.DeviceControl.ItemBarCode,
			WsSqlBoxModel => LocaleCore.DeviceControl.ItemBox,
			WsSqlBundleModel => LocaleCore.DeviceControl.ItemBundle,
			WsSqlContragentModel => LocaleCore.DeviceControl.ItemContragent,
			WsSqlDeviceModel => LocaleCore.DeviceControl.ItemDevice,
			WsSqlDeviceScaleFkModel => LocaleCore.DeviceControl.ItemDeviceScaleFk,
			WsSqlDeviceTypeFkModel => LocaleCore.DeviceControl.ItemDeviceTypeFk,
			WsSqlDeviceTypeModel => LocaleCore.DeviceControl.ItemDeviceType,
			WsSqlLogModel => LocaleCore.Strings.ItemLog,
            WsSqlPluGroupModel => LocaleCore.DeviceControl.ItemNomenclatureGroup,
            WsSqlOrderModel => LocaleCore.DeviceControl.ItemOrder,
			WsSqlOrderWeighingModel => LocaleCore.DeviceControl.ItemOrderWeighing,
			WsSqlOrganizationModel => LocaleCore.DeviceControl.ItemOrganization,
			WsSqlPluBundleFkModel => LocaleCore.DeviceControl.ItemPluBundleFk,
			WsSqlPluLabelModel => LocaleCore.DeviceControl.ItemLabel,
			WsSqlPluModel => LocaleCore.DeviceControl.ItemPlu,
			WsSqlPluScaleModel => LocaleCore.DeviceControl.ItemPluScale,
			WsSqlPluWeighingModel => LocaleCore.DeviceControl.ItemPluWeighing,
			WsSqlPrinterModel => LocaleCore.Print.Name,
			WsSqlPrinterResourceFkModel => LocaleCore.Print.Resources,
			WsSqlPrinterTypeModel => LocaleCore.Print.Types,
			WsSqlProductionFacilityModel => LocaleCore.DeviceControl.ItemProductionFacility,
			WsSqlProductSeriesModel => LocaleCore.DeviceControl.ItemProductSeries,
			WsSqlScaleModel => LocaleCore.DeviceControl.ItemScale,
			WsSqlScaleScreenShotModel => LocaleCore.DeviceControl.ItemScreenShot,
			WsSqlTemplateModel => LocaleCore.DeviceControl.ItemTemplate,
			WsSqlTemplateResourceModel => LocaleCore.DeviceControl.ItemTemplateResource,
			WsSqlWorkShopModel => LocaleCore.DeviceControl.ItemWorkShop,
			_ => string.Empty
        };

	protected string GetSectionTitle(WsSqlTableBase? item) => item switch
		{
            WsSqlPluGroupModel => LocaleCore.DeviceControl.SectionNomenclaturesGroups,
            WsSqlOrderModel => LocaleCore.DeviceControl.SectionOrders,
            WsSqlScaleScreenShotModel => LocaleCore.DeviceControl.SectionScreenShots,
            WsSqlTemplateModel => LocaleCore.DeviceControl.SectionTemplates,
			WsSqlAccessModel => LocaleCore.Strings.SectionAccess,
			WsSqlBarCodeModel => LocaleCore.DeviceControl.SectionBarCodes,
			WsSqlBoxModel => LocaleCore.DeviceControl.SectionBoxes,
			WsSqlBundleModel => LocaleCore.DeviceControl.SectionBundles,
			WsSqlContragentModel => LocaleCore.DeviceControl.SectionContragents,
			WsSqlDeviceModel => LocaleCore.DeviceControl.SectionDevices,
			WsSqlDeviceScaleFkModel => LocaleCore.DeviceControl.SectionDevicesScalesFk,
			WsSqlDeviceTypeFkModel => LocaleCore.DeviceControl.SectionDevicesTypesFk,
			WsSqlDeviceTypeModel => LocaleCore.DeviceControl.SectionDevicesTypes,
			WsSqlLogModel => LocaleCore.Strings.SectionLog,
			WsSqlOrderWeighingModel => LocaleCore.DeviceControl.SectionOrdersWeighings,
			WsSqlOrganizationModel => LocaleCore.DeviceControl.SectionOrganizations,
			WsSqlPluBundleFkModel => LocaleCore.DeviceControl.SectionPlusBundlesFk,
			WsSqlPluLabelModel => LocaleCore.DeviceControl.SectionLabels,
			WsSqlPluModel => LocaleCore.DeviceControl.SectionPlus,
			WsSqlPluNestingFkModel => LocaleCore.DeviceControl.SectionPlusNestingFk,
			WsSqlPluScaleModel => LocaleCore.DeviceControl.SectionPlusScales,
			WsSqlPluWeighingModel => LocaleCore.DeviceControl.SectionPlusWeightings,
			WsSqlPrinterModel => LocaleCore.Print.Name,
			WsSqlPrinterResourceFkModel => LocaleCore.Print.Resources,
			WsSqlPrinterTypeModel => LocaleCore.Print.Types,
			WsSqlProductionFacilityModel => LocaleCore.DeviceControl.SectionProductionFacilities,
			WsSqlProductSeriesModel => LocaleCore.DeviceControl.SectionProductSeries,
			WsSqlScaleModel => LocaleCore.DeviceControl.SectionScales,
			WsSqlTemplateResourceModel => LocaleCore.DeviceControl.SectionTemplateResources,
			WsSqlWorkShopModel => LocaleCore.DeviceControl.SectionWorkShops,
			_ => string.Empty
		};

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

	protected async Task SqlItemCancelAsync()
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        SetRouteSectionNavigate();
	}

	protected TItem SqlItemNewEmpty<TItem>() where TItem : WsSqlTableBase, new()
	{
		TItem item = ContextManager.AccessManager.AccessItem.GetItemNewEmpty<TItem>();
		item.FillProperties();
		return item;
	}

	private void SqlItemSave<T>(T? item) where T : WsSqlTableBase, new()
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

	private void SqlItemsSave<T>(List<T>? items) where T : WsSqlTableBase, new()
	{
		if (items is null) return;

		foreach (T item in items)
			SqlItemSave(item);
	}

	protected async Task SqlItemSaveAsync()
	{
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

	protected async Task SqlItemPrinterResourcesClear(WsSqlPrinterModel printer)
	{
        if (User?.IsInRole(UserAccessStr.Write) == false) return;
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsWithQeustion(LocaleCore.Print.ResourcesClear, GetQuestionAdd(), () =>
		{
			SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(false, false);
			List<WsSqlTemplateResourceModel> templateResources = ContextManager.ContextList.GetListNotNullableTemplateResources(sqlCrudConfig);
			foreach (WsSqlTemplateResourceModel templateResource in templateResources)
			{
				if (templateResource.Name.Contains("TTF"))
				{
					TcpClient client = MdZplUtils.TcpClientSendData(printer.Ip, printer.Port,
						new()
						{
							new($"^XA^ID"),
							new(templateResource.Name),
							new($"^FS^XZ")
						});
				}
			}
        });
	}

	protected async Task SqlItemPrinterResourcesLoad(WsSqlPrinterModel printer, string fileType)
	{
        if (User?.IsInRole(UserAccessStr.Write) == false) return;
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsWithQeustion(LocaleCore.Print.ResourcesLoadTtf, GetQuestionAdd(), () =>
		{
			SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
                new SqlFieldOrderModel { Name = nameof(WsSqlTableBase.Description), Direction = WsSqlOrderDirection.Asc}, false, false);
			List<WsSqlTemplateResourceModel> templateResources = ContextManager.ContextList.GetListNotNullable<WsSqlTemplateResourceModel>(sqlCrudConfig);
			foreach (WsSqlTemplateResourceModel templateResource in templateResources)
			{
				if (templateResource.Name.Contains(fileType))
				{
					TcpClient client = MdZplUtils.TcpClientSendData(printer.Ip, printer.Port,
						new()
						{
							new($"^XA^MNN^LL500~DYE:{templateResource.Name}.TTF,B,T,{templateResource.DataValue.Length},,"),
							new(templateResource.DataValue),
							new($"^XZ")
						});
				}
			}
        });
	}
    
    protected async void CopyToClipboard(string textToCopy)
    {
        if (JsRuntime != null)
            await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", textToCopy);
    }

	#endregion
}
