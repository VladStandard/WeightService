// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL;
using DataProjectsCore.DAL.TableModels;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DataProjectsCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class _TemplateSqlExecuteTests
    {
        public void SqlPrepare()
        {
            _ = SqlConnectFactory.GetConnection(Utils.ConectionStringDevelop);
            TestContext.WriteLine($"{nameof(SqlConnectFactory)}: {SqlConnectFactory.GetConnection().ConnectionString}");
        }

        private void ExecuteReaderTemplate()
        {
            SqlPrepare();
            TestContext.WriteLine($"[db_scales].[Scales]");
            for (int id = 0; id < 10; id++)
            {
                _TemplateSqlExecute.ExecuteReaderTemplate(id);
                TestContext.WriteLine($"SCALE. ID: {id}. Description: {_TemplateSqlExecute.Result}");
            }
        }

        [Test]
        public void ExecuteReaderTemplate_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                ExecuteReaderTemplate();
            });
            TestContext.WriteLine();

            Assert.DoesNotThrowAsync(async () => await Task.Run(() =>
            {
                ExecuteReaderTemplate();
            }));

            Utils.MethodComplete();
        }
    }
}
