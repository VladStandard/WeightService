// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Apps;

namespace WsStorageCoreTests.TableScaleModels.Apps;

[TestFixture]
public sealed class AppValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlAppModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlAppModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlAppModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlAppModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}