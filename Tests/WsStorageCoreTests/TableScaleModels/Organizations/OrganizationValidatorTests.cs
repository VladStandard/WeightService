// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Organizations;

namespace WsStorageCoreTests.TableScaleModels.Organizations;

[TestFixture]
public sealed class OrganizationValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        OrganizationModel item = WsTestsUtils.DataTests.CreateNewSubstitute<OrganizationModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        OrganizationModel item = WsTestsUtils.DataTests.CreateNewSubstitute<OrganizationModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}