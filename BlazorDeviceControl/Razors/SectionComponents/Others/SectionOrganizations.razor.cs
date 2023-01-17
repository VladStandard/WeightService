// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleModels.Organizations;

namespace BlazorDeviceControl.Razors.SectionComponents.Others;

public partial class SectionOrganizations : RazorComponentSectionBase<OrganizationModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor


    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
	            SqlSectionCast = DataContext.GetListNotNullable<OrganizationModel>(SqlCrudConfigSection);
            }
        });
    }

    #endregion
}