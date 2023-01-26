// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusClipsFks;

namespace DataCoreTests.Sql.TableScaleFkModels.PlusClipsFks;

[TestFixture]
internal class PluClipFkValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        PluClipFkModel item = DataCore.CreateNewSubstitute<PluClipFkModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluClipFkModel item = DataCore.CreateNewSubstitute<PluClipFkModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}