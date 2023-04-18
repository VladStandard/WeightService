// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.TableScaleFkModels;

[TestFixture]
public sealed class PluCharacteristicsFkContentTests
{
	[Test]
    public void Item_Content_Validate()
    {
		WsTestsUtils.DataCore.AssertSqlDbContentValidate<PluCharacteristicsFkModel>();
	}
}