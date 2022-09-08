// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql;

[TestFixture]
internal class TablesAssertSerializeTests
{
	#region Public and private fields, properties, constructor

	private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void Table_AssertSerialize_AccessModel()
	{
		Helper.TableBaseModelAssertSerialize<AccessModel>();
	}

	[Test]
	public void Table_AssertSerialize_AppModel()
	{
		Helper.TableBaseModelAssertSerialize<AppModel>();
	}

	[Test]
	public void Table_AssertSerialize_BarCodeModel()
	{
		Helper.TableBaseModelAssertSerialize<BarCodeModel>();
	}

	[Test]
	public void Table_AssertSerialize_BarCodeTypeModel()
	{
		Helper.TableBaseModelAssertSerialize<BarCodeTypeModel>();
	}

	[Test]
	public void Table_AssertSerialize_ContragentModel()
	{
		Helper.TableBaseModelAssertSerialize<ContragentModel>();
	}

	[Test]
	public void Table_AssertSerialize_HostModel()
	{
		Helper.TableBaseModelAssertSerialize<HostModel>();
	}

	[Test]
	public void Table_AssertSerialize_LogModel()
	{
		Helper.TableBaseModelAssertSerialize<LogModel>();
	}

	[Test]
	public void Table_AssertSerialize_LogTypeModel()
	{
		Helper.TableBaseModelAssertSerialize<LogTypeModel>();
	}

	[Test]
	public void Table_AssertSerialize_NomenclatureModel()
	{
		Helper.TableBaseModelAssertSerialize<NomenclatureModel>();
	}

	[Test]
	public void Table_AssertSerialize_OrderModel()
	{
		Helper.TableBaseModelAssertSerialize<OrderModel>();
	}

	[Test]
	public void Table_AssertSerialize_OrderWeighingModel()
	{
		Helper.TableBaseModelAssertSerialize<OrderWeighingModel>();
	}

	[Test]
	public void Table_AssertSerialize_OrganizationModel()
	{
		Helper.TableBaseModelAssertSerialize<OrganizationModel>();
	}

	[Test]
	public void Table_AssertSerialize_PluModel()
	{
		Helper.TableBaseModelAssertSerialize<PluModel>();
	}

	[Test]
	public void Table_AssertSerialize_PluLabelModel()
	{
		Helper.TableBaseModelAssertSerialize<PluLabelModel>();
	}

	[Test]
	public void Table_AssertSerialize_PluObsoleteModel()
	{
		Helper.TableBaseModelAssertSerialize<PluObsoleteModel>();
	}

	[Test]
	public void Table_AssertSerialize_PluScaleModel()
	{
		Helper.TableBaseModelAssertSerialize<PluScaleModel>();
	}

	[Test]
	public void Table_AssertSerialize_PluWeighingModel()
	{
		Helper.TableBaseModelAssertSerialize<PluWeighingModel>();
	}

	[Test]
	public void Table_AssertSerialize_PrinterModel()
	{
		Helper.TableBaseModelAssertSerialize<PrinterModel>();
	}

	[Test]
	public void Table_AssertSerialize_PrinterResourceModel()
	{
		Helper.TableBaseModelAssertSerialize<PrinterResourceModel>();
	}

	[Test]
	public void Table_AssertSerialize_PrinterTypeModel()
	{
		Helper.TableBaseModelAssertSerialize<PrinterTypeModel>();
	}

	[Test]
	public void Table_AssertSerialize_ProductionFacilityModel()
	{
		Helper.TableBaseModelAssertSerialize<ProductionFacilityModel>();
	}

	[Test]
	public void Table_AssertSerialize_ProductSeriesModel()
	{
		Helper.TableBaseModelAssertSerialize<ProductSeriesModel>();
	}

	[Test]
	public void Table_AssertSerialize_ScaleModel()
	{
		Helper.TableBaseModelAssertSerialize<ScaleModel>();
	}

	[Test]
	public void Table_AssertSerialize_TaskModel()
	{
		Helper.TableBaseModelAssertSerialize<TaskModel>();
	}

	[Test]
	public void Table_AssertSerialize_TaskTypeModel()
	{
		Helper.TableBaseModelAssertSerialize<TaskTypeModel>();
	}

	[Test]
	public void Table_AssertSerialize_TemplateModel()
	{
		Helper.TableBaseModelAssertSerialize<TemplateModel>();
	}

	[Test]
	public void Table_AssertSerialize_TemplateResourceModel()
	{
		Helper.TableBaseModelAssertSerialize<TemplateResourceModel>();
	}

	[Test]
	public void Table_AssertSerialize_VersionModel()
	{
		Helper.TableBaseModelAssertSerialize<VersionModel>();
	}

	[Test]
	public void Table_AssertSerialize_WorkShopModel()
	{
		Helper.TableBaseModelAssertSerialize<WorkShopModel>();
	}

	#endregion
}
