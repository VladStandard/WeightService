using Ws.Labels.Service.Extensions;

namespace Ws.Labels.Tests.TypeUtils;

public class StrUtilsTests
{
    [Fact]
    public void Str_To_Len()
    {
        Assert.Equal("00001", "1".ToLenWithZero(5));
        Assert.Equal("010", "10".ToLenWithZero(3));
        Assert.Equal("015", "15".ToLenWithZero(3));
        Assert.Equal("0100", "100".ToLenWithZero(4));
        Assert.Equal("01323", "1323".ToLenWithZero(5));
        Assert.Equal("2132", "21323".ToLenWithZero(4));
    }
}