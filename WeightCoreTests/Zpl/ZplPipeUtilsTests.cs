//namespace HardwareTests.Zpl
//{
//    [TestFixture]
//    internal class ZplPipeUtilsTests
//    {
//        [Test]
//        public void ZplPipeUtils_IsDigit_True()
//        {
//            Utils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                // Digits.
//                Assert.AreEqual(true, ZplPipeUtils.IsDigit('0'));
//                Assert.AreEqual(true, ZplPipeUtils.IsDigit('1'));
//                Assert.AreEqual(true, ZplPipeUtils.IsDigit('2'));
//                Assert.AreEqual(true, ZplPipeUtils.IsDigit('3'));
//                Assert.AreEqual(true, ZplPipeUtils.IsDigit('4'));
//                Assert.AreEqual(true, ZplPipeUtils.IsDigit('5'));
//                Assert.AreEqual(true, ZplPipeUtils.IsDigit('6'));
//                Assert.AreEqual(true, ZplPipeUtils.IsDigit('7'));
//                Assert.AreEqual(true, ZplPipeUtils.IsDigit('8'));
//                Assert.AreEqual(true, ZplPipeUtils.IsDigit('9'));
//            });

//            Utils.MethodComplete();
//        }

//        [Test]
//        public void ZplPipeUtils_IsSpecial_True()
//        {
//            Utils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                // ' ', ',', '.', '-', 
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial(' '));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial(','));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('.'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('-'));

//                // '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=',
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('~'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('!'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('#'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('$'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('%'));
//                Assert.AreEqual(false, ZplPipeUtils.IsSpecial('^'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('&'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('*'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial(')'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('_'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('+'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('='));

//                // '"', '№', ';', ':', '?', 
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('"'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('№'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial(';'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial(':'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('?'));

//                // '/', '|', '\\', '{', '}', '<', '>'
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('/'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('|'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('\\'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('{'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('}'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('<'));
//                Assert.AreEqual(true, ZplPipeUtils.IsSpecial('>'));
//            });

//            Utils.MethodComplete();
//        }

//        [Test]
//        public void ZplPipeUtils_IsCyrillic_False()
//        {
//            Utils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                // Digits.
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('0'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('1'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('2'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('3'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('4'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('5'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('6'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('7'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('8'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('9'));

//                // ' ', ',', '.', '-', 
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic(' '));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic(','));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('.'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('-'));

//                // '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=',
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('~'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('!'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('#'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('$'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('%'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('^'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('&'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('*'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic(')'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('_'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('+'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('='));

//                // '"', '№', ';', ':', '?', 
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('"'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('№'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic(';'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic(':'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('?'));

//                // '/', '|', '\\', '{', '}'
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('/'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('|'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('\\'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('{'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('}'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('<'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('>'));

//                // English lower letters.
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('q'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('w'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('e'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('r'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('t'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('y'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('u'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('i'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('o'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('p'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('a'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('s'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('d'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('f'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('g'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('h'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('j'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('k'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('l'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('z'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('x'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('c'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('v'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('b'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('n'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('m'));

//                // English upper letters.
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('Q'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('W'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('E'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('R'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('T'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('Y'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('U'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('I'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('O'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('P'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('A'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('S'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('D'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('F'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('G'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('H'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('J'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('K'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('L'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('Z'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('X'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('C'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('V'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('B'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('N'));
//                Assert.AreEqual(false, ZplPipeUtils.IsCyrillic('M'));
//            });

//            Utils.MethodComplete();
//        }

//        [Test]
//        public void ZplPipeUtils_IsCyrillic_True()
//        {
//            Utils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                // Russian lower letters.
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('й'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('ц'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('у'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('к'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('е'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('н'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('г'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('ш'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('щ'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('з'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('х'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('ъ'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('ф'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('ы'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('в'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('а'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('п'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('р'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('о'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('л'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('д'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('ж'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('э'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('я'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('ч'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('с'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('м'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('и'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('т'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('ь'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('б'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('ю'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('ё'));
                
//                // Russian upper letters.
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Й'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Ц'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('У'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('К'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Е'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Н'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Г'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Ш'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Щ'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('З'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Х'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Ъ'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Ф'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Ы'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('В'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('А'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('П'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Р'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('О'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Л'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Д'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Ж'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Э'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Я'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Ч'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('С'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('М'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('И'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Т'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Ь'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Б'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Ю'));
//                Assert.AreEqual(true, ZplPipeUtils.IsCyrillic('Ё'));
//            });

//            Utils.MethodComplete();
//        }

//        [Test]
//        public void ZplPipeUtils_ToCodePoints_GetZplSample1()
//        {
//            Utils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                TestContext.WriteLine(ZplPipeUtils.ToCodePoints(ZplSamples.GetSample1));
//            });

//            Utils.MethodComplete();
//        }

//        [Test]
//        public void ZplPipeUtils_ToCodePoints_GetZplSample2()
//        {
//            Utils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                TestContext.WriteLine(ZplPipeUtils.ToCodePoints(ZplSamples.GetSample2));
//            });

//            Utils.MethodComplete();
//        }

//        [Test]
//        public void ZplPipeUtils_ToCodePoints_GetZplSample3()
//        {
//            Utils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                TestContext.WriteLine(ZplPipeUtils.ToCodePoints(ZplSamples.GetSample3));
//            });

//            Utils.MethodComplete();
//        }

//        [Test]
//        public void ZplPipeUtils_ToCodePoints_GetZplSampleFull()
//        {
//            Utils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                var zpl = ZplPipeUtils.ToCodePoints(ZplSamples.GetSampleFull);
//                //zpl = zpl.Replace("[EAC_107x109_090]", ZplSamples.GetEac);
//                //zpl = zpl.Replace("[FISH_94x115_000]", ZplSamples.GetFish);
//                //zpl = zpl.Replace("[TEMP6_116x113_090]", ZplSamples.GetTemp6);
//                TestContext.WriteLine(zpl);
//            });

//            Utils.MethodComplete();
//        }
//    }
//}
