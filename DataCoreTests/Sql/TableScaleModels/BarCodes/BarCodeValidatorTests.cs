// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.BarCodes;

namespace DataCoreTests.Sql.TableScaleModels.BarCodes;

[TestFixture]
internal class BarCodeValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        // Arrange & Act.
        BarCodeModel item = DataCore.CreateNewSubstitute<BarCodeModel>(false);
        // Assert.
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        // Arrange & Act.
        BarCodeModel item = DataCore.CreateNewSubstitute<BarCodeModel>(true);
        // Assert.
        DataCore.AssertSqlValidate(item, true);
    }
}
