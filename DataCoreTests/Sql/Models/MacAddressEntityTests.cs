// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DataCoreTests.Sql.Models;

[TestFixture]
internal class MacAddressEntityTests
{
    [Test]
    public void MacEntityTests_Ctor_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () => await Task.Run(() =>
        {
            MacAddressEntity mac = new();
            foreach (string address in TestsEnums.GetString())
            {
                mac = new(address);
            }
        }));
    }
}
