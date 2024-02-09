using Ws.Shared.TypeUtils;

namespace Ws.Shared.Tests.TypeUtils;

public class DecimalUtilsTests
{
    [Fact]
    public void Convert_Decimal_To_Str_To_Len()
    {
        Assert.Equal("00565", DecimalUtils.ToStrToLen(5.65m, 5));
        Assert.Equal("01250", DecimalUtils.ToStrToLen(1.250m, 5));
        Assert.Equal("005650", DecimalUtils.ToStrToLen(5.650m, 6));
        Assert.Equal("20150", DecimalUtils.ToStrToLen(20.150m, 5));
        Assert.Equal("020150", DecimalUtils.ToStrToLen(20.150m, 6));
    }

    [Fact]
    public void Convert_Decimal_To_Str_With_Separator()
    {
        Assert.Equal("5,65", DecimalUtils.ToStrWithSep(5.65m, ","));
        Assert.Equal("25/65", DecimalUtils.ToStrWithSep(25.65m, "/"));
        Assert.Equal("1.1", DecimalUtils.ToStrWithSep(1.1m, "."));
    }
}