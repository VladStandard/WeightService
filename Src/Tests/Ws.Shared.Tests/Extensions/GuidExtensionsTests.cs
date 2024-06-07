using System.Diagnostics.CodeAnalysis;
using Ws.Shared.Constants;
using Ws.Shared.Extensions;

namespace Ws.Shared.Tests.Extensions;

[SuppressMessage("Usage", "xUnit1044:Avoid using TheoryData type arguments that are not serializable")]
public class GuidExtensionsTests
{
    public static TheoryData<Guid, bool> TestSepData() =>
        new() { { new Guid(), false }, { BaseConsts.GuidMax, true } };

    [Theory]
    [MemberData(nameof(TestSepData))]
    public void Test_Guid_Max_Equal(Guid input, bool expected)
    {
        input.IsMax().Should().Be(expected);
    }
}