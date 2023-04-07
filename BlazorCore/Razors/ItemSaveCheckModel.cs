//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore.Sql.Core;
//using DataCore.Sql.Tables;
//using Radzen;

//namespace BlazorCore.Razors;

//public class ItemSaveCheckModel
//{
//	#region Public and private fields, properties, constructor

//	private AppSettingsHelper AppSettings { get; } = AppSettingsHelper.Instance;

//	#endregion

//	#region Public and private methods

//	public void Scale(NotificationService? notificationService, WsSqlTableBase? item, SqlTableActionEnum tableAction)
//	{
//		if (item is null) return;

//		if (item is ScaleModel scale)
//		{
//			// Check PrinterMain is null.
//			if (!FieldControl.ValidateModel(notificationService, scale, LocaleCore.Table.Device)) return;

//			if (scale.Host?.IdentityValueId != 0)
//			{
//				scale.Host = AppSettings.DataAccess.GetItemById<HostModel>(scale.Host?.IdentityValueId);
//				if (!FieldControl.ValidateModel(notificationService, scale.Host, LocaleCore.Table.Host)) return;
//			}
//			else
//				scale.Host = new();


//			if (scale.PrinterMain?.IdentityValueId != 0)
//			{
//				scale.PrinterMain = AppSettings.DataAccess.GetItemById<PrinterModel>(scale.PrinterMain?.IdentityValueId);
//				if (!FieldControl.ValidateModel(notificationService, scale.PrinterMain, LocaleCore.Table.Printer)) return;
//			}
//			else
//				scale.PrinterMain = null;

//			if (scale.PrinterShipping?.IdentityValueId != 0)
//			{
//				scale.PrinterShipping =
//					AppSettings.DataAccess.GetItemById<PrinterModel>(scale.PrinterShipping?.IdentityValueId);
//				if (!FieldControl.ValidateModel(notificationService, scale.PrinterShipping,
//					    LocaleCore.Table.Printer)) return;
//			}
//			else
//				scale.PrinterShipping = null;

//			if (scale.TemplateDefault?.IdentityValueId != 0)
//			{
//				scale.TemplateDefault =
//					AppSettings.DataAccess.GetItemById<TemplateModel>(scale.TemplateDefault?.IdentityValueId);
//				if (!FieldControl.ValidateModel(notificationService, scale.TemplateDefault,
//					    LocaleCore.Table.Template)) return;
//			}
//			else
//				scale.TemplateDefault = null;

//			if (scale.TemplateSeries?.IdentityValueId != 0)
//			{
//				scale.TemplateSeries =
//					AppSettings.DataAccess.GetItemById<TemplateModel>(scale.TemplateSeries?.IdentityValueId);
//				if (!FieldControl.ValidateModel(notificationService, scale.TemplateSeries,
//					    LocaleCore.Table.Template)) return;
//			}
//			else
//				scale.TemplateSeries = null;

//			if (scale.WorkShop?.IdentityValueId != 0)
//			{
//				scale.WorkShop = AppSettings.DataAccess.GetItemById<WorkShopModel>(scale.WorkShop?.IdentityValueId);
//				if (!FieldControl.ValidateModel(notificationService, scale.WorkShop, LocaleCore.Table.Template)) return;
//			}
//			else
//				scale.WorkShop = null;

//			AppSettings.DataAccess.SaveOrUpdate(item, tableAction);
//		}
//	}

//	#endregion
//}
