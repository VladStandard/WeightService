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
        Assert.Throws<FileNotFoundException>(() => Xml.Checks("test", new Collection<XmlTag>() { new XmlTag(null) }));
        Assert.Throws<FileNotFoundException>(() => Xml.Checks("test", new Collection<XmlTag>() { new XmlTag(null) }, "test"));
        Assert.Throws<FileNotFoundException>(() => Xml.Checks("test", new Collection<XmlTag>() { new XmlTag("test", "test") }, "test"));

        Assert.Throws<ArgumentNullException>(() => Xml.Checks(TestFile, new Collection<XmlTag>() { new XmlTag(null) }));
        Assert.Throws<ArgumentNullException>(() => Xml.Checks(TestFile, new Collection<XmlTag>() { new XmlTag(string.Empty, string.Empty) }, string.Empty));
        Assert.Throws<ArgumentNullException>(() => Xml.Checks(TestFile, new Collection<XmlTag>() { new XmlTag("", "") }, ""));
        Assert.Throws<ArgumentNullException>(() => Xml.Checks(TestFile, new Collection<XmlTag>() { new XmlTag(null) }));
        Assert.Throws<ArgumentNullException>(() => Xml.Checks(TestFile, new Collection<XmlTag>() { new XmlTag(null) }, "test"));
        //Assert.Throws<ArgumentNullException>(() => _xml.Checks(TestFile, new Collection<XmlAttribute>() { new XmlAttribute("test", "test", null) }, null));

        Assert.DoesNotThrow(() => Xml.Checks(TestFile, new Collection<XmlTag>() { new XmlTag("test", "test") }));
        Assert.DoesNotThrow(() => Xml.Checks(TestFile, new Collection<XmlTag>() { new XmlTag("test", "test") }, "test"));
    }

    [Test]
    public void Read_AreEqual()
    {
        ResultXmlRead actual = Xml.Read(TestFile, new Collection<XmlTag>()
        {
            new XmlTag("Wizard", "name", "SavedWIZ"),
            new XmlTag("Page", "resID", "IDR_DUI_SAVED"),
            new XmlTag("Item", "name", "group2Link"),
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