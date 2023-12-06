using Ws.Labels.Models;

namespace Ws.Labels.Tests;

public class WeightLabelModelTests
{
    [Fact]
    public void Check_WeightLabelModel_Barcodes()
    {
        WeightLabelModel model = new()
        {
            Weight = 16.696m,
            Kneading = 16,
            LineCounter = 288095,
            LineNumber = 10430,
            ProductDtValue = new(2023, 12, 5, 15, 19, 49),
            PluGtin = "02600770000002",
            PluNumber = 333 ,
        };
        Assert.Equal("2991043000288095",model.BarCodeRight);
        Assert.Equal("298104300028809523120515194933316696016",model.BarCodeTop);
        Assert.Equal("(01)02600770000002(3103)016696(11)231205(10)2312",model.BarCodeBottom);
    }
}