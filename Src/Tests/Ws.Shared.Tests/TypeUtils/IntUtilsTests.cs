using Ws.Shared.TypeUtils;

namespace Ws.Shared.Tests.TypeUtils;

public class IntUtilsTests
{
    [Fact]
    public void Convert_Int_To_Str_To_Len()
    {
        Assert.Equal("00001", IntUtils.ToStringToLen(1, 5));
        Assert.Equal("010", IntUtils.ToStringToLen(10, 3));
        Assert.Equal("015", IntUtils.ToStringToLen(15, 3));
        Assert.Equal("0100", IntUtils.ToStringToLen(100, 4));
        Assert.Equal("01323", IntUtils.ToStringToLen(1323, 5));
        Assert.Equal("2132", IntUtils.ToStringToLen(21323, 4));
    }
}