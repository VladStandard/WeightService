// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetBarcode;
using System.Drawing.Imaging;

namespace BlazorDeviceControl.Shared.Component;

public partial class NetBarcodePage
{
    #region Public and private fields, properties, constructor

    private Barcode Barcode { get; set; }
    private BarcodeType BarcodeType { get; set; }
    private ImageFormat ImageFormat { get; set; }
    private List<BarcodeType> BarcodeTypes { get; set; }
    private List<ImageFormat> ImageFormats { get; set; }
    private string BarcodeValue { get; set; }
    private string BarcodeImage { get; set; }
    private Exception? Exception { get; set; }

#endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Barcode = new();
        BarcodeTypes = new();
        foreach (BarcodeType value in Enum.GetValues(typeof(BarcodeType)))
	        BarcodeTypes.Add(value);
        BarcodeType = BarcodeType.Code128;
        ImageFormats = new()
        {
	        ImageFormat.Bmp,
	        ImageFormat.Emf,
	        ImageFormat.Exif,
	        ImageFormat.Gif,
	        ImageFormat.Icon,
	        ImageFormat.Jpeg,
	        ImageFormat.MemoryBmp,
	        ImageFormat.Png,
	        ImageFormat.Tiff,
	        ImageFormat.Wmf
        };
        ImageFormat = ImageFormat.Jpeg;
        BarcodeValue = string.Empty;
        BarcodeImage = string.Empty;
	}

    protected override void OnParametersSet()
	{
		base.OnParametersSet();
		SetParametersWithAction(new()
		{
			() =>
			{
				//
			}
		});
	}

	private void RedrawImage()
    {
        try
        {
            Exception = null;
            BarcodeImage = string.Empty;

            Barcode.Configure(new BarcodeSettings { BarcodeType = BarcodeType });
            byte[]? barcodeByteArray = Barcode.GetByteArray(BarcodeValue, ImageFormat);
            BarcodeImage = barcodeByteArray == null
                ? string.Empty
                : "data:image/png;base64, " + Convert.ToBase64String(barcodeByteArray);
        }
        catch (Exception ex)
        {
            Exception = ex;
        }
        finally
        {
            StateHasChanged();
        }
    }

    #endregion

}
