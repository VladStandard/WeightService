// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.DataModels;
using System;

namespace DataCoreTests.Sql.DataModels;

[TestFixture]
internal class BarcodeHelperTests
{
    private BarcodeHelper Barcode { get; } = BarcodeHelper.Instance;

    [Test]
    public void BarcodeHelper_GetEanCheckDigit_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            Barcode.GetEanCheckDigit("4607100235477");
            Barcode.GetEanCheckDigit("4607100235866");
            Barcode.GetEanCheckDigit("4607100235866");
            Barcode.GetEanCheckDigit("4607100235873");
            Barcode.GetEanCheckDigit("4607100235859");
            Barcode.GetEanCheckDigit("4607100234869");
        });
    }

    [Test]
    public void SubstituteBarcode_GetEanCheckDigit_Throws()
    {
        IBarcodeHelper barcode = Substitute.For<IBarcodeHelper>();
        barcode.GetEanCheckDigit("01234546789012").Returns(1);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            Barcode.GetEanCheckDigit("01234546789012");
        });
    }

    [Test]
    public void BarcodeHelper_GetGtinCheckDigitV1_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            Barcode.GetGtinCheckDigitV1("460710023547");
            Barcode.GetGtinCheckDigitV1("460710023586");
            Barcode.GetGtinCheckDigitV1("460710023586");
            Barcode.GetGtinCheckDigitV1("460710023587");
            Barcode.GetGtinCheckDigitV1("460710023585");
            Barcode.GetGtinCheckDigitV1("460710023486");

            Barcode.GetGtinCheckDigitV2("460710023547");
            Barcode.GetGtinCheckDigitV2("460710023586");
            Barcode.GetGtinCheckDigitV2("460710023586");
            Barcode.GetGtinCheckDigitV2("460710023587");
            Barcode.GetGtinCheckDigitV2("460710023585");
            Barcode.GetGtinCheckDigitV2("460710023486");

            Barcode.GetGtinCheckDigitV3("460710023547");
            Barcode.GetGtinCheckDigitV3("460710023586");
            Barcode.GetGtinCheckDigitV3("460710023586");
            Barcode.GetGtinCheckDigitV3("460710023587");
            Barcode.GetGtinCheckDigitV3("460710023585");
            Barcode.GetGtinCheckDigitV3("460710023486");
        });
    }

    [Test]
    public void BarcodeHelper_GetEanCheckDigit_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            Assert.AreEqual(7, Barcode.GetEanCheckDigit("460710023547"));
            Assert.AreEqual(6, Barcode.GetEanCheckDigit("460710023586"));
            Assert.AreEqual(6, Barcode.GetEanCheckDigit("460710023586"));
            Assert.AreEqual(3, Barcode.GetEanCheckDigit("460710023587"));
            Assert.AreEqual(9, Barcode.GetEanCheckDigit("460710023585"));
            Assert.AreEqual(9, Barcode.GetEanCheckDigit("460710023486"));
        });
    }

    [Test]
    public void BarcodeHelper_GetGtinCheckDigit_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            Assert.AreEqual(6, Barcode.GetGtinCheckDigitV1("4607100235477"));
            Assert.AreEqual(8, Barcode.GetGtinCheckDigitV1("4607100235866"));
            Assert.AreEqual(8, Barcode.GetGtinCheckDigitV1("4607100235866"));
            Assert.AreEqual(6, Barcode.GetGtinCheckDigitV1("4607100235873"));
            Assert.AreEqual(0, Barcode.GetGtinCheckDigitV1("4607100235859"));
            Assert.AreEqual(0, Barcode.GetGtinCheckDigitV1("4607100234869"));

            Assert.AreEqual(6, Barcode.GetGtinCheckDigitV2("4607100235477"));
            Assert.AreEqual(8, Barcode.GetGtinCheckDigitV2("4607100235866"));
            Assert.AreEqual(8, Barcode.GetGtinCheckDigitV2("4607100235866"));
            Assert.AreEqual(6, Barcode.GetGtinCheckDigitV2("4607100235873"));
            Assert.AreEqual(0, Barcode.GetGtinCheckDigitV2("4607100235859"));
            Assert.AreEqual(0, Barcode.GetGtinCheckDigitV2("4607100234869"));

            Assert.AreEqual(6, Barcode.GetGtinCheckDigitV3("4607100235477"));
            Assert.AreEqual(8, Barcode.GetGtinCheckDigitV3("4607100235866"));
            Assert.AreEqual(8, Barcode.GetGtinCheckDigitV3("4607100235866"));
            Assert.AreEqual(6, Barcode.GetGtinCheckDigitV3("4607100235873"));
            Assert.AreEqual(0, Barcode.GetGtinCheckDigitV3("4607100235859"));
            Assert.AreEqual(0, Barcode.GetGtinCheckDigitV3("4607100234869"));
        });
    }

    [Test]
    public void BarcodeHelper_GetGtin_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            Assert.AreEqual("46071002354776", Barcode.GetGtinWithCheckDigit("4607100235477", EnumGtinVariant.Var1));
            Assert.AreEqual("46071002358668", Barcode.GetGtinWithCheckDigit("4607100235866", EnumGtinVariant.Var1));
            Assert.AreEqual("46071002358668", Barcode.GetGtinWithCheckDigit("4607100235866", EnumGtinVariant.Var1));
            Assert.AreEqual("46071002358736", Barcode.GetGtinWithCheckDigit("4607100235873", EnumGtinVariant.Var1));
            Assert.AreEqual("46071002358590", Barcode.GetGtinWithCheckDigit("4607100235859", EnumGtinVariant.Var1));
            Assert.AreEqual("46071002348690", Barcode.GetGtinWithCheckDigit("4607100234869", EnumGtinVariant.Var1));

            Assert.AreEqual("46071002354776", Barcode.GetGtinWithCheckDigit("4607100235477", EnumGtinVariant.Var2));
            Assert.AreEqual("46071002358668", Barcode.GetGtinWithCheckDigit("4607100235866", EnumGtinVariant.Var2));
            Assert.AreEqual("46071002358668", Barcode.GetGtinWithCheckDigit("4607100235866", EnumGtinVariant.Var2));
            Assert.AreEqual("46071002358736", Barcode.GetGtinWithCheckDigit("4607100235873", EnumGtinVariant.Var2));
            Assert.AreEqual("46071002358590", Barcode.GetGtinWithCheckDigit("4607100235859", EnumGtinVariant.Var2));
            Assert.AreEqual("46071002348690", Barcode.GetGtinWithCheckDigit("4607100234869", EnumGtinVariant.Var2));

            Assert.AreEqual("46071002354776", Barcode.GetGtinWithCheckDigit("4607100235477"));
            Assert.AreEqual("46071002358668", Barcode.GetGtinWithCheckDigit("4607100235866"));
            Assert.AreEqual("46071002358668", Barcode.GetGtinWithCheckDigit("4607100235866"));
            Assert.AreEqual("46071002358736", Barcode.GetGtinWithCheckDigit("4607100235873"));
            Assert.AreEqual("46071002358590", Barcode.GetGtinWithCheckDigit("4607100235859"));
            Assert.AreEqual("46071002348690", Barcode.GetGtinWithCheckDigit("4607100234869"));
        });
    }
}
