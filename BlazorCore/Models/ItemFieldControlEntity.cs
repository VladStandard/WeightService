// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Sql.Models;
using FluentValidation;
using FluentValidation.Results;
using NHibernate.Impl;
using Radzen;
using static DataCore.ShareEnums;

namespace BlazorCore.Models;

public class ItemFieldControlEntity
{
    #region Public and private fields, properties, constructor

    private AppSettingsHelper AppSettings { get; } = AppSettingsHelper.Instance;

    #endregion

    #region Public and private methods

    public bool ValidateEntity(NotificationService notificationService, BaseEntity? item, string field)
    {
        bool result = item != null;
        string detailAddition = Environment.NewLine;
        switch (item)
        {
            case AccessEntity access:
                result = SqlBaseUtils.IsValidation(access, ref detailAddition);
				break;
            case BarCodeTypeEntity barCodeType:
                result = SqlBaseUtils.IsValidation(barCodeType, ref detailAddition);
				break;
            case ContragentEntity contragent:
                result = SqlBaseUtils.IsValidation(contragent, ref detailAddition);
				break;
            case HostEntity host:
                result = SqlBaseUtils.IsValidation(host, ref detailAddition);
				break;
            case NomenclatureEntity nomenclature:
                result = SqlBaseUtils.IsValidation(nomenclature, ref detailAddition);
				break;
            case OrderEntity order:
                result = SqlBaseUtils.IsValidation(order, ref detailAddition);
				break;
            case OrderWeighingEntity orderWeighing:
                result = SqlBaseUtils.IsValidation(orderWeighing, ref detailAddition);
				break;
            case PluEntity plu:
                result = SqlBaseUtils.IsValidation(plu, ref detailAddition);
                break;
            case PluLabelEntity pluLabel:
                result = SqlBaseUtils.IsValidation(pluLabel, ref detailAddition);
                break;
            case PluObsoleteEntity pluObsolete:
                result = SqlBaseUtils.IsValidation(pluObsolete, ref detailAddition);
                break;
            case PluScaleEntity pluScale:
                result = SqlBaseUtils.IsValidation(pluScale, ref detailAddition);
                break;
            case PluWeighingEntity pluWeighing:
                result = SqlBaseUtils.IsValidation(pluWeighing, ref detailAddition);
                break;
            case PrinterEntity printer:
                result = SqlBaseUtils.IsValidation(printer, ref detailAddition);
                break;
            case PrinterResourceEntity printerResource:
	            result = SqlBaseUtils.IsValidation(printerResource, ref detailAddition);
                break;
            case PrinterTypeEntity printerType:
                result = SqlBaseUtils.IsValidation(printerType, ref detailAddition);
                break;
            case ProductionFacilityEntity productionFacility:
                result = SqlBaseUtils.IsValidation(productionFacility, ref detailAddition);
				break;
            case ProductSeriesEntity productSeries:
                result = SqlBaseUtils.IsValidation(productSeries, ref detailAddition);
				break;
            case ScaleEntity scale:
                result = SqlBaseUtils.IsValidation(scale, ref detailAddition);
				break;
            case TaskEntity task:
                result = SqlBaseUtils.IsValidation(task, ref detailAddition);
				break;
            case TaskTypeEntity taskType:
                result = SqlBaseUtils.IsValidation(taskType, ref detailAddition);
				break;
            case TemplateResourceEntity templateResource:
                result = SqlBaseUtils.IsValidation(templateResource, ref detailAddition);
				break;
            case TemplateEntity template:
                result = SqlBaseUtils.IsValidation(template, ref detailAddition);
				break;
            case WorkShopEntity workshop:
                result = SqlBaseUtils.IsValidation(workshop, ref detailAddition);
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
