using Ws.Domain.Models.Entities.Ref1c.Plu;

namespace Ws.Domain.Models.Tests.Models;

public class PluModelTests
{
    [Fact]
    public void Check_Gtin_Piece_Plu()
    {
        PluEntity data = new()
        {
            Ean13 = "2600890000005",
            IsCheckWeight = true
        };
        data.Gtin.Should().Be("02600890000005");
    }

    [Fact]
    public void Check_Gtin_Weight_Plu()
    {
        PluEntity data = new()
        {
            Itf14 = "14607100238871",
            IsCheckWeight = false
        };
        data.Gtin.Should().Be("14607100238871");
    }
}