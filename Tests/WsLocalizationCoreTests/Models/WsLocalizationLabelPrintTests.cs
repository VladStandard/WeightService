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
            WsLocalizationLabelPrint wsLocalization = new()
            {
                Lang = WsEnumLanguage.English
            };
            Assert.That(wsLocalization.Lang, Is.EqualTo(WsEnumLanguage.English));
            Assert.That(wsLocalization.Locale.CurrentLanguage, Is.EqualTo("en"));
        });
    }
    
    [Test]
    public void Using_multiplayer()
    {
        Assert.DoesNotThrow(() =>
        {
            WsLocalizationLabelPrint wsLocalization1 = new();
            WsLocalizationLabelPrint wsLocalization2 = new();
            TestContext.WriteLine($"CurrentLanguage 1: {wsLocalization1.Locale.CurrentLanguage}");
            TestContext.WriteLine($"CurrentLanguage 2: {wsLocalization2.Locale.CurrentLanguage}");

            wsLocalization1.Lang = WsEnumLanguage.English;
            TestContext.WriteLine($"CurrentLanguage 1: {wsLocalization1.Locale.CurrentLanguage}");
            TestContext.WriteLine($"CurrentLanguage 2: {wsLocalization2.Locale.CurrentLanguage}");
            TestContext.WriteLine($"{nameof(wsLocalization1.AppLoad)}: {wsLocalization1.AppLoad}");
            TestContext.WriteLine($"{nameof(wsLocalization2.AppLoad)}: {wsLocalization2.AppLoad}");

            Assert.That(wsLocalization1.Lang, Is.EqualTo(WsEnumLanguage.English));
            Assert.That(wsLocalization1.Locale.CurrentLanguage, Is.EqualTo("en"));
            Assert.That(wsLocalization2.Lang, Is.EqualTo(WsEnumLanguage.Russian));
            Assert.That(wsLocalization2.Locale.CurrentLanguage, Is.EqualTo("ru"));
            
            Assert.That(wsLocalization1.AppLoad, Is.EqualTo("Loading"));
            Assert.That(wsLocalization2.AppLoad, Is.EqualTo("Загрузка"));
        });
    }
}