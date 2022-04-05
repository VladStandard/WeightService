// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DataCoreTests.DAL.Models
{
    [TestFixture]
    internal class MacAddressEntityTests
    {
        [Test]
        public void MacEntityTests_Ctor_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrowAsync(async () => await Task.Run(() =>
            {
                MacAddressEntity mac = new();
                foreach (string address in TestsEnums.GetString())
                {
                    mac = new(address);
                }
            }));

            TestsUtils.MethodComplete();
        }
    }
}
