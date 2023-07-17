// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleModels.Organizations;

[TestFixture]
public sealed class OrganizationModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<WsSqlOrganizationModel>(nameof(WsSqlTableBase.CreateDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<WsSqlOrganizationModel>(nameof(WsSqlTableBase.ChangeDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckBool<WsSqlOrganizationModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertToString<WsSqlOrganizationModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertEqualsNew<WsSqlOrganizationModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertSerialize<WsSqlOrganizationModel>();
    }
}