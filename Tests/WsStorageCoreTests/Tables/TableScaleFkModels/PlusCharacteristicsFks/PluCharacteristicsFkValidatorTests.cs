// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusCharacteristicsFks;

[TestFixture]
public sealed class PluCharacteristicsFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlPluCharacteristicsFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluCharacteristicsFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlPluCharacteristicsFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluCharacteristicsFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}