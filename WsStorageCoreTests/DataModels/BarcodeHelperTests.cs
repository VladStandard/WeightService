// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.DataModels;

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
            Assert.That(Barcode.GetEanCheckDigit("460710023547"), Is.EqualTo(7));
            Assert.That(Barcode.GetEanCheckDigit("460710023586"), Is.EqualTo(6));
            Assert.That(Barcode.GetEanCheckDigit("460710023586"), Is.EqualTo(6));
            Assert.That(Barcode.GetEanCheckDigit("460710023587"), Is.EqualTo(3));
            Assert.That(Barcode.GetEanCheckDigit("460710023585"), Is.EqualTo(9));
            Assert.That(Barcode.GetEanCheckDigit("460710023486"), Is.EqualTo(9));
        });
    }

    [Test]
    public void BarcodeHelper_GetGtinCheckDigit_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            Assert.That(Barcode.GetGtinCheckDigitV1("4607100235477"), Is.EqualTo(6));
            Assert.That(Barcode.GetGtinCheckDigitV1("4607100235866"), Is.EqualTo(8));
            Assert.That(Barcode.GetGtinCheckDigitV1("4607100235866"), Is.EqualTo(8));
            Assert.That(Barcode.GetGtinCheckDigitV1("4607100235873"), Is.EqualTo(6));
            Assert.That(Barcode.GetGtinCheckDigitV1("4607100235859"), Is.EqualTo(0));
            Assert.That(Barcode.GetGtinCheckDigitV1("4607100234869"), Is.EqualTo(0));

            Assert.That(Barcode.GetGtinCheckDigitV2("4607100235477"), Is.EqualTo(6));
            Assert.That(Barcode.GetGtinCheckDigitV2("4607100235866"), Is.EqualTo(8));
            Assert.That(Barcode.GetGtinCheckDigitV2("4607100235866"), Is.EqualTo(8));
            Assert.That(Barcode.GetGtinCheckDigitV2("4607100235873"), Is.EqualTo(6));
            Assert.That(Barcode.GetGtinCheckDigitV2("4607100235859"), Is.EqualTo(0));
            Assert.That(Barcode.GetGtinCheckDigitV2("4607100234869"), Is.EqualTo(0));

            Assert.That(Barcode.GetGtinCheckDigitV3("4607100235477"), Is.EqualTo(6));
            Assert.That(Barcode.GetGtinCheckDigitV3("4607100235866"), Is.EqualTo(8));
            Assert.That(Barcode.GetGtinCheckDigitV3("4607100235866"), Is.EqualTo(8));
            Assert.That(Barcode.GetGtinCheckDigitV3("4607100235873"), Is.EqualTo(6));
            Assert.That(Barcode.GetGtinCheckDigitV3("4607100235859"), Is.EqualTo(0));
            Assert.That(Barcode.GetGtinCheckDigitV3("4607100234869"), Is.EqualTo(0));
        });
    }

    [Test]
    public void BarcodeHelper_GetGtin_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            Assert.That(Barcode.GetGtinWithCheckDigit("4607100235477", EnumGtinVariant.Var1), Is.EqualTo("46071002354776"));
            Assert.That(Barcode.GetGtinWithCheckDigit("4607100235866", EnumGtinVariant.Var1), Is.EqualTo("46071002358668"));
            Assert.That(Barcode.GetGtinWithCheckDigit("4607100235866", EnumGtinVariant.Var1), Is.EqualTo("46071002358668"));
            Assert.That(Barcode.GetGtinWithCheckDigit("4607100235873", EnumGtinVariant.Var1), Is.EqualTo("46071002358736"));
            Assert.That(Barcode.GetGtinWithCheckDigit("4607100235859", EnumGtinVariant.Var1), Is.EqualTo("46071002358590"));
            Assert.That(Barcode.GetGtinWithCheckDigit("4607100234869", EnumGtinVariant.Var1), Is.EqualTo("46071002348690"));

            Assert.That(Barcode.GetGtinWithCheckDigit("4607100235477", EnumGtinVariant.Var2), Is.EqualTo("46071002354776"));
            Assert.That(Barcode.GetGtinWithCheckDigit("4607100235866", EnumGtinVariant.Var2), Is.EqualTo("46071002358668"));
            Assert.That(Barcode.GetGtinWithCheckDigit("4607100235866", EnumGtinVariant.Var2), Is.EqualTo("46071002358668"));
            Assert.That(Barcode.GetGtinWithCheckDigit("4607100235873", EnumGtinVariant.Var2), Is.EqualTo("46071002358736"));
            Assert.That(Barcode.GetGtinWithCheckDigit("4607100235859", EnumGtinVariant.Var2), Is.EqualTo("46071002358590"));
            Assert.That(Barcode.GetGtinWithCheckDigit("4607100234869", EnumGtinVariant.Var2), Is.EqualTo("46071002348690"));

            Assert.That(Barcode.GetGtinWithCheckDigit("4607100235477"), Is.EqualTo("46071002354776"));
            Assert.That(Barcode.GetGtinWithCheckDigit("4607100235866"), Is.EqualTo("46071002358668"));
            Assert.That(Barcode.GetGtinWithCheckDigit("4607100235866"), Is.EqualTo("46071002358668"));
            Assert.That(Barcode.GetGtinWithCheckDigit("4607100235873"), Is.EqualTo("46071002358736"));
            Assert.That(Barcode.GetGtinWithCheckDigit("4607100235859"), Is.EqualTo("46071002358590"));
            Assert.That(Barcode.GetGtinWithCheckDigit("4607100234869"), Is.EqualTo("46071002348690"));
        });
    }
}
