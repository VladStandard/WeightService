using Ws.Shared.Constants;

namespace Ws.Shared.Tests.Constants;

public class BaseConstsTests
{
    [Fact]
    public void Test_Guid_Max()
    {
        BaseConsts.GuidMax.Should().Be(Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"));
        BaseConsts.GuidMax.Should().Be(Guid.Parse("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"));
    }
}