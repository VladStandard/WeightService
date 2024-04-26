using Ws.Labels.Service.Extensions;

namespace Ws.Labels.Tests.TypeUtils;

public class StrUtilsTests
{
    [Fact]
    public void Str_To_Len()
    {
        "1".ToLenWithZero(5).Should().Be("00001");
        "10".ToLenWithZero(3).Should().Be("010");
        "15".ToLenWithZero(3).Should().Be("015");
        "100".ToLenWithZero(4).Should().Be("0100");
        "1323".ToLenWithZero(5).Should().Be("01323");
        "21323".ToLenWithZero(4).Should().Be("2132");
    }
}