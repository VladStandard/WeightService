using PrinterCore.Zpl;
namespace WsLabelCoreTests.Zpl;

[TestFixture]
public sealed class ZplUtilsTests
{
    [Test]
    public void ZplUtils_IsDigit_True()
    {
        Assert.DoesNotThrow(() =>
        {
            // Digits.
            Assert.AreEqual(true, WsDataFormatUtils.IsDigit('0'));
            Assert.AreEqual(true, WsDataFormatUtils.IsDigit('1'));
            Assert.AreEqual(true, WsDataFormatUtils.IsDigit('2'));
            Assert.AreEqual(true, WsDataFormatUtils.IsDigit('3'));
            Assert.AreEqual(true, WsDataFormatUtils.IsDigit('4'));
            Assert.AreEqual(true, WsDataFormatUtils.IsDigit('5'));
            Assert.AreEqual(true, WsDataFormatUtils.IsDigit('6'));
            Assert.AreEqual(true, WsDataFormatUtils.IsDigit('7'));
            Assert.AreEqual(true, WsDataFormatUtils.IsDigit('8'));
            Assert.AreEqual(true, WsDataFormatUtils.IsDigit('9'));
        });
    }

    [Test]
    public void ZplUtils_IsSpecial_True()
    {
        Assert.DoesNotThrow(() =>
        {
            // ' ', ',', '.', '-', 
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial(' '));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial(','));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('.'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('-'));

            // '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=',
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('~'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('!'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('#'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('$'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('%'));
            Assert.AreEqual(false, WsDataFormatUtils.IsSpecial('^'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('&'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('*'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial(')'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('_'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('+'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('='));

            // '"', '№', ';', ':', '?', 
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('"'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('№'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial(';'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial(':'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('?'));

            // '/', '|', '\\', '{', '}', '<', '>'
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('/'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('|'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('\\'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('{'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('}'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('<'));
            Assert.AreEqual(true, WsDataFormatUtils.IsSpecial('>'));
        });
    }

    [Test]
    public void ZplUtils_IsCyrillic_False()
    {
        Assert.DoesNotThrow(() =>
        {
            // Digits.
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('0'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('1'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('2'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('3'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('4'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('5'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('6'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('7'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('8'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('9'));

            // ' ', ',', '.', '-', 
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic(' '));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic(','));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('.'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('-'));

            // '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=',
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('~'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('!'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('#'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('$'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('%'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('^'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('&'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('*'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic(')'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('_'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('+'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('='));

            // '"', '№', ';', ':', '?', 
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('"'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('№'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic(';'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic(':'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('?'));

            // '/', '|', '\\', '{', '}'
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('/'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('|'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('\\'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('{'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('}'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('<'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('>'));

            // English lower letters.
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('q'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('w'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('e'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('r'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('t'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('y'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('u'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('i'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('o'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('p'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('a'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('s'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('d'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('f'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('g'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('h'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('j'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('k'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('l'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('z'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('x'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('c'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('v'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('b'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('n'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('m'));

            // English upper letters.
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('Q'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('W'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('E'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('R'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('T'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('Y'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('U'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('I'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('O'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('P'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('A'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('S'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('D'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('F'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('G'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('H'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('J'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('K'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('L'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('Z'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('X'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('C'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('V'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('B'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('N'));
            Assert.AreEqual(false, WsDataFormatUtils.IsCyrillic('M'));
        });
    }

    [Test]
    public void ZplUtils_IsCyrillic_True()
    {
        Assert.DoesNotThrow(() =>
        {
            // Russian lower letters.
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('й'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('ц'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('у'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('к'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('е'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('н'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('г'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('ш'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('щ'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('з'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('х'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('ъ'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('ф'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('ы'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('в'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('а'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('п'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('р'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('о'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('л'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('д'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('ж'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('э'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('я'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('ч'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('с'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('м'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('и'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('т'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('ь'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('б'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('ю'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('ё'));

            // Russian upper letters.
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Й'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Ц'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('У'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('К'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Е'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Н'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Г'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Ш'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Щ'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('З'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Х'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Ъ'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Ф'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Ы'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('В'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('А'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('П'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Р'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('О'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Л'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Д'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Ж'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Э'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Я'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Ч'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('С'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('М'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('И'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Т'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Ь'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Б'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Ю'));
            Assert.AreEqual(true, WsDataFormatUtils.IsCyrillic('Ё'));
        });
    }

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