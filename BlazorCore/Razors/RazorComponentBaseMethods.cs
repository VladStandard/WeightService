// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Settings;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Protocols;
using DataCore.Sql.Fields;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.BarCodes;
using DataCore.Sql.TableScaleModels.Contragents;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.Logs;
using DataCore.Sql.TableScaleModels.Nomenclatures;
using DataCore.Sql.TableScaleModels.Orders;
using DataCore.Sql.TableScaleModels.OrdersWeighings;
using DataCore.Sql.TableScaleModels.Organizations;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusLabels;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.PlusWeighings;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.PrintersResources;
using DataCore.Sql.TableScaleModels.PrintersTypes;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.ProductSeries;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using DataCore.Sql.TableScaleModels.WorkShops;
using Radzen;
using System.Collections.Generic;
using System.Net.Sockets;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleFkModels.BundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleModels.NomenclaturesGroups;
using DataCore.Sql.TableScaleModels.ScalesScreenshots;

namespace BlazorCore.Razors;

public partial class RazorComponentBase
{
	#region Public and private methods

	private string GetQuestionAdd()
	{
		return ParentRazor?.SqlItem?.Identity.Name switch
		{
			SqlFieldIdentityEnum.Id =>
				LocaleCore.Dialog.DialogQuestion + Environment.NewLine +
				$"{nameof(ParentRazor.SqlItem.IdentityValueId)}: {ParentRazor.SqlItem.IdentityValueId}",
			SqlFieldIdentityEnum.Uid =>
				LocaleCore.Dialog.DialogQuestion + Environment.NewLine +
				$"{nameof(ParentRazor.SqlItem.IdentityValueUid)}: {ParentRazor.SqlItem.IdentityValueUid}",
			_ => string.Empty
		};
	}

	protected string GetItemTitle(SqlTableBase? item)
	{
		string result = string.Empty;
		result = item switch
		{
			AccessModel => LocaleCore.Strings.ItemAccess,
			BarCodeModel => LocaleCore.DeviceControl.ItemBarCode,
			BoxModel => LocaleCore.DeviceControl.ItemBox,
			BundleFkModel => LocaleCore.DeviceControl.ItemBundleFk,
			BundleModel => LocaleCore.DeviceControl.ItemBundle,
			ContragentModel => LocaleCore.DeviceControl.ItemContragent,
			DeviceModel => LocaleCore.DeviceControl.ItemDevice,
			DeviceScaleFkModel => LocaleCore.DeviceControl.ItemDeviceScaleFk,
			DeviceTypeFkModel => LocaleCore.DeviceControl.ItemDeviceTypeFk,
			DeviceTypeModel => LocaleCore.DeviceControl.ItemDeviceType,
			LogModel => LocaleCore.Strings.ItemLog,
			NomenclatureModel => LocaleCore.DeviceControl.ItemNomenclature,
            NomenclatureGroupModel => LocaleCore.DeviceControl.ItemNomenclatureGroup,
            OrderModel => LocaleCore.DeviceControl.ItemOrder,
			OrderWeighingModel => LocaleCore.DeviceControl.ItemOrderWeighing,
			OrganizationModel => LocaleCore.DeviceControl.ItemOrganization,
			PluBundleFkModel => LocaleCore.DeviceControl.ItemPluBundleFk,
			PluLabelModel => LocaleCore.DeviceControl.ItemLabel,
			PluModel => LocaleCore.DeviceControl.ItemPlu,
			PluScaleModel => LocaleCore.DeviceControl.ItemPluScale,
			PluWeighingModel => LocaleCore.DeviceControl.ItemPluWeighing,
			PrinterModel => LocaleCore.Print.Name,
			PrinterResourceModel => LocaleCore.Print.Resources,
			PrinterTypeModel => LocaleCore.Print.Types,
			ProductionFacilityModel => LocaleCore.DeviceControl.ItemProductionFacility,
			ProductSeriesModel => LocaleCore.DeviceControl.ItemProductSeries,
			ScaleModel => LocaleCore.DeviceControl.ItemScale,
			ScaleScreenShotModel => LocaleCore.DeviceControl.ItemScreenShot,
			TemplateModel => LocaleCore.DeviceControl.ItemTemplate,
			TemplateResourceModel => LocaleCore.DeviceControl.ItemTemplateResource,
			WorkShopModel => LocaleCore.DeviceControl.ItemWorkShop,
			_ => result
		};
		return result;
	}

