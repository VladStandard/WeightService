// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Sql.Tables;
using Radzen;

namespace BlazorCore.Models;

public class ItemFieldControlEntity
{
    #region Public and private fields, properties, constructor

    private AppSettingsHelper AppSettings { get; } = AppSettingsHelper.Instance;

    #endregion

    #region Public and private methods

    public bool ValidateEntity(NotificationService notificationService, TableModel? item, string field)
    {
        bool result = item != null;
        string detailAddition = Environment.NewLine;
        switch (item)
        {
            case AccessEntity access:
                result = SqlUtils.IsValidation(access, ref detailAddition);
                break;
            case BarCodeTypeEntity barCodeType:
                result = SqlUtils.IsValidation(barCodeType, ref detailAddition);
                break;
            case ContragentEntity contragent:
                result = SqlUtils.IsValidation(contragent, ref detailAddition);
                break;
            case HostEntity host:
                result = SqlUtils.IsValidation(host, ref detailAddition);
                break;
            case NomenclatureEntity nomenclature:
                result = SqlUtils.IsValidation(nomenclature, ref detailAddition);
                break;
            case OrderEntity order:
                result = SqlUtils.IsValidation(order, ref detailAddition);
                break;
            case OrderWeighingEntity orderWeighing:
                result = SqlUtils.IsValidation(orderWeighing, ref detailAddition);
                break;
            case PluEntity plu:
                result = SqlUtils.IsValidation(plu, ref detailAddition);
                break;
            case PluLabelEntity pluLabel:
                result = SqlUtils.IsValidation(pluLabel, ref detailAddition);
                break;
            case PluObsoleteEntity pluObsolete:
                result = SqlUtils.IsValidation(pluObsolete, ref detailAddition);
                break;
            case PluScaleEntity pluScale:
                result = SqlUtils.IsValidation(pluScale, ref detailAddition);
                break;
            case PluWeighingEntity pluWeighing:
                result = SqlUtils.IsValidation(pluWeighing, ref detailAddition);
                break;
            case PrinterEntity printer:
                result = SqlUtils.IsValidation(printer, ref detailAddition);
                break;
            case PrinterResourceEntity printerResource:
                result = SqlUtils.IsValidation(printerResource, ref detailAddition);
                break;
            case PrinterTypeEntity printerType:
                result = SqlUtils.IsValidation(printerType, ref detailAddition);
                break;
            case ProductionFacilityEntity productionFacility:
                result = SqlUtils.IsValidation(productionFacility, ref detailAddition);
                break;
            case ProductSeriesEntity productSeries:
                result = SqlUtils.IsValidation(productSeries, ref detailAddition);
                break;
            case ScaleEntity scale:
                result = SqlUtils.IsValidation(scale, ref detailAddition);
                break;
            case TaskEntity task:
                result = SqlUtils.IsValidation(task, ref detailAddition);
                break;
            case TaskTypeEntity taskType:
                result = SqlUtils.IsValidation(taskType, ref detailAddition);
                break;
            case TemplateResourceEntity templateResource:
                result = SqlUtils.IsValidation(templateResource, ref detailAddition);
                break;
            case TemplateEntity template:
                result = SqlUtils.IsValidation(template, ref detailAddition);
                break;
            case WorkShopEntity workshop:
                result = SqlUtils.IsValidation(workshop, ref detailAddition);
                break;
        }
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
