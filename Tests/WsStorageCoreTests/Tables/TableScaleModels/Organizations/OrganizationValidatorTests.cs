// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleModels.Organizations;

[TestFixture]
public sealed class OrganizationValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlOrganizationModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlOrganizationModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlOrganizationModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlOrganizationModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}