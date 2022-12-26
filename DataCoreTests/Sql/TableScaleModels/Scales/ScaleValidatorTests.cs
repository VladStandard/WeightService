// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Scales;

namespace DataCoreTests.Sql.TableScaleModels.Scales;

[TestFixture]
internal class ScaleValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        ScaleModel item = DataCore.CreateNewSubstitute<ScaleModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        ScaleModel item = DataCore.CreateNewSubstitute<ScaleModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
