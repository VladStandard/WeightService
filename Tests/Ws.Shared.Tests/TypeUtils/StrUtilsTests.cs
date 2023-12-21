using Ws.Shared.TypeUtils;

namespace Ws.Shared.Tests.TypeUtils;

public class StrUtilsTests
{
    [Fact]
    public void Str_To_Len()
    {
        Assert.Equal("00001", StrUtils.ToLen("1", 5));
        Assert.Equal("010", StrUtils.ToLen("10", 3));
        Assert.Equal("015", StrUtils.ToLen("15", 3));
        Assert.Equal("0100", StrUtils.ToLen("100", 4));
        Assert.Equal("01323", StrUtils.ToLen("1323", 5));
        Assert.Equal("2132", StrUtils.ToLen("21323", 4));
    }
}