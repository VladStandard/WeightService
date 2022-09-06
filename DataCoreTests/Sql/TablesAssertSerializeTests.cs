// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql;

[TestFixture]
internal class TablesAssertSerializeTests
{
	private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

	[Test]
	public void Table_AssertSerialize_AccessModel()
	{
		DataCore.TableBaseModelAssertSerialize<AccessModel>();
	}

	[Test]
	public void Table_AssertSerialize_AppModel()
	{
		DataCore.TableBaseModelAssertSerialize<AppModel>();
	}

	[Test]
	public void Table_AssertSerialize_BarCodeModel()
	{
		DataCore.TableBaseModelAssertSerialize<BarCodeModel>();
	}

	[Test]
	public void Table_AssertSerialize_BarCodeTypeModel()
	{
		DataCore.TableBaseModelAssertSerialize<BarCodeTypeModel>();
	}

	[Test]
	public void Table_AssertSerialize_ContragentModel()
	{
		DataCore.TableBaseModelAssertSerialize<ContragentModel>();
	}

	[Test]
	public void Table_AssertSerialize_HostModel()
	{
		DataCore.TableBaseModelAssertSerialize<HostModel>();
	}

	[Test]
	public void Table_AssertSerialize_LogModel()
	{
		DataCore.TableBaseModelAssertSerialize<LogModel>();
	}

	[Test]
	public void Table_AssertSerialize_LogTypeModel()
	{
		DataCore.TableBaseModelAssertSerialize<LogTypeModel>();
	}

	[Test]
	public void Table_AssertSerialize_NomenclatureModel()
	{
		DataCore.TableBaseModelAssertSerialize<NomenclatureModel>();
	}

	[Test]
	public void Table_AssertSerialize_OrderModel()
	{
		DataCore.TableBaseModelAssertSerialize<OrderModel>();
	}

	[Test]
	public void Table_AssertSerialize_OrderWeighingModel()
	{
		DataCore.TableBaseModelAssertSerialize<OrderWeighingModel>();
	}

	[Test]
	public void Table_AssertSerialize_OrganizationModel()
	{
		DataCore.TableBaseModelAssertSerialize<OrganizationModel>();
	}

	[Test]
	public void Table_AssertSerialize_PluModel()
	{
		DataCore.TableBaseModelAssertSerialize<PluModel>();
	}

	[Test]
	public void Table_AssertSerialize_PluLabelModel()
	{
		DataCore.TableBaseModelAssertSerialize<PluLabelModel>();
	}

	[Test]
	public void Table_AssertSerialize_PluObsoleteModel()
	{
		DataCore.TableBaseModelAssertSerialize<PluObsoleteModel>();
	}

	[Test]
	public void Table_AssertSerialize_PluScaleModel()
	{
		DataCore.TableBaseModelAssertSerialize<PluScaleModel>();
	}

	[Test]
	public void Table_AssertSerialize_PluWeighingModel()
	{
		DataCore.TableBaseModelAssertSerialize<PluWeighingModel>();
	}

	[Test]
	public void Table_AssertSerialize_PrinterModel()
	{
		DataCore.TableBaseModelAssertSerialize<PrinterModel>();
	}

	[Test]
	public void Table_AssertSerialize_PrinterResourceModel()
	{
		DataCore.TableBaseModelAssertSerialize<PrinterResourceModel>();
	}

	[Test]
	public void Table_AssertSerialize_PrinterTypeModel()
	{
		DataCore.TableBaseModelAssertSerialize<PrinterTypeModel>();
	}

	[Test]
	public void Table_AssertSerialize_ProductionFacilityModel()
	{
		DataCore.TableBaseModelAssertSerialize<ProductionFacilityModel>();
	}

	[Test]
	public void Table_AssertSerialize_ProductSeriesModel()
	{
		DataCore.TableBaseModelAssertSerialize<ProductSeriesModel>();
	}

	[Test]
	public void Table_AssertSerialize_ScaleModel()
	{
		DataCore.TableBaseModelAssertSerialize<ScaleModel>();
	}

	[Test]
	public void Table_AssertSerialize_TaskModel()
	{
		DataCore.TableBaseModelAssertSerialize<TaskModel>();
	}

	[Test]
	public void Table_AssertSerialize_TaskTypeModel()
	{
		DataCore.TableBaseModelAssertSerialize<TaskTypeModel>();
	}

	[Test]
	public void Table_AssertSerialize_TemplateModel()
	{
		DataCore.TableBaseModelAssertSerialize<TemplateModel>();
	}

	[Test]
	public void Table_AssertSerialize_TemplateResourceModel()
	{
		DataCore.TableBaseModelAssertSerialize<TemplateResourceModel>();
	}

	[Test]
	public void Table_AssertSerialize_VersionModel()
	{
		DataCore.TableBaseModelAssertSerialize<VersionModel>();
	}

	[Test]
	public void Table_AssertSerialize_WorkShopModel()
	{
		DataCore.TableBaseModelAssertSerialize<WorkShopModel>();
	}
}
