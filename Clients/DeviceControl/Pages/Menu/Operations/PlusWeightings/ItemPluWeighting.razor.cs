namespace DeviceControl.Pages.Menu.Operations.PlusWeightings;

public sealed partial class ItemPluWeighting : ItemBase<WsSqlPluWeighingEntity>
{
    #region Public and private fields, properties, constructor

    public ItemPluWeighting() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }

    #endregion
}
