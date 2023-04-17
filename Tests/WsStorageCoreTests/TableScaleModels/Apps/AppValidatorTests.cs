// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleModels.Apps;

namespace WsStorageCoreTests.TableScaleModels.Apps;

[TestFixture]
internal class AppValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        AppModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<AppModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        AppModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<AppModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}