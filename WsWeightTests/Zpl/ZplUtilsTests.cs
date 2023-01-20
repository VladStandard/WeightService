// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWeightTests.Zpl;

[TestFixture]
public class ZplUtilsTests
{
    [Test]
    public void ZplUtils_IsDigit_True()
    {
        Assert.DoesNotThrow(() =>
        {
            // Digits.
            Assert.AreEqual(true, DataFormatUtils.IsDigit('0'));
            Assert.AreEqual(true, DataFormatUtils.IsDigit('1'));
            Assert.AreEqual(true, DataFormatUtils.IsDigit('2'));
            Assert.AreEqual(true, DataFormatUtils.IsDigit('3'));
            Assert.AreEqual(true, DataFormatUtils.IsDigit('4'));
            Assert.AreEqual(true, DataFormatUtils.IsDigit('5'));
            Assert.AreEqual(true, DataFormatUtils.IsDigit('6'));
            Assert.AreEqual(true, DataFormatUtils.IsDigit('7'));
            Assert.AreEqual(true, DataFormatUtils.IsDigit('8'));
            Assert.AreEqual(true, DataFormatUtils.IsDigit('9'));
        });
    }

    [Test]
    public void ZplUtils_IsSpecial_True()
    {
        Assert.DoesNotThrow(() =>
        {
            // ' ', ',', '.', '-', 
            Assert.AreEqual(true, DataFormatUtils.IsSpecial(' '));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial(','));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('.'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('-'));

            // '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=',
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('~'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('!'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('#'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('$'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('%'));
            Assert.AreEqual(false, DataFormatUtils.IsSpecial('^'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('&'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('*'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial(')'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('_'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('+'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('='));

            // '"', '№', ';', ':', '?', 
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('"'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('№'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial(';'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial(':'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('?'));

            // '/', '|', '\\', '{', '}', '<', '>'
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('/'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('|'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('\\'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('{'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('}'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('<'));
            Assert.AreEqual(true, DataFormatUtils.IsSpecial('>'));
        });
    }

    [Test]
    public void ZplUtils_IsCyrillic_False()
    {
        Assert.DoesNotThrow(() =>
        {
            // Digits.
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('0'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('1'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('2'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('3'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('4'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('5'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('6'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('7'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('8'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('9'));

            // ' ', ',', '.', '-', 
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic(' '));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic(','));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('.'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('-'));

            // '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=',
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('~'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('!'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('#'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('$'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('%'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('^'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('&'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('*'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic(')'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('_'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('+'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('='));

            // '"', '№', ';', ':', '?', 
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('"'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('№'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic(';'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic(':'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('?'));

            // '/', '|', '\\', '{', '}'
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('/'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('|'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('\\'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('{'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('}'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('<'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('>'));

            // English lower letters.
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('q'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('w'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('e'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('r'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('t'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('y'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('u'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('i'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('o'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('p'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('a'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('s'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('d'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('f'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('g'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('h'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('j'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('k'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('l'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('z'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('x'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('c'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('v'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('b'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('n'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('m'));

            // English upper letters.
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('Q'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('W'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('E'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('R'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('T'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('Y'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('U'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('I'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('O'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('P'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('A'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('S'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('D'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('F'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('G'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('H'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('J'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('K'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('L'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('Z'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('X'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('C'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('V'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('B'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('N'));
            Assert.AreEqual(false, DataFormatUtils.IsCyrillic('M'));
        });
    }

    [Test]
    public void ZplUtils_IsCyrillic_True()
    {
        Assert.DoesNotThrow(() =>
        {
            // Russian lower letters.
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('й'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('ц'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('у'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('к'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('е'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('н'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('г'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('ш'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('щ'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('з'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('х'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('ъ'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('ф'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('ы'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('в'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('а'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('п'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('р'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('о'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('л'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('д'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('ж'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('э'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('я'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('ч'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('с'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('м'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('и'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('т'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('ь'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('б'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('ю'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('ё'));

            // Russian upper letters.
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Й'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Ц'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('У'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('К'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Е'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Н'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Г'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Ш'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Щ'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('З'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Х'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Ъ'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Ф'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Ы'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('В'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('А'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('П'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Р'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('О'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Л'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Д'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Ж'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Э'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Я'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Ч'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('С'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('М'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('И'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Т'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Ь'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Б'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Ю'));
            Assert.AreEqual(true, DataFormatUtils.IsCyrillic('Ё'));
        });
    }

    [Test]
    public void ZplUtils_ToCodePoints_GetZplSample1()
    {
        Assert.DoesNotThrow(() =>
        {
            TestContext.WriteLine(MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(ZplSamples.GetSample1));
        });
    }

    [Test]
    public void ZplUtils_ToCodePoints_GetZplSample2()
    {
        Assert.DoesNotThrow(() =>
        {
            TestContext.WriteLine(MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(ZplSamples.GetSample2));
        });
    }

    [Test]
    public void ZplUtils_ToCodePoints_GetZplSample3()
    {
        Assert.DoesNotThrow(() =>
        {
            TestContext.WriteLine(MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(ZplSamples.GetSample3));
        });
    }

    [Test]
    public void ZplUtils_ToCodePoints_GetZplSampleFull()
    {
        Assert.DoesNotThrow(() =>
        {
            var zpl = MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(ZplSamples.GetSampleFull);
            //zpl = zpl.Replace("[EAC_107x109_090]", ZplSamples.GetEac);
            //zpl = zpl.Replace("[FISH_94x115_000]", ZplSamples.GetFish);
            //zpl = zpl.Replace("[TEMP6_116x113_090]", ZplSamples.GetTemp6);
            TestContext.WriteLine(zpl);
        });
    }
}
