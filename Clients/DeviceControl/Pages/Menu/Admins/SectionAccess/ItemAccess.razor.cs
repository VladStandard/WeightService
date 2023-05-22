// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Enums;
using WsStorageCore.TableScaleModels.Access;

namespace BlazorDeviceControl.Pages.Menu.Admins.SectionAccess;

public sealed partial class ItemAccess : RazorComponentItemBase<WsSqlAccessModel>
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
