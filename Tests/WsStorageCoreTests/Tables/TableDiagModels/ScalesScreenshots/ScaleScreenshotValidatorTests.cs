// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableDiagModels.ScalesScreenshots;

[TestFixture]
public sealed class ScaleScreenShotValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlScaleScreenShotModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlScaleScreenShotModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlScaleScreenShotModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlScaleScreenShotModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}