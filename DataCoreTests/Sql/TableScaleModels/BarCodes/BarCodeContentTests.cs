// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.BarCodes;

namespace DataCoreTests.Sql.TableScaleModels.BarCodes;

[TestFixture]
internal class BarCodeContentTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

	[Test]
	public void DbContent_Validate_BarCodeModel()
	{
		DataCore.AssertSqlDbContentValidate<BarCodeModel>();
	}
}
