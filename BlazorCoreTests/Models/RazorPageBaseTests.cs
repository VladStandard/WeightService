// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels;
using DataCore.Sql.Xml;
using NUnit.Framework;

namespace BlazorCoreTests.Models;

[TestFixture]
internal class RazorPageBaseTests
{
	#region Public and private fields, properties, constructor

	private BlazorCoreHelper Helper { get; } = BlazorCoreHelper.Instance;

	#endregion
	
	#region Public and private methods

	[Test]
	public void Model_GetRoutePathItem_IsNotEmpty()
	{
		Helper.Model_GetRoutePathItem_IsNotEmpty<AccessModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<AppModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<BarCodeModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<BarCodeTypeModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<ContragentModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<HostModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<LogModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<LogTypeModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<LogQuickModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<NomenclatureModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<OrderModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<OrderWeighingModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<OrganizationModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<PluLabelModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<PluModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<PluObsoleteModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<PluScaleModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<PluWeighingModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<PrinterModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<PrinterResourceModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<PrinterTypeModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<ProductionFacilityModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<ProductSeriesModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<ScaleModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<TaskModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<TaskTypeModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<TemplateModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<TemplateResourceModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<VersionModel>();
		Helper.Model_GetRoutePathItem_IsNotEmpty<WorkShopModel>();
	}

	[Test]
	public void Model_GetRoutePathSection_IsNotEmpty()
	{
		Helper.Model_GetRoutePathSection_IsNotEmpty<AccessModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<AppModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<BarCodeModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<BarCodeTypeModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<ContragentModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<HostModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<LogModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<LogTypeModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<LogQuickModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<NomenclatureModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<OrderModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<OrderWeighingModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<OrganizationModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<PluLabelModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<PluModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<PluObsoleteModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<PluScaleModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<PluWeighingModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<PrinterModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<PrinterResourceModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<PrinterTypeModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<ProductionFacilityModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<ProductSeriesModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<ScaleModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<TaskModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<TaskTypeModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<TemplateModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<TemplateResourceModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<VersionModel>();
		Helper.Model_GetRoutePathSection_IsNotEmpty<WorkShopModel>();
	}

	#endregion
}
