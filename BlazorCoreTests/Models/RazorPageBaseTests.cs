// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.BarCodes;
using DataCore.Sql.TableScaleModels.Contragents;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.Logs;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.Nomenclatures;
using DataCore.Sql.TableScaleModels.Orders;
using DataCore.Sql.TableScaleModels.OrdersWeighings;
using DataCore.Sql.TableScaleModels.Organizations;
using DataCore.Sql.TableScaleModels.Packages;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusLabels;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.PlusWeighings;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.PrintersResources;
using DataCore.Sql.TableScaleModels.PrintersTypes;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.ProductSeries;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.Tasks;
using DataCore.Sql.TableScaleModels.TasksTypes;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using DataCore.Sql.TableScaleModels.Versions;
using DataCore.Sql.TableScaleModels.WorkShops;
using DataCore.Sql.Xml;
using NUnit.Framework;

namespace BlazorCoreTests.Models;

[TestFixture]
internal class RazorComponentBaseTests
{
	#region Public and private fields, properties, constructor

	private BlazorCoreHelper BlazorCore { get; } = BlazorCoreHelper.Instance;

	#endregion
	
	#region Public and private methods

	[Test]
	public void Model_GetRoutePathItem_IsNotEmpty()
	{
		BlazorCore.DataCore.SetupDebug();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<AccessModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<AppModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<BarCodeModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<ContragentModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<DeviceModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<DeviceTypeModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<DeviceTypeFkModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<DeviceScaleFkModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<LogModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<LogTypeModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<LogQuickModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<NomenclatureModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<OrderModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<OrderWeighingModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<OrganizationModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<PackageModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<PluLabelModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<PluModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<PluScaleModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<PluWeighingModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<PrinterModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<PrinterResourceModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<PrinterTypeModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<ProductionFacilityModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<ProductSeriesModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<ScaleModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<TaskModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<TaskTypeModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<TemplateModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<TemplateResourceModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<VersionModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WorkShopModel>();
	}

	[Test]
	public void Model_GetRoutePathSection_IsNotEmpty()
	{
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<AccessModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<AppModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<BarCodeModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<ContragentModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<DeviceModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<DeviceTypeModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<DeviceTypeFkModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<DeviceScaleFkModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<LogModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<LogTypeModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<LogQuickModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<NomenclatureModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<OrderModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<OrderWeighingModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<OrganizationModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<PackageModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<PluLabelModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<PluModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<PluScaleModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<PluWeighingModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<PrinterModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<PrinterResourceModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<PrinterTypeModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<ProductionFacilityModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<ProductSeriesModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<ScaleModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<TaskModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<TaskTypeModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<TemplateModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<TemplateResourceModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<VersionModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WorkShopModel>();
	}

	#endregion
}
