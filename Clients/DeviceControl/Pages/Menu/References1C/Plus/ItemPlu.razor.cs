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
        ButtonSettings.IsShowSave = false;
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
