// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCoreTests.Utils;

[TestFixture]
public sealed class DataFormatUtilsTests
{
    #region Public methods

    [Test]
    public void AppHelper_GetCurrentVersion_AreEqual()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlTemplateResourceModel> templateResources = MDSoft.BarcodePrintUtils.Utils.DataFormatUtils.LoadTemplatesResources(true);
            Assert.IsTrue(templateResources.Any());
            foreach (WsSqlTemplateResourceModel templateResource in templateResources)
            {
                TestContext.WriteLine(templateResource);
            }
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.DevelopVS });
    }

    #endregion
}