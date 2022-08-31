// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class ProductionFacilityModelTests
{
    [Test]
    public void Model_Equals_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            // Arrange.
            ProductionFacilityModel item = new();
            ProductionFacilityModel item2 = Substitute.For<ProductionFacilityModel>();
            //item.GetHashCode().Returns(0);
            // Act.
            bool success = item.EqualsNew();
            // Assert.
            Assert.True(success);
            // Act.
            success = item.EqualsDefault();
            // Assert.
            Assert.True(success);
        });
    }
}
