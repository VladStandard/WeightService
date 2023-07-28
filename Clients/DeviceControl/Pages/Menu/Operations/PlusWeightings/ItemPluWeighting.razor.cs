// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Settings;
using WsStorageCore.Tables.TableScaleModels.PlusWeighings;

namespace DeviceControl.Pages.Menu.Operations.PlusWeightings;

public sealed partial class ItemPluWeighting : ItemBase<WsSqlPluWeighingModel>
{
    #region Public and private fields, properties, constructor

    public ItemPluWeighting() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }

    #endregion
}