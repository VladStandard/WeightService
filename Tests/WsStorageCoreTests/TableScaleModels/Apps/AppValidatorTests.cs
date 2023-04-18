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
        AppModel item = WsTestsUtils.DataCore.CreateNewSubstitute<AppModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        AppModel item = WsTestsUtils.DataCore.CreateNewSubstitute<AppModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}