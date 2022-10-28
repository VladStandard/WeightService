// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql;

[TestFixture]
internal class TablesToStringTests
{
    private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

    [Test]
    public void DbTable_Validate_AccessModel()
    {
        DataCore.TableBaseModelAssertToString<AccessModel>();
    }

    [Test]
    public void DbTable_Validate_AppModel()
    {
        DataCore.TableBaseModelAssertToString<AppModel>();
    }

    [Test]
    public void DbTable_Validate_BarCodeModel()
    {
        DataCore.TableBaseModelAssertToString<BarCodeModel>();
    }

    //[Test]
    //public void DbTable_Validate_BarCodeTypeModel()
    //{
    //    DataCore.TableBaseModelAssertToString<BarCodeTypeModel>();
    //}

    [Test]
    public void DbTable_Validate_ContragentModel()
    {
        DataCore.TableBaseModelAssertToString<ContragentModel>();
    }

    [Test]
    public void DbTable_Validate_DeviceModel()
    {
	    DataCore.TableBaseModelAssertToString<DeviceModel>();
    }

	[Test]
	public void DbTable_Validate_DeviceTypeModel()
	{
		DataCore.TableBaseModelAssertToString<DeviceTypeModel>();
	}

	[Test]
	public void DbTable_Validate_DeviceTypeFkModel()
	{
		DataCore.TableBaseModelAssertToString<DeviceTypeFkModel>();
	}

	[Test]
	public void DbTable_Validate_DeviceScaleFkModel()
	{
		DataCore.TableBaseModelAssertToString<DeviceScaleFkModel>();
	}

    [Test]
    public void DbTable_Validate_LogModel()
    {
        DataCore.TableBaseModelAssertToString<LogModel>();
    }

    [Test]
    public void DbTable_Validate_LogTypeModel()
    {
        DataCore.TableBaseModelAssertToString<LogTypeModel>();
    }

    [Test]
    public void DbTable_Validate_NomenclatureModel()
    {
        DataCore.TableBaseModelAssertToString<NomenclatureModel>();
    }

    [Test]
    public void DbTable_Validate_OrderModel()
    {
        DataCore.TableBaseModelAssertToString<OrderModel>();
    }

    [Test]
    public void DbTable_Validate_OrderWeighingModel()
    {
        DataCore.TableBaseModelAssertToString<OrderWeighingModel>();
    }

    [Test]
    public void DbTable_Validate_OrganizationModel()
    {
        DataCore.TableBaseModelAssertToString<OrganizationModel>();
    }

    [Test]
    public void DbTable_Validate_PackageModel()
    {
        DataCore.TableBaseModelAssertToString<PackageModel>();
    }

    [Test]
    public void DbTable_Validate_PluModel()
    {
        DataCore.TableBaseModelAssertToString<PluModel>();
    }

    [Test]
    public void DbTable_Validate_PluLabelModel()
    {
        DataCore.TableBaseModelAssertToString<PluLabelModel>();
    }

    [Test]
    public void DbTable_Validate_PluScaleModel()
    {
        DataCore.TableBaseModelAssertToString<PluScaleModel>();
    }

    [Test]
    public void DbTable_Validate_PluWeighingModel()
    {
        DataCore.TableBaseModelAssertToString<PluWeighingModel>();
    }

    [Test]
    public void DbTable_Validate_PrinterModel()
    {
        DataCore.TableBaseModelAssertToString<PrinterModel>();
    }

    [Test]
    public void DbTable_Validate_PrinterResourceModel()
    {
        DataCore.TableBaseModelAssertToString<PrinterResourceModel>();
    }

    [Test]
    public void DbTable_Validate_PrinterTypeModel()
    {
        DataCore.TableBaseModelAssertToString<PrinterTypeModel>();
    }

    [Test]
    public void DbTable_Validate_ProductionFacilityModel()
    {
        DataCore.TableBaseModelAssertToString<ProductionFacilityModel>();
    }

    [Test]
    public void DbTable_Validate_ProductSeriesModel()
    {
        DataCore.TableBaseModelAssertToString<ProductSeriesModel>();
    }

    [Test]
    public void DbTable_Validate_ScaleModel()
    {
        DataCore.TableBaseModelAssertToString<ScaleModel>();
    }

    [Test]
    public void DbTable_Validate_TaskModel()
    {
        DataCore.TableBaseModelAssertToString<TaskModel>();
    }

    [Test]
    public void DbTable_Validate_TaskTypeModel()
    {
        DataCore.TableBaseModelAssertToString<TaskTypeModel>();
    }

    [Test]
    public void DbTable_Validate_TemplateModel()
    {
        DataCore.TableBaseModelAssertToString<TemplateModel>();
    }

    [Test]
    public void DbTable_Validate_TemplateResourceModel()
    {
        DataCore.TableBaseModelAssertToString<TemplateResourceModel>();
    }

    [Test]
    public void DbTable_Validate_VersionModel()
    {
        DataCore.TableBaseModelAssertToString<VersionModel>();
    }

    [Test]
    public void DbTable_Validate_WorkShopModel()
    {
        DataCore.TableBaseModelAssertToString<WorkShopModel>();
    }
}
