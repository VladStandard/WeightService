using CoreTests;
using DataShareCore.DAL.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BlazorCoreTests.DAL.Models
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
