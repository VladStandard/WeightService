// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Item;
using WsDataCore.Enums;
using WsStorageCore.TableScaleModels.Access;

namespace DeviceControl.Pages.Menu.Admins.Access;

public sealed partial class ItemAccess : ItemBase<WsSqlAccessModel>
{
    #region Public and private fields, properties, constructor

    private List<WsEnumTypeModel<WsEnumAccessRights>> TemplateAccessRights { get; set; }

    private WsEnumAccessRights Rights
    {
        get => (WsEnumAccessRights)SqlItemCast.Rights;
        set => SqlItemCast.Rights = (byte)value;
    }

    public ItemAccess() : base()
    {
        TemplateAccessRights = BlazorAppSettings.DataSourceDics.GetTemplateAccessRights();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        TemplateAccessRights = BlazorAppSettings.DataSourceDics.GetTemplateAccessRights(SqlItemCast.Rights);
    }

    #endregion
}
