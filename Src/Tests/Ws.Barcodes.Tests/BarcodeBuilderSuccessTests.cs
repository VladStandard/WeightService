using FluentAssertions;
using Ws.Barcodes.Models;
using Xunit;

namespace Ws.Barcodes.Tests;

public class BarcodeBuilderSuccessTests
{
    #region TestData

    private static BarcodeBuilder CreateBarcodeBuilder()
    {
        return new()
        {
            LineNumber = 96446,
            LineCounter = 507602,
            PluGtin = "14607100238871",
            PluEan13 = "4607100238874",
            PluNumber = 331,
            ProductDt = new(2024, 9, 19, 12, 55, 0),
            ExpirationDt = new(2024, 10, 19, 12, 55, 0),
            ExpirationDay = 56,
            Kneading = 332,
            BundleCount = 20,
            WeightNet = 21.212m
        };
    }

    public static IEnumerable<object[]> GetBarcodeVariables()
    {
        yield return
        [
            new List<BarcodeVar>
            {
                new("01", "({0})"),
                new(nameof(BarcodeBuilder.PluGtin), "{0:D14}"),
                new("3103", "({0})"),
                new(nameof(BarcodeBuilder.WeightNet), "{0:D6}"),
                new("11", "({0})"),
                new(nameof(BarcodeBuilder.ProductDt), "{0:yyMMdd}"),
                new("17", "({0})"),
                new(nameof(BarcodeBuilder.ExpirationDt), "{0:yyMMdd}"),
                new("37", "({0})"),
                new(nameof(BarcodeBuilder.BundleCount), "{0:D2}"),
                new("10", "#({0})"),
                new(nameof(BarcodeBuilder.ProductDt), "{0:MMdd}"),
                new("250", "#({0})"),
                new(nameof(BarcodeBuilder.LineNumber), "{0:D5}"),
                new(nameof(BarcodeBuilder.LineCounter), "{0:D6}"),
            },
            "(01)14607100238871(3103)021212(11)240919(17)241019(37)20#(10)0919#(250)96446507602"
        ];

        yield return
        [
            new List<BarcodeVar>
            {
                new("233"),
                new(nameof(BarcodeBuilder.LineNumber), "{0:D5}"),
                new(nameof(BarcodeBuilder.BundleCount), "{0:D2}"),
                new(nameof(BarcodeBuilder.LineCounter), "{0:D6}"),
                new(nameof(BarcodeBuilder.ProductDt), "{0:yyMMddHHmmss}"),
                new(nameof(BarcodeBuilder.PluNumber), "{0:D3}"),
                new(nameof(BarcodeBuilder.Kneading), "{0:D8}"),
            },
            "233964462050760224091912550033100000332"
        ];

        yield return
        [
            new List<BarcodeVar>
            {
                new("233"),
                new(nameof(BarcodeBuilder.LineNumber), "{0:D5}"),
                new(nameof(BarcodeBuilder.BundleCount), "{0:D2}"),
                new(nameof(BarcodeBuilder.LineCounter), "{0:D6}"),
                new(nameof(BarcodeBuilder.ProductDt), "{0:yyMMddHHmmss}"),
                new(nameof(BarcodeBuilder.PluNumber), "{0:D3}"),
                new(nameof(BarcodeBuilder.Kneading), "{0:D8}"),
            },
            "233964462050760224091912550033100000332"
        ];
    }

    #endregion

    [Theory]
    [MemberData(nameof(GetBarcodeVariables))]
    public void Test_Success_Barcodes(List<BarcodeVar> variables, string expectedBarcode)
    {
        VerifySuccessResult(CreateBarcodeBuilder().Build(variables), expectedBarcode);
    }

    #region Private

    private static void VerifySuccessResult(BarcodeResult result, string barcodeFull)
    {
        result.Zpl.Should().Be(barcodeFull.Replace("(", "").Replace(")", ""));
        result.Clean.Should().Be(barcodeFull.Replace("(", "").Replace(")", "").Replace("#", ""));
        result.Friendly.Should().Be(barcodeFull.Replace("#", ""));
    }

    #endregion
}