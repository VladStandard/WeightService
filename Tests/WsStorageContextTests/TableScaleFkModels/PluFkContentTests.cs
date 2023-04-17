// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleFkModels.PlusFks;

namespace WsStorageContextTests.TableScaleFkModels;

[TestFixture]
internal class PluFkContentTests
{
	[Test]
    public void Item_Content_Validate()
    {
		DataCoreTestsUtils.DataCore.AssertSqlDbContentValidate<PluFkModel>();
	}
}