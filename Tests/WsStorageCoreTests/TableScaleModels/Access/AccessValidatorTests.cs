// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Access;

[TestFixture]
public sealed class AccessValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        AccessModel item = WsTestsUtils.DataCore.CreateNewSubstitute<AccessModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        AccessModel item = WsTestsUtils.DataCore.CreateNewSubstitute<AccessModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}