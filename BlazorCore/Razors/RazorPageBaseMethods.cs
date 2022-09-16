// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Net.Sockets;
using DataCore.Localizations;
using DataCore.Protocols;
using DataCore.Sql.Core;
using DataCore.Sql.Models;
using DataCore.Sql.Tables;
using Radzen;

namespace BlazorCore.Razors;

public partial class RazorPageBase
{
	#region Public and private methods

	private string GetQuestionAdd()
	{
		return ParentRazor?.Item?.Identity.Name switch
		{
			SqlFieldIdentityEnum.Id =>
				LocaleCore.Dialog.DialogQuestion + Environment.NewLine +
				$"{nameof(ParentRazor.Item.Identity.Id)}: {ParentRazor.Item.Identity.Id}",
			SqlFieldIdentityEnum.Uid =>
				LocaleCore.Dialog.DialogQuestion + Environment.NewLine +
				$"{nameof(ParentRazor.Item.Identity.Uid)}: {ParentRazor.Item.Identity.Uid}",
			_ => string.Empty
		};
	}

	protected string GetItemTitle(SqlTableBase? item)
	{
		string result = string.Empty;
		result = item switch
		{
			AccessModel => LocaleCore.Strings.ItemAccess,
			LogModel => LocaleCore.Strings.ItemLog,
			BarCodeModel => LocaleCore.DeviceControl.ItemBarCode,
			BarCodeTypeModel => LocaleCore.DeviceControl.ItemBarCodeType,
			ContragentModel => LocaleCore.DeviceControl.ItemContragent,
			HostModel => LocaleCore.DeviceControl.ItemHost,
			PluLabelModel => LocaleCore.DeviceControl.ItemLabel,
			NomenclatureModel => LocaleCore.DeviceControl.ItemNomenclature,
			OrderModel => LocaleCore.DeviceControl.ItemOrder,
			OrderWeighingModel => LocaleCore.DeviceControl.ItemOrderWeighing,
			//PluObsoleteModel => LocaleCore.DeviceControl.ItemPlu,
			PluModel => LocaleCore.DeviceControl.ItemPlu,
			PluScaleModel => LocaleCore.DeviceControl.ItemPluScale,
			PrinterModel => LocaleCore.Print.Name,
			PrinterResourceModel => LocaleCore.Print.Resources,
			PrinterTypeModel => LocaleCore.Print.Types,
			ProductSeriesModel => LocaleCore.DeviceControl.ItemProductSeries,
			ProductionFacilityModel => LocaleCore.DeviceControl.ItemProductionFacility,
			ScaleModel => LocaleCore.DeviceControl.ItemScale,
			TemplateResourceModel => LocaleCore.DeviceControl.ItemTemplateResource,
			TemplateModel => LocaleCore.DeviceControl.ItemTemplate,
			PluWeighingModel => LocaleCore.DeviceControl.ItemPluWeighing,
			WorkShopModel => LocaleCore.DeviceControl.ItemWorkShop,
			OrganizationModel => LocaleCore.DeviceControl.ItemOrganization,
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
			LogModel => LocaleCore.Strings.SectionLog,
			BarCodeModel => LocaleCore.DeviceControl.SectionBarCodes,
			BarCodeTypeModel => LocaleCore.DeviceControl.SectionBarCodeTypes,
			ContragentModel => LocaleCore.DeviceControl.SectionContragents,
			HostModel => LocaleCore.DeviceControl.SectionHosts,
			PluLabelModel => LocaleCore.DeviceControl.SectionLabels,
			NomenclatureModel => LocaleCore.DeviceControl.SectionNomenclatures,
			OrderModel => LocaleCore.DeviceControl.SectionOrders,
			OrderWeighingModel => LocaleCore.DeviceControl.SectionOrdersWeighings,
			//PluObsoleteModel => LocaleCore.DeviceControl.SectionPlus,
			PluModel => LocaleCore.DeviceControl.SectionPlus,
			PluScaleModel => LocaleCore.DeviceControl.SectionPlusScales,
			PrinterModel => LocaleCore.Print.Name,
			PrinterResourceModel => LocaleCore.Print.Resources,
			PrinterTypeModel => LocaleCore.Print.Types,
			ProductSeriesModel => LocaleCore.DeviceControl.SectionProductSeries,
			ProductionFacilityModel => LocaleCore.DeviceControl.SectionProductionFacilities,
			ScaleModel => LocaleCore.DeviceControl.SectionScales,
			TemplateResourceModel => LocaleCore.DeviceControl.SectionTemplateResources,
			TemplateModel => LocaleCore.DeviceControl.SectionTemplates,
			PluWeighingModel => LocaleCore.DeviceControl.SectionPlusWeighings,
			WorkShopModel => LocaleCore.DeviceControl.SectionWorkShops,
			OrganizationModel => LocaleCore.DeviceControl.SectionOrganizations,
			_ => result
		};
		return result;
	}

