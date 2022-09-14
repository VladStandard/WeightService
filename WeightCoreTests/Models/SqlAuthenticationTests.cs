// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using DataCore.Utils;
using NUnit.Framework;
using System.Diagnostics;

namespace WeightCoreTests.Models;

public class SqlAuthenticationTests
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
    public void Constructor_Create_Correct()
    {
        TestContext.WriteLine(@"--------------------------------------------------------------------------------");
        TestContext.WriteLine($@"{nameof(Constructor_Create_Correct)} start.");
        Stopwatch stopwatch = Stopwatch.StartNew();

        foreach (ushort port in EnumValuesUtils.GetUshort())
        {
            foreach (bool persistSecurityInfo in EnumValuesUtils.GetBool())
            {
                foreach (bool integratedSecurity in EnumValuesUtils.GetBool())
                {
                    foreach (bool encrypt in EnumValuesUtils.GetBool())
                    {
                        foreach (string userId in EnumValuesUtils.GetString())
                        {
                            foreach (string password in EnumValuesUtils.GetString())
                            {
                                foreach (string database in EnumValuesUtils.GetString())
                                {
                                    foreach (string server in EnumValuesUtils.GetString())
                                    {
                                        Assert.DoesNotThrow(() => { SqlAuthenticationModel sqlAu = new(server, database, 
                                            persistSecurityInfo, integratedSecurity, userId, password, encrypt, port); });
                                        TestContext.WriteLine($@"new SqlAuthentication({persistSecurityInfo}, {integratedSecurity}, {userId.AsString()}, {password.AsString()}, {encrypt})");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        TestContext.WriteLine($@"{nameof(Constructor_Create_Correct)} complete. Elapsed time: {stopwatch.Elapsed}");
        stopwatch.Stop();
    }

    [Test]
    public void Exists_Execute_Assert()
    {
        TestContext.WriteLine(@"--------------------------------------------------------------------------------");
        TestContext.WriteLine($@"{nameof(Exists_Execute_Assert)} start.");
        Stopwatch stopwatch = Stopwatch.StartNew();

        SqlAuthenticationModel sqlAu = new();
        Assert.IsFalse(sqlAu.Exists());

        sqlAu = new SqlAuthenticationModel();
        Assert.IsTrue(sqlAu.Exists());

        TestContext.WriteLine($@"{nameof(Exists_Execute_Assert)} complete. Elapsed time: {stopwatch.Elapsed}");
        stopwatch.Stop();
    }

    #endregion
}