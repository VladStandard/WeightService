namespace Ws.LocalizationCoreTests.Common;

[TestFixture]
public sealed class WsLocalizationBaseTests
{
    [Test]
    public void Check_default_language()
    {
        Assert.DoesNotThrow(() =>
        {
            WsLocalizationBase wsLocalization = new();
            Assert.That(wsLocalization.Lang, Is.EqualTo(WsEnumLanguage.Russian));
            Assert.That(wsLocalization.Locale.CurrentLanguage, Is.EqualTo("ru"));
        });
    }
    
    [Test]
    public void Change_language()
    {
        Assert.DoesNotThrow(() =>
        {
            WsLocalizationBase wsLocalization = new();
            wsLocalization.Lang = WsEnumLanguage.English;
            Assert.That(wsLocalization.Lang, Is.EqualTo(WsEnumLanguage.English));
            Assert.That(wsLocalization.Locale.CurrentLanguage, Is.EqualTo("en"));
        });
    }

    [Test]
    public void Using_multiplayer()
    {
        Assert.DoesNotThrow(() =>
        {
            WsLocalizationBase wsLocalization1 = new();
            WsLocalizationBase wsLocalization2 = new();
            TestContext.WriteLine($"wsLocalization1.Locale.CurrentLanguage: {wsLocalization1.Locale.CurrentLanguage}");
            TestContext.WriteLine($"wsLocalization2.Locale.CurrentLanguage: {wsLocalization2.Locale.CurrentLanguage}");

            wsLocalization1.Lang = WsEnumLanguage.English;
            TestContext.WriteLine($"wsLocalization1.Locale.CurrentLanguage: {wsLocalization1.Locale.CurrentLanguage}");
            TestContext.WriteLine($"wsLocalization2.Locale.CurrentLanguage: {wsLocalization2.Locale.CurrentLanguage}");

            Assert.That(wsLocalization1.Lang, Is.EqualTo(WsEnumLanguage.English));
            Assert.That(wsLocalization1.Locale.CurrentLanguage, Is.EqualTo("en"));
            Assert.That(wsLocalization2.Lang, Is.EqualTo(WsEnumLanguage.Russian));
            Assert.That(wsLocalization2.Locale.CurrentLanguage, Is.EqualTo("ru"));
        });
    }
}