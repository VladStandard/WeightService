// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.PlusGroups;

[TestFixture]
internal class PluGroupValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluGroupModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluGroupModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluGroupModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluGroupModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}