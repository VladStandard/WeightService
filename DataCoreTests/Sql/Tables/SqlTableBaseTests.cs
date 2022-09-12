// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using System;

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
		Helper.AssertSqlFieldDtCheck<AccessModel>(nameof(AccessModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<AccessModel>(nameof(AccessModel.ChangeDt));
	}

	[Test]
	public void AppModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<AppModel>(nameof(AppModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<AppModel>(nameof(AppModel.ChangeDt));
	}

	[Test]
	public void BarCodeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<BarCodeModel>(nameof(BarCodeModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<BarCodeModel>(nameof(BarCodeModel.ChangeDt));
	}

	[Test]
	public void BarCodeTypeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<BarCodeTypeModel>(nameof(BarCodeTypeModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<BarCodeTypeModel>(nameof(BarCodeTypeModel.ChangeDt));
	}

	[Test]
	public void ContragentModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<ContragentModel>(nameof(ContragentModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<ContragentModel>(nameof(ContragentModel.ChangeDt));
	}

	[Test]
	public void HostModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<HostModel>(nameof(HostModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<HostModel>(nameof(HostModel.ChangeDt));
	}

	[Test]
	public void LogModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<LogModel>(nameof(LogModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<LogModel>(nameof(LogModel.ChangeDt));
	}

	[Test]
	public void LogTypeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<LogTypeModel>(nameof(LogTypeModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<LogTypeModel>(nameof(LogTypeModel.ChangeDt));
	}

	[Test]
	public void NomenclatureModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<NomenclatureModel>(nameof(NomenclatureModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<NomenclatureModel>(nameof(NomenclatureModel.ChangeDt));
	}

	[Test]
	public void OrderModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<OrderModel>(nameof(OrderModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<OrderModel>(nameof(OrderModel.ChangeDt));
	}

	[Test]
	public void OrderWeighingModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<OrderWeighingModel>(nameof(OrderWeighingModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<OrderWeighingModel>(nameof(OrderWeighingModel.ChangeDt));
	}

	[Test]
	public void OrganizationModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<OrganizationModel>(nameof(OrganizationModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<OrganizationModel>(nameof(OrganizationModel.ChangeDt));
	}

	[Test]
	public void PluLabelModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<PluLabelModel>(nameof(PluLabelModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<PluLabelModel>(nameof(PluLabelModel.ChangeDt));
	}

	[Test]
	public void PluModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<PluModel>(nameof(PluModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<PluModel>(nameof(PluModel.ChangeDt));
	}

	[Test]
	public void PluObsoleteModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<PluObsoleteModel>(nameof(PluObsoleteModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<PluObsoleteModel>(nameof(PluObsoleteModel.ChangeDt));
	}

	[Test]
	public void PluScaleModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<PluScaleModel>(nameof(PluScaleModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<PluScaleModel>(nameof(PluScaleModel.ChangeDt));
	}

	[Test]
	public void PluWeighingModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<PluWeighingModel>(nameof(PluWeighingModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<PluWeighingModel>(nameof(PluWeighingModel.ChangeDt));
	}

	[Test]
	public void PrinterModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<PrinterModel>(nameof(PrinterModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<PrinterModel>(nameof(PrinterModel.ChangeDt));
	}

	[Test]
	public void PrinterResourceModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<PrinterResourceModel>(nameof(PrinterResourceModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<PrinterResourceModel>(nameof(PrinterResourceModel.ChangeDt));
	}

	[Test]
	public void PrinterTypeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<PrinterTypeModel>(nameof(PrinterTypeModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<PrinterTypeModel>(nameof(PrinterTypeModel.ChangeDt));
	}

	[Test]
	public void ProductionFacilityModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<ProductionFacilityModel>(nameof(ProductionFacilityModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<ProductionFacilityModel>(nameof(ProductionFacilityModel.ChangeDt));
	}

	[Test]
	public void ProductSeriesModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<ProductSeriesModel>(nameof(ProductSeriesModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<ProductSeriesModel>(nameof(ProductSeriesModel.ChangeDt));
	}

	[Test]
	public void ScaleModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<ScaleModel>(nameof(ScaleModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<ScaleModel>(nameof(ScaleModel.ChangeDt));
	}

	[Test]
	public void TaskModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<TaskModel>(nameof(TaskModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<TaskModel>(nameof(TaskModel.ChangeDt));
	}

	[Test]
	public void TaskTypeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<TaskTypeModel>(nameof(TaskTypeModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<TaskTypeModel>(nameof(TaskTypeModel.ChangeDt));
	}

	[Test]
	public void TemplateModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<TemplateModel>(nameof(TemplateModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<TemplateModel>(nameof(TemplateModel.ChangeDt));
	}

	[Test]
	public void TemplateResourceModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<TemplateResourceModel>(nameof(TemplateResourceModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<TemplateResourceModel>(nameof(TemplateResourceModel.ChangeDt));
	}

	[Test]
	public void VersionModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<VersionModel>(nameof(VersionModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<VersionModel>(nameof(VersionModel.ChangeDt));
		Helper.AssertSqlFieldDtCheck<VersionModel>(nameof(VersionModel.ReleaseDt));
		Helper.AssertSqlFieldStringCheck<VersionModel>(nameof(VersionModel.Description));
	}

	[Test]
	public void WorkShopModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		Helper.AssertSqlFieldDtCheck<WorkShopModel>(nameof(WorkShopModel.CreateDt));
		Helper.AssertSqlFieldDtCheck<WorkShopModel>(nameof(WorkShopModel.ChangeDt));
	}

	#endregion
}
