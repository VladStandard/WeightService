// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWeightTests.Helpers;

[TestFixture]
public class HardwareHelperTests
{
    private HardwareHelper Hard { get; set; } = HardwareHelper.Instance;

    [Test]
    public void SearchingDriver_AreEqual()
    {
        Assert.AreEqual(true, true);
    }
}
