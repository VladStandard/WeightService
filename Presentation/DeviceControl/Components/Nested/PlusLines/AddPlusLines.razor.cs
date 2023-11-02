using WsStorageCore.OrmUtils;
namespace DeviceControl.Components.Nested.PlusLines;

public sealed partial class AddPlusLines
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    [Inject] protected IJSRuntime JsRuntime { get; set; } = default!;
    
    [Parameter] public WsSqlScaleEntity Line { get; set; }
    
    private List<WsSqlPluEntity> Plus { get; set; }
    
    private List<WsSqlPluEntity> SelectedPlus { get; set; }
    
    public ButtonSettingsModel ButtonSettings { get; set; }
    
    public AddPlusLines()
    {
        Plus = new();
        SelectedPlus = new();
        ButtonSettings = ButtonSettingsModel.CreateForItem();
    }
    
    #region Public and private methods

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            WsSqlCrudConfigModel sqlCrud = WsSqlCrudConfigFactory.GetCrudActual();
            sqlCrud.AddFilters(new() {
                SqlRestrictions.EqualFk(nameof(WsSqlPluScaleEntity.Line), Line),
                SqlRestrictions.Equal(nameof(WsSqlPluScaleEntity.IsActive), true)
            });
            List<short> pluNumbersActive = new WsSqlPluLineRepository().GetList(sqlCrud).Select(plusScale => plusScale.Plu.Number).ToList();
            
            sqlCrud = WsSqlCrudConfigFactory.GetCrudActual();
            sqlCrud.AddFilters(new() {
                SqlRestrictions.Equal(nameof(WsSqlPluEntity.IsGroup), false),
                SqlRestrictions.NotIn(nameof(WsSqlPluEntity.Number),  pluNumbersActive.Cast<object>().ToList())
            });
            Plus = new WsSqlPluRepository().GetEnumerable(sqlCrud).ToList();
            StateHasChanged();
        }
        base.OnAfterRender(firstRender);
    }
    
    private void ReloadPage()
    {
        JsRuntime.InvokeVoidAsync("location.reload");
    }
    
    private void SaveItem()
    {
        foreach (WsSqlPluEntity plu in SelectedPlus)
        {
            WsSqlPluScaleEntity pluScale = new WsSqlPluLineRepository().GetItemByLinePlu(Line, plu);
            pluScale.IsActive = true;
            if (pluScale.IsNew)
            {
                pluScale.Line = Line;
                pluScale.Plu = plu;
            }
            else
            {
                SqlCore.Update(pluScale);
            }
            SqlCore.Save(pluScale);
        }
        ReloadPage();
    }
    
    #endregion
}
