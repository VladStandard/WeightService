// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCoreTests.Sql.Core;

[TestFixture]
internal class SqlTableBaseTests
{
	#region Public and private fields, properties, constructor

	private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void AssertSqlFieldsDt_Exec_AsString()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldsDt<AccessModel>(true, nameof(AccessModel.ChangeDt));
		Helper.AssertSqlFieldsDt<AccessModel>(true, nameof(AccessModel.CreateDt));
		Helper.AssertSqlFieldsDt<AppModel>(true, nameof(AppModel.ChangeDt));
		Helper.AssertSqlFieldsDt<AppModel>(true, nameof(AppModel.CreateDt));
		Helper.AssertSqlFieldsDt<BarCodeModel>(true, nameof(BarCodeModel.ChangeDt));
		Helper.AssertSqlFieldsDt<BarCodeModel>(true, nameof(BarCodeModel.CreateDt));
		Helper.AssertSqlFieldsDt<BarCodeTypeModel>(true, nameof(BarCodeTypeModel.ChangeDt));
		Helper.AssertSqlFieldsDt<BarCodeTypeModel>(true, nameof(BarCodeTypeModel.CreateDt));
		Helper.AssertSqlFieldsDt<ContragentModel>(true, nameof(ContragentModel.ChangeDt));
		Helper.AssertSqlFieldsDt<ContragentModel>(true, nameof(ContragentModel.CreateDt));
		Helper.AssertSqlFieldsDt<HostModel>(true, nameof(HostModel.ChangeDt));
		Helper.AssertSqlFieldsDt<HostModel>(true, nameof(HostModel.CreateDt));
		Helper.AssertSqlFieldsDt<LogModel>(true, nameof(LogModel.ChangeDt));
		Helper.AssertSqlFieldsDt<LogModel>(true, nameof(LogModel.CreateDt));
		Helper.AssertSqlFieldsDt<LogTypeModel>(true, nameof(LogTypeModel.ChangeDt));
		Helper.AssertSqlFieldsDt<LogTypeModel>(true, nameof(LogTypeModel.CreateDt));
		Helper.AssertSqlFieldsDt<NomenclatureModel>(true, nameof(NomenclatureModel.ChangeDt));
		Helper.AssertSqlFieldsDt<NomenclatureModel>(true, nameof(NomenclatureModel.CreateDt));
		Helper.AssertSqlFieldsDt<OrderModel>(true, nameof(OrderModel.ChangeDt));
		Helper.AssertSqlFieldsDt<OrderModel>(true, nameof(OrderModel.CreateDt));
		Helper.AssertSqlFieldsDt<OrderWeighingModel>(true, nameof(OrderWeighingModel.ChangeDt));
		Helper.AssertSqlFieldsDt<OrderWeighingModel>(true, nameof(OrderWeighingModel.CreateDt));
		Helper.AssertSqlFieldsDt<OrganizationModel>(true, nameof(OrganizationModel.ChangeDt));
		Helper.AssertSqlFieldsDt<OrganizationModel>(true, nameof(OrganizationModel.CreateDt));
		Helper.AssertSqlFieldsDt<PluLabelModel>(true, nameof(PluLabelModel.ChangeDt));
		Helper.AssertSqlFieldsDt<PluLabelModel>(true, nameof(PluLabelModel.CreateDt));
		Helper.AssertSqlFieldsDt<PluModel>(true, nameof(PluModel.ChangeDt));
		Helper.AssertSqlFieldsDt<PluModel>(true, nameof(PluModel.CreateDt));
		Helper.AssertSqlFieldsDt<PluObsoleteModel>(true, nameof(PluObsoleteModel.ChangeDt));
		Helper.AssertSqlFieldsDt<PluObsoleteModel>(true, nameof(PluObsoleteModel.CreateDt));
		Helper.AssertSqlFieldsDt<PluScaleModel>(true, nameof(PluScaleModel.ChangeDt));
		Helper.AssertSqlFieldsDt<PluScaleModel>(true, nameof(PluScaleModel.CreateDt));
		Helper.AssertSqlFieldsDt<PluWeighingModel>(true, nameof(PluWeighingModel.ChangeDt));
		Helper.AssertSqlFieldsDt<PluWeighingModel>(true, nameof(PluWeighingModel.CreateDt));
		Helper.AssertSqlFieldsDt<PrinterModel>(true, nameof(PrinterModel.ChangeDt));
		Helper.AssertSqlFieldsDt<PrinterModel>(true, nameof(PrinterModel.CreateDt));
		Helper.AssertSqlFieldsDt<PrinterResourceModel>(true, nameof(PrinterResourceModel.ChangeDt));
		Helper.AssertSqlFieldsDt<PrinterResourceModel>(true, nameof(PrinterResourceModel.CreateDt));
		Helper.AssertSqlFieldsDt<PrinterTypeModel>(true, nameof(PrinterTypeModel.ChangeDt));
		Helper.AssertSqlFieldsDt<PrinterTypeModel>(true, nameof(PrinterTypeModel.CreateDt));
		Helper.AssertSqlFieldsDt<ProductionFacilityModel>(true, nameof(ProductionFacilityModel.ChangeDt));
		Helper.AssertSqlFieldsDt<ProductionFacilityModel>(true, nameof(ProductionFacilityModel.CreateDt));
		Helper.AssertSqlFieldsDt<ProductSeriesModel>(true, nameof(ProductSeriesModel.ChangeDt));
		Helper.AssertSqlFieldsDt<ProductSeriesModel>(true, nameof(ProductSeriesModel.CreateDt));
		Helper.AssertSqlFieldsDt<ScaleModel>(true, nameof(ScaleModel.ChangeDt));
		Helper.AssertSqlFieldsDt<ScaleModel>(true, nameof(ScaleModel.CreateDt));
		Helper.AssertSqlFieldsDt<TaskModel>(true, nameof(TaskModel.ChangeDt));
		Helper.AssertSqlFieldsDt<TaskModel>(true, nameof(TaskModel.CreateDt));
		Helper.AssertSqlFieldsDt<TaskTypeModel>(true, nameof(TaskTypeModel.ChangeDt));
		Helper.AssertSqlFieldsDt<TaskTypeModel>(true, nameof(TaskTypeModel.CreateDt));
		Helper.AssertSqlFieldsDt<TemplateModel>(true, nameof(TemplateModel.ChangeDt));
		Helper.AssertSqlFieldsDt<TemplateModel>(true, nameof(TemplateModel.ChangeDt));
		Helper.AssertSqlFieldsDt<TemplateModel>(true, nameof(TemplateModel.CreateDt));
		Helper.AssertSqlFieldsDt<TemplateModel>(true, nameof(TemplateModel.CreateDt));
		Helper.AssertSqlFieldsDt<TemplateResourceModel>(true, nameof(TemplateResourceModel.ChangeDt));
		Helper.AssertSqlFieldsDt<TemplateResourceModel>(true, nameof(TemplateResourceModel.CreateDt));
		Helper.AssertSqlFieldsDt<VersionModel>(true, nameof(VersionModel.ChangeDt));
		Helper.AssertSqlFieldsDt<VersionModel>(true, nameof(VersionModel.CreateDt));
		Helper.AssertSqlFieldsDt<VersionModel>(true, nameof(VersionModel.ReleaseDt));
		Helper.AssertSqlFieldsDt<WorkShopModel>(true, nameof(WorkShopModel.ChangeDt));
		Helper.AssertSqlFieldsDt<WorkShopModel>(true, nameof(WorkShopModel.CreateDt));
	}

	#endregion
}
