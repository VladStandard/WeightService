// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.Core;

[TestFixture]
internal class SqlTableBaseTests
{
	#region Public and private fields, properties, constructor

	private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void AccessModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<AccessModel>(nameof(AccessModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<AccessModel>(nameof(AccessModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<AccessModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void AppModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<AppModel>(nameof(AppModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<AppModel>(nameof(AppModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<AppModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void BarCodeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<BarCodeModel>(nameof(BarCodeModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<BarCodeModel>(nameof(BarCodeModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<BarCodeModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void BarCodeTypeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<BarCodeTypeModel>(nameof(BarCodeTypeModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<BarCodeTypeModel>(nameof(BarCodeTypeModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<BarCodeTypeModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void ContragentModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<ContragentModel>(nameof(ContragentModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<ContragentModel>(nameof(ContragentModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<ContragentModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void HostModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<HostModel>(nameof(HostModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<HostModel>(nameof(HostModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<HostModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void LogModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<LogModel>(nameof(LogModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<LogModel>(nameof(LogModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<LogModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void LogTypeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<LogTypeModel>(nameof(LogTypeModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<LogTypeModel>(nameof(LogTypeModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<LogTypeModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void NomenclatureModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<NomenclatureModel>(nameof(NomenclatureModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<NomenclatureModel>(nameof(NomenclatureModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<NomenclatureModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void OrderModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<OrderModel>(nameof(OrderModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<OrderModel>(nameof(OrderModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<OrderModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void OrderWeighingModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<OrderWeighingModel>(nameof(OrderWeighingModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<OrderWeighingModel>(nameof(OrderWeighingModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<OrderWeighingModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void OrganizationModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<OrganizationModel>(nameof(OrganizationModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<OrganizationModel>(nameof(OrganizationModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<OrganizationModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PackageModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<PackageModel>(nameof(PackageModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<PackageModel>(nameof(PackageModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<PackageModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PluLabelModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<PluLabelModel>(nameof(PluLabelModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<PluLabelModel>(nameof(PluLabelModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<PluLabelModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PluModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<PluModel>(nameof(PluModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<PluModel>(nameof(PluModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<PluModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PluScaleModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<PluScaleModel>(nameof(PluScaleModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<PluScaleModel>(nameof(PluScaleModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<PluScaleModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PluWeighingModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<PluWeighingModel>(nameof(PluWeighingModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<PluWeighingModel>(nameof(PluWeighingModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<PluWeighingModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PrinterModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<PrinterModel>(nameof(PrinterModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<PrinterModel>(nameof(PrinterModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<PrinterModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PrinterResourceModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<PrinterResourceModel>(nameof(PrinterResourceModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<PrinterResourceModel>(nameof(PrinterResourceModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<PrinterResourceModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PrinterTypeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<PrinterTypeModel>(nameof(PrinterTypeModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<PrinterTypeModel>(nameof(PrinterTypeModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<PrinterTypeModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void ProductionFacilityModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<ProductionFacilityModel>(nameof(ProductionFacilityModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<ProductionFacilityModel>(nameof(ProductionFacilityModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<ProductionFacilityModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void ProductSeriesModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<ProductSeriesModel>(nameof(ProductSeriesModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<ProductSeriesModel>(nameof(ProductSeriesModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<ProductSeriesModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void ScaleModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<ScaleModel>(nameof(ScaleModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<ScaleModel>(nameof(ScaleModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<ScaleModel>(nameof(SqlTableBase.IsMarked));
		//Helper.AssertSqlFieldStringCheck<ScaleModel>(nameof(ScaleModel.Host.Name));
		Helper.AssertSqlPropertyCheckString<ScaleModel>(nameof(ScaleModel.Description));
	}

	[Test]
	public void TaskModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<TaskModel>(nameof(TaskModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<TaskModel>(nameof(TaskModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<TaskModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void TaskTypeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<TaskTypeModel>(nameof(TaskTypeModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<TaskTypeModel>(nameof(TaskTypeModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<TaskTypeModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void TemplateModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<TemplateModel>(nameof(TemplateModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<TemplateModel>(nameof(TemplateModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<TemplateModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void TemplateResourceModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<TemplateResourceModel>(nameof(TemplateResourceModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<TemplateResourceModel>(nameof(TemplateResourceModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<TemplateResourceModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void VersionModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<VersionModel>(nameof(VersionModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<VersionModel>(nameof(VersionModel.ChangeDt));
		Helper.AssertSqlPropertyCheckDt<VersionModel>(nameof(VersionModel.ReleaseDt));
		Helper.AssertSqlPropertyCheckString<VersionModel>(nameof(VersionModel.Description));
		Helper.AssertSqlPropertyCheckString<VersionModel>(nameof(VersionModel.Version));
		Helper.AssertSqlPropertyCheckBool<VersionModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void WorkShopModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlPropertyCheckDt<WorkShopModel>(nameof(WorkShopModel.CreateDt));
		Helper.AssertSqlPropertyCheckDt<WorkShopModel>(nameof(WorkShopModel.ChangeDt));
		Helper.AssertSqlPropertyCheckBool<WorkShopModel>(nameof(WorkShopModel.IsMarked));
	}

	#endregion
}
