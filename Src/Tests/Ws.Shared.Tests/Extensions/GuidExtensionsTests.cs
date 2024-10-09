using Ws.Shared.Constants;
using Ws.Shared.Extensions;

namespace Ws.Shared.Tests.Extensions;

public class GuidExtensionsTests
{
    [Theory]
    [MemberData(nameof(TestCases.IsMaxTestCases), MemberType = typeof(TestCases))]
    public void Test_Guid_Max_Equal(Guid input, bool expected)
    {
        input.IsMax().Should().Be(expected);
    }
}

#region Test cases

file static class TestCases
{
    public static IEnumerable<object[]> IsMaxTestCases()
    {
        yield return [new Guid(), false];
        yield return [DefaultTypes.GuidMax, true];
    }
}

#endregion