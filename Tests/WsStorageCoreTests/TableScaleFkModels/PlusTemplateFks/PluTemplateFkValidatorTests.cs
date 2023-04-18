// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusTemplatesFks;

namespace WsStorageCoreTests.TableScaleFkModels.PlusTemplateFks;

[TestFixture]
public sealed class PluTemplateFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluTemplateFkModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PluTemplateFkModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluTemplateFkModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PluTemplateFkModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}