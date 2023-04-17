// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.Models;

namespace WsStorageCoreTests;


[TestFixture]
public class SqlAuthenticationTests
{
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
                                        Assert.DoesNotThrow(() =>
                                        {
                                            SqlAuthenticationModel sqlAu = new(server, database,
                                            persistSecurityInfo, integratedSecurity, userId, password, encrypt, port);
                                        });
                                        TestContext.WriteLine($@"new SqlAuthentication({persistSecurityInfo}, {integratedSecurity}, {userId}, {password}, {encrypt})");
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

        SqlAuthenticationModel sqlAuthentication = new();
        Assert.IsFalse(sqlAuthentication.Exists());

        TestContext.WriteLine($@"{nameof(Exists_Execute_Assert)} complete. Elapsed time: {stopwatch.Elapsed}");
        stopwatch.Stop();
    }

    #endregion
}