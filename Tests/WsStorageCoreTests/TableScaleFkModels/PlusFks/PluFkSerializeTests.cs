// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusFks;

namespace WsStorageCoreTests.TableScaleFkModels.PlusFks;

[TestFixture]
public sealed class PluGroupFkSerializeTests
{
	[Test]
    public void Item_Serialize_Validate()
    {
		WsTestsUtils.DataTests.AssertSqlDbContentSerialize<PluFkModel>();
	}
}