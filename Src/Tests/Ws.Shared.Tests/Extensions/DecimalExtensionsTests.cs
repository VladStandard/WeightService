using System.Diagnostics.CodeAnalysis;
using Ws.Shared.Extensions;

namespace Ws.Shared.Tests.Extensions;

[SuppressMessage("Usage", "xUnit1012:Null should only be used for nullable parameters")]
public class DecimalExtensionsTests
{
    public static TheoryData<decimal, string, string> TestSepData()
    {
        return new()
        {
            { 234567.89m, "", "23456789" },
            { 1.89m, "/", "1/89" },
            { 100.00m, ",", "100,00" },
            { 999.99m, ".", "999.99" },
            { 42.42m, "-", "42-42" }
        };
    }

    [Theory]
    [MemberData(nameof(TestSepData))]
    public void Test_Decimal_Sep(decimal input, string sep, string expected)
    {
        input.ToSepStr(sep).Should().Be(expected);
    }
}