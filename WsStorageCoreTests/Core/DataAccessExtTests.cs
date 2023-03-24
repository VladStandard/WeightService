// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;

namespace WsStorageCoreTests.Core;

[TestFixture]
internal class DataAccessExtTests
{
	#region Public and private methods

	[Test]
	public void DataAccess_GetListPluScales_CountExists()
	{
        DataCoreTestsUtils.DataCore.AssertAction(() =>
		{
			SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(true, true);
			// Arrange.
			List<ScaleModel> scales = DataCoreTestsUtils.DataCore.DataContext.GetListNotNullable<ScaleModel>(sqlCrudConfig);
			TestContext.WriteLine($"{nameof(scales)}.{nameof(scales.Count)}: {scales.Count}");
			// Assert.
			Assert.IsTrue(scales.Count > 0);
			foreach (ScaleModel scale in scales)
			{
				if (scale.IdentityValueId == 5)
				{
					TestContext.WriteLine($"{nameof(scale)}: {scale.IdentityValueId} | {scale}");
					sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(scale, nameof(PluScaleModel.Scale),
                        false, true, false, false);
					List<PluScaleModel> pluScales = DataCoreTestsUtils.DataCore.DataContext.GetListNotNullable<PluScaleModel>(sqlCrudConfig);
					// Act.
					TestContext.WriteLine($"{nameof(pluScales)}.{nameof(pluScales.Count)}: {pluScales.Count}");
				}
			}
        }, false, new() { Configuration.DevelopVS, Configuration.ReleaseVS });
    }

	[Test]
	public void DataAccess_GetListPluBundlesFks_CountExists()
	{
		DataCoreTestsUtils.DataCore.AssertAction(() =>
		{
			SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(true, true);
			// Arrange.
			List<PluModel> plus = DataCoreTestsUtils.DataCore.DataContext.GetListNotNullable<PluModel>(sqlCrudConfig);
			TestContext.WriteLine($"{nameof(plus)}.{nameof(plus.Count)}: {plus.Count}");
			// Assert.
			Assert.IsTrue(plus.Count > 0);
			foreach (PluModel plu in plus)
			{
				if (plu.Number == 113)
				{
					TestContext.WriteLine($"{nameof(plu)}: {plu.IdentityValueId} | {plu}");
					sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(plu, nameof(PluBundleFkModel.Plu),
                        false, true, false, false);
                    List<PluBundleFkModel> pluPackages = DataCoreTestsUtils.DataCore.DataContext.GetListNotNullable<PluBundleFkModel>(sqlCrudConfig);
					// Act.
					TestContext.WriteLine($"{nameof(pluPackages)}.{nameof(pluPackages.Count)}: {pluPackages.Count}");
				}
			}
        }, false, new() { Configuration.DevelopVS, Configuration.ReleaseVS });
    }

	#endregion
}