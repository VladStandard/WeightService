namespace DeviceControl.Pages.Menu.Admins.Access;

public sealed partial class ItemAccess : ItemBase<WsSqlAccessModel>
{
    #region Public and private fields, properties, constructor
    
    private List<WsEnumAccessRights> TemplateAccessRights { get; set; }

    private WsEnumAccessRights Rights
    {
        get => (WsEnumAccessRights)SqlItemCast.Rights;
        set => SqlItemCast.Rights = (byte)value;
    }
    
    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        TemplateAccessRights = GetTemplateAccessRights();
    }
    
    private List<WsEnumAccessRights> GetTemplateAccessRights()
    {
        List<WsEnumAccessRights> result = new()
        {
            WsEnumAccessRights.None,
            WsEnumAccessRights.Read,
            WsEnumAccessRights.Write
        };
        if (SqlItemCast.Rights >= (byte)WsEnumAccessRights.Write)
            result.Add(WsEnumAccessRights.Admin);
        return result;
    }

    #endregion
}