	protected string GetSectionTitle(SqlTableBase? item)
	{
		string result = string.Empty;
		result = item switch
		{
			AccessModel => LocaleCore.Strings.SectionAccess,
			BarCodeModel => LocaleCore.DeviceControl.SectionBarCodes,
			BoxModel => LocaleCore.DeviceControl.SectionBoxes,
			BundleFkModel => LocaleCore.DeviceControl.SectionBundlesFk,
			BundleModel => LocaleCore.DeviceControl.SectionBundles,
			ContragentModel => LocaleCore.DeviceControl.SectionContragents,
			DeviceModel => LocaleCore.DeviceControl.SectionDevices,
			DeviceScaleFkModel => LocaleCore.DeviceControl.SectionDevicesScalesFk,
			DeviceTypeFkModel => LocaleCore.DeviceControl.SectionDevicesTypesFk,
			DeviceTypeModel => LocaleCore.DeviceControl.SectionDevicesTypes,
			LogModel => LocaleCore.Strings.SectionLog,
			NomenclatureModel => LocaleCore.DeviceControl.SectionNomenclatures,
            NomenclatureGroupModel => LocaleCore.DeviceControl.SectionNomenclaturesGroups,
            OrderModel => LocaleCore.DeviceControl.SectionOrders,
			OrderWeighingModel => LocaleCore.DeviceControl.SectionOrdersWeighings,
			OrganizationModel => LocaleCore.DeviceControl.SectionOrganizations,
			PluBundleFkModel => LocaleCore.DeviceControl.SectionPlusBundlesFk,
			PluLabelModel => LocaleCore.DeviceControl.SectionLabels,
			PluModel => LocaleCore.DeviceControl.SectionPlus,
			PluScaleModel => LocaleCore.DeviceControl.SectionPlusScales,
			PluWeighingModel => LocaleCore.DeviceControl.SectionPlusWeighings,
			PrinterModel => LocaleCore.Print.Name,
			PrinterResourceModel => LocaleCore.Print.Resources,
			PrinterTypeModel => LocaleCore.Print.Types,
			ProductionFacilityModel => LocaleCore.DeviceControl.SectionProductionFacilities,
			ProductSeriesModel => LocaleCore.DeviceControl.SectionProductSeries,
			ScaleModel => LocaleCore.DeviceControl.SectionScales,
            ScaleScreenShotModel => LocaleCore.DeviceControl.SectionScreenShots,
            TemplateModel => LocaleCore.DeviceControl.SectionTemplates,
			TemplateResourceModel => LocaleCore.DeviceControl.SectionTemplateResources,
			WorkShopModel => LocaleCore.DeviceControl.SectionWorkShops,
			_ => result
		};
		return result;
	}

	#endregion

	#region Public and private methods - Actions

