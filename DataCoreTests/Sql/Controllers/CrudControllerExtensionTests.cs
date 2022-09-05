// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace DataCoreTests.Sql.Controllers;

[TestFixture]
internal class CrudControllerExtensionTests
{
	private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

	[Test]
	public void GetFreeHosts_Exec_DoesNotThrow()
	{
		DataCore.AssertAction(() =>
		{
			foreach (long? id in DataCoreEnums.GetLongNullable())
			{
				foreach (bool? isMarked in DataCoreEnums.GetBoolNullable())
				{
					List<HostModel> hosts = DataCore.DataAccess.GetListHostsFree(id, isMarked);
				}
			}
		});
	}

	[Test]
	public void GetBusyHosts_Exec_DoesNotThrow()
	{
		DataCore.AssertAction(() =>
		{
			foreach (int? id in DataCoreEnums.GetIntNullable())
			{
				foreach (bool? isMarked in DataCoreEnums.GetBoolNullable())
				{
					List<HostModel> hosts = DataCore.DataAccess.GetListHostsBusy(id, isMarked);
				}
			}
		});
	}
	
	[Test]
	public void DbTable_Validate_AccessModel()
	{
		DataCore.AssertSqlExtensionValidate<AccessModel>();
	}
	
	[Test]
	public void DbTable_Validate_AppModel()
	{
		DataCore.AssertSqlExtensionValidate<AppModel>();
	}
	
	[Test]
	public void DbTable_Validate_BarCodeModel()
	{
		DataCore.AssertSqlExtensionValidate<BarCodeModel>();
	}
	
	[Test]
	public void DbTable_Validate_BarCodeTypeModel()
	{
		DataCore.AssertSqlExtensionValidate<BarCodeTypeModel>();
	}
	
	[Test]
	public void DbTable_Validate_ContragentModel()
	{
		DataCore.AssertSqlExtensionValidate<ContragentModel>();
	}
	
	[Test]
	public void DbTable_Validate_HostModel()
	{
		DataCore.AssertSqlExtensionValidate<HostModel>();
	}
	
	[Test]
	public void DbTable_Validate_LogModel()
	{
		DataCore.AssertSqlExtensionValidate<LogModel>();
	}
	
	[Test]
	public void DbTable_Validate_LogTypeModel()
	{
		DataCore.AssertSqlExtensionValidate<LogTypeModel>();
	}
	
	[Test]
	public void DbTable_Validate_NomenclatureModel()
	{
		DataCore.AssertSqlExtensionValidate<NomenclatureModel>();
	}
	
	[Test]
	public void DbTable_Validate_OrderModel()
	{
		DataCore.AssertSqlExtensionValidate<OrderModel>();
	}
	
	[Test]
	public void DbTable_Validate_OrderWeighingModel()
	{
		DataCore.AssertSqlExtensionValidate<OrderWeighingModel>();
	}
	
	[Test]
	public void DbTable_Validate_OrganizationModel()
	{
		DataCore.AssertSqlExtensionValidate<OrganizationModel>();
	}
	
	[Test]
	public void DbTable_Validate_PluModel()
	{
		DataCore.AssertSqlExtensionValidate<PluModel>();
	}
	
	[Test]
	public void DbTable_Validate_PluObsoleteModel()
	{
		DataCore.AssertSqlExtensionValidate<PluObsoleteModel>();
	}
	
	[Test]
	public void DbTable_Validate_PluScaleModel()
	{
		DataCore.AssertSqlExtensionValidate<PluScaleModel>();
	}
	
	[Test]
	public void DbTable_Validate_PluWeighingModel()
	{
		DataCore.AssertSqlExtensionValidate<PluWeighingModel>();
	}
	
	[Test]
	public void DbTable_Validate_PrinterModel()
	{
		DataCore.AssertSqlExtensionValidate<PrinterModel>();
	}
	
	[Test]
	public void DbTable_Validate_PrinterResourceModel()
	{
		DataCore.AssertSqlExtensionValidate<PrinterResourceModel>();
	}
	
	[Test]
	public void DbTable_Validate_PrinterTypeModel()
	{
		DataCore.AssertSqlExtensionValidate<PrinterTypeModel>();
	}
	
	[Test]
	public void DbTable_Validate_ProductionFacilityModel()
	{
		DataCore.AssertSqlExtensionValidate<ProductionFacilityModel>();
	}
	
	[Test]
	public void DbTable_Validate_ProductSeriesModel()
	{
		DataCore.AssertSqlExtensionValidate<ProductSeriesModel>();
	}
	
	[Test]
	public void DbTable_Validate_ScaleModel()
	{
		DataCore.AssertSqlExtensionValidate<ScaleModel>();
	}
	
	[Test]
	public void DbTable_Validate_TaskModel()
	{
		DataCore.AssertSqlExtensionValidate<TaskModel>();
	}
	
	[Test]
	public void DbTable_Validate_TaskTypeModel()
	{
		DataCore.AssertSqlExtensionValidate<TaskTypeModel>();
	}
	
	[Test]
	public void DbTable_Validate_TemplateModel()
	{
		DataCore.AssertSqlExtensionValidate<TemplateModel>();
	}
	
	[Test]
	public void DbTable_Validate_TemplateResourceModel()
	{
		DataCore.AssertSqlExtensionValidate<TemplateResourceModel>();
	}
	
	[Test]
	public void DbTable_Validate_VersionModel()
	{
		DataCore.AssertSqlExtensionValidate<VersionModel>();
	}
	
	[Test]
	public void DbTable_Validate_WorkShopModel()
	{
		DataCore.AssertSqlExtensionValidate<WorkShopModel>();
	}
}
