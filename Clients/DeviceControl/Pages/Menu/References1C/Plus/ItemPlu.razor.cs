// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableScaleFkModels.PlusStorageMethodsFks;
using WsStorageCore.Tables.TableScaleFkModels.PlusTemplatesFks;

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

        ButtonSettings = ButtonSettingsModel.CreateForItem();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        PluTemplateFk = new WsSqlPluTemplateFkRepository().GetItemByPlu(SqlItemCast);
        StorageMethodFk = new WsSqlPluStorageMethodFkRepository().GetItemByPlu(SqlItemCast);
        Template = PluTemplateFk.Template;
        StorageMethod = StorageMethodFk.Method;
    }

    #endregion
}