	private bool SqlItemValidate<T>(NotificationService? notificationService, T? item) where T : SqlTableBase, new()
	{
		bool result = item is not null;
		string detailAddition = Environment.NewLine;
		if (result)
		{
			result = ValidationUtils.IsValidation(item, ref detailAddition);
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

	protected async Task SqlItemCancelAsync()
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsSafe(LocaleCore.Table.TableCancel, () =>
		{
			SetRouteSectionNavigate();
			OnChangeAsync();
		});
	}

	protected TItem SqlItemNew<TItem>() where TItem : SqlTableBase, new()
	{
		TItem item = new();
		item.FillProperties();
		return item;
	}

	private void SqlItemSave<T>(T? item) where T : SqlTableBase, new()
	{
		if (item is null) return;
		if (item.IdentityIsNew)
		{
			BlazorAppSettings.DataAccess.Save(item);
		}
		else
		{
			if (!SqlItemValidate(NotificationService, item)) return;
			BlazorAppSettings.DataAccess.Update(item);
		}
	}

	private void SqlItemsSave<T>(List<T>? items) where T : SqlTableBase, new()
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
			SqlItemSave(SqlItem);
			switch (SqlItem)
			{
				case DeviceModel device:
                    if (SqlLinkedItems is not null && SqlLinkedItems.Any())
                    {
                        foreach (SqlTableBase item in SqlLinkedItems)
                        {
                            if (item is DeviceTypeModel deviceType)
                            {
                                DeviceTypeFkModel? deviceTypeFk = DataAccess.GetItemDeviceTypeFkNullable(device);
                                if (deviceType is not null && deviceType.IdentityIsNotNew)
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
                                        DataAccess.Delete(deviceTypeFk);
                                }
                            }
                        }
                    }
                    break;
                case ScaleModel scale:
					if (SqlLinkedItems is not null && SqlLinkedItems.Any())
					{
						foreach (SqlTableBase item in SqlLinkedItems)
						{
							if (item is DeviceModel device)
							{
								DeviceScaleFkModel? deviceScaleFk = DataAccess.GetItemDeviceScaleFkNullable(scale);
								if (device is not null && device.IdentityIsNotNew)
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
										DataAccess.Delete(deviceScaleFk);
								}
							}
						}
					}
					break;
			}
			SetRouteSectionNavigate();
			OnChangeAsync();
		});
	}

	protected async Task SqlItemsSaveAsync()
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsWithQeustion(LocaleCore.Table.TableSave, GetQuestionAdd(), () =>
		{
			SqlItemsSave(SqlSectionOnTable);
			OnChangeAsync();
		});
	}

	protected async Task SqlItemNewAsync<TItem>() where TItem : SqlTableBase, new()
	{
		if (UserSettings is null || !UserSettings.AccessRightsIsWrite) return;
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsWithQeustion(LocaleCore.Table.TableNew, GetQuestionAdd(), () =>
		{
			SqlItem = SqlItemNew<TItem>();
			SetRouteItemNavigate(SqlItem);
		});
	}

	protected async Task SqlItemCopyAsync()
	{
		if (UserSettings is null || !UserSettings.AccessRightsIsWrite) return;
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

	protected async Task SqlItemEditAsync()
	{
		if (UserSettings is null || !UserSettings.AccessRightsIsWrite) return;
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsSafe(string.Empty, () =>
		{
			SetRouteItemNavigate(SqlItem);
			OnChangeAsync();
		});
	}

	protected async Task SqlItemEditAsync<TItem>(TItem item) where TItem : SqlTableBase, new()
	{
		if (UserSettings is null || !UserSettings.AccessRightsIsWrite) return;
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsSafe(string.Empty, () =>
		{
			SetRouteItemNavigate(SqlItem);
			OnChangeAsync();
		});
	}

	protected void RowRender<TItem>(RowRenderEventArgs<TItem> args) where TItem : SqlTableBase, new()
	{
		if (UserSettings is null || !UserSettings.AccessRightsIsWrite) return;
		if (args.Data is AccessModel access)
		{
			args.Attributes.Add("class", UserSettings.GetColorAccessRights((AccessRightsEnum)access.Rights));
		}
	}

	protected async Task SqlItemSetAsync<TItem>(TItem item) where TItem : SqlTableBase, new()
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsSafe(string.Empty, () =>
		{
			SqlItem = item;
		});
	}

    protected async Task SqlItemMarkAsync()
	{
		if (UserSettings is null || !UserSettings.AccessRightsIsWrite) return;
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		if (SqlItem is null)
		{
			await ShowDialog(LocaleCore.Sql.SqlItemIsNotSelect, LocaleCore.Sql.SqlItemDoSelect).ConfigureAwait(true);
			return;
		}
		
		RunActionsWithQeustion(LocaleCore.Table.TableMark, GetQuestionAdd(), () =>
		{
			BlazorAppSettings.DataAccess.Mark(SqlItem);
			OnChangeAsync();
		});
	}

	protected async Task SqlItemDeleteAsync()
	{
		if (UserSettings is null || !UserSettings.AccessRightsIsWrite) return;
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		if (SqlItem is null)
		{
			await ShowDialog(LocaleCore.Sql.SqlItemIsNotSelect, LocaleCore.Sql.SqlItemDoSelect).ConfigureAwait(true);
			return;
		}
		
		RunActionsWithQeustion(LocaleCore.Table.TableDelete, GetQuestionAdd(), () =>
		{
			BlazorAppSettings.DataAccess.Delete(SqlItem);
			OnChangeAsync();
		});
	}

	protected async Task SqlItemPrinterResourcesClear(PrinterModel printer)
	{
		if (UserSettings is null || !UserSettings.AccessRightsIsWrite) return;
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsWithQeustion(LocaleCore.Print.ResourcesClear, GetQuestionAdd(), () =>
		{
			SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(false, false);
			List<TemplateResourceModel> templateResources = DataContext.GetListNotNullable<TemplateResourceModel>(sqlCrudConfig);
			foreach (TemplateResourceModel templateResource in templateResources)
			{
				if (templateResource.Name.Contains("TTF"))
				{
					TcpClient client = ZplUtils.TcpClientSendData(printer.Ip, printer.Port,
						new()
						{
							new($"^XA^ID"),
							new(templateResource.Name),
							new($"^FS^XZ")
						});
				}
			}
			OnChangeAsync();
		});
	}

	protected async Task SqlItemPrinterResourcesLoad(PrinterModel printer, string fileType)
	{
		if (UserSettings is null || !UserSettings.AccessRightsIsWrite) return;
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsWithQeustion(LocaleCore.Print.ResourcesLoadTtf, GetQuestionAdd(), () =>
		{
			SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
                new SqlFieldOrderModel(nameof(SqlTableBase.Description), SqlFieldOrderEnum.Asc), false, false);
			List<TemplateResourceModel> templateResources = BlazorAppSettings.DataAccess.GetListNotNullable<TemplateResourceModel>(sqlCrudConfig);
			foreach (TemplateResourceModel templateResource in templateResources)
			{
				if (templateResource.Name.Contains(fileType))
				{
					TcpClient client = ZplUtils.TcpClientSendData(printer.Ip, printer.Port,
						new()
						{
							new($"^XA^MNN^LL500~DYE:{templateResource.Name}.TTF,B,T,{templateResource.ImageData.Value.Length},,"),
							new(templateResource.ImageData.Value),
							new($"^XZ")
						});
				}
			}
			OnChangeAsync();
		});
	}

	#endregion
}