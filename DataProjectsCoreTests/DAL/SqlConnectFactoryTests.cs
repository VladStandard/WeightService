// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL;
using DataProjectsCore.DAL.TableModels;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DataProjectsCoreTests.DAL
{
    [TestFixture]
    internal class SqlConnectFactoryTests
    {
        public void SqlPrepare()
        {
            _ = SqlConnectFactory.GetConnection(Utils.ConectionStringDevelop);
            TestContext.WriteLine($"{nameof(SqlConnectFactory)}: {SqlConnectFactory.GetConnection().ConnectionString}");
        }

        [Test]
        public void ExecuteReader_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                SqlPrepare();
                TestContext.WriteLine($"[db_scales].[Scales]");
                for (int id = 0; id < 10; id++)
                {
                    SqlConnectFactoryTemplate.ExecuteReaderTemplate(id);
                    TestContext.WriteLine($"SCALE. ID: {id}. Description: {SqlConnectFactoryTemplate.Result}");
                }
            });
            TestContext.WriteLine();

            Utils.MethodComplete();
        }
    }
}
