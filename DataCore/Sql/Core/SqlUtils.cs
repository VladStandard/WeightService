// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels;
using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace DataCore.Sql.Core;

public static partial class SqlUtils
{
	#region Public and private fields and properties

	public static SqlConnectFactory SqlConnect { get; } = SqlConnectFactory.Instance;
	public static DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
	public static readonly string FilePathToken = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\scalesui.xml";
	public static readonly string FilePathLog = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\scalesui.log";

	#endregion

	#region Public and private methods

	private static void FailureLog(ValidationResult result, ref string detailAddition)
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

	public static IValidator GetSqlValidator<T>() where T : SqlTableBase, new()
	{
		return typeof(T) switch
		{
			var cls when cls == typeof(AccessModel) => new AccessValidator(),
			var cls when cls == typeof(AppModel) => new AppValidator(),
			var cls when cls == typeof(BarCodeModel) => new BarCodeValidator(),
			var cls when cls == typeof(BarCodeTypeModel) => new BarCodeTypeValidator(),
			var cls when cls == typeof(ContragentModel) => new ContragentValidator(),
			var cls when cls == typeof(HostModel) => new HostValidator(),
			var cls when cls == typeof(LogModel) => new LogValidator(),
			var cls when cls == typeof(LogTypeModel) => new LogTypeValidator(),
			var cls when cls == typeof(NomenclatureModel) => new NomenclatureValidator(),
			var cls when cls == typeof(OrderModel) => new OrderValidator(),
			var cls when cls == typeof(OrderWeighingModel) => new OrderWeighingValidator(),
			var cls when cls == typeof(OrganizationModel) => new OrganizationValidator(),
			var cls when cls == typeof(PluModel) => new PluValidator(),
			var cls when cls == typeof(PluLabelModel) => new PluLabelValidator(),
			var cls when cls == typeof(PluScaleModel) => new PluScaleValidator(),
			var cls when cls == typeof(PluWeighingModel) => new PluWeighingValidator(),
			var cls when cls == typeof(PrinterModel) => new PrinterValidator(),
			var cls when cls == typeof(PrinterResourceModel) => new PrinterResourceValidator(),
			var cls when cls == typeof(PrinterTypeModel) => new PrinterTypeValidator(),
			var cls when cls == typeof(ProductionFacilityModel) => new ProductionFacilityValidator(),
			var cls when cls == typeof(ProductSeriesModel) => new ProductSeriesValidator(),
			var cls when cls == typeof(ScaleModel) => new ScaleValidator(),
			var cls when cls == typeof(VersionModel) => new VersionValidator(),
			var cls when cls == typeof(TaskModel) => new TaskValidator(),
			var cls when cls == typeof(TaskTypeModel) => new TaskTypeValidator(),
			var cls when cls == typeof(TemplateModel) => new TemplateValidator(),
			var cls when cls == typeof(TemplateResourceModel) => new TemplateResourceValidator(),
			var cls when cls == typeof(WorkShopModel) => new WorkShopValidator(),
			_ => throw new NotImplementedException()
		};
	}

