// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesCoreTests.Models
{
    public class SqlAuthenticationTests
    {
        #region Private fields and properties

        //

        #endregion

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
            var sw = Stopwatch.StartNew();

            foreach (var port in EnumValuesUtils.GetUshort())
            {
                foreach (var usePort in EnumValuesUtils.GetBool())
                {
                    foreach (var persistSecurityInfo in EnumValuesUtils.GetBool())
                    {
                        foreach (var integratedSecurity in EnumValuesUtils.GetBool())
                        {
                            foreach (var encrypt in EnumValuesUtils.GetBool())
                            {
                                foreach (var userId in EnumValuesUtils.GetString())
                                {
                                    foreach (var password in EnumValuesUtils.GetString())
                                    {
                                        foreach (var database in EnumValuesUtils.GetString())
                                        {
                                            foreach (var server in EnumValuesUtils.GetString())
                                            {
                                                Assert.DoesNotThrow(() => { var sqlAu = new SqlAuthentication(server, database, persistSecurityInfo, integratedSecurity, userId, password, encrypt, usePort, port); });
                                                TestContext.WriteLine($@"new SqlAuthentication({persistSecurityInfo}, {integratedSecurity}, {userId.AsString()}, {password.AsString()}, {encrypt})");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            sw.Stop();
            NUnit.Framework.TestContext.WriteLine($@"{nameof(Constructor_Create_Correct)} complete. Elapsed time: {sw.Elapsed}");
        }

        [Test]
        public void Exists_Execute_Assert()
        {
            NUnit.Framework.TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            NUnit.Framework.TestContext.WriteLine($@"{nameof(Exists_Execute_Assert)} start.");
            var sw = Stopwatch.StartNew();

            var sqlAu = new SqlAuthentication(null, null, null, null);
            Assert.IsFalse(sqlAu.Exists());

            sqlAu = new SqlAuthentication("server", "database", "user", "password");
            Assert.IsTrue(sqlAu.Exists());

            sw.Stop();
            NUnit.Framework.TestContext.WriteLine($@"{nameof(Exists_Execute_Assert)} complete. Elapsed time: {sw.Elapsed}");
        }

        #endregion
    }
}