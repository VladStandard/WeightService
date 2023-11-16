namespace Ws.LocalizationCoreTests.Models;

[TestFixture]
public sealed class LocalizationLabelPrintTests
{
    [Test]
    public void Check_default_language()
    {
        Assert.DoesNotThrow(() =>
        {
            LocalizationLabelPrint localization = new();
            Assert.That(localization.Lang, Is.EqualTo(EnumLanguage.Russian));
            Assert.That(localization.Locale.CurrentLanguage, Is.EqualTo("ru"));
        });
    }
    
    [Test]
    public void Change_language()
    {
        Assert.DoesNotThrow(() =>
        {
            LocalizationLabelPrint localization = new()
            {
                Lang = EnumLanguage.English
            };
            Assert.That(localization.Lang, Is.EqualTo(EnumLanguage.English));
            Assert.That(localization.Locale.CurrentLanguage, Is.EqualTo("en"));
        });
    }
    
    [Test]
    public void Using_multiplayer()
    {
        Assert.DoesNotThrow(() =>
        {
            LocalizationLabelPrint localization1 = new();
            LocalizationLabelPrint localization2 = new();
            TestContext.WriteLine($"CurrentLanguage 1: {localization1.Locale.CurrentLanguage}");
            TestContext.WriteLine($"CurrentLanguage 2: {localization2.Locale.CurrentLanguage}");

            localization1.Lang = EnumLanguage.English;
            TestContext.WriteLine($"CurrentLanguage 1: {localization1.Locale.CurrentLanguage}");
            TestContext.WriteLine($"CurrentLanguage 2: {localization2.Locale.CurrentLanguage}");
            TestContext.WriteLine($"{nameof(localization1.AppLoad)}: {localization1.AppLoad}");
            TestContext.WriteLine($"{nameof(localization2.AppLoad)}: {localization2.AppLoad}");

            Assert.That(localization1.Lang, Is.EqualTo(EnumLanguage.English));
            Assert.That(localization1.Locale.CurrentLanguage, Is.EqualTo("en"));
            Assert.That(localization2.Lang, Is.EqualTo(EnumLanguage.Russian));
            Assert.That(localization2.Locale.CurrentLanguage, Is.EqualTo("ru"));
            
            Assert.That(localization1.AppLoad, Is.EqualTo("Loading"));
            Assert.That(localization2.AppLoad, Is.EqualTo("Загрузка"));
        });
    }
}