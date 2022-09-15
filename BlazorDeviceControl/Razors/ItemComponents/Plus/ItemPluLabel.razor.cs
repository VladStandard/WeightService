// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Plus;

public partial class ItemPluLabel : RazorPageItemBase<PluLabelModel>
{
    #region Public and private fields, properties, constructor

    public ItemPluLabel()
    {
		//
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                switch (TableAction)
                {
                    case SqlTableActionEnum.New:
                        ItemCast = new();
                        ItemCast.SetDtNow();
						break;
                    default:
	                    ItemCast = AppSettings.DataAccess.GetItemByIdNotNull<PluLabelModel>(IdentityId);
                        break;
                }

                ButtonSettings = new(false, false, false, false, false, false, true);
            }
        });
    }

    #endregion
}
