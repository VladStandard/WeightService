// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;
using WeightCore.Zpl;

namespace WeightCoreTests.Zpl
{
    [TestFixture]
    internal class ZplUtilsTests
    {
        [Test]
        public void ZplUtils_IsDigit_True()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                // Digits.
                Assert.AreEqual(true, ZplUtils.IsDigit('0'));
                Assert.AreEqual(true, ZplUtils.IsDigit('1'));
                Assert.AreEqual(true, ZplUtils.IsDigit('2'));
                Assert.AreEqual(true, ZplUtils.IsDigit('3'));
                Assert.AreEqual(true, ZplUtils.IsDigit('4'));
                Assert.AreEqual(true, ZplUtils.IsDigit('5'));
                Assert.AreEqual(true, ZplUtils.IsDigit('6'));
                Assert.AreEqual(true, ZplUtils.IsDigit('7'));
                Assert.AreEqual(true, ZplUtils.IsDigit('8'));
                Assert.AreEqual(true, ZplUtils.IsDigit('9'));
            });

            Utils.MethodComplete();
        }

        [Test]
        public void ZplUtils_IsSpecial_True()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                // ' ', ',', '.', '-', 
                Assert.AreEqual(true, ZplUtils.IsSpecial(' '));
                Assert.AreEqual(true, ZplUtils.IsSpecial(','));
                Assert.AreEqual(true, ZplUtils.IsSpecial('.'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('-'));

                // '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=',
                Assert.AreEqual(true, ZplUtils.IsSpecial('~'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('!'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('#'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('$'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('%'));
                Assert.AreEqual(false, ZplUtils.IsSpecial('^'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('&'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('*'));
                Assert.AreEqual(true, ZplUtils.IsSpecial(')'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('_'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('+'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('='));

                // '"', '№', ';', ':', '?', 
                Assert.AreEqual(true, ZplUtils.IsSpecial('"'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('№'));
                Assert.AreEqual(true, ZplUtils.IsSpecial(';'));
                Assert.AreEqual(true, ZplUtils.IsSpecial(':'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('?'));

                // '/', '|', '\\', '{', '}', '<', '>'
                Assert.AreEqual(true, ZplUtils.IsSpecial('/'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('|'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('\\'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('{'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('}'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('<'));
                Assert.AreEqual(true, ZplUtils.IsSpecial('>'));
            });

            Utils.MethodComplete();
        }

        [Test]
        public void ZplUtils_IsCyrillic_False()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                // Digits.
                Assert.AreEqual(false, ZplUtils.IsCyrillic('0'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('1'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('2'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('3'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('4'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('5'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('6'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('7'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('8'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('9'));

                // ' ', ',', '.', '-', 
                Assert.AreEqual(false, ZplUtils.IsCyrillic(' '));
                Assert.AreEqual(false, ZplUtils.IsCyrillic(','));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('.'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('-'));

                // '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=',
                Assert.AreEqual(false, ZplUtils.IsCyrillic('~'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('!'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('#'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('$'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('%'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('^'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('&'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('*'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic(')'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('_'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('+'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('='));

                // '"', '№', ';', ':', '?', 
                Assert.AreEqual(false, ZplUtils.IsCyrillic('"'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('№'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic(';'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic(':'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('?'));

                // '/', '|', '\\', '{', '}'
                Assert.AreEqual(false, ZplUtils.IsCyrillic('/'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('|'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('\\'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('{'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('}'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('<'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('>'));

                // English lower letters.
                Assert.AreEqual(false, ZplUtils.IsCyrillic('q'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('w'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('e'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('r'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('t'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('y'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('u'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('i'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('o'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('p'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('a'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('s'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('d'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('f'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('g'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('h'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('j'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('k'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('l'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('z'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('x'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('c'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('v'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('b'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('n'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('m'));

                // English upper letters.
                Assert.AreEqual(false, ZplUtils.IsCyrillic('Q'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('W'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('E'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('R'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('T'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('Y'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('U'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('I'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('O'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('P'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('A'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('S'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('D'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('F'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('G'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('H'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('J'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('K'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('L'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('Z'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('X'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('C'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('V'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('B'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('N'));
                Assert.AreEqual(false, ZplUtils.IsCyrillic('M'));
            });

            Utils.MethodComplete();
        }

        [Test]
        public void ZplUtils_IsCyrillic_True()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                // Russian lower letters.
                Assert.AreEqual(true, ZplUtils.IsCyrillic('й'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('ц'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('у'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('к'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('е'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('н'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('г'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('ш'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('щ'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('з'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('х'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('ъ'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('ф'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('ы'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('в'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('а'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('п'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('р'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('о'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('л'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('д'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('ж'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('э'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('я'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('ч'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('с'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('м'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('и'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('т'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('ь'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('б'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('ю'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('ё'));

                // Russian upper letters.
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Й'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Ц'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('У'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('К'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Е'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Н'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Г'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Ш'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Щ'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('З'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Х'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Ъ'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Ф'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Ы'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('В'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('А'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('П'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Р'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('О'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Л'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Д'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Ж'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Э'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Я'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Ч'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('С'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('М'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('И'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Т'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Ь'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Б'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Ю'));
                Assert.AreEqual(true, ZplUtils.IsCyrillic('Ё'));
            });

            Utils.MethodComplete();
        }

        [Test]
        public void ZplUtils_ToCodePoints_GetZplSample1()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                TestContext.WriteLine(MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(ZplSamples.GetSample1));
            });

            Utils.MethodComplete();
        }

        [Test]
        public void ZplUtils_ToCodePoints_GetZplSample2()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                TestContext.WriteLine(MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(ZplSamples.GetSample2));
            });

            Utils.MethodComplete();
        }

        [Test]
        public void ZplUtils_ToCodePoints_GetZplSample3()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                TestContext.WriteLine(MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(ZplSamples.GetSample3));
            });

            Utils.MethodComplete();
        }

        [Test]
        public void ZplUtils_ToCodePoints_GetZplSampleFull()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var zpl = MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(ZplSamples.GetSampleFull);
                //zpl = zpl.Replace("[EAC_107x109_090]", ZplSamples.GetEac);
                //zpl = zpl.Replace("[FISH_94x115_000]", ZplSamples.GetFish);
                //zpl = zpl.Replace("[TEMP6_116x113_090]", ZplSamples.GetTemp6);
                TestContext.WriteLine(zpl);
            });

            Utils.MethodComplete();
        }
    }
}
