// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Organizations;

namespace BlazorDeviceControl.Pages.Menu.References.SectionOrganizations;

public sealed partial class ItemOrganization : RazorComponentItemBase<OrganizationModel>
{
    #region Public and private fields, properties, constructor

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new List<Action>
        {
            () =>
            {
                SqlItemCast =
                    ContextManager.AccessManager.AccessItem.GetItemNotNullable<OrganizationModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                    SqlItemCast = SqlItemNew<OrganizationModel>();
            }
        });
    }

    #endregion
}