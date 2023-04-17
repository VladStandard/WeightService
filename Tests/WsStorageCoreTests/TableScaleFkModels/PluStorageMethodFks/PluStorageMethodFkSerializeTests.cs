// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleFkModels.PlusStorageMethodsFks;

namespace WsStorageCoreTests.TableScaleFkModels.PluStorageMethodFks;

[TestFixture]
internal class PluStorageMethodFkContentTests
{
	[Test]
    public void Item_Serialize_Validate()
    {
		DataCoreTestsUtils.DataCore.AssertSqlDbContentSerialize<PluStorageMethodFkModel>();
	}
}