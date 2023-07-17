// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleModels.Clips;

public sealed class ClipValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlClipModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlClipModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlClipModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlClipModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}