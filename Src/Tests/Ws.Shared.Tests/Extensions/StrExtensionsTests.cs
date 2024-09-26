using Ws.Shared.Extensions;

namespace Ws.Shared.Tests.Extensions;

public class StrExtensionsTests
{
    [Theory]
    [MemberData(nameof(TestCases.IsDigitTestCases), MemberType = typeof(TestCases))]
    public void Test_Is_Digits_Only(string input, bool expected)
    {
        input.IsDigitsOnly().Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(TestCases.TranslitTestCases), MemberType = typeof(TestCases))]
    public void Test_Translit(string input, string result)
    {
        input.Transliterate().Should().Be(result);
    }

    [Theory]
    [InlineData("александров даниил дмитриевич", "Александров Даниил Дмитриевич")]
    [InlineData("Власов артем", "Власов Артем")]
    public void Test_Capitalize(string input, string expected)
    {
        input.Capitalize().Should().Be(expected);
    }
}

#region Test cases

file static class TestCases
{
    public static IEnumerable<object[]> TranslitTestCases()
    {
        // VALID
        yield return ["Замороженное", "Zamorozhennoe"];
        yield return ["Охлаждённое", "Okhlazhdennoe"];
        yield return ["Александров Даниил Дмитриевич", "Aleksandrov Daniil Dmitrievich"];
        yield return ["Власов Артём Алексеевич", "Vlasov Artem Alekseevich"];
        yield return ["Государство", "Gosudarstvo"];
        yield return ["Помещение", "Pomeshchenie"];
        yield return ["Объект", "Obieekt"];
    }

    public static IEnumerable<object[]> IsDigitTestCases()
    {
        // VALID
        yield return ["0", true];
        yield return ["1234567890", true];

        // INVALID
        yield return ["", false];
        yield return ["12ab34cd", false];
        yield return ["191230123o", false];
    }
}

#endregion