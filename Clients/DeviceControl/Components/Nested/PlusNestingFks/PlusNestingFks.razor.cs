using WsStorageCore.Tables.TableRef1cModels.Plus;
namespace DeviceControl.Components.Nested.PlusNestingFks;

public sealed partial class PlusNestingFks : SectionBase<WsSqlPluNestingFkModel>
{
    #region Public and private fields, properties, constructor
    
    [Parameter] public WsSqlPluModel Plu { get; set; }
    private WsSqlPluNestingFkRepository PluNestingFkRepository { get; } = new();
    public PlusNestingFks() : base()
    {
        IsGuiShowFilterMarked = false;
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = PluNestingFkRepository.GetEnumerableByPluUidActual(Plu).ToList();
    }

    #endregion
}
