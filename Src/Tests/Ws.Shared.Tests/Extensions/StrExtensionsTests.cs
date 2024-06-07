using Ws.Shared.Extensions;

namespace Ws.Shared.Tests.Extensions;

public class StrExtensionsTests
{
    [Theory]
    [InlineData("0", true)]
    [InlineData("1234567890", true)]
    [InlineData("", false)]
    [InlineData("12ab34cd", false)]
    [InlineData("191230123o", false)]
    public void Test_Is_Digit_Only(string input, bool expected)
    {
        input.IsDigitsOnly().Should().Be(expected);
    }

    [Theory]
    [InlineData("ddMMyy", true)]
    [InlineData("HHmmss", true)]
    [InlineData("", false)]
    [InlineData("HH:mm", false)]
    [InlineData("dd/MM/yy", false)]
    [InlineData("HH.mm.ss", false)]
    [InlineData("yyyy.MM.dd", false)]
    [InlineData("MM-dd-yyyy", false)]
    [InlineData("yyyy-MM-dd HH:mm:ss", false)]
    public void Test_Is_Date_Format(string input, bool expected)
    {
        input.IsDateFormat().Should().Be(expected);
    }
}