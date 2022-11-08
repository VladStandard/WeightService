// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Wmi;
using NUnit.Framework;
using WeightCore.Helpers;

namespace WeightCoreTests.Helpers;

[TestFixture]
public class WinHelperTests
{
    private RegHelper Win { get; set; } = RegHelper.Instance;

    [Test]
    public void SearchingSoftware_AreEqual_Empty()
    {
        foreach (WinProviderEnum provider in Enum.GetValues(typeof(WinProviderEnum)))
        {
            foreach (StringTemplateEnum template in Enum.GetValues(typeof(StringTemplateEnum)))
            {
                WmiSoftwareModel actual = Win.SearchingSoftware(WinProviderEnum.Alias, "Unknown Software", template);
                WmiSoftwareModel expected = new();
                TestContext.WriteLine($@"actual = {actual}");
                Assert.AreEqual(expected.ToString(), actual.ToString());
            }
        }
    }

    [Test]
    public void SearchingSoftware_AreEqual_FromRegistry()
    {
        WmiSoftwareModel actual = Win.SearchingSoftware(WinProviderEnum.Registry, "Microsoft .NET Framework", StringTemplateEnum.StartsWith);
        TestContext.WriteLine($"actual: {actual}");
        TestContext.WriteLine($"actual.Name: {actual.Name}");
        Assert.AreEqual("Microsoft Corporation", actual.Vendor);
    }
}
