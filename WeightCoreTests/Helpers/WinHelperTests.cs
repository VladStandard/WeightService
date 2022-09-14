// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;
using System;
using System.Diagnostics;
using DataCore.Models;
using DataCore.Wmi;
using WeightCore.Helpers;

namespace WeightCoreTests.Helpers;

internal class WinHelperTests
{
    private RegHelper Win { get; set; } = RegHelper.Instance;

    /// <summary>
    /// Setup private fields.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        TestContext.WriteLine(@"--------------------------------------------------------------------------------");
        TestContext.WriteLine($@"{nameof(Setup)} start.");
        //
        TestContext.WriteLine($@"{nameof(Setup)} complete.");
    }

    /// <summary>
    /// Reset private fields to default state.
    /// </summary>
    [TearDown]
    public void Teardown()
    {
        TestContext.WriteLine(@"--------------------------------------------------------------------------------");
        TestContext.WriteLine($@"{nameof(Teardown)} start.");
        //
        TestContext.WriteLine($@"{nameof(Teardown)} complete.");
        TestContext.WriteLine(@"--------------------------------------------------------------------------------");
    }

    [Test]
    public void SearchingSoftware_AreEqual_Empty()
    {
        TestContext.WriteLine(@"--------------------------------------------------------------------------------");
        TestContext.WriteLine($@"{nameof(SearchingSoftware_AreEqual_Empty)} start.");
        Stopwatch stopwatch = Stopwatch.StartNew();

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

        TestContext.WriteLine($@"{nameof(SearchingSoftware_AreEqual_Empty)} complete. Elapsed time: {stopwatch.Elapsed}");
        stopwatch.Stop();
    }

    [Test]
    public void SearchingSoftware_AreEqual_FromRegistry()
    {
        TestContext.WriteLine(@"--------------------------------------------------------------------------------");
        TestContext.WriteLine($@"{nameof(SearchingSoftware_AreEqual_FromRegistry)} start.");
        Stopwatch stopwatch = Stopwatch.StartNew();

        WmiSoftwareModel actual = Win.SearchingSoftware(WinProviderEnum.Registry, "Microsoft .NET Framework", StringTemplateEnum.StartsWith);
        TestContext.WriteLine($"actual: {actual}");
        TestContext.WriteLine($"actual.Name: {actual.Name}");
        Assert.AreEqual("Microsoft Corporation", actual.Vendor);

        TestContext.WriteLine($@"{nameof(SearchingSoftware_AreEqual_FromRegistry)} complete. Elapsed time: {stopwatch.Elapsed}");
        stopwatch.Stop();
    }
}
