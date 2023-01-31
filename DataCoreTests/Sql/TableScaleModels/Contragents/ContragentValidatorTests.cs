// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Contragents;

namespace DataCoreTests.Sql.TableScaleModels.Contragents;

[TestFixture]
internal class ContragentValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        ContragentModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<ContragentModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        ContragentModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<ContragentModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}