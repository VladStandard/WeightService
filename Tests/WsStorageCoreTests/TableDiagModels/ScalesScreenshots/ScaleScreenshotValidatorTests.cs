// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableDiagModels.ScalesScreenshots;

namespace WsStorageCoreTests.TableDiagModels.ScalesScreenshots;

[TestFixture]
internal class ScaleScreenShotValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        ScaleScreenShotModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<ScaleScreenShotModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        ScaleScreenShotModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<ScaleScreenShotModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}