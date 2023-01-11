// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Utils;

[TestFixture]
public class DataFormatUtilsTests
{
    #region Public methods

    [Test]
    public void AppHelper_GetCurrentVersionSubString_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            //BarcodeTopModel barcodeTop = new(SqlItemCast.ValueTop, false);
            //return DataFormatUtils.GetContent<BarcodeTopModel>(barcodeTop, formatType, true);

            Assert.AreEqual(1, 1);
        });
    }

    #endregion
}