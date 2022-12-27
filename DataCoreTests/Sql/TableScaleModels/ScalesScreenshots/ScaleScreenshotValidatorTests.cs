// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.ScalesScreenshots;

namespace DataCoreTests.Sql.TableScaleModels.ScalesScreenshots;

[TestFixture]
internal class ScaleScreenShotValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        ScaleScreenShotModel item = DataCore.CreateNewSubstitute<ScaleScreenShotModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        ScaleScreenShotModel item = DataCore.CreateNewSubstitute<ScaleScreenShotModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
