// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusLabels;

namespace BlazorDeviceControl.Pages.Menu.Operations.SectionPlusLabels;

public sealed partial class ItemPluLabel : RazorComponentItemBase<PluLabelModel>
{
    #region Public and private fields, properties, constructor
    
    private bool IsWeighted => SqlItemCast.PluScale.Plu.IsCheckWeight;
    public ItemPluLabel() : base()
    {
        ButtonSettings = new(false, false, false, false, false, false, true);
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlItemCast = DataAccess.GetItemNotNullable<PluLabelModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                {
                    SqlItemCast = SqlItemNew<PluLabelModel>();
                }
            }
        });
    }

    #endregion
}
