// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Brands;

namespace DeviceControl.Pages.Menu.References1C.SectionBrands;

public sealed partial class Brands : RazorComponentSectionBase<WsSqlBrandModel>
{
    #region Public and private fields, properties, constructor

    public Brands() : base()
    {
        ButtonSettings = new(false, true, true, true, false, false, false);
    }

    #endregion
}