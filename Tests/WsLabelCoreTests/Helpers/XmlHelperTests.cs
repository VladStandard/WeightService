namespace WsLabelCoreTests.Helpers;

[TestFixture]
public sealed class XmlHelperTests
{
    private WsXmlHelper Xml => WsXmlHelper.Instance;
    private const string TestFile = @"c:\Program Files\Common Files\microsoft shared\ink\Content.xml";

    [Test]
    public void Checks_Throws_Exception()
    {
        Assert.Throws<FileNotFoundException>(() => Xml.Checks("test", new() { new(null) }));
        Assert.Throws<FileNotFoundException>(() => Xml.Checks("test", new() { new(null) }, "test"));
        Assert.Throws<FileNotFoundException>(() => Xml.Checks("test", new() { new("test", "test") }, "test"));

        Assert.Throws<ArgumentNullException>(() => Xml.Checks(TestFile, new() { new(null) }));
        Assert.Throws<ArgumentNullException>(() => Xml.Checks(TestFile, new() { new(string.Empty, string.Empty) }, string.Empty));
        Assert.Throws<ArgumentNullException>(() => Xml.Checks(TestFile, new() { new("", "") }, ""));
        Assert.Throws<ArgumentNullException>(() => Xml.Checks(TestFile, new() { new(null) }));
        Assert.Throws<ArgumentNullException>(() => Xml.Checks(TestFile, new() { new(null) }, "test"));
        //Assert.Throws<ArgumentNullException>(() => _xml.Checks(TestFile, new Collection<XmlAttribute>() { new XmlAttribute("test", "test", null) }, null));

        Assert.DoesNotThrow(() => Xml.Checks(TestFile, new() { new("test", "test") }));
        Assert.DoesNotThrow(() => Xml.Checks(TestFile, new() { new("test", "test") }, "test"));
    }

    [Test]
    public void Read_AreEqual()
    {
        ResultXmlReadModel actual = Xml.Read(TestFile, new()
        {
            new("Wizard", "name", "SavedWIZ"),
            new("Page", "resID", "IDR_DUI_SAVED"),
            new("Item", "name", "group2Link"),
        }, "enabled");
        TestContext.WriteLine(@"Wizard name=""SavedWIZ""");
        TestContext.WriteLine(@"  Page resID=""IDR_DUI_SAVED""");
        TestContext.WriteLine(@"    Item name=""group2Link""");
        TestContext.WriteLine($@"      enabled=""{actual.Value}""");

        TestContext.WriteLine("----------------------------------------------");
        TestContext.WriteLine(string.Join(Environment.NewLine, actual.Str));
        TestContext.WriteLine("----------------------------------------------");

        Assert.AreEqual(actual.NoError ? "NOT IsPersonalizationRestricted()" : string.Empty, actual.Value);
    }
}