// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Protocols;
using DataCore.Sql.Core;
using DataCore.Sql.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Sockets;
using DataCore.Sql.Tables;
using Environment = System.Environment;

namespace BlazorCore.Models;

public partial class RazorPageBase : LayoutComponentBase
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

	protected string GetItemTitle(TableBase? item)
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
			PluObsoleteModel => LocaleCore.DeviceControl.ItemPlu,
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

	protected string GetSectionTitle(TableBase? item)
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
			PluObsoleteModel => LocaleCore.DeviceControl.SectionPlus,
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

	protected async Task ItemCancel()
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsSafe(LocaleCore.Table.TableCancel, LocaleCore.Dialog.DialogResultSuccess, LocaleCore.Dialog.DialogResultFail,
			() =>
			{
				SetRouteSectionNavigate(false);
			});

		OnChangeAsync();
	}

	private void ItemScaleSave()
	{
		switch (Item)
		{
			case AccessModel:
				ItemSaveCheck.Access(NotificationService, (AccessModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
				break;
			case TaskModel:
				ItemSaveCheck.Task(NotificationService, (TaskModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
				break;
			case TaskTypeModel:
				ItemSaveCheck.TaskType(NotificationService, (TaskTypeModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
				break;
			case BarCodeTypeModel:
				ItemSaveCheck.BarcodeType(NotificationService, (BarCodeTypeModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
				break;
			case ContragentModel:
				ItemSaveCheck.Contragent(NotificationService, (ContragentModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
				break;
			case HostModel:
				ItemSaveCheck.Host(NotificationService, (HostModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
				break;
			case NomenclatureModel:
				ItemSaveCheck.Nomenclature(NotificationService, (NomenclatureModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
				break;
			case PluObsoleteModel:
				ItemSaveCheck.PluObsolete(NotificationService, (PluObsoleteModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
				break;
			case PluModel:
				ItemSaveCheck.Plu(NotificationService, (PluModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
				break;
			case PluScaleModel:
				if (ParentRazor?.Items is not null)
				{
					List<PluScaleModel> pluScales = ParentRazor.Items.Cast<PluScaleModel>().ToList();
					foreach (PluScaleModel pluScale in pluScales)
					{
						ItemSaveCheck.PluScale(NotificationService, pluScale, SqlTableActionEnum.Save);
					}
				}
				else if (ParentRazor?.Item is not null)
				{
					ItemSaveCheck.PluScale(NotificationService, (PluScaleModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
				}
				break;
			case PrinterResourceModel:
				ItemSaveCheck.PrinterResource(NotificationService, (PrinterResourceModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
				break;
			case PrinterModel:
				ItemSaveCheck.Printer(NotificationService, (PrinterModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
				break;
			case PrinterTypeModel:
				ItemSaveCheck.PrinterType(NotificationService, (PrinterTypeModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
				break;
			case ProductionFacilityModel:
				ItemSaveCheck.ProductionFacility(NotificationService, (ProductionFacilityModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
				break;
			case ScaleModel:
				ItemSaveCheck.Scale(NotificationService, Item, SqlTableActionEnum.Save);
				break;
			case TemplateModel:
				ItemSaveCheck.Template(NotificationService, (TemplateModel?)ParentRazor?.Item, ParentRazor?.TableAction);
				break;
			case TemplateResourceModel:
				ItemSaveCheck.TemplateResource(NotificationService, (TemplateResourceModel?)ParentRazor?.Item, ParentRazor?.TableAction);
				break;
			case WorkShopModel:
				ItemSaveCheck.Workshop(NotificationService, (WorkShopModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
				break;
		}
	}

	protected async Task ItemSave()
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsWithQeustion(LocaleCore.Table.TableSave, LocaleCore.Dialog.DialogResultSuccess,
			LocaleCore.Dialog.DialogResultFail, GetQuestionAdd(),
			() =>
			{
				ItemScaleSave();
				SetRouteSectionNavigate(false);
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
				SetRouteItemNavigate(false, Item, SqlTableActionEnum.Copy);
			});

		OnChangeAsync();
	}

	protected async Task ActionEditAsync(UserSettingsModel? userSettings, bool isNewWindow, bool isParentRazor)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		if (userSettings is null || !userSettings.AccessRightsIsWrite)
			return;

		RunActionsSafe(LocaleCore.Table.TableEdit, LocaleCore.Dialog.DialogResultSuccess, LocaleCore.Dialog.DialogResultFail,
			() =>
			{
				SetRouteItemNavigate(isNewWindow, isParentRazor ? ParentRazor?.Item : Item, SqlTableActionEnum.Edit);
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

	public async Task ActionSaveAsync(UserSettingsModel? userSettings, bool isNewWindow, bool isParentRazor)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		if (userSettings is null || !userSettings.AccessRightsIsWrite)
			return;

		RunActionsSafe(LocaleCore.Table.TableSave, LocaleCore.Dialog.DialogResultSuccess, LocaleCore.Dialog.DialogResultFail,
			() =>
			{
				//AppSettings.DataAccess.Save(isParentRazor ? ParentRazor?.Item : Item);
				InvokeAsync(StateHasChanged);
			});

		OnChangeAsync();
	}

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
