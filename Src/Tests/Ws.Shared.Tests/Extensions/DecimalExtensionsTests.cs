using System.Diagnostics.CodeAnalysis;
using Ws.Shared.Extensions;

namespace Ws.Shared.Tests.Extensions;

[SuppressMessage("Usage", "xUnit1012:Null should only be used for nullable parameters")]
public class DecimalExtensionsTests
{
    public static TheoryData<decimal, char?, string> TestSepData()
    {
        return new()
        {
            { 234567.89m, null, "234567890" },
            { 1.89m, '/', "1/890" },
            { 100.00m, ',', "100,000" },
            { 999.99m, '.', "999.990" },
            { 42.42m, '-', "42-420" }
        };
    }

    [Theory]
    [MemberData(nameof(TestSepData))]
    public void Test_Decimal_Sep(decimal input, char? sep, string expected)
    {
        input.ToSepStr(sep).Should().Be(expected);
    }
}