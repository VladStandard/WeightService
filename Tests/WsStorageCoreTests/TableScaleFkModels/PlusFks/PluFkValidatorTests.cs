// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleFkModels.PlusFks;

namespace WsStorageCoreTests.TableScaleFkModels.PlusFks;

[TestFixture]
internal class PluFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluFkModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluFkModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}