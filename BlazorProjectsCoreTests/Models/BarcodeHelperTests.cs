using NUnit.Framework;
using System;

namespace BlazorCoreTests.Models
{
    [TestFixture]
    internal class BarcodeHelperTests
    {
        private readonly BarcodeHelper _barcode = BarcodeHelper.Instance;

        [Test]
        public void BarcodeHelper_GetEanCheckDigit_Throws()
        {
            TestsUtils.MethodStart();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _barcode.GetEanCheckDigit("4607100235477");
                _barcode.GetEanCheckDigit("4607100235866");
                _barcode.GetEanCheckDigit("4607100235866");
                _barcode.GetEanCheckDigit("4607100235873");
                _barcode.GetEanCheckDigit("4607100235859");
                _barcode.GetEanCheckDigit("4607100234869");
            });

            TestsUtils.MethodComplete();
        }
        
        [Test]
        public void BarcodeHelper_GetGtinCheckDigitV1_Throws()
        {
            TestsUtils.MethodStart();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _barcode.GetGtinCheckDigitV1("460710023547");
                _barcode.GetGtinCheckDigitV1("460710023586");
                _barcode.GetGtinCheckDigitV1("460710023586");
                _barcode.GetGtinCheckDigitV1("460710023587");
                _barcode.GetGtinCheckDigitV1("460710023585");
                _barcode.GetGtinCheckDigitV1("460710023486");
                
                _barcode.GetGtinCheckDigitV2("460710023547");
                _barcode.GetGtinCheckDigitV2("460710023586");
                _barcode.GetGtinCheckDigitV2("460710023586");
                _barcode.GetGtinCheckDigitV2("460710023587");
                _barcode.GetGtinCheckDigitV2("460710023585");
                _barcode.GetGtinCheckDigitV2("460710023486");
                
                _barcode.GetGtinCheckDigitV3("460710023547");
                _barcode.GetGtinCheckDigitV3("460710023586");
                _barcode.GetGtinCheckDigitV3("460710023586");
                _barcode.GetGtinCheckDigitV3("460710023587");
                _barcode.GetGtinCheckDigitV3("460710023585");
                _barcode.GetGtinCheckDigitV3("460710023486");
            });

            TestsUtils.MethodComplete();
        }
        
        [Test]
        public void BarcodeHelper_GetEanCheckDigit_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                Assert.AreEqual(7, _barcode.GetEanCheckDigit("460710023547"));
                Assert.AreEqual(6, _barcode.GetEanCheckDigit("460710023586"));
                Assert.AreEqual(6, _barcode.GetEanCheckDigit("460710023586"));
                Assert.AreEqual(3, _barcode.GetEanCheckDigit("460710023587"));
                Assert.AreEqual(9, _barcode.GetEanCheckDigit("460710023585"));
                Assert.AreEqual(9, _barcode.GetEanCheckDigit("460710023486"));
            });

            TestsUtils.MethodComplete();
        }
        
        [Test]
        public void BarcodeHelper_GetGtinCheckDigit_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                Assert.AreEqual(6, _barcode.GetGtinCheckDigitV1("4607100235477"));
                Assert.AreEqual(8, _barcode.GetGtinCheckDigitV1("4607100235866"));
                Assert.AreEqual(8, _barcode.GetGtinCheckDigitV1("4607100235866"));
                Assert.AreEqual(6, _barcode.GetGtinCheckDigitV1("4607100235873"));
                Assert.AreEqual(0, _barcode.GetGtinCheckDigitV1("4607100235859"));
                Assert.AreEqual(0, _barcode.GetGtinCheckDigitV1("4607100234869"));

                Assert.AreEqual(6, _barcode.GetGtinCheckDigitV2("4607100235477"));
                Assert.AreEqual(8, _barcode.GetGtinCheckDigitV2("4607100235866"));
                Assert.AreEqual(8, _barcode.GetGtinCheckDigitV2("4607100235866"));
                Assert.AreEqual(6, _barcode.GetGtinCheckDigitV2("4607100235873"));
                Assert.AreEqual(0, _barcode.GetGtinCheckDigitV2("4607100235859"));
                Assert.AreEqual(0, _barcode.GetGtinCheckDigitV2("4607100234869"));

                Assert.AreEqual(6, _barcode.GetGtinCheckDigitV3("4607100235477"));
                Assert.AreEqual(8, _barcode.GetGtinCheckDigitV3("4607100235866"));
                Assert.AreEqual(8, _barcode.GetGtinCheckDigitV3("4607100235866"));
                Assert.AreEqual(6, _barcode.GetGtinCheckDigitV3("4607100235873"));
                Assert.AreEqual(0, _barcode.GetGtinCheckDigitV3("4607100235859"));
                Assert.AreEqual(0, _barcode.GetGtinCheckDigitV3("4607100234869"));
            });

            TestsUtils.MethodComplete();
        }
        
        [Test]
        public void BarcodeHelper_GetGtin_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                Assert.AreEqual("46071002354776", _barcode.GetGtin("4607100235477", EnumGtinVariant.Var1));
                Assert.AreEqual("46071002358668", _barcode.GetGtin("4607100235866", EnumGtinVariant.Var1));
                Assert.AreEqual("46071002358668", _barcode.GetGtin("4607100235866", EnumGtinVariant.Var1));
                Assert.AreEqual("46071002358736", _barcode.GetGtin("4607100235873", EnumGtinVariant.Var1));
                Assert.AreEqual("46071002358590", _barcode.GetGtin("4607100235859", EnumGtinVariant.Var1));
                Assert.AreEqual("46071002348690", _barcode.GetGtin("4607100234869", EnumGtinVariant.Var1));

                Assert.AreEqual("46071002354776", _barcode.GetGtin("4607100235477", EnumGtinVariant.Var2));
                Assert.AreEqual("46071002358668", _barcode.GetGtin("4607100235866", EnumGtinVariant.Var2));
                Assert.AreEqual("46071002358668", _barcode.GetGtin("4607100235866", EnumGtinVariant.Var2));
                Assert.AreEqual("46071002358736", _barcode.GetGtin("4607100235873", EnumGtinVariant.Var2));
                Assert.AreEqual("46071002358590", _barcode.GetGtin("4607100235859", EnumGtinVariant.Var2));
                Assert.AreEqual("46071002348690", _barcode.GetGtin("4607100234869", EnumGtinVariant.Var2));

                Assert.AreEqual("46071002354776", _barcode.GetGtin("4607100235477"));
                Assert.AreEqual("46071002358668", _barcode.GetGtin("4607100235866"));
                Assert.AreEqual("46071002358668", _barcode.GetGtin("4607100235866"));
                Assert.AreEqual("46071002358736", _barcode.GetGtin("4607100235873"));
                Assert.AreEqual("46071002358590", _barcode.GetGtin("4607100235859"));
                Assert.AreEqual("46071002348690", _barcode.GetGtin("4607100234869"));
            });

            TestsUtils.MethodComplete();
        }
    }
}
