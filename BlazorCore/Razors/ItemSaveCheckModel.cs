//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore.Localizations;
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

//	public void Scale(NotificationService? notificationService, SqlTableBase? item, SqlTableActionEnum tableAction)
//	{
//		if (item is null) return;

//		if (item is ScaleModel scale)
//		{
//			// Check PrinterMain is null.
//			if (!FieldControl.ValidateModel(notificationService, scale, LocaleCore.Table.Device)) return;

//			if (scale.Host?.Identity.Id != 0)
//			{
//				scale.Host = AppSettings.DataAccess.GetItemById<HostModel>(scale.Host?.Identity.Id);
//				if (!FieldControl.ValidateModel(notificationService, scale.Host, LocaleCore.Table.Host)) return;
//			}
//			else
//				scale.Host = new();


//			if (scale.PrinterMain?.Identity.Id != 0)
//			{
//				scale.PrinterMain = AppSettings.DataAccess.GetItemById<PrinterModel>(scale.PrinterMain?.Identity.Id);
//				if (!FieldControl.ValidateModel(notificationService, scale.PrinterMain, LocaleCore.Table.Printer)) return;
//			}
//			else
//				scale.PrinterMain = null;

//			if (scale.PrinterShipping?.Identity.Id != 0)
//			{
//				scale.PrinterShipping =
//					AppSettings.DataAccess.GetItemById<PrinterModel>(scale.PrinterShipping?.Identity.Id);
//				if (!FieldControl.ValidateModel(notificationService, scale.PrinterShipping,
//					    LocaleCore.Table.Printer)) return;
//			}
//			else
//				scale.PrinterShipping = null;

//			if (scale.TemplateDefault?.Identity.Id != 0)
//			{
//				scale.TemplateDefault =
//					AppSettings.DataAccess.GetItemById<TemplateModel>(scale.TemplateDefault?.Identity.Id);
//				if (!FieldControl.ValidateModel(notificationService, scale.TemplateDefault,
//					    LocaleCore.Table.Template)) return;
//			}
//			else
//				scale.TemplateDefault = null;

//			if (scale.TemplateSeries?.Identity.Id != 0)
//			{
//				scale.TemplateSeries =
//					AppSettings.DataAccess.GetItemById<TemplateModel>(scale.TemplateSeries?.Identity.Id);
//				if (!FieldControl.ValidateModel(notificationService, scale.TemplateSeries,
//					    LocaleCore.Table.Template)) return;
//			}
//			else
//				scale.TemplateSeries = null;

//			if (scale.WorkShop?.Identity.Id != 0)
//			{
//				scale.WorkShop = AppSettings.DataAccess.GetItemById<WorkShopModel>(scale.WorkShop?.Identity.Id);
//				if (!FieldControl.ValidateModel(notificationService, scale.WorkShop, LocaleCore.Table.Template)) return;
//			}
//			else
//				scale.WorkShop = null;

//			AppSettings.DataAccess.SaveOrUpdate(item, tableAction);
//		}
//	}

//	#endregion
//}
