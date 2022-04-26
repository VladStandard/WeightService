// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.TableScaleModels;
using NUnit.Framework;
using System.Collections.Generic;

namespace DataCoreTests.DAL.TableDirectModels
{
    [TestFixture]
    internal class CrudControllerTests
    {
        [Test]
        public void GetFreeHosts_Exec_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
                foreach (long? id in TestsEnums.GetLongNullable())
                {
                    foreach (bool? isMarked in TestsEnums.GetBoolNullable())
                    {
                        TestContext.WriteLine($"{nameof(id)}: {id}. {nameof(isMarked)}: {isMarked}");
                        List<HostEntity> hosts = TestsUtils.DataAccess.Crud.GetFreeHosts(id, isMarked);
                        foreach (HostEntity host in hosts)
                        {
                            TestContext.WriteLine($"{host}");
                        }
                        TestContext.WriteLine();
                    }
                }
            });
            TestContext.WriteLine();
        }

        [Test]
        public void GetBusyHosts_Exec_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
                foreach (int? id in TestsEnums.GetIntNullable())
                {
                    foreach (bool? isMarked in TestsEnums.GetBoolNullable())
                    {
                        TestContext.WriteLine($"{nameof(id)}: {id}. {nameof(isMarked)}: {isMarked}");
                        List<HostEntity> hosts = TestsUtils.DataAccess.Crud.GetBusyHosts(null, null);
                        foreach (HostEntity host in hosts)
                        {
                            TestContext.WriteLine($"{host}");
                        }
                        TestContext.WriteLine();
                    }
                }
            });
            TestContext.WriteLine();
        }
    }
}
