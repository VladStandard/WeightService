// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.PluStorageMethods;

[TestFixture]
internal class PluStorageMethodValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluStorageMethodModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluStorageMethodModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluStorageMethodModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluStorageMethodModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}