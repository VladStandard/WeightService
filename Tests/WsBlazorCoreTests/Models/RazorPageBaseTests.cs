// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Boxes;

namespace WsBlazorCoreTests.Models;

[TestFixture]
public sealed class RazorComponentBaseTests
{
	#region Public and private fields, properties, constructor

	private BlazorCoreHelper BlazorCore { get; } = BlazorCoreHelper.Instance;

	#endregion
	
	#region Public and private methods

	[Test]
	public void Model_GetRoutePathItem_IsNotEmpty()
	{
		BlazorCore.DataCore.SetupDevelopVs(false);
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlAccessModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlAppModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<BarCodeModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<BoxModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<BundleModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<ContragentModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<DeviceModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlDeviceScaleFkModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlDeviceTypeFkModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<DeviceTypeModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<LogModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<LogTypeModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<OrderModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<OrderWeighingModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<OrganizationModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlPluBundleFkModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlPluLabelModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlPluModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<PluScaleModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlPluWeighingModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<PrinterModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlPrinterResourceFkModel>();
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
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlAccessModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlAppModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<BarCodeModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<BoxModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<BundleModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<ContragentModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<DeviceModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlDeviceScaleFkModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlDeviceTypeFkModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<DeviceTypeModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<LogModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<LogTypeModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<OrderModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<OrderWeighingModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<OrganizationModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlPluBundleFkModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlPluLabelModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlPluModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<PluScaleModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlPluWeighingModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<PrinterModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlPrinterResourceFkModel>();
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
