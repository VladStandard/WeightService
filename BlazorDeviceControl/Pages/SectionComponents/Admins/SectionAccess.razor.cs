// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Access;

namespace BlazorDeviceControl.Pages.SectionComponents.Admins;

public partial class SectionAccess : RazorComponentSectionBase<AccessModel>
{
    #region Public and private fields, properties, constructor

    public SectionAccess() : base()
    {
	    SqlCrudConfigSection.IsResultOrder = true;
    }
    
    #endregion

    #region Public and private methods

    #endregion
}
