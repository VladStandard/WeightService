using Ws.Shared.Constants;

namespace Ws.Shared.Tests.Constants;

public class DefaultConstsTests
{
    [Fact]
    public void Test_Guid_Max()
    {
        DefaultConsts.GuidMax.Should().Be(Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"));
        DefaultConsts.GuidMax.Should().Be(Guid.Parse("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"));
    }
}