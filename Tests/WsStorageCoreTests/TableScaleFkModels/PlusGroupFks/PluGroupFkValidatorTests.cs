// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleFkModels.PlusGroupsFks;

namespace WsStorageCoreTests.TableScaleFkModels.PlusGroupFks;

[TestFixture]
internal class PluGroupFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluGroupFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluGroupFkModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluGroupFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluGroupFkModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}