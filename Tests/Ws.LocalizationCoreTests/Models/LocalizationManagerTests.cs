namespace Ws.LocalizationCoreTests.Models;

[TestFixture]
public sealed class LocalizationManagerTests
{
    [Test]
    public void Check_default_language()
    {
        Assert.DoesNotThrow(() =>
        {
            LocalizationManager localization = new();
            Assert.That(localization.Lang, Is.EqualTo(EnumLanguage.Russian));
            Assert.That(localization.Locale.CurrentLanguage, Is.EqualTo("ru"));
            Assert.That(localization.LabelPrint.Lang, Is.EqualTo(EnumLanguage.Russian));
            Assert.That(localization.LabelPrint.Locale.CurrentLanguage, Is.EqualTo("ru"));
        });
    }
    
    [Test]
    public void Change_language()
    {
        Assert.DoesNotThrow(() =>
        {
            LocalizationManager localization = new()
            {
                Lang = EnumLanguage.English
            };
            Assert.That(localization.Lang, Is.EqualTo(EnumLanguage.English));
            Assert.That(localization.Locale.CurrentLanguage, Is.EqualTo("en"));
            Assert.That(localization.LabelPrint.Lang, Is.EqualTo(EnumLanguage.English));
            Assert.That(localization.LabelPrint.Locale.CurrentLanguage, Is.EqualTo("en"));
        });
    }

    [Test]
    public void Using_multiplayer()
    {
        Assert.DoesNotThrow(() =>
        {
            LocalizationManager localization1 = new();
            LocalizationManager localization2 = new();
            TestContext.WriteLine($"CurrentLanguage 1: {localization1.Locale.CurrentLanguage}");
            TestContext.WriteLine($"CurrentLanguage 2: {localization2.Locale.CurrentLanguage}");

            localization1.Lang = EnumLanguage.English;
            TestContext.WriteLine($"CurrentLanguage 1: {localization1.Locale.CurrentLanguage}");
            TestContext.WriteLine($"CurrentLanguage 2: {localization2.Locale.CurrentLanguage}");

            Assert.That(localization1.Lang, Is.EqualTo(EnumLanguage.English));
            Assert.That(localization1.Locale.CurrentLanguage, Is.EqualTo("en"));
            Assert.That(localization2.Lang, Is.EqualTo(EnumLanguage.Russian));
            Assert.That(localization2.Locale.CurrentLanguage, Is.EqualTo("ru"));
        });
    }

    [Test]
    public void Using_multiplayer_for_label_print()
    {
        Assert.DoesNotThrow(() =>
        {
            LocalizationManager localization1 = new();
            LocalizationManager localization2 = new();
            TestContext.WriteLine($"CurrentLanguage 1: {localization1.Locale.CurrentLanguage}");
            TestContext.WriteLine($"CurrentLanguage 2: {localization2.Locale.CurrentLanguage}");

            localization1.Lang = EnumLanguage.English;
            TestContext.WriteLine($"CurrentLanguage 1: {localization1.LabelPrint.MassaK}");
            TestContext.WriteLine($"CurrentLanguage 2: {localization2.LabelPrint.MassaK}");

            Assert.That(localization1.LabelPrint.MassaK, Is.EqualTo("Scales Massa-K"));
            Assert.That(localization1.LabelPrint.Locale.CurrentLanguage, Is.EqualTo("en"));
            Assert.That(localization2.LabelPrint.MassaK, Is.EqualTo("Весы Масса-К"));
            Assert.That(localization2.LabelPrint.Locale.CurrentLanguage, Is.EqualTo("ru"));
        });
    }
}