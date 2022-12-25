// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.CssStyles;
using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.NomenclaturesCharacteristicsFks;
using DataCore.Sql.TableScaleFkModels.NomenclaturesGroupsFks;
using DataCore.Sql.TableScaleFkModels.PlusTemplatesFks;
using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.BarCodes;
using DataCore.Sql.TableScaleModels.Brands;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Contragents;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.Logs;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.Nomenclatures;
using DataCore.Sql.TableScaleModels.NomenclaturesCharacteristics;
using DataCore.Sql.TableScaleModels.NomenclaturesGroups;
using DataCore.Sql.TableScaleModels.Orders;
using DataCore.Sql.TableScaleModels.OrdersWeighings;
using DataCore.Sql.TableScaleModels.Organizations;
using DataCore.Sql.TableScaleModels.Packages;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusLabels;
using DataCore.Sql.TableScaleModels.PlusPackages;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.PlusWeighings;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.PrintersResources;
using DataCore.Sql.TableScaleModels.PrintersTypes;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.ProductSeries;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.ScalesScreenshots;
using DataCore.Sql.TableScaleModels.Tasks;
using DataCore.Sql.TableScaleModels.TasksTypes;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using DataCore.Sql.TableScaleModels.Versions;
using DataCore.Sql.TableScaleModels.WorkShops;
using FluentValidation.Results;

namespace DataCore.Utils;

public class ValidationUtils
{
    private static void SetValidationFailureLog(ValidationResult result, ref string detailAddition)
    {
        switch (result.IsValid)
        {
            case false:
                foreach (ValidationFailure failure in result.Errors)
                {
                    detailAddition += $"{LocaleCore.Validator.Property} {failure.PropertyName} {LocaleCore.Validator.FailedValidation}. {LocaleCore.Validator.Error}: {failure.ErrorMessage}";
                }
                break;
        }
    }

    public static IValidator GetSqlValidator<T>() where T : SqlTableBase, new() =>
        typeof(T) switch
        {
            var cls when cls == typeof(AccessModel) => new AccessValidator(),
            var cls when cls == typeof(AppModel) => new AppValidator(),
            var cls when cls == typeof(BarCodeModel) => new BarCodeValidator(),
            var cls when cls == typeof(BrandModel) => new BrandValidator(),
            var cls when cls == typeof(BundleModel) => new BundleValidator(),
            var cls when cls == typeof(ContragentModel) => new ContragentValidator(),
            var cls when cls == typeof(DeviceModel) => new DeviceValidator(),
            var cls when cls == typeof(DeviceScaleFkModel) => new DeviceScaleFkValidator(),
            var cls when cls == typeof(DeviceTypeFkModel) => new DeviceTypeFkValidator(),
            var cls when cls == typeof(DeviceTypeModel) => new DeviceTypeValidator(),
            var cls when cls == typeof(LogModel) => new LogValidator(),
            var cls when cls == typeof(LogTypeModel) => new LogTypeValidator(),
            var cls when cls == typeof(NomenclatureModel) => new NomenclatureValidator(),
            var cls when cls == typeof(NomenclatureV2Model) => new NomenclatureV2Validator(),
            var cls when cls == typeof(NomenclatureGroupModel) => new NomenclatureGroupValidator(),
            var cls when cls == typeof(NomenclaturesCharacteristicsModel) => new NomenclaturesCharacteristicsValidator(),
            var cls when cls == typeof(NomenclaturesCharacteristicsFkModel) => new NomenclaturesCharacteristicsFkValidator(),
            var cls when cls == typeof(OrderModel) => new OrderValidator(),
            var cls when cls == typeof(OrderWeighingModel) => new OrderWeighingValidator(),
            var cls when cls == typeof(OrganizationModel) => new OrganizationValidator(),
            var cls when cls == typeof(PackageModel) => new PackageValidator(),
            var cls when cls == typeof(PluLabelModel) => new PluLabelValidator(),
            var cls when cls == typeof(PluModel) => new PluValidator(),
            var cls when cls == typeof(PluScaleModel) => new PluScaleValidator(),
            var cls when cls == typeof(PluWeighingModel) => new PluWeighingValidator(),
            var cls when cls == typeof(PrinterModel) => new PrinterValidator(),
            var cls when cls == typeof(PrinterResourceModel) => new PrinterResourceValidator(),
            var cls when cls == typeof(PrinterTypeModel) => new PrinterTypeValidator(),
            var cls when cls == typeof(ProductionFacilityModel) => new ProductionFacilityValidator(),
            var cls when cls == typeof(ProductSeriesModel) => new ProductSeriesValidator(),
            var cls when cls == typeof(ScaleModel) => new ScaleValidator(),
            var cls when cls == typeof(TaskModel) => new TaskValidator(),
            var cls when cls == typeof(TaskTypeModel) => new TaskTypeValidator(),
            var cls when cls == typeof(TemplateModel) => new TemplateValidator(),
            var cls when cls == typeof(TemplateResourceModel) => new TemplateResourceValidator(),
            var cls when cls == typeof(VersionModel) => new VersionValidator(),
            var cls when cls == typeof(WorkShopModel) => new WorkShopValidator(),
            _ => throw new NotImplementedException()
        };

