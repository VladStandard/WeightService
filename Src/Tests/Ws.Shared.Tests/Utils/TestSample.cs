namespace Ws.Shared.Tests.Utils;

public class TestSample
{
    [Fact]
    public void Test()
    {
        const string number = "342134";
        const string testFormat = "({0})";
        string formatted = string.Format(testFormat, number);
    }
}