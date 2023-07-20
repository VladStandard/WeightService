// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusCharacteristicsFks;

[TestFixture]
public sealed class PluCharacteristicsFkSerializeTests
{
	[Test]
    public void Item_Serialize_Validate()
    {
		WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluCharacteristicsFkModel>();
	}
}