// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Versions;

[TestFixture]
public sealed class VersionValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlVersionModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlVersionModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlVersionModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlVersionModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}