// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Clips;

namespace DataCoreTests.Sql.TableScaleModels.Clips;

internal class ClipValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        ClipModel item = DataCore.CreateNewSubstitute<ClipModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        ClipModel item = DataCore.CreateNewSubstitute<ClipModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}