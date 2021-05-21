using Hardware.Zpl;
using NUnit.Framework;
using System.Linq;

namespace HardwareTests.Zpl
{
    [TestFixture]
    internal class ZplPipeClassTests
    {
        [Test]
        public void ZplPipeClass_IsDigit_True()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                // Digits.
                Assert.AreEqual(true, ZplPipeClass.IsDigit('0'));
                Assert.AreEqual(true, ZplPipeClass.IsDigit('1'));
                Assert.AreEqual(true, ZplPipeClass.IsDigit('2'));
                Assert.AreEqual(true, ZplPipeClass.IsDigit('3'));
                Assert.AreEqual(true, ZplPipeClass.IsDigit('4'));
                Assert.AreEqual(true, ZplPipeClass.IsDigit('5'));
                Assert.AreEqual(true, ZplPipeClass.IsDigit('6'));
                Assert.AreEqual(true, ZplPipeClass.IsDigit('7'));
                Assert.AreEqual(true, ZplPipeClass.IsDigit('8'));
                Assert.AreEqual(true, ZplPipeClass.IsDigit('9'));
            });

            Utils.MethodComplete();
        }

        [Test]
        public void ZplPipeClass_IsSpecial_True()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                // ' ', ',', '.', '-', 
                Assert.AreEqual(true, ZplPipeClass.IsSpecial(' '));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial(','));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('.'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('-'));

                // '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=',
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('~'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('!'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('#'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('$'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('%'));
                Assert.AreEqual(false, ZplPipeClass.IsSpecial('^'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('&'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('*'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial(')'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('_'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('+'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('='));

                // '"', '№', ';', ':', '?', 
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('"'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('№'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial(';'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial(':'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('?'));

                // '/', '|', '\\', '{', '}', '<', '>'
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('/'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('|'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('\\'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('{'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('}'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('<'));
                Assert.AreEqual(true, ZplPipeClass.IsSpecial('>'));
            });

            Utils.MethodComplete();
        }

        [Test]
        public void ZplPipeClass_IsCyrillic_False()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                // Digits.
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('0'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('1'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('2'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('3'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('4'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('5'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('6'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('7'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('8'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('9'));

                // ' ', ',', '.', '-', 
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic(' '));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic(','));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('.'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('-'));

                // '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=',
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('~'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('!'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('#'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('$'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('%'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('^'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('&'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('*'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic(')'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('_'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('+'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('='));

                // '"', '№', ';', ':', '?', 
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('"'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('№'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic(';'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic(':'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('?'));

                // '/', '|', '\\', '{', '}'
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('/'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('|'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('\\'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('{'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('}'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('<'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('>'));

                // English lower letters.
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('q'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('w'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('e'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('r'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('t'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('y'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('u'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('i'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('o'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('p'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('a'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('s'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('d'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('f'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('g'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('h'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('j'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('k'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('l'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('z'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('x'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('c'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('v'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('b'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('n'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('m'));

                // English upper letters.
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('Q'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('W'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('E'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('R'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('T'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('Y'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('U'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('I'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('O'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('P'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('A'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('S'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('D'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('F'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('G'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('H'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('J'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('K'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('L'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('Z'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('X'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('C'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('V'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('B'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('N'));
                Assert.AreEqual(false, ZplPipeClass.IsCyrillic('M'));
            });

            Utils.MethodComplete();
        }

        [Test]
        public void ZplPipeClass_IsCyrillic_True()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                // Russian lower letters.
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('й'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('ц'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('у'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('к'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('е'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('н'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('г'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('ш'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('щ'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('з'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('х'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('ъ'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('ф'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('ы'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('в'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('а'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('п'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('р'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('о'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('л'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('д'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('ж'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('э'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('я'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('ч'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('с'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('м'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('и'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('т'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('ь'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('б'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('ю'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('ё'));
                
                // Russian upper letters.
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Й'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Ц'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('У'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('К'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Е'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Н'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Г'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Ш'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Щ'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('З'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Х'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Ъ'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Ф'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Ы'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('В'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('А'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('П'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Р'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('О'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Л'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Д'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Ж'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Э'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Я'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Ч'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('С'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('М'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('И'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Т'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Ь'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Б'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Ю'));
                Assert.AreEqual(true, ZplPipeClass.IsCyrillic('Ё'));
            });

            Utils.MethodComplete();
        }

        private string GetZplSampleFull => @"
^XA
^CI28
^CWK,E:COURB.TTF
^CWL,E:COURBI.TTF
^CWM,E:COURBD.TTF
^CWN,E:COUR.TTF
^CWZ,E:ARIAL.TTF
^CWW,E:ARIALBI.TTF
^CWE,E:ARIALBD.TTF
^CWR,E:ARIALI.TTF

^LH0,10
^FWR

^LL1180
^PW944
^FO820,50
^CFZ,24,20
^FB1100,4,0,C,0
^FH^FDИзготовитель: ООО ""Владимирский стандарт"" Россия, 600910 Владимирская обл. г.Радужный квартал 13/13 дом 20^FS
^FO510,50
^CFE,44,34
^FB910,4,0,J,0
^FH^FDКолбасные изделия вареные куриные 1 сорта, Колбаса вареная ""Докторская стандарт"" охлажденная ТУ 10,13,14-005-91005552-2016, ц/ф (500г)^FS
^FO350,50
^CFZ,36,20
^FB800,4,0,J,0
^FH^FDСрок годности: 30 суток при температуре от 0°С до +6°С и относительной влажности воздуха 75%-78%. Упаковано под вакуумом,^FS
^FO320,50
^CFZ,25,20
^FB170,1,0,L,0
^FH^FDДата изгот.: ^FS
^FO270,50
^CFK,56,40
^FB300,1,0,L,0
^FH^FD21.05.2021^FS
^FO320,360
^CFZ,25,20
^FB170,1,0,L,0
^FH^FDГоден до: ^FS
^FO270,360
^CFK,56,40
^FB300,1,0,L,0
^FH^FD20.06.2021^FS
^FO320,720
^CFZ,25,20
^FB100,1,0,L,0
^FH^FDКол-во:^FS
^FO270,720
^CFK,56,40
^FB150,1,0,L,0
^FH^FD15^FS
^FO270,800
^CFM,42,38
^FB100,1,0,L,0
^FH^FDШТ^FS
^FO200,50
^CFZ,25,20
^FB200,1,0,L,0
^FH^FDЗамес: 1^FS
^FO200,200
^CFZ,25,20
^FB450,1,0,L,0
^FH^FDЦех/Линия: Весы разработчика^FS

^BY2
^FO200,1000
^BCN,120,Y,N,N
^FD2990000400000413^FS

^BY3
^FO740,20
^B2R,120,Y,N,Y,Y
^FD298000040000041321052116332400100000001^FS

^BY3
^FO50,50
^BCR,120,Y,N,Y,D
^FD(01)4607100234500(37)15>8(11)210521(10)0521>8^FS


^FO200,888
^FS

^FO315,888
^FS

^FO435,888
^FS


^XZ
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');

        private string GetZplSample1 => @"
^FO820,50
^CFZ,24,20
^FB1100,4,0,C,0
^FH^FDИзготовитель: ООО ""Владимирский стандарт"" Россия, 600910 Владимирская обл. г.Радужный квартал 13/13 дом 20^FS
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');

        private string GetZplSample2 => @"
^FO510,50
^CFE,44,34
^FB910,4,0,J,0
^FH^FDКолбасные изделия вареные куриные 1 сорта, Колбаса вареная ""Докторская стандарт"" охлажденная ТУ 10,13,14-005-91005552-2016, ц/ф (500г)^FS
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');

        private string GetZplSample3 => @"
^FO350,50
^CFZ,36,20
^FB800,4,0,J,0
^FH^FDСрок годности: 30 суток при температуре от 0°С до +6°С и относительной влажности воздуха 75%-78%. Упаковано под вакуумом,^FS
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');

        [Test]
        public void ZplPipeClass_ToCodePoints_GetZplSample1()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                TestContext.WriteLine(ZplPipeClass.ToCodePoints(GetZplSample1));
            });

            Utils.MethodComplete();
        }

        [Test]
        public void ZplPipeClass_ToCodePoints_GetZplSample2()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                TestContext.WriteLine(ZplPipeClass.ToCodePoints(GetZplSample2));
            });

            Utils.MethodComplete();
        }

        [Test]
        public void ZplPipeClass_ToCodePoints_GetZplSample3()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                TestContext.WriteLine(ZplPipeClass.ToCodePoints(GetZplSample3));
            });

            Utils.MethodComplete();
        }

        [Test]
        public void ZplPipeClass_ToCodePoints_GetZplSampleFull()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                TestContext.WriteLine(ZplPipeClass.ToCodePoints(GetZplSampleFull));
            });

            Utils.MethodComplete();
        }
    }
}
