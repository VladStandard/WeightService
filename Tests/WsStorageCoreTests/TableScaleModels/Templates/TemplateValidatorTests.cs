// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Templates;

namespace WsStorageCoreTests.TableScaleModels.Templates;

[TestFixture]
public sealed class TemplateValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        TemplateModel item = WsTestsUtils.DataCore.CreateNewSubstitute<TemplateModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        TemplateModel item = WsTestsUtils.DataCore.CreateNewSubstitute<TemplateModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}