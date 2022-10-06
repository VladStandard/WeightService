// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Diagnostics;

namespace DataCoreTests.Sql;


[TestFixture]
public class SqlTableFieldTests
{
    #region Public methods

    //[Test]
    //public void Constructor_Create_Error()
    //{
    //    TestContext.WriteLine(@"--------------------------------------------------------------------------------");
    //    TestContext.WriteLine($@"{nameof(Constructor_Create_Error)} start.");
    //    Stopwatch stopwatch = Stopwatch.StartNew();

    //    Assert.Catch<ArgumentException>(() => { SqlTableIdentityModel field = new(); });
    //    foreach (SqlFieldIdentityEnum name in Enum.GetValues(typeof(SqlFieldIdentityEnum)))
    //    {
    //        Assert.Catch<ArgumentException>(() => { SqlTableIdentityModel field = new(name); });
    //    }

    //    TestContext.WriteLine($@"{nameof(Constructor_Create_Error)} complete. Elapsed time: {stopwatch.Elapsed}");
    //    stopwatch.Stop();
    //}

    [Test]
    public void Constructor_Create_Correct()
    {
        TestContext.WriteLine(@"--------------------------------------------------------------------------------");
        TestContext.WriteLine($@"{nameof(Constructor_Create_Correct)} start.");
        Stopwatch stopwatch = Stopwatch.StartNew();

        SqlTableIdentityModel field = new(SqlFieldIdentityEnum.Empty);
        Assert.AreEqual(SqlFieldIdentityEnum.Empty, field.Name);

        foreach (SqlFieldIdentityEnum name in Enum.GetValues(typeof(SqlFieldIdentityEnum)))
        {
            field = new(name);
            Assert.AreEqual(name, field.Name);
        }

        TestContext.WriteLine($@"{nameof(Constructor_Create_Correct)} complete. Elapsed time: {stopwatch.Elapsed}");
        stopwatch.Stop();
    }

    #endregion
}