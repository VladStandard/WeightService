namespace Ws.LocalizationCoreTests.Utils;

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