	public static ValidationResult GetSqlValidationResult<T>(T? item) where T : SqlTableBase, new()
	{
		switch (typeof(T))
		{
			case var cls when cls == typeof(AccessModel):
				if (item is AccessModel access)
					return new AccessValidator().Validate(access);
				break;
			case var cls when cls == typeof(AppModel):
				if (item is AppModel app)
					return new AppValidator().Validate(app);
				break;
			case var cls when cls == typeof(BarCodeModel):
				if (item is BarCodeModel barCode)
					return new BarCodeValidator().Validate(barCode);
				break;
			case var cls when cls == typeof(BarCodeTypeModel):
				if (item is BarCodeTypeModel barCodeType)
					return new BarCodeTypeValidator().Validate(barCodeType);
				break;
			case var cls when cls == typeof(ContragentModel):
				if (item is ContragentModel contragent)
					return new ContragentValidator().Validate(contragent);
				break;
			case var cls when cls == typeof(HostModel):
				if (item is HostModel host)
					return new HostValidator().Validate(host);
				break;
			case var cls when cls == typeof(LogModel):
				if (item is LogModel log)
					return new LogValidator().Validate(log);
				break;
			case var cls when cls == typeof(LogTypeModel):
				if (item is LogTypeModel logType)
					return new LogTypeValidator().Validate(logType);
				break;
			case var cls when cls == typeof(NomenclatureModel):
				if (item is NomenclatureModel nomenclature)
					return new NomenclatureValidator().Validate(nomenclature);
				break;
			case var cls when cls == typeof(OrderModel):
				if (item is OrderModel order)
					return new OrderValidator().Validate(order);
				break;
			case var cls when cls == typeof(OrderWeighingModel):
				if (item is OrderWeighingModel orderWeighing)
					return new OrderWeighingValidator().Validate(orderWeighing);
				break;
			case var cls when cls == typeof(OrganizationModel):
				if (item is OrganizationModel organization)
					return new OrganizationValidator().Validate(organization);
				break;
			case var cls when cls == typeof(PluModel):
				if (item is PluModel plu)
					return new PluValidator().Validate(plu);
				break;
			case var cls when cls == typeof(PluLabelModel):
				if (item is PluLabelModel pluLabel)
					return new PluLabelValidator().Validate(pluLabel);
				break;
			case var cls when cls == typeof(PluScaleModel):
				if (item is PluScaleModel pluScale)
					return new PluScaleValidator().Validate(pluScale);
				break;
			case var cls when cls == typeof(PluWeighingModel):
				if (item is PluWeighingModel pluWeighing)
					return new PluWeighingValidator().Validate(pluWeighing);
				break;
			case var cls when cls == typeof(PrinterModel):
				if (item is PrinterModel printer)
					return new PrinterValidator().Validate(printer);
				break;
			case var cls when cls == typeof(PrinterResourceModel):
				if (item is PrinterResourceModel printerResource)
					return new PrinterResourceValidator().Validate(printerResource);
				break;
			case var cls when cls == typeof(PrinterTypeModel):
				if (item is PrinterTypeModel printerType)
					return new PrinterTypeValidator().Validate(printerType);
				break;
			case var cls when cls == typeof(ProductionFacilityModel):
				if (item is ProductionFacilityModel productionFacility)
					return new ProductionFacilityValidator().Validate(productionFacility);
				break;
			case var cls when cls == typeof(ProductSeriesModel):
				if (item is ProductSeriesModel productSeries)
					return new ProductSeriesValidator().Validate(productSeries);
				break;
			case var cls when cls == typeof(ScaleModel):
				if (item is ScaleModel scale)
					return new ScaleValidator().Validate(scale);
				break;
			case var cls when cls == typeof(VersionModel):
				if (item is VersionModel version)
					return new VersionValidator().Validate(version);
				break;
			case var cls when cls == typeof(TaskModel):
				if (item is TaskModel task)
					return new TaskValidator().Validate(task);
				break;
			case var cls when cls == typeof(TaskTypeModel):
				if (item is TaskTypeModel taskType)
					return new TaskTypeValidator().Validate(taskType);
				break;
			case var cls when cls == typeof(TemplateModel):
				if (item is TemplateModel template)
					return new TemplateValidator().Validate(template);
				break;
			case var cls when cls == typeof(TemplateResourceModel):
				if (item is TemplateResourceModel templateResource)
					return new TemplateResourceValidator().Validate(templateResource);
				break;
			case var cls when cls == typeof(WorkShopModel):
				if (item is WorkShopModel workShop)
					return new WorkShopValidator().Validate(workShop);
				break;
		}
		return new(new List<ValidationFailure>() { new(nameof(item), "is not found!") });
	}

	public static bool IsValidation<T>(T? item, ref string detailAddition) where T : SqlTableBase, new()
	{
		if (item is null)
		{
			detailAddition = $"{nameof(item)} is null!";
			return false;
		}

		ValidationResult validationResult = GetSqlValidationResult<T>(item);
		if (!validationResult.IsValid)
		{
			FailureLog(validationResult, ref detailAddition);
			return false;
		}

		return true;
	}

	public static SqlCrudConfigModel GetCrudConfigIsMarked() => GetCrudConfig(null, null, 0, false, false);

	public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldFilterModel>? filters, SqlFieldOrderModel? order, int maxResults, bool isShowMarked, bool isShowOnlyTop)
	{
		maxResults = isShowOnlyTop ? DataAccess.JsonSettingsLocal.SelectTopRowsCount : maxResults;
		SqlCrudConfigModel sqlCrudConfig = new(filters, order, maxResults);
		List<SqlFieldFilterModel> filtersMarked = new() { new(SqlFieldEnum.IsMarked, SqlFieldComparerEnum.Equal, false) };
		if (!isShowMarked)
		{
			switch (sqlCrudConfig.Filters)
			{
				case null:
					sqlCrudConfig.Filters = filtersMarked;
					break;
				default:
					sqlCrudConfig.Filters.AddRange(filtersMarked);
					break;
			}
		}

		return sqlCrudConfig;
	}

	//public static SqlTableScaleEnum GetTableScale(string tableName)
	//{
	//	if (Enum.TryParse(tableName, out SqlTableScaleEnum tableScale))
	//		return tableScale;
	//	return SqlTableScaleEnum.Empty;
	//}

	#endregion
}
