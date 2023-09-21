namespace DeviceControl.Components.Nested.PlusLines;

public sealed partial class AddPlusLines
{
    [Inject] protected IJSRuntime JsRuntime { get; set; } = default!;
    
    [Parameter] public WsSqlScaleModel Line { get; set; }
    
    private List<WsSqlPluModel> Plus { get; set; }
    
    private List<WsSqlPluModel> SelectedPlus { get; set; }
    
    public ButtonSettingsModel ButtonSettings { get; set; }
    protected static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    
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
                SqlRestrictions.EqualFk(nameof(WsSqlPluScaleModel.Line), Line),
                SqlRestrictions.Equal(nameof(WsSqlPluScaleModel.IsActive), true)
            });
            List<short> pluNumbersActive = new WsSqlPluLineRepository().GetList(sqlCrud).Select(plusScale => plusScale.Plu.Number).ToList();
            
            sqlCrud = WsSqlCrudConfigFactory.GetCrudActual();
            sqlCrud.AddFilters(new() {
                SqlRestrictions.Equal(nameof(WsSqlPluModel.IsGroup), false),
                SqlRestrictions.NotIn(nameof(WsSqlPluModel.Number),  pluNumbersActive.Cast<object>().ToList())
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
        foreach (WsSqlPluModel plu in SelectedPlus)
        {
            WsSqlPluScaleModel pluScale = new WsSqlPluLineRepository().GetItemByLinePlu(Line, plu);
            pluScale.IsActive = true;
            if (pluScale.IsNew)
            {
                pluScale.Line = Line;
                pluScale.Plu = plu;
            }
            else
            {
                ContextManager.SqlCore.Update(pluScale);
            }
            ContextManager.SqlCore.Save(pluScale);
        }
        ReloadPage();
    }
    
    #endregion
}
