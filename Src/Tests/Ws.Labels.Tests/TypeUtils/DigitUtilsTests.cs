using Ws.Labels.Service.Extensions;

namespace Ws.Labels.Tests.TypeUtils;

public class DigitUtilsTests
{

    [Fact]
    public void Convert_Decimal_To_Str_To_Len()
    {
        Assert.Equal("00565", 5.65m.ToStrWithLen(5));
        Assert.Equal("01250", 1.250m.ToStrWithLen(5));
        Assert.Equal("005650", 5.650m.ToStrWithLen(6));
        Assert.Equal("20150", 20.150m.ToStrWithLen(5));
        Assert.Equal("020150", 20.150m.ToStrWithLen(6));
    }

    [Fact]
    public void Convert_Decimal_To_Str_With_Separator()
    {
        Assert.Equal("5,65", 5.65m.ToStrWithSep(","));
        Assert.Equal("25/65", 25.65m.ToStrWithSep("/"));
        Assert.Equal("1.1", 1.1m.ToStrWithSep("."));
    }

    [Fact]
    public void Convert_Int_To_Str_To_Len()
    {
        Assert.Equal("00001", 1.ToStrLenWithZero(5));
        Assert.Equal("010", 10.ToStrLenWithZero(3));
        Assert.Equal("015", 15.ToStrLenWithZero(3));
        Assert.Equal("0100", 100.ToStrLenWithZero(4));
        Assert.Equal("01323", 1323.ToStrLenWithZero(5));
        Assert.Equal("2132", 21321.ToStrLenWithZero(4));
    }
}