// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleFkModels.PlusClipsFks;

[TestFixture]
internal class PluClipFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluClipFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluClipFkModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluClipFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluClipFkModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}