// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Utils;
using NUnit.Framework;
using WeightCore.Zpl;

namespace WeightCoreTests.Zpl;

[TestFixture]
public class ZplUtilsTests
{
    [Test]
    public void ZplUtils_IsDigit_True()
    {
        Assert.DoesNotThrow(() =>
        {
            // Digits.
            Assert.AreEqual(true, XmlUtils.IsDigit('0'));
            Assert.AreEqual(true, XmlUtils.IsDigit('1'));
            Assert.AreEqual(true, XmlUtils.IsDigit('2'));
            Assert.AreEqual(true, XmlUtils.IsDigit('3'));
            Assert.AreEqual(true, XmlUtils.IsDigit('4'));
            Assert.AreEqual(true, XmlUtils.IsDigit('5'));
            Assert.AreEqual(true, XmlUtils.IsDigit('6'));
            Assert.AreEqual(true, XmlUtils.IsDigit('7'));
            Assert.AreEqual(true, XmlUtils.IsDigit('8'));
            Assert.AreEqual(true, XmlUtils.IsDigit('9'));
        });
    }

    [Test]
    public void ZplUtils_IsSpecial_True()
    {
        Assert.DoesNotThrow(() =>
        {
            // ' ', ',', '.', '-', 
            Assert.AreEqual(true, XmlUtils.IsSpecial(' '));
            Assert.AreEqual(true, XmlUtils.IsSpecial(','));
            Assert.AreEqual(true, XmlUtils.IsSpecial('.'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('-'));

            // '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=',
            Assert.AreEqual(true, XmlUtils.IsSpecial('~'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('!'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('#'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('$'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('%'));
            Assert.AreEqual(false, XmlUtils.IsSpecial('^'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('&'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('*'));
            Assert.AreEqual(true, XmlUtils.IsSpecial(')'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('_'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('+'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('='));

            // '"', '№', ';', ':', '?', 
            Assert.AreEqual(true, XmlUtils.IsSpecial('"'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('№'));
            Assert.AreEqual(true, XmlUtils.IsSpecial(';'));
            Assert.AreEqual(true, XmlUtils.IsSpecial(':'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('?'));

            // '/', '|', '\\', '{', '}', '<', '>'
            Assert.AreEqual(true, XmlUtils.IsSpecial('/'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('|'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('\\'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('{'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('}'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('<'));
            Assert.AreEqual(true, XmlUtils.IsSpecial('>'));
        });
    }

    [Test]
    public void ZplUtils_IsCyrillic_False()
    {
        Assert.DoesNotThrow(() =>
        {
            // Digits.
            Assert.AreEqual(false, XmlUtils.IsCyrillic('0'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('1'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('2'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('3'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('4'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('5'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('6'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('7'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('8'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('9'));

            // ' ', ',', '.', '-', 
            Assert.AreEqual(false, XmlUtils.IsCyrillic(' '));
            Assert.AreEqual(false, XmlUtils.IsCyrillic(','));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('.'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('-'));

            // '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=',
            Assert.AreEqual(false, XmlUtils.IsCyrillic('~'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('!'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('#'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('$'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('%'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('^'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('&'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('*'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic(')'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('_'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('+'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('='));

            // '"', '№', ';', ':', '?', 
            Assert.AreEqual(false, XmlUtils.IsCyrillic('"'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('№'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic(';'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic(':'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('?'));

            // '/', '|', '\\', '{', '}'
            Assert.AreEqual(false, XmlUtils.IsCyrillic('/'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('|'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('\\'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('{'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('}'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('<'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('>'));

            // English lower letters.
            Assert.AreEqual(false, XmlUtils.IsCyrillic('q'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('w'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('e'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('r'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('t'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('y'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('u'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('i'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('o'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('p'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('a'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('s'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('d'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('f'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('g'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('h'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('j'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('k'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('l'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('z'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('x'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('c'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('v'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('b'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('n'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('m'));

            // English upper letters.
            Assert.AreEqual(false, XmlUtils.IsCyrillic('Q'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('W'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('E'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('R'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('T'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('Y'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('U'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('I'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('O'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('P'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('A'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('S'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('D'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('F'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('G'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('H'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('J'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('K'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('L'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('Z'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('X'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('C'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('V'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('B'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('N'));
            Assert.AreEqual(false, XmlUtils.IsCyrillic('M'));
        });
    }

    [Test]
    public void ZplUtils_IsCyrillic_True()
    {
        Assert.DoesNotThrow(() =>
        {
            // Russian lower letters.
            Assert.AreEqual(true, XmlUtils.IsCyrillic('й'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('ц'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('у'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('к'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('е'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('н'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('г'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('ш'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('щ'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('з'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('х'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('ъ'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('ф'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('ы'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('в'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('а'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('п'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('р'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('о'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('л'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('д'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('ж'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('э'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('я'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('ч'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('с'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('м'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('и'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('т'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('ь'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('б'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('ю'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('ё'));

            // Russian upper letters.
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Й'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Ц'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('У'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('К'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Е'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Н'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Г'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Ш'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Щ'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('З'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Х'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Ъ'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Ф'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Ы'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('В'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('А'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('П'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Р'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('О'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Л'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Д'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Ж'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Э'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Я'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Ч'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('С'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('М'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('И'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Т'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Ь'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Б'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Ю'));
            Assert.AreEqual(true, XmlUtils.IsCyrillic('Ё'));
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
