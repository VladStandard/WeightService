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

    public bool ValidateModel<T>(NotificationService notificationService, T? item, string field) where T : TableModel, new()
    {
        bool result = item is not null;
        string detailAddition = Environment.NewLine;
        if (result)
        {
	        result = SqlUtils.IsValidation<T>(item, ref detailAddition);
        }
		//switch (item)
  //      {
  //          case AccessModel access:
  //              result = SqlUtils.IsValidation(access, ref detailAddition);
  //              break;
  //          case BarCodeTypeModel barCodeType:
  //              result = SqlUtils.IsValidation(barCodeType, ref detailAddition);
  //              break;
  //          case ContragentModel contragent:
  //              result = SqlUtils.IsValidation(contragent, ref detailAddition);
  //              break;
  //          case HostModel host:
  //              result = SqlUtils.IsValidation(host, ref detailAddition);
  //              break;
  //          case NomenclatureModel nomenclature:
  //              result = SqlUtils.IsValidation(nomenclature, ref detailAddition);
  //              break;
  //          case OrderModel order:
  //              result = SqlUtils.IsValidation(order, ref detailAddition);
  //              break;
  //          case OrderWeighingModel orderWeighing:
  //              result = SqlUtils.IsValidation(orderWeighing, ref detailAddition);
  //              break;
  //          case PluModel plu:
  //              result = SqlUtils.IsValidation(plu, ref detailAddition);
  //              break;
  //          case PluLabelModel pluLabel:
  //              result = SqlUtils.IsValidation(pluLabel, ref detailAddition);
  //              break;
  //          case PluObsoleteModel pluObsolete:
  //              result = SqlUtils.IsValidation(pluObsolete, ref detailAddition);
  //              break;
  //          case PluScaleModel pluScale:
  //              result = SqlUtils.IsValidation(pluScale, ref detailAddition);
  //              break;
  //          case PluWeighingModel pluWeighing:
  //              result = SqlUtils.IsValidation(pluWeighing, ref detailAddition);
  //              break;
  //          case PrinterModel printer:
  //              result = SqlUtils.IsValidation(printer, ref detailAddition);
  //              break;
  //          case PrinterResourceModel printerResource:
  //              result = SqlUtils.IsValidation(printerResource, ref detailAddition);
  //              break;
  //          case PrinterTypeModel printerType:
  //              result = SqlUtils.IsValidation(printerType, ref detailAddition);
  //              break;
  //          case ProductionFacilityModel productionFacility:
  //              result = SqlUtils.IsValidation(productionFacility, ref detailAddition);
  //              break;
  //          case ProductSeriesModel productSeries:
  //              result = SqlUtils.IsValidation(productSeries, ref detailAddition);
  //              break;
  //          case ScaleModel scale:
  //              result = SqlUtils.IsValidation(scale, ref detailAddition);
  //              break;
  //          case TaskModel task:
  //              result = SqlUtils.IsValidation(task, ref detailAddition);
  //              break;
  //          case TaskTypeModel taskType:
  //              result = SqlUtils.IsValidation(taskType, ref detailAddition);
  //              break;
  //          case TemplateResourceModel templateResource:
  //              result = SqlUtils.IsValidation(templateResource, ref detailAddition);
  //              break;
  //          case TemplateModel template:
  //              result = SqlUtils.IsValidation(template, ref detailAddition);
  //              break;
  //          case WorkShopModel workshop:
  //              result = SqlUtils.IsValidation(workshop, ref detailAddition);
  //              break;
  //      }
        if (!result)
        {
            NotificationMessage msg = new()
            {
                Severity = NotificationSeverity.Warning,
                Summary = LocaleCore.Action.ActionDataControl,
                Detail = $"{LocaleCore.Action.ActionDataControlField} [{field}]!" +
                    (Equals(detailAddition, Environment.NewLine) ? string.Empty : detailAddition),
                Duration = AppSettingsHelper.Delay
            };
            notificationService.Notify(msg);
            return false;
        }
        return true;
    }

    #endregion
}
