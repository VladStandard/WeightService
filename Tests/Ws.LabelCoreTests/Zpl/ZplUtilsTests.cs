using PrinterCore.Zpl;

namespace Ws.LabelCoreTests.Zpl;

[TestFixture]
public sealed class ZplUtilsTests
{
    [Test]
    public void ZplUtils_ToCodePoints_GetZplSample1()
    {
        Assert.DoesNotThrow(() =>
        {
            TestContext.WriteLine(ZplUtils.ConvertStringToHex(ZplSamples.GetSample1));
        });
    }

    [Test]
    public void ZplUtils_ToCodePoints_GetZplSample2()
    {
        Assert.DoesNotThrow(() =>
        {
            TestContext.WriteLine(ZplUtils.ConvertStringToHex(ZplSamples.GetSample2));
        });
    }

    [Test]
    public void ZplUtils_ToCodePoints_GetZplSample3()
    {
        Assert.DoesNotThrow(() =>
        {
            TestContext.WriteLine(ZplUtils.ConvertStringToHex(ZplSamples.GetSample3));
        });
    }

    [Test]
    public void ZplUtils_ToCodePoints_GetZplSampleFull()
    {
        Assert.DoesNotThrow(() =>
        {
            string zpl = ZplUtils.ConvertStringToHex(ZplSamples.GetSampleFull);
            TestContext.WriteLine(zpl);
        });
    }
}