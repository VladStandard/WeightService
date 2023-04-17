// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleModels.Organizations;

namespace WsStorageCoreTests.TableScaleModels.Organizations;

[TestFixture]
internal class OrganizationValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        OrganizationModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<OrganizationModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        OrganizationModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<OrganizationModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}