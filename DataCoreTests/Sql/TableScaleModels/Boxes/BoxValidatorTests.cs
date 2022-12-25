// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Boxes;

namespace DataCoreTests.Sql.TableScaleModels.Boxes;

[TestFixture]
internal class BoxValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        BoxModel item = DataCore.CreateNewSubstitute<BoxModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        BoxModel item = DataCore.CreateNewSubstitute<BoxModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}