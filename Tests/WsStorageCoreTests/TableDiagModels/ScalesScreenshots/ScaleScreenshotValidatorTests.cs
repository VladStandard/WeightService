// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableDiagModels.ScalesScreenshots;

[TestFixture]
public sealed class ScaleScreenShotValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        ScaleScreenShotModel item = WsTestsUtils.DataTests.CreateNewSubstitute<ScaleScreenShotModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        ScaleScreenShotModel item = WsTestsUtils.DataTests.CreateNewSubstitute<ScaleScreenShotModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}