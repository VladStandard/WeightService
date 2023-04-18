// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.BarCodes;
using WsStorageCore.TableScaleModels.BarCodes;

namespace BlazorDeviceControl.Pages.Menu.Operations.SectionBarcodes;

public sealed partial class BarCodes : RazorComponentSectionBase<BarCodeModel>
{
	#region Public and private fields, properties, constructor

	public BarCodes() : base()
	{
        ButtonSettings = new(false, true, true, true, false, false, false);
    }

    #endregion

	#region Public and private methods

    #endregion
}
