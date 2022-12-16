// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Plus;

namespace DataCoreTests.Sql.TableScaleModels.Plus;

[TestFixture]
internal class PluValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        // Arrange & Act.
        PluModel item = DataCore.CreateNewSubstitute<PluModel>(false);
        // Assert.
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        // Arrange & Act.
        PluModel item = DataCore.CreateNewSubstitute<PluModel>(true);
        // Assert.
        DataCore.AssertSqlValidate(item, true);
    }
}
