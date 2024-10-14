using System.Net;
using Ws.Shared.Utils;

namespace Ws.Shared.Tests.Utils;

public class StrUtilsTests
{
    [Theory]
    [MemberData(nameof(TestCases.IpAddressTestCases), MemberType = typeof(TestCases))]
    public void Test_Parse_To_Ip_V4_Address(string input, bool expectedResult)
    {
        bool result = StrUtils.TryParseToIpV4Address(input, out IPAddress? ipAddress);
        expectedResult.Should().Be(result);

        if (expectedResult)
        {
            ipAddress.Should().NotBeNull();
            ipAddress.Should().Be(IPAddress.Parse(input));
        }
        else
            ipAddress.Should().BeNull();
    }
}

#region Test cases

file static class TestCases
{
    public static IEnumerable<object[]> IpAddressTestCases()
    {
        // VALID
        yield return ["192.168.1.1", true];
        yield return ["255.255.255.255", true];
        yield return ["0.0.0.0", true];
        yield return ["127.0.0.1", true];

        // INVALID
        yield return ["   ", false];
        yield return ["192.168.1", false];
        yield return ["192.168.1.1.1", false];
        yield return ["256.256.256.256", false];
        yield return ["abc.def.ghi.jkl", false];
        yield return ["2001:0db8:85a3:0000:0000:8a2e:0370:7334", false];
    }
}

#endregion