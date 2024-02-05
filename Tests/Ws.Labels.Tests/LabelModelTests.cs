using Ws.Labels.Service.Features.PrintLabel.Models;

namespace Ws.Labels.Tests;

public class LabelModelTests
{
    [Fact]
    public void Check_LabelModel_Barcodes()
    {
        LabelModel model = new()
        {
            BundleCount = 15,
            Kneading = 1,
            LineCounter = 101,
            LineNumber = 12312,
            ProductDtValue = new(2023, 11, 20, 11, 40, 29),
            PluGtin = "14607100238000",
            PluNumber = 844,
        };
        Assert.Equal("23412312000101231120",model.BarCodeRight);
        Assert.Equal("233123121500010123112011402984400000001",model.BarCodeTop);
        Assert.Equal("(01)14607100238000(37)00000015(11)231120(10)2311",model.BarCodeBottom);
    }
}