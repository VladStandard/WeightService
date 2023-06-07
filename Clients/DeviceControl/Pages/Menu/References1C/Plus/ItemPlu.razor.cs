// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Item;
using WsBlazorCore.Settings;
using WsStorageCore.TableScaleFkModels.PlusStorageMethodsFks;
using WsStorageCore.TableScaleFkModels.PlusTemplatesFks;
using WsStorageCore.TableScaleModels.Plus;
using WsStorageCore.TableScaleModels.PlusStorageMethods;
using WsStorageCore.TableScaleModels.Templates;

namespace DeviceControl.Pages.Menu.References1C.Plus;

public sealed partial class ItemPlu : ItemBase<WsSqlPluModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlTemplateModel Template { get; set; }
    private WsSqlPluStorageMethodModel StorageMethod { get; set; }
    private WsSqlPluStorageMethodFkModel StorageMethodFk { get; set; }
    private WsSqlPluTemplateFkModel PluTemplateFk { get; set; }

    public ItemPlu() : base()
    {
        Template = new();
        PluTemplateFk = new();

        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        PluTemplateFk = ContextManager.ContextItem.GetItemPluTemplateFkNotNullable(SqlItemCast);
        StorageMethodFk = ContextManager.ContextItem.GetItemPluStorageMethodFkNotNullable(SqlItemCast);
        Template = PluTemplateFk.Template.IsNotNew
            ? PluTemplateFk.Template
            : ContextManager.AccessManager.AccessItem.GetItemNewEmpty<WsSqlTemplateModel>();
        StorageMethod = StorageMethodFk.Method.IsNotNew
            ? StorageMethodFk.Method
            : ContextManager.AccessManager.AccessItem.GetItemNewEmpty<WsSqlPluStorageMethodModel>();
    }

    #endregion
}