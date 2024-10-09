using Ws.Shared.ValueTypes;

namespace Ws.Shared.Tests.ValueTypes;

public class ValueTypesTests
{
    [Theory]
    [MemberData(nameof(TestCases.FioTestCases), MemberType = typeof(TestCases))]
    public void Test_Fio(Fio fio, string fullName, string shortName)
    {
        fio.DisplayFullName.Should().Be(fullName);
        fio.DisplayShortName.Should().Be(shortName);
    }
}

#region Test cases

file static class TestCases
{
    public static IEnumerable<object[]> FioTestCases()
    {
        yield return [
            new Fio("Даниил", "александров", "дмитриевич"),
            "Александров Даниил Дмитриевич",
            "Александров Д.Д"
        ];
        yield return [
            new Fio("артем", "Власов", "Алексеевич"),
            "Власов Артем Алексеевич",
            "Власов А.А"
        ];
        yield return [
            new Fio("Дмитрий", "Александров", ""),
            "Александров Дмитрий",
            "Александров Д."
        ];
    }
}

#endregion