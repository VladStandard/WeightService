// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Common;

namespace WsStorageCoreTests.TableScaleFkModels.PluStorageMethodFks;

[TestFixture]
public sealed class PluStorageMethodFkContentTests
{
	[Test]
    public void Item_Serialize_Validate()
    {
		WsTestsUtils.DataTests.AssertSqlDbContentSerialize<WsSqlPluStorageMethodFkModel>(WsSqlEnumIsMarked.ShowAll);
	}
}