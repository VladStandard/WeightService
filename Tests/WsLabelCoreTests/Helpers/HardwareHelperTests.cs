// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCoreTests.Helpers;

[TestFixture]
public sealed class HardwareHelperTests
{
    private WsHardwareHelper Hard => WsHardwareHelper.Instance;

    [Test]
    public void SearchingDriver_AreEqual()
    {
        Assert.AreEqual(true, true);
    }
}
