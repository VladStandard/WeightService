// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Utils;

namespace WsStorageCoreTests.Core;

[TestFixture]
public sealed class DataAccessExtTests
{
	#region Public and private methods

	[Test]
	public void DataAccess_GetListPluScales_CountExists()
	{
        WsTestsUtils.DataTests.AssertAction(() =>
		{
			WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(true, true);
			// Arrange.
			List<WsSqlScaleModel> scales = WsTestsUtils.DataTests.ContextManager.ContextList.GetListNotNullableScales(sqlCrudConfig);
			TestContext.WriteLine($"{nameof(scales)}.{nameof(scales.Count)}: {scales.Count}");
			// Assert.
			Assert.IsTrue(scales.Count > 0);
			foreach (WsSqlScaleModel scale in scales)
			{
				if (scale.IdentityValueId == 5)
				{
					TestContext.WriteLine($"{nameof(scale)}: {scale.IdentityValueId} | {scale}");
					sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(scale, nameof(WsSqlPluScaleModel.Scale),
                        false, true, false, false);
					List<WsSqlPluScaleModel> pluScales = WsTestsUtils.DataTests.ContextManager.ContextList.GetListNotNullablePlusScales(sqlCrudConfig);
					// Act.
					TestContext.WriteLine($"{nameof(pluScales)}.{nameof(pluScales.Count)}: {pluScales.Count}");
				}
			}
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

	[Test]
	public void DataAccess_GetListPluBundlesFks_CountExists()
	{
		WsTestsUtils.DataTests.AssertAction(() =>
		{
			WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(true, true);
			// Arrange.
			List<WsSqlPluModel> plus = WsTestsUtils.DataTests.ContextManager.ContextList.GetListNotNullablePlus(sqlCrudConfig);
			TestContext.WriteLine($"{nameof(plus)}.{nameof(plus.Count)}: {plus.Count}");
			// Assert.
			Assert.IsTrue(plus.Count > 0);
			foreach (WsSqlPluModel plu in plus)
			{
				if (plu.Number == 113)
				{
					TestContext.WriteLine($"{nameof(plu)}: {plu.IdentityValueId} | {plu}");
					sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(plu, nameof(WsSqlPluBundleFkModel.Plu),
                        false, true, false, false);
                    List<WsSqlPluBundleFkModel> pluPackages = WsTestsUtils.DataTests.ContextManager.ContextList.GetListNotNullablePlusBundlesFks(sqlCrudConfig);
					// Act.
					TestContext.WriteLine($"{nameof(pluPackages)}.{nameof(pluPackages.Count)}: {pluPackages.Count}");
				}
			}
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

	#endregion
}