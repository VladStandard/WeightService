// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCoreTests.Models;

[TestFixture]
public sealed class WsLocalizationModelTests
{
    [Test]
    public void Check_default_language()
    {
        Assert.DoesNotThrow(() =>
        {
            WsLocalizationModel wsLocalization = new();
            Assert.That(wsLocalization.Lang, Is.EqualTo(WsEnumLanguage.Russian));
            Assert.That(wsLocalization.Locale.CurrentLanguage, Is.EqualTo("ru"));
            Assert.That(wsLocalization.LabelPrint.Lang, Is.EqualTo(WsEnumLanguage.Russian));
            Assert.That(wsLocalization.LabelPrint.Locale.CurrentLanguage, Is.EqualTo("ru"));
        });
    }
    
    [Test]
    public void Change_language()
    {
        Assert.DoesNotThrow(() =>
        {
            WsLocalizationModel wsLocalization = new();
            wsLocalization.SetLanguage(WsEnumLanguage.English);
            Assert.That(wsLocalization.Lang, Is.EqualTo(WsEnumLanguage.English));
            Assert.That(wsLocalization.Locale.CurrentLanguage, Is.EqualTo("en"));
            Assert.That(wsLocalization.LabelPrint.Lang, Is.EqualTo(WsEnumLanguage.English));
            Assert.That(wsLocalization.LabelPrint.Locale.CurrentLanguage, Is.EqualTo("en"));
        });
    }
}