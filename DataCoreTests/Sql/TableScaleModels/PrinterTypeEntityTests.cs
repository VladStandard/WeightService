// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class PrinterTypeEntityTests
{
    [Test]
    public void Entity_Equals_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PrinterTypeEntity item = new();
            Assert.AreEqual(true, item.EqualsNew());
            Assert.AreEqual(true, item.EqualsDefault());
        });
    }
}
