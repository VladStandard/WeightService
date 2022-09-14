// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Utils;
using NUnit.Framework;
using System;
using System.Diagnostics;
using DataCore.Sql.Core;

namespace WeightCoreTests.Models;

public class SqlTableFieldTests
{
    #region Test methods

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

    #endregion

    #region Public methods

    [Test]
    public void Constructor_Create_Error()
    {
        TestContext.WriteLine(@"--------------------------------------------------------------------------------");
        TestContext.WriteLine($@"{nameof(Constructor_Create_Error)} start.");
        Stopwatch stopwatch = Stopwatch.StartNew();

        Assert.Catch<ArgumentException>(() => { SqlTableIdentityModel field = new(); });
        foreach (SqlFieldIdentityEnum name in Enum.GetValues(typeof(SqlFieldIdentityEnum)))
        {
            Assert.Catch<ArgumentException>(() => { SqlTableIdentityModel field = new(name); });
        }

        TestContext.WriteLine($@"{nameof(Constructor_Create_Error)} complete. Elapsed time: {stopwatch.Elapsed}");
        stopwatch.Stop();
    }

    [Test]
    public void Constructor_Create_Correct()
    {
        TestContext.WriteLine(@"--------------------------------------------------------------------------------");
        TestContext.WriteLine($@"{nameof(Constructor_Create_Correct)} start.");
        Stopwatch stopwatch = Stopwatch.StartNew();

        SqlTableIdentityModel field = new(SqlFieldIdentityEnum.Empty);
        Assert.AreEqual("FieldName", field.Name);

        foreach (string value in EnumValuesUtils.GetString())
        {
            field = new(SqlFieldIdentityEnum.Empty);
            Assert.AreEqual("FieldName", field.Name);
				foreach (SqlFieldIdentityEnum name in Enum.GetValues(typeof(SqlFieldIdentityEnum)))
				{
                field = new(name);
                Assert.AreEqual(SqlFieldIdentityEnum.Empty, field.Name);
            }
        }

        TestContext.WriteLine($@"{nameof(Constructor_Create_Correct)} complete. Elapsed time: {stopwatch.Elapsed}");
        stopwatch.Stop();
    }

    #endregion
}