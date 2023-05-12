// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCoreTests.Helpers;

[TestFixture]
public sealed class XmlHelperTests
{
    private XmlHelper Xml => XmlHelper.Instance;
    private const string TestFile = @"c:\Program Files\Common Files\microsoft shared\ink\Content.xml";

    [Test]
    public void Checks_Throws_Exception()
    {
        Assert.Throws<FileNotFoundException>(() => Xml.Checks("test", new Collection<WsXmlTag>() { new WsXmlTag(null) }));
        Assert.Throws<FileNotFoundException>(() => Xml.Checks("test", new Collection<WsXmlTag>() { new WsXmlTag(null) }, "test"));
        Assert.Throws<FileNotFoundException>(() => Xml.Checks("test", new Collection<WsXmlTag>() { new WsXmlTag("test", "test") }, "test"));

        Assert.Throws<ArgumentNullException>(() => Xml.Checks(TestFile, new Collection<WsXmlTag>() { new WsXmlTag(null) }));
        Assert.Throws<ArgumentNullException>(() => Xml.Checks(TestFile, new Collection<WsXmlTag>() { new WsXmlTag(string.Empty, string.Empty) }, string.Empty));
        Assert.Throws<ArgumentNullException>(() => Xml.Checks(TestFile, new Collection<WsXmlTag>() { new WsXmlTag("", "") }, ""));
        Assert.Throws<ArgumentNullException>(() => Xml.Checks(TestFile, new Collection<WsXmlTag>() { new WsXmlTag(null) }));
        Assert.Throws<ArgumentNullException>(() => Xml.Checks(TestFile, new Collection<WsXmlTag>() { new WsXmlTag(null) }, "test"));
        //Assert.Throws<ArgumentNullException>(() => _xml.Checks(TestFile, new Collection<XmlAttribute>() { new XmlAttribute("test", "test", null) }, null));

        Assert.DoesNotThrow(() => Xml.Checks(TestFile, new Collection<WsXmlTag>() { new WsXmlTag("test", "test") }));
        Assert.DoesNotThrow(() => Xml.Checks(TestFile, new Collection<WsXmlTag>() { new WsXmlTag("test", "test") }, "test"));
    }

    [Test]
    public void Read_AreEqual()
    {
        ResultXmlRead actual = Xml.Read(TestFile, new Collection<WsXmlTag>()
        {
            new WsXmlTag("Wizard", "name", "SavedWIZ"),
            new WsXmlTag("Page", "resID", "IDR_DUI_SAVED"),
            new WsXmlTag("Item", "name", "group2Link"),
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