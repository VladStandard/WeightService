// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableScaleFkModels.PlusClipsFks;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusClipsFks;

[TestFixture]
public sealed class PluClipFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlPluClipFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluClipFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlPluClipFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluClipFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}