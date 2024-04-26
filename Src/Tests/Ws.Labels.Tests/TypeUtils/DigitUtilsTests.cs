using Ws.Labels.Service.Extensions;

namespace Ws.Labels.Tests.TypeUtils;

public class DigitUtilsTests
{

    [Fact]
    public void Convert_Decimal_To_Str_To_Len()
    {
        5.65m.ToStrWithLen(5).Should().Be("00565");
        1.250m.ToStrWithLen(5).Should().Be("01250");
        5.650m.ToStrWithLen(6).Should().Be("005650");
        20.150m.ToStrWithLen(5).Should().Be("20150");
        20.150m.ToStrWithLen(6).Should().Be("020150");
    }

    [Fact]
    public void Convert_Decimal_To_Str_With_Separator()
    {
        5.65m.ToStrWithSep(",").Should().Be("5,65");
        25.65m.ToStrWithSep("/").Should().Be("25/65");
        1.1m.ToStrWithSep(".").Should().Be("1.1");
    }

    [Fact]
    public void Convert_Int_To_Str_To_Len()
    {
        1.ToStrLenWithZero(5).Should().Be("00001");
        10.ToStrLenWithZero(3).Should().Be("010");
        15.ToStrLenWithZero(3).Should().Be("015");
        100.ToStrLenWithZero(4).Should().Be("0100");
        1323.ToStrLenWithZero(5).Should().Be("01323");
        21321.ToStrLenWithZero(4).Should().Be("2132");
    }
}