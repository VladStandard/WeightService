namespace WsLocalizationCoreTests.Models;

[TestFixture]
public sealed class WsLocalizationManagerTests
{
    [Test]
    public void Check_default_language()
    {
        Assert.DoesNotThrow(() =>
        {
            WsLocalizationManager wsLocalization = new();
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
            WsLocalizationManager wsLocalization = new()
            {
                Lang = WsEnumLanguage.English
            };
            Assert.That(wsLocalization.Lang, Is.EqualTo(WsEnumLanguage.English));
            Assert.That(wsLocalization.Locale.CurrentLanguage, Is.EqualTo("en"));
            Assert.That(wsLocalization.LabelPrint.Lang, Is.EqualTo(WsEnumLanguage.English));
            Assert.That(wsLocalization.LabelPrint.Locale.CurrentLanguage, Is.EqualTo("en"));
        });
    }

    [Test]
    public void Using_multiplayer()
    {
        Assert.DoesNotThrow(() =>
        {
            WsLocalizationManager wsLocalization1 = new();
            WsLocalizationManager wsLocalization2 = new();
            TestContext.WriteLine($"CurrentLanguage 1: {wsLocalization1.Locale.CurrentLanguage}");
            TestContext.WriteLine($"CurrentLanguage 2: {wsLocalization2.Locale.CurrentLanguage}");

            wsLocalization1.Lang = WsEnumLanguage.English;
            TestContext.WriteLine($"CurrentLanguage 1: {wsLocalization1.Locale.CurrentLanguage}");
            TestContext.WriteLine($"CurrentLanguage 2: {wsLocalization2.Locale.CurrentLanguage}");

            Assert.That(wsLocalization1.Lang, Is.EqualTo(WsEnumLanguage.English));
            Assert.That(wsLocalization1.Locale.CurrentLanguage, Is.EqualTo("en"));
            Assert.That(wsLocalization2.Lang, Is.EqualTo(WsEnumLanguage.Russian));
            Assert.That(wsLocalization2.Locale.CurrentLanguage, Is.EqualTo("ru"));
        });
    }

    [Test]
    public void Using_multiplayer_for_label_print()
    {
        Assert.DoesNotThrow(() =>
        {
            WsLocalizationManager wsLocalization1 = new();
            WsLocalizationManager wsLocalization2 = new();
            TestContext.WriteLine($"CurrentLanguage 1: {wsLocalization1.Locale.CurrentLanguage}");
            TestContext.WriteLine($"CurrentLanguage 2: {wsLocalization2.Locale.CurrentLanguage}");

            wsLocalization1.Lang = WsEnumLanguage.English;
            TestContext.WriteLine($"CurrentLanguage 1: {wsLocalization1.LabelPrint.MassaK}");
            TestContext.WriteLine($"CurrentLanguage 2: {wsLocalization2.LabelPrint.MassaK}");

            Assert.That(wsLocalization1.LabelPrint.MassaK, Is.EqualTo("Scales Massa-K"));
            Assert.That(wsLocalization1.LabelPrint.Locale.CurrentLanguage, Is.EqualTo("en"));
            Assert.That(wsLocalization2.LabelPrint.MassaK, Is.EqualTo("Весы Масса-К"));
            Assert.That(wsLocalization2.LabelPrint.Locale.CurrentLanguage, Is.EqualTo("ru"));
        });
    }
}