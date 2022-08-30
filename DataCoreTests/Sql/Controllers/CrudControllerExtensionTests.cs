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
					List<HostEntity> hosts = DataCore.DataAccess.Crud.GetListHostsFree(id, isMarked);
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
					List<HostEntity> hosts = DataCore.DataAccess.Crud.GetListHostsBusy(id, isMarked);
				}
			}
		});
	}
	
	[Test]
	public void DbTable_Validate_AccessEntity()
	{
		DataCore.AssertSqlExtensionValidate<AccessEntity>();
	}
	
	[Test]
	public void DbTable_Validate_AppEntity()
	{
		DataCore.AssertSqlExtensionValidate<AppEntity>();
	}
	
	[Test]
	public void DbTable_Validate_BarCodeEntity()
	{
		DataCore.AssertSqlExtensionValidate<BarCodeEntity>();
	}
	
	[Test]
	public void DbTable_Validate_BarCodeTypeEntity()
	{
		DataCore.AssertSqlExtensionValidate<BarCodeTypeEntity>();
	}
	
	[Test]
	public void DbTable_Validate_ContragentEntity()
	{
		DataCore.AssertSqlExtensionValidate<ContragentEntity>();
	}
	
	[Test]
	public void DbTable_Validate_HostEntity()
	{
		DataCore.AssertSqlExtensionValidate<HostEntity>();
	}
	
	[Test]
	public void DbTable_Validate_LogEntity()
	{
		DataCore.AssertSqlExtensionValidate<LogEntity>();
	}
	
	[Test]
	public void DbTable_Validate_LogTypeEntity()
	{
		DataCore.AssertSqlExtensionValidate<LogTypeEntity>();
	}
	
	[Test]
	public void DbTable_Validate_NomenclatureEntity()
	{
		DataCore.AssertSqlExtensionValidate<NomenclatureEntity>();
	}
	
	[Test]
	public void DbTable_Validate_OrderEntity()
	{
		DataCore.AssertSqlExtensionValidate<OrderEntity>();
	}
	
	[Test]
	public void DbTable_Validate_OrderWeighingEntity()
	{
		DataCore.AssertSqlExtensionValidate<OrderWeighingEntity>();
	}
	
	[Test]
	public void DbTable_Validate_OrganizationEntity()
	{
		DataCore.AssertSqlExtensionValidate<OrganizationEntity>();
	}
	
	[Test]
	public void DbTable_Validate_PluEntity()
	{
		DataCore.AssertSqlExtensionValidate<PluEntity>();
	}
	
	[Test]
	public void DbTable_Validate_PluObsoleteEntity()
	{
		DataCore.AssertSqlExtensionValidate<PluObsoleteEntity>();
	}
	
	[Test]
	public void DbTable_Validate_PluScaleEntity()
	{
		DataCore.AssertSqlExtensionValidate<PluScaleEntity>();
	}
	
	[Test]
	public void DbTable_Validate_PluWeighingEntity()
	{
		DataCore.AssertSqlExtensionValidate<PluWeighingEntity>();
	}
	
	[Test]
	public void DbTable_Validate_PrinterEntity()
	{
		DataCore.AssertSqlExtensionValidate<PrinterEntity>();
	}
	
	[Test]
	public void DbTable_Validate_PrinterResourceEntity()
	{
		DataCore.AssertSqlExtensionValidate<PrinterResourceEntity>();
	}
	
	[Test]
	public void DbTable_Validate_PrinterTypeEntity()
	{
		DataCore.AssertSqlExtensionValidate<PrinterTypeEntity>();
	}
	
	[Test]
	public void DbTable_Validate_ProductionFacilityEntity()
	{
		DataCore.AssertSqlExtensionValidate<ProductionFacilityEntity>();
	}
	
	[Test]
	public void DbTable_Validate_ProductSeriesEntity()
	{
		DataCore.AssertSqlExtensionValidate<ProductSeriesEntity>();
	}
	
	[Test]
	public void DbTable_Validate_ScaleEntity()
	{
		DataCore.AssertSqlExtensionValidate<ScaleEntity>();
	}
	
	[Test]
	public void DbTable_Validate_TaskEntity()
	{
		DataCore.AssertSqlExtensionValidate<TaskEntity>();
	}
	
	[Test]
	public void DbTable_Validate_TaskTypeEntity()
	{
		DataCore.AssertSqlExtensionValidate<TaskTypeEntity>();
	}
	
	[Test]
	public void DbTable_Validate_TemplateEntity()
	{
		DataCore.AssertSqlExtensionValidate<TemplateEntity>();
	}
	
	[Test]
	public void DbTable_Validate_TemplateResourceEntity()
	{
		DataCore.AssertSqlExtensionValidate<TemplateResourceEntity>();
	}
	
	[Test]
	public void DbTable_Validate_VersionEntity()
	{
		DataCore.AssertSqlExtensionValidate<VersionEntity>();
	}
	
	[Test]
	public void DbTable_Validate_WorkShopEntity()
	{
		DataCore.AssertSqlExtensionValidate<WorkShopEntity>();
	}
}
