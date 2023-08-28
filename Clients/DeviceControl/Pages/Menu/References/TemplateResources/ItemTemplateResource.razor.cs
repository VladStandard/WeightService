namespace DeviceControl.Pages.Menu.References.TemplateResources;

public sealed partial class ItemTemplateResource : ItemBase<WsSqlTemplateResourceModel>
{
    #region Public and private methods

    private static bool IsNotBlackType(string type)
    {
        foreach (WsEnumTemplateResourceBlackType value in Enum.GetValues(typeof(WsEnumTemplateResourceBlackType)))
            if (type.Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase))
                return false;
        return true;
    }

    #endregion
}