	#endregion

	#region Public and private methods - Actions

	public bool ItemValidate<T>(NotificationService? notificationService, T? item) where T : SqlTableBase, new()
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
					Duration = AppSettingsHelper.Delay
				};
				notificationService?.Notify(msg);
				return false;
			}
			default:
				return true;
		}
	}

	protected async Task ItemCancelAsync()
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsSafe(LocaleCore.Table.TableCancel, LocaleCore.Dialog.DialogResultSuccess, LocaleCore.Dialog.DialogResultFail,
			SetRouteSectionNavigate);

		OnChangeAsync();
	}

	private void ItemSave<T>(T? item) where T : SqlTableBase, new()
	{
		if (item is null) return;
		if (!ItemValidate(NotificationService, item)) return;

		AppSettings.DataAccess.SaveOrUpdate(item, SqlTableActionEnum.Save);
	}

	private void ItemsSave<T>(List<T>? items) where T : SqlTableBase, new()
	{
		if (Items is null) return;

		switch (typeof(T))
		{
			case var cls when cls == typeof(PluScaleModel):
				if (items is not null)
					foreach (T item in items)
						ItemSave(item);
				break;
		}
	}

	protected async Task ItemSaveAsync()
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsWithQeustion(LocaleCore.Table.TableSave, LocaleCore.Dialog.DialogResultSuccess,
			LocaleCore.Dialog.DialogResultFail, GetQuestionAdd(),
			() =>
			{
				ItemSave(Item);
				ItemsSave(Items);
				SetRouteSectionNavigate();
			});

		OnChangeAsync();
	}

	protected async Task ActionNewAsync(UserSettingsModel? userSettings)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		if (userSettings is null || !userSettings.AccessRightsIsWrite)
			return;

		RunActionsSafe(LocaleCore.Table.TableNew, string.Empty, LocaleCore.Dialog.DialogResultFail,
			() =>
			{
				throw new NotImplementedException("Fix here!");
				// Uncomment here.
				//item = new();
				//Identity.Id = null;
				//IdentityUid = null;
				//RouteItemNavigate(isNewWindow, item, DbTableAction.New);
			});

		OnChangeAsync();
	}

	protected async Task ActionCopyAsync(UserSettingsModel? userSettings)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		if (userSettings is null || !userSettings.AccessRightsIsWrite)
			return;

		RunActionsSafe(LocaleCore.Table.TableCopy, LocaleCore.Dialog.DialogResultSuccess, LocaleCore.Dialog.DialogResultFail,
			() =>
			{
				SetRouteItemNavigate(Item, SqlTableActionEnum.Copy);
			});

		OnChangeAsync();
	}

	protected async Task ActionEditAsync(UserSettingsModel? userSettings)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		if (userSettings is null || !userSettings.AccessRightsIsWrite)
			return;

		RunActionsSafe(LocaleCore.Table.TableEdit, LocaleCore.Dialog.DialogResultSuccess, LocaleCore.Dialog.DialogResultFail,
			() =>
			{
				SetRouteItemNavigate(Item, SqlTableActionEnum.Edit);
				InvokeAsync(StateHasChanged);
			});

		OnChangeAsync();
	}

	protected async Task ActionPluScalePlusClickAsync(UserSettingsModel? userSettings, PluScaleModel pluScale, List<PluScaleModel> pluScales)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		if (userSettings is null || !userSettings.AccessRightsIsWrite)
			return;

		//foreach (PluScaleModel item in pluScales)
		//{
		// if (item.Identity.Uid.Equals(pluScale.Identity.Uid))
		// {
		//  item.IsActive = pluScale.IsActive;
		// }
		//}

		OnChangeAsync();
	}

	//public async Task ActionSaveAsync(UserSettingsModel? userSettings, bool isNewWindow, bool isParentRazor)
	//{
	//	await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

	//	if (userSettings is null || !userSettings.AccessRightsIsWrite)
	//		return;

	//	RunActionsSafe(LocaleCore.Table.TableSave, LocaleCore.Dialog.DialogResultSuccess, LocaleCore.Dialog.DialogResultFail,
	//		() =>
	//		{
	//			InvokeAsync(StateHasChanged);
	//		});

	//	OnChangeAsync();
	//}

	protected async Task ActionMarkAsync(UserSettingsModel? userSettings, bool isNewWindow, bool isParentRazor)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		if (userSettings is null || !userSettings.AccessRightsIsWrite)
			return;

		RunActionsWithQeustion(LocaleCore.Table.TableMark, LocaleCore.Dialog.DialogResultSuccess,
			LocaleCore.Dialog.DialogResultFail, GetQuestionAdd(),
			() =>
			{
				AppSettings.DataAccess.Mark(isParentRazor ? ParentRazor?.Item : Item);
			});

		OnChangeAsync();
	}

	protected async Task ActionDeleteAsync(UserSettingsModel? userSettings, bool isParentRazor)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		if (userSettings is null || !userSettings.AccessRightsIsWrite)
			return;

		RunActionsWithQeustion(LocaleCore.Table.TableDelete, LocaleCore.Dialog.DialogResultSuccess,
			LocaleCore.Dialog.DialogResultFail, GetQuestionAdd(),
			() =>
			{
				AppSettings.DataAccess.Delete(isParentRazor ? ParentRazor?.Item : Item);
			});

		OnChangeAsync();
	}

	protected async Task PrinterResourcesClear(UserSettingsModel? userSettings, PrinterModel printer)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		if (userSettings is null || !userSettings.AccessRightsIsWrite)
			return;

		RunActionsWithQeustion(LocaleCore.Print.ResourcesClear, LocaleCore.Dialog.DialogResultSuccess,
			LocaleCore.Dialog.DialogResultFail, GetQuestionAdd(),
			() =>
			{
				SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Description), 0, false, false);
				List<TemplateResourceModel> templateResources = AppSettings.DataAccess.GetList<TemplateResourceModel>(sqlCrudConfig);
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
			});

		OnChangeAsync();
	}

	protected async Task PrinterResourcesLoad(UserSettingsModel? userSettings, PrinterModel printer, string fileType)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		if (userSettings is null || !userSettings.AccessRightsIsWrite)
			return;

		RunActionsWithQeustion(LocaleCore.Print.ResourcesLoadTtf, LocaleCore.Dialog.DialogResultSuccess,
			LocaleCore.Dialog.DialogResultFail, GetQuestionAdd(),
			() =>
			{
				SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Description), 0, false, false);
				List<TemplateResourceModel> templateResources = AppSettings.DataAccess.GetList<TemplateResourceModel>(sqlCrudConfig);
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
			});

		OnChangeAsync();
	}

	#endregion
}
