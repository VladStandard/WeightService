// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.Models;

[TestFixture]
internal class CrudControllerTests
{
    [Test]
    public void GetFreeHosts_Exec_DoesNotThrow()
    {
        DataCoreUtils.AssertAction(() =>
        {
            foreach (long? id in DataCoreEnums.GetLongNullable())
            {
                foreach (bool? isMarked in DataCoreEnums.GetBoolNullable())
                {
                    TestContext.WriteLine($"{nameof(id)}: {id}. {nameof(isMarked)}: {isMarked}");
                    List<HostEntity> hosts = DataCoreUtils.DataAccess.CrudHost.GetFree(id, isMarked);
                    foreach (HostEntity host in hosts)
                    {
                        TestContext.WriteLine(host);
                    }
                }
            }
        });
    }

    [Test]
    public void GetBusyHosts_Exec_DoesNotThrow()
    {
        DataCoreUtils.AssertAction(() =>
        {
            foreach (int? id in DataCoreEnums.GetIntNullable())
            {
                foreach (bool? isMarked in DataCoreEnums.GetBoolNullable())
                {
                    TestContext.WriteLine($"{nameof(id)}: {id}. {nameof(isMarked)}: {isMarked}");
                    List<HostEntity> hosts = DataCoreUtils.DataAccess.CrudHost.GetBusy(null, null);
                    foreach (HostEntity host in hosts)
                    {
                        TestContext.WriteLine(host);
                    }
                }
            }
        });
    }
}
