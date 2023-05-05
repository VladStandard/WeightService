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
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlBarCodeModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlBoxModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlBundleModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlContragentModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlDeviceModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlDeviceScaleFkModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlDeviceTypeFkModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlDeviceTypeModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlLogModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlLogTypeModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlOrderModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlOrderWeighingModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlOrganizationModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlPluBundleFkModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlPluLabelModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlPluModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlPluScaleModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlPluWeighingModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlPrinterModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlPrinterResourceFkModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlPrinterTypeModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlProductionFacilityModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlProductSeriesModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlScaleModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlTaskModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlTaskTypeModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlTemplateModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlTemplateResourceModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlVersionModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<WsSqlWorkShopModel>();
	}

	[Test]
	public void Model_GetRoutePathSection_IsNotEmpty()
	{
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlAccessModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlAppModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlBarCodeModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlBoxModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlBundleModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlContragentModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlDeviceModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlDeviceScaleFkModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlDeviceTypeFkModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlDeviceTypeModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlLogModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlLogTypeModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlOrderModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlOrderWeighingModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlOrganizationModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlPluBundleFkModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlPluLabelModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlPluModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlPluScaleModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlPluWeighingModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlPrinterModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlPrinterResourceFkModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlPrinterTypeModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlProductionFacilityModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlProductSeriesModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlScaleModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlTaskModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlTaskTypeModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlTemplateModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlTemplateResourceModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlVersionModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<WsSqlWorkShopModel>();
	}

	#endregion
}
