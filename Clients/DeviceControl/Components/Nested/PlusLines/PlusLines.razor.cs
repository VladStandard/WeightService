namespace DeviceControl.Components.Nested.PlusLines;

public sealed partial class PlusLines : SectionBase<WsSqlPluScaleModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlPluLineRepository PluLineRepository { get; } = new();
    [Parameter] public WsSqlScaleModel Scale { get; set; }
    private bool HideNoneActivePlu { get; set; }

    public PlusLines() : base()
    {
        HideNoneActivePlu = true;
        SqlCrudConfigSection.IsResultOrder = true;
        ButtonSettings.IsShowSave = true;
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        // TODO: to repos
        if (!HideNoneActivePlu)
            SqlCrudConfigSection.ClearFilters();
        else
            SqlCrudConfigSection.AddFilter(
                    new()
                    {
                        Name = nameof(WsSqlPluScaleModel.IsActive),
                        Value = true
                    }
                );
        SqlSectionCast = PluLineRepository.GetListByLine(Scale, SqlCrudConfigSection);
    }

    #endregion
}
