// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Access;

[TestFixture]
internal class AccessValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        AccessModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<AccessModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        AccessModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<AccessModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}