// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleDbContent;

[TestFixture]
internal class DbContentValidatorTests
{
	private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

	[Test]
	public void DbContent_Validate_Access()
	{
		DataCore.AssertSqlDataValidate<AccessModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_App()
	{
		DataCore.AssertSqlDataValidate<AppModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_BarCode()
	{
		DataCore.AssertSqlDataValidate<BarCodeModel>(1_000);
	}
	
	[Test]
	public void DbContent_Validate_BarCodeType()
	{
		DataCore.AssertSqlDataValidate<BarCodeTypeModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_Contragent()
	{
		DataCore.AssertSqlDataValidate<ContragentModel>(1_000);
	}


	[Test]
	public void DbContent_Validate_Host()
	{
		DataCore.AssertSqlDataValidate<HostModel>(1_000);
	}


	[Test]
	public void DbContent_Validate_Log()
	{
		DataCore.AssertSqlDataValidate<LogModel>(1_000);
	}
	
	[Test]
	public void DbContent_Validate_LogType()
	{
		DataCore.AssertSqlDataValidate<LogTypeModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_Nomenclature()
	{
		DataCore.AssertSqlDataValidate<NomenclatureModel>(1_000);
	}


	[Test]
	public void DbContent_Validate_Order()
	{
		DataCore.AssertSqlDataValidate<OrderModel>(1_000);
	}
	
	[Test]
	public void DbContent_Validate_OrderWeighing()
	{
		DataCore.AssertSqlDataValidate<OrderWeighingModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_Organization()
	{
		DataCore.AssertSqlDataValidate<OrganizationModel>(1_000);
	}


	[Test]
	public void DbContent_Validate_Plu()
	{
		DataCore.AssertSqlDataValidate<PluModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_PluLabel()
	{
		DataCore.AssertSqlDataValidate<PluLabelModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_PluScale()
	{
		DataCore.AssertSqlDataValidate<PluScaleModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_PluWeighing()
	{
		DataCore.AssertSqlDataValidate<PluWeighingModel>(1_000);
	}
	
	[Test]
	public void DbContent_Validate_PrinterResource()
	{
		DataCore.AssertSqlDataValidate<PrinterResourceModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_PrinterTypeModel()
	{
		DataCore.AssertSqlDataValidate<PrinterTypeModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_Printer()
	{
		DataCore.AssertSqlDataValidate<PrinterModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_ProductionFacility()
	{
		DataCore.AssertSqlDataValidate<ProductionFacilityModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_ProductSeries()
	{
		DataCore.AssertSqlDataValidate<ProductSeriesModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_Scale()
	{
		DataCore.AssertSqlDataValidate<ScaleModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_TaskType()
	{
		DataCore.AssertSqlDataValidate<TaskTypeModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_Task()
	{
		DataCore.AssertSqlDataValidate<TaskModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_TemplateResource()
	{
		DataCore.AssertSqlDataValidate<TemplateResourceModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_Template()
	{
		DataCore.AssertSqlDataValidate<TemplateModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_Version()
	{
		DataCore.AssertSqlDataValidate<VersionModel>(1_000);
	}

	[Test]
	public void DbContent_Validate_WorkShop()
	{
		DataCore.AssertSqlDataValidate<WorkShopModel>(1_000);
	}

}
