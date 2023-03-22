// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;
using DataCore.Sql.TableScaleModels.TemplatesResources;

namespace DataCoreTests.Utils;

[TestFixture]
public class DataFormatUtilsTests
{
    #region Public methods

    [Test]
    public void AppHelper_GetCurrentVersion_AreEqual()
    {
        DataCoreTestsUtils.DataCore.AssertAction(() =>
        {
            List<TemplateResourceModel> templateResources = DataFormatUtils.LoadTemplatesResources(true);
            Assert.IsTrue(templateResources.Any());
            foreach (TemplateResourceModel templateResource in templateResources)
            {
                TestContext.WriteLine(templateResource);
            }
        }, false, new() { Configuration.DevelopVS, Configuration.DevelopVS });
    }

    #endregion
}