    public static ValidationResult GetValidationResult<T>(T? item) where T : class, new() =>
        item switch
        {
            // CssStyle
            CssStyleRadzenColumnModel cssStyleRadzenColumn => new CssStyleRadzenColumnValidator().Validate(cssStyleRadzenColumn),
            CssStyleTableBodyModel cssStyleTableBody => new CssStyleTableBodyValidator().Validate(cssStyleTableBody),
            CssStyleTableHeadModel cssStyleTableHead => new CssStyleTableHeadValidator().Validate(cssStyleTableHead),
            // SqlTable
            AccessModel access => new AccessValidator().Validate(access),
            AppModel app => new AppValidator().Validate(app),
            BarCodeModel barCode => new BarCodeValidator().Validate(barCode),
            BrandModel brand => new BrandValidator().Validate(brand),
            BundleModel bundle => new BundleValidator().Validate(bundle),
            ContragentModel contragent => new ContragentValidator().Validate(contragent),
            LogModel log => new LogValidator().Validate(log),
            LogTypeModel logType => new LogTypeValidator().Validate(logType),
            NomenclatureModel nomenclature => new NomenclatureValidator().Validate(nomenclature),
            NomenclatureV2Model nomenclatureV2 => new NomenclatureV2Validator().Validate(nomenclatureV2),
            NomenclaturesCharacteristicsModel nomenclaturesCharacteristics => new NomenclaturesCharacteristicsValidator().Validate(nomenclaturesCharacteristics),
            NomenclaturesCharacteristicsFkModel nomenclatureCharacteristicsFk => new NomenclaturesCharacteristicsFkValidator().Validate(nomenclatureCharacteristicsFk),
            NomenclatureGroupModel nomenclatureGroup => new NomenclatureGroupValidator().Validate(nomenclatureGroup),
            NomenclaturesGroupFkModel nomenclatureGroupFk => new NomenclaturesGroupFkValidator().Validate(nomenclatureGroupFk),
            OrderModel order => new OrderValidator().Validate(order),
            OrderWeighingModel orderWeighing => new OrderWeighingValidator().Validate(orderWeighing),
            OrganizationModel organization => new OrganizationValidator().Validate(organization),
            PackageModel package => new PackageValidator().Validate(package),
            PluLabelModel pluLabel => new PluLabelValidator().Validate(pluLabel),
            PluModel plu => new PluValidator().Validate(plu),
            PluPackageModel package => new PluPackageValidator().Validate(package),
            PluScaleModel pluScale => new PluScaleValidator().Validate(pluScale),
            PluTemplateFkModel pluTemplate => new PluTemplateFkValidator().Validate(pluTemplate),
            PluWeighingModel pluWeighing => new PluWeighingValidator().Validate(pluWeighing),
            PrinterModel printer => new PrinterValidator().Validate(printer),
            PrinterResourceModel printerResource => new PrinterResourceValidator().Validate(printerResource),
            PrinterTypeModel printerType => new PrinterTypeValidator().Validate(printerType),
            ProductionFacilityModel productionFacility => new ProductionFacilityValidator().Validate(productionFacility),
            ProductSeriesModel productSeries => new ProductSeriesValidator().Validate(productSeries),
            ScaleModel scale => new ScaleValidator().Validate(scale),
            ScaleScreenShotModel scaleScreenShot => new ScaleScreenShotValidator().Validate(scaleScreenShot),
            TaskModel task => new TaskValidator().Validate(task),
            TaskTypeModel taskType => new TaskTypeValidator().Validate(taskType),
            TemplateModel template => new TemplateValidator().Validate(template),
            TemplateResourceModel templateResource => new TemplateResourceValidator().Validate(templateResource),
            VersionModel version => new VersionValidator().Validate(version),
            WorkShopModel workShop => new WorkShopValidator().Validate(workShop),
            DeviceModel device => new DeviceValidator().Validate(device),
            DeviceScaleFkModel deviceScaleFk => new DeviceScaleFkValidator().Validate(deviceScaleFk),
            DeviceTypeFkModel deviceTypeFk => new DeviceTypeFkValidator().Validate(deviceTypeFk),
            DeviceTypeModel deviceType => new DeviceTypeValidator().Validate(deviceType),
            _ => throw new NullReferenceException(nameof(item))
        };

    public static bool IsValidation<T>(T? item, ref string detailAddition) where T : class, new()
    {
        if (item is null)
        {
            detailAddition = $"{nameof(item)} is null!";
            return false;
        }

        ValidationResult validationResult = GetValidationResult(item);
        if (!validationResult.IsValid)
        {
            SetValidationFailureLog(validationResult, ref detailAddition);
            return false;
        }

        return true;
    }
}
