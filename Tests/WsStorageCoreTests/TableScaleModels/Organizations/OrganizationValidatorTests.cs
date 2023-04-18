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
        OrganizationModel item = WsTestsUtils.DataCore.CreateNewSubstitute<OrganizationModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        OrganizationModel item = WsTestsUtils.DataCore.CreateNewSubstitute<OrganizationModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}