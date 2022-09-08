// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using FluentValidation.Results;

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

	public static IValidator<T> GetSqlValidator<T>(T? item) where T : TableBase, new()
	{
		return item switch
		{
			AccessModel => new AccessValidator(),
			AppModel => new AppValidator(),
			BarCodeModel => new BarCodeValidator(),
			BarCodeTypeModel => new BarCodeTypeValidator(),
			ContragentModel => new ContragentValidator(),
			HostModel => new HostValidator(),
			LogModel => new LogValidator(),
			LogTypeModel => new LogTypeValidator(),
			NomenclatureModel => new NomenclatureValidator(),
			OrderModel => new OrderValidator(),
			OrderWeighingModel => new OrderWeighingValidator(),
			OrganizationModel => new OrganizationValidator(),
			PluModel => new PluValidator(),
			PluLabelModel => new PluLabelValidator(),
			PluObsoleteModel => new PluObsoleteValidator(),
			PluScaleModel => new PluScaleValidator(),
			PluWeighingModel => new PluWeighingValidator(),
			PrinterModel => new PrinterValidator(),
			PrinterResourceModel => new PrinterResourceValidator(),
			PrinterTypeModel => new PrinterTypeValidator(),
			ProductionFacilityModel => new ProductionFacilityValidator(),
			ProductSeriesModel => new ProductSeriesValidator(),
			ScaleModel => new ScaleValidator(),
			VersionModel => new VersionValidator(),
			TaskModel => new TaskValidator(),
			TaskTypeModel => new TaskTypeValidator(),
			TemplateModel => new TemplateValidator(),
			TemplateResourceModel => new TemplateResourceValidator(),
			WorkShopModel => new WorkShopValidator(),
			_ => throw new NotImplementedException()
		};
	}

	public static bool IsValidation<T>(T? item, ref string detailAddition) where T : TableBase, new()
	{
		if (item is null)
		{
			detailAddition = $"{nameof(item)} is null!";
			return false;
		}

		IValidator<T> validator = GetSqlValidator(item);
		ValidationResult validationResult = validator.Validate(item);
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
