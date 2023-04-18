// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Enums;
using WsStorageCore.TableScaleFkModels.PlusBundlesFks;
using WsStorageCore.TableScaleModels.Plus;
using WsStorageCore.TableScaleModels.PlusScales;
using WsStorageCore.TableScaleModels.Scales;
using WsStorageCore.Utils;

namespace WsStorageCoreTests.Core;

[TestFixture]
public sealed class DataAccessExtTests
{
	#region Public and private methods

	[Test]
	public void DataAccess_GetListPluScales_CountExists()
	{
        WsTestsUtils.DataCore.AssertAction(() =>
		{
			SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(true, true);
			// Arrange.
			List<ScaleModel> scales = WsTestsUtils.DataCore.DataContext.GetListNotNullableScales(sqlCrudConfig);
			TestContext.WriteLine($"{nameof(scales)}.{nameof(scales.Count)}: {scales.Count}");
			// Assert.
			Assert.IsTrue(scales.Count > 0);
			foreach (ScaleModel scale in scales)
			{
				if (scale.IdentityValueId == 5)
				{
					TestContext.WriteLine($"{nameof(scale)}: {scale.IdentityValueId} | {scale}");
					sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(scale, nameof(PluScaleModel.Scale),
                        false, true, false, false);
					List<PluScaleModel> pluScales = WsTestsUtils.DataCore.DataContext.GetListNotNullablePlusScales(sqlCrudConfig);
					// Act.
					TestContext.WriteLine($"{nameof(pluScales)}.{nameof(pluScales.Count)}: {pluScales.Count}");
				}
			}
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }

	[Test]
	public void DataAccess_GetListPluBundlesFks_CountExists()
	{
		WsTestsUtils.DataCore.AssertAction(() =>
		{
			SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(true, true);
			// Arrange.
			List<PluModel> plus = WsTestsUtils.DataCore.DataContext.GetListNotNullablePlus(sqlCrudConfig);
			TestContext.WriteLine($"{nameof(plus)}.{nameof(plus.Count)}: {plus.Count}");
			// Assert.
			Assert.IsTrue(plus.Count > 0);
			foreach (PluModel plu in plus)
			{
				if (plu.Number == 113)
				{
					TestContext.WriteLine($"{nameof(plu)}: {plu.IdentityValueId} | {plu}");
					sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(plu, nameof(PluBundleFkModel.Plu),
                        false, true, false, false);
                    List<PluBundleFkModel> pluPackages = WsTestsUtils.DataCore.DataContext.GetListNotNullablePlusBundlesFks(sqlCrudConfig);
					// Act.
					TestContext.WriteLine($"{nameof(pluPackages)}.{nameof(pluPackages.Count)}: {pluPackages.Count}");
				}
			}
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }

	#endregion
}