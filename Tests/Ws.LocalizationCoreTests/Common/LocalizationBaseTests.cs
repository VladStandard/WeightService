namespace Ws.LocalizationCoreTests.Common;

[TestFixture]
public sealed class LocalizationBaseTests
{
    [Test]
    public void Check_default_language()
    {
        Assert.DoesNotThrow(() =>
        {
            LocalizationBase localization = new();
            Assert.That(localization.Lang, Is.EqualTo(EnumLanguage.Russian));
            Assert.That(localization.Locale.CurrentLanguage, Is.EqualTo("ru"));
        });
    }
    
    [Test]
    public void Change_language()
    {
        Assert.DoesNotThrow(() =>
        {
            LocalizationBase localization = new();
            localization.Lang = EnumLanguage.English;
            Assert.That(localization.Lang, Is.EqualTo(EnumLanguage.English));
            Assert.That(localization.Locale.CurrentLanguage, Is.EqualTo("en"));
        });
    }

    [Test]
    public void Using_multiplayer()
    {
        Assert.DoesNotThrow(() =>
        {
            LocalizationBase localization1 = new();
            LocalizationBase localization2 = new();
            TestContext.WriteLine($"localization1.Locale.CurrentLanguage: {localization1.Locale.CurrentLanguage}");
            TestContext.WriteLine($"localization2.Locale.CurrentLanguage: {localization2.Locale.CurrentLanguage}");

            localization1.Lang = EnumLanguage.English;
            TestContext.WriteLine($"localization1.Locale.CurrentLanguage: {localization1.Locale.CurrentLanguage}");
            TestContext.WriteLine($"localization2.Locale.CurrentLanguage: {localization2.Locale.CurrentLanguage}");

            Assert.That(localization1.Lang, Is.EqualTo(EnumLanguage.English));
            Assert.That(localization1.Locale.CurrentLanguage, Is.EqualTo("en"));
            Assert.That(localization2.Lang, Is.EqualTo(EnumLanguage.Russian));
            Assert.That(localization2.Locale.CurrentLanguage, Is.EqualTo("ru"));
        });
    }
}