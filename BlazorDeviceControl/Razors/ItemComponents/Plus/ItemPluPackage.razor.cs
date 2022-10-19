// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Plus;

public partial class ItemPluPackage : RazorComponentItemBase<PluPackageModel>
{
    #region Public and private fields, properties, constructor

    private List<PluModel> Plus { get; set; }
    private List<PackageModel> Packages { get; set; }

    public ItemPluPackage()
    {
        Plus = new();
        Packages = new();
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlItemCast = AppSettings.DataAccess.GetItemByUidNotNull<PluPackageModel>(IdentityUid);
                Plus = AppSettings.DataAccess.GetListPlus(false, false, false);
				Packages = AppSettings.DataAccess.GetListPackages(false, false, false);
                
				if (SqlItemCast.IdentityIsNew)
                {
	                SqlItem = SqlItemNew<PluPackageModel>();
                    if (Plus.Any())
						SqlItemCast.Plu = Plus.First();
                    if (Packages.Any())
						SqlItemCast.Package = Packages.First();
                }

	            ButtonSettings = new(false, false, false, false, false, true, true);
            }
        });
    }

    #endregion
}
