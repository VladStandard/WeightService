// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Enums;
using WsStorageCore.Tables.TableScaleModels.TemplatesResources;

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