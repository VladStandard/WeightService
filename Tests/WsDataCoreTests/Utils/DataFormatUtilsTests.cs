// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableScaleModels.TemplatesResources;

namespace WsDataCoreTests.Utils;

[TestFixture]
public sealed class DataFormatUtilsTests
{
    [Test]
    public void AppHelper_GetCurrentVersion_AreEqual()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlTemplateResourceModel> templateResources = MDSoft.BarcodePrintUtils.Utils.DataFormatUtils.LoadTemplatesResources(true);
            Assert.That(templateResources.Any(), Is.True);
            foreach (WsSqlTemplateResourceModel templateResource in templateResources)
            {
                TestContext.WriteLine(templateResource);
            }
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.DevelopVS });
    }
}