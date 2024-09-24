using Ws.Barcodes.Formatters;

namespace Ws.Barcodes.Tests.Shared;

public static class TestData
{
    public static IEnumerable<object[]> GetSuccessFormatters()
    {
        yield return ["01", "#({0:C})", "#(01)"];
        yield return [1m, "{0:D6}", "000001"];
        yield return [172.2m, "{0:D5}", "01722"];
        yield return [(ushort)5, "{0:D5}", "00005"];
        yield return [(uint)10, "{0:D7}", "0000010"];
        yield return [(uint)10, "{0:D7}", "0000010"];
        yield return ["4607100238874", "{0:D13}", "4607100238874"];
        yield return ["14607100238871", "{0:D14}", "14607100238871"];
        yield return ["14607100238871", "{0:D20}", "00000014607100238871"];
        yield return [DateTime.Now, "{0:mmyyMMdd}", DateTime.Now.ToString("mmyyMMdd")];
        yield return [DateTime.Now, "{0:yyMMddHHmmss}", DateTime.Now.ToString("yyMMddHHmmss")];
    }

    public static IEnumerable<object[]> GetFailedFormatters()
    {
        yield return ["46071002388741", "{0:D5}"];
        yield return [172.2m, "{0:D3}"];
        yield return [-10, "{0:D7}"];
        yield return [null!, "{0:D7}"];
        yield return [DateTime.Now, "{0:T}"];
        yield return [DateTime.Now, "{0:MMMM}"];
        yield return [DateTime.Now, "{0:M}"];
    }
}

public class BarcodeFormatterTests
{
    [Theory]
    [MemberData(nameof(TestData.GetSuccessFormatters), MemberType = typeof(TestData))]
    public void Test_Success_Format(object? obj, string mask, string expected)
    {
        string.Format(BarcodeFormatter.Default, mask, obj).Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(TestData.GetFailedFormatters), MemberType = typeof(TestData))]
    public void Test_Failed_Format(object? obj, string mask)
    {
        Action act = () =>
        {
            string _ = string.Format(BarcodeFormatter.Default, mask, obj);
        };

        act.Should().Throw<FormatException>();
    }
}