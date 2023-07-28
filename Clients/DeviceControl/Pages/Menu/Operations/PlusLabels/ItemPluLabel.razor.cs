// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Settings;
using WsStorageCore.Tables.TableScaleFkModels.PlusTemplatesFks;
using WsStorageCore.Tables.TableScaleModels.PlusLabels;

namespace DeviceControl.Pages.Menu.Operations.PlusLabels;

public sealed partial class ItemPluLabel : ItemBase<WsSqlPluLabelModel>
{
    #region Public and private fields, properties, constructor

    private bool IsWeighted => SqlItemCast.PluScale.Plu.IsCheckWeight;
    private WsSqlPluTemplateFkRepository PluTemplateFkRepository { get; } = new();
    
    public ItemPluLabel() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }

    #endregion
}