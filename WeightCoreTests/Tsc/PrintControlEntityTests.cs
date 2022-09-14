// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;

namespace WeightCoreTests.Tsc;

[TestFixture]
internal class PrintControlEntityTests
{
    private readonly byte[] _byteArray = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x10, 0x20 };

    [Test]
    public void PrintControlEntity_GetStatusAsEnum_DoesNotThrow()
    {
        Utils.MethodStart();

        Assert.DoesNotThrow(() =>
        {
            //var printControl = new PrintControlEntity(PrintInterface.Ethernet, "192.168.6.132");
            //foreach (var b in _byteArray)
            //{
            //    TestContext.WriteLine(printControl.GetStatusAsEnum(b));
            //}
        });

        Utils.MethodComplete();
    }

    [Test]
    public void PrintControlEntity_GetStatusAsStringEng_DoesNotThrow()
    {
        Utils.MethodStart();

        Assert.DoesNotThrow(() =>
        {
            //var printControl = new PrintControlEntity(PrintInterface.Ethernet, "192.168.6.132");
            //foreach (var b in _byteArray)
            //{
            //    TestContext.WriteLine(printControl.GetStatusAsStringEng(b));
            //}
        });

        Utils.MethodComplete();
    }

    [Test]
    public void PrintControlEntity_GetStatusAsStringRus_DoesNotThrow()
    {
        Utils.MethodStart();

        Assert.DoesNotThrow(() =>
        {
            //var printControl = new PrintControlEntity(PrintInterface.Ethernet, "192.168.6.132");
            //foreach (var b in _byteArray)
            //{
            //    TestContext.WriteLine(printControl.GetStatusAsStringRus(b));
            //}
        });

        Utils.MethodComplete();
    }
}
