// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCoreTests.Models;

[TestFixture]
public sealed class WsLocalizationLabelPrintTests
{
    [Test]
    public void Check_default_language()
    {
        Assert.DoesNotThrow(() =>
        {
            WsLocalizationLabelPrint wsLocalization = new();
            Assert.That(wsLocalization.Lang, Is.EqualTo(WsEnumLanguage.Russian));
            Assert.That(wsLocalization.Locale.CurrentLanguage, Is.EqualTo("ru"));
        });
    }
    
    [Test]
    public void Change_language()
    {
        Assert.DoesNotThrow(() =>
        {
            WsLocalizationLabelPrint wsLocalization = new();
            wsLocalization.SetLanguage(WsEnumLanguage.English);
            Assert.That(wsLocalization.Lang, Is.EqualTo(WsEnumLanguage.English));
            Assert.That(wsLocalization.Locale.CurrentLanguage, Is.EqualTo("en"));
        });
    }
    
    [Test]
    public void Create_2_instances()
    {
        Assert.DoesNotThrow(() =>
        {
            WsLocalizationLabelPrint wsLocalization1 = new();
            wsLocalization1.SetLanguage(WsEnumLanguage.English);
            WsLocalizationLabelPrint wsLocalization2 = new();

            Assert.That(wsLocalization1.Lang, Is.EqualTo(WsEnumLanguage.English));
            Assert.That(wsLocalization1.Locale.CurrentLanguage, Is.EqualTo("en"));
            Assert.That(wsLocalization2.Lang, Is.EqualTo(WsEnumLanguage.Russian));
            Assert.That(wsLocalization2.Locale.CurrentLanguage, Is.EqualTo("ru"));
        });
    }
}