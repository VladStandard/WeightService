// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using System.IO;
using DataCore.Helpers;
using DataCore.Models;
using WeightCore.Helpers;

namespace WeightCoreTests.Helpers;

[TestFixture]
public class XmlHelperTests
{
    private XmlHelper Xml { get; set; } = XmlHelper.Instance;
    private SettingsHelper Settings { get; set; } = SettingsHelper.Instance;
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

    [Test]
    public void ReadScalesUI_ConnectionString_AreEqual()
    {
        if (File.Exists(Settings.GetScalesConfigFileName()))
        {
            ResultXmlRead actual = Xml.Read(Settings.GetScalesConfigFileName(), new Collection<XmlTag>()
            {
                new XmlTag("connectionStrings"),
                new XmlTag("add", "name", "ScalesUI.Properties.Settings.ConnectionString"),
            }, "connectionString");
            TestContext.WriteLine($@"ConnectionString=""{actual.Value}""");
            TestContext.WriteLine($@"actual.NoError=""{actual.NoError}""");

            TestContext.WriteLine("----------------------------------------------");
            TestContext.WriteLine(string.Join(Environment.NewLine, actual.Str));
            TestContext.WriteLine("----------------------------------------------");

            Assert.AreEqual(
                actual.NoError
                    ? @"Server=CREATIO\INS1;Database=Scales;Uid=scale01;Pwd=scale01;"
                    : string.Empty, actual.Value);
        }
        else
            Assert.AreEqual(string.Empty, string.Empty);
    }

    [Test]
    public void ReadScalesUI_ScalesID_AreEqual()
    {
        if (File.Exists(Settings.GetScalesConfigFileName()))
        {
            ResultXmlRead actual = Xml.Read(Settings.GetScalesConfigFileName(), new Collection<XmlTag>()
            {
                new XmlTag("applicationSettings"),
                new XmlTag("ScalesUI.Properties.Settings"),
                new XmlTag("setting", "name", "ScalesID"),
                new XmlTag("value"),
            });
            TestContext.WriteLine($@"ScalesID=""{actual.Value}""");
            TestContext.WriteLine($@"actual.NoError=""{actual.NoError}""");

            TestContext.WriteLine("----------------------------------------------");
            TestContext.WriteLine(string.Join(Environment.NewLine, actual.Str));
            TestContext.WriteLine("----------------------------------------------");

            Assert.AreEqual(actual.NoError ? @"F1A90176-894E-11EA-9E4C-4CCC6A93A440" : string.Empty, actual.Value);
        }
        else
            Assert.AreEqual(string.Empty, string.Empty);
    }
}
