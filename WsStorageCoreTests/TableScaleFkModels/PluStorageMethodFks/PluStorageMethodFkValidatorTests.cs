// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusStorageMethodsFks;

namespace WsStorageCoreTests.TableScaleFkModels.PluStorageMethodFks;

[TestFixture]
internal class PluStorageMethodFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluStorageMethodFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluStorageMethodFkModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluStorageMethodFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluStorageMethodFkModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}