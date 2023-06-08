// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCoreTests.Utils;

[TestFixture]
public sealed class WsLocalizationLabelPrintTests
{
    [Test]
    public void GetListLanguages()
    {
        Assert.DoesNotThrow(() =>
        {
            List<string> list = WsLocalizationUtils.GetListLanguages();
            Assert.IsTrue(list.Any());
            foreach (string language in list) { TestContext.WriteLine(language); }
        });
    }
}