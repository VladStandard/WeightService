// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests;


[TestFixture]
public class SqlAuthenticationTests
{
    #region Public methods

    [Test]
    public void Constructor_Create_Correct()
    {
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
                                            WsSqlAuthenticationModel sqlAu = new(server, database,
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
    }

    [Test]
    public void Exists_Execute_Assert()
    {
        WsSqlAuthenticationModel sqlAuthentication = new();
        Assert.IsFalse(sqlAuthentication.Exists());
    }

    #endregion
}