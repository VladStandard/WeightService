namespace DeviceControl.Pages.Menu.Operations.PlusWeightings;

public sealed partial class ItemPluWeighting : ItemBase<WsSqlPluWeighingModel>
{
    #region Public and private fields, properties, constructor

    public ItemPluWeighting() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }

    #endregion
}
