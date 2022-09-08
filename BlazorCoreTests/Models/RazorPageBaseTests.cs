// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels;
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
	public void Model_GetRoutePath_IsNotEmpty()
	{
		Helper.Model_GetRoutePath_IsNotEmpty<AccessModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<AppModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<BarCodeModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<BarCodeTypeModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<ContragentModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<HostModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<LogModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<LogTypeModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<NomenclatureModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<OrderModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<OrderWeighingModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<OrganizationModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<PluLabelModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<PluModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<PluObsoleteModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<PluScaleModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<PluWeighingModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<PrinterModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<PrinterResourceModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<PrinterTypeModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<ProductionFacilityModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<ProductSeriesModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<ScaleModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<TaskModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<TaskTypeModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<TemplateModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<TemplateResourceModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<VersionModel>();
		Helper.Model_GetRoutePath_IsNotEmpty<WorkShopModel>();
	}

	#endregion
}
