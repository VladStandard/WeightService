using Ws.Labels.Service.Features.Generate.Features.Piece.Models;

namespace Ws.Labels.Tests.Features;

public class XmlPieceLabelTests
{
    [Fact]
    public void Check_LabelModel_Barcodes()
    {
        XmlPieceLabel xmlPieceLabel = new()
        {
            BundleCount = 15,
            Kneading = 1,
            LineCounter = 101,
            LineNumber = 12312,
            ProductDtValue = new DateTime(2023, 11, 20, 11, 40, 29),
            PluGtin = "14607100238000",
            PluNumber = 844,
            ExpirationDtValue = default,
            LineName = "",
            LineAddress = "",
            PluFullName = "",
            PluDescription = ""
        };
        xmlPieceLabel.BarCodeRight.Should().Be("23412312000101231120");
        xmlPieceLabel.BarCodeTop.Should().Be("233123121500010123112011402984400000001");
        xmlPieceLabel.BarCodeBottom.Should().Be("(01)14607100238000(37)00000015(11)231120(10)2311");
    }
}