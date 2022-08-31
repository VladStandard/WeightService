// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
					List<HostModel> hosts = DataCore.DataAccess.Crud.GetListHostsFree(id, isMarked);
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
					List<HostModel> hosts = DataCore.DataAccess.Crud.GetListHostsBusy(id, isMarked);
				}
			}
		});
	}
	
	[Test]
	public void DbTable_Validate_AccessEntity()
	{
		DataCore.AssertSqlExtensionValidate<AccessModel>();
	}
	
	[Test]
	public void DbTable_Validate_AppEntity()
	{
		DataCore.AssertSqlExtensionValidate<AppModel>();
	}
	
	[Test]
	public void DbTable_Validate_BarCodeEntity()
	{
		DataCore.AssertSqlExtensionValidate<BarCodeModel>();
	}
	
	[Test]
	public void DbTable_Validate_BarCodeTypeEntity()
	{
		DataCore.AssertSqlExtensionValidate<BarCodeTypeModel>();
	}
	
	[Test]
	public void DbTable_Validate_ContragentEntity()
	{
		DataCore.AssertSqlExtensionValidate<ContragentModel>();
	}
	
	[Test]
	public void DbTable_Validate_HostEntity()
	{
		DataCore.AssertSqlExtensionValidate<HostModel>();
	}
	
	[Test]
	public void DbTable_Validate_LogEntity()
	{
		DataCore.AssertSqlExtensionValidate<LogModel>();
	}
	
	[Test]
	public void DbTable_Validate_LogTypeEntity()
	{
		DataCore.AssertSqlExtensionValidate<LogTypeModel>();
	}
	
	[Test]
	public void DbTable_Validate_NomenclatureEntity()
	{
		DataCore.AssertSqlExtensionValidate<NomenclatureModel>();
	}
	
	[Test]
	public void DbTable_Validate_OrderEntity()
	{
		DataCore.AssertSqlExtensionValidate<OrderModel>();
	}
	
	[Test]
	public void DbTable_Validate_OrderWeighingEntity()
	{
		DataCore.AssertSqlExtensionValidate<OrderWeighingModel>();
	}
	
	[Test]
	public void DbTable_Validate_OrganizationEntity()
	{
		DataCore.AssertSqlExtensionValidate<OrganizationModel>();
	}
	
	[Test]
	public void DbTable_Validate_PluEntity()
	{
		DataCore.AssertSqlExtensionValidate<PluModel>();
	}
	
	[Test]
	public void DbTable_Validate_PluObsoleteEntity()
	{
		DataCore.AssertSqlExtensionValidate<PluObsoleteModel>();
	}
	
	[Test]
	public void DbTable_Validate_PluScaleEntity()
	{
		DataCore.AssertSqlExtensionValidate<PluScaleModel>();
	}
	
	[Test]
	public void DbTable_Validate_PluWeighingEntity()
	{
		DataCore.AssertSqlExtensionValidate<PluWeighingModel>();
	}
	
	[Test]
	public void DbTable_Validate_PrinterEntity()
	{
		DataCore.AssertSqlExtensionValidate<PrinterModel>();
	}
	
	[Test]
	public void DbTable_Validate_PrinterResourceEntity()
	{
		DataCore.AssertSqlExtensionValidate<PrinterResourceModel>();
	}
	
	[Test]
	public void DbTable_Validate_PrinterTypeEntity()
	{
		DataCore.AssertSqlExtensionValidate<PrinterTypeModel>();
	}
	
	[Test]
	public void DbTable_Validate_ProductionFacilityEntity()
	{
		DataCore.AssertSqlExtensionValidate<ProductionFacilityModel>();
	}
	
	[Test]
	public void DbTable_Validate_ProductSeriesEntity()
	{
		DataCore.AssertSqlExtensionValidate<ProductSeriesModel>();
	}
	
	[Test]
	public void DbTable_Validate_ScaleEntity()
	{
		DataCore.AssertSqlExtensionValidate<ScaleModel>();
	}
	
	[Test]
	public void DbTable_Validate_TaskEntity()
	{
		DataCore.AssertSqlExtensionValidate<TaskModel>();
	}
	
	[Test]
	public void DbTable_Validate_TaskTypeEntity()
	{
		DataCore.AssertSqlExtensionValidate<TaskTypeModel>();
	}
	
	[Test]
	public void DbTable_Validate_TemplateEntity()
	{
		DataCore.AssertSqlExtensionValidate<TemplateModel>();
	}
	
	[Test]
	public void DbTable_Validate_TemplateResourceEntity()
	{
		DataCore.AssertSqlExtensionValidate<TemplateResourceModel>();
	}
	
	[Test]
	public void DbTable_Validate_VersionEntity()
	{
		DataCore.AssertSqlExtensionValidate<VersionModel>();
	}
	
	[Test]
	public void DbTable_Validate_WorkShopEntity()
	{
		DataCore.AssertSqlExtensionValidate<WorkShopModel>();
	}
}
