// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.Core;

[TestFixture]
internal class DataAcessExtTests
{
	#region Public and private fields, properties, constructor

	private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void DataAccess_GetListPluScales_CountExists()
	{
		Helper.AssertAction(() =>
		{
			// Arrange.
			List<ScaleModel> scales = Helper.DataAccess.GetListScales(true, false, false);
			TestContext.WriteLine($"{nameof(scales)}.{nameof(scales.Count)}: {scales.Count}");
			// Assert.
			Assert.IsTrue(scales.Count > 0);
			foreach (ScaleModel scale in scales)
			{
				if (scale.IdentityValueId == 5)
				{
					TestContext.WriteLine($"{nameof(scale)}: {scale.IdentityValueId} | {scale}");
					List<PluScaleModel> pluScales = Helper.DataAccess.GetListPluScales(false, true, scale);
					// Act.
					TestContext.WriteLine($"{nameof(pluScales)}.{nameof(pluScales.Count)}: {pluScales.Count}");
					// Assert.
					//Assert.IsTrue(pluScales.Count > 0);
				}
			}
		});
	}

	#endregion
}
