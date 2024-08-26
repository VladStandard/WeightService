using Ws.Shared.Constants;

namespace Ws.Shared.Tests.Constants;

public class BaseConstantsTests
{
    [Fact]
    public void Test_Guid_Max()
    {
        BaseConstants.GuidMax.Should().Be(Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"));
        BaseConstants.GuidMax.Should().Be(Guid.Parse("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"));
    }
}