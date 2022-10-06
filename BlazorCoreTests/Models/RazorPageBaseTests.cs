// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels;
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
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<AccessModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<AppModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<BarCodeModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<BarCodeTypeModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<ContragentModel>();
		BlazorCore.Model_GetRoutePathItem_IsNotEmpty<HostModel>();
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
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<BarCodeTypeModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<ContragentModel>();
		BlazorCore.Model_GetRoutePathSection_IsNotEmpty<HostModel>();
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
