using Ws.Shared.Utils;

namespace Ws.Shared.Tests.Utils;

public class TranslitTests
{
    [Theory]
    [InlineData("Замороженное", "Zamorozhennoe")]
    [InlineData("Охлаждённое", "Okhlazhdennoe")]
    [InlineData("Александров Даниил Дмитриевич", "Aleksandrov Daniil Dmitrievich")]
    [InlineData("Власов Артём Алексеевич", "Vlasov Artem Alekseevich")]
    [InlineData("Государство", "Gosudarstvo")]
    [InlineData("Помещение", "Pomeshchenie")]
    [InlineData("Объект", "Obieekt")]
    public void Test_translit(string input, string result)
    {
        TranslitUtil.Transliterate(input).Should().Be(result);
    }
}