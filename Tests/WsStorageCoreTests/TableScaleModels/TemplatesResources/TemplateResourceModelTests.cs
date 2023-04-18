// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.TemplatesResources;

namespace WsStorageCoreTests.TableScaleModels.TemplatesResources;

[TestFixture]
public sealed class TemplateResourceModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        WsTestsUtils.DataCore.AssertSqlPropertyCheckDt<TemplateResourceModel>(nameof(WsSqlTableBase.CreateDt));
        WsTestsUtils.DataCore.AssertSqlPropertyCheckDt<TemplateResourceModel>(nameof(WsSqlTableBase.ChangeDt));
        WsTestsUtils.DataCore.AssertSqlPropertyCheckBool<TemplateResourceModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertToString<TemplateResourceModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertEqualsNew<TemplateResourceModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertSerialize<TemplateResourceModel>();
    }
}