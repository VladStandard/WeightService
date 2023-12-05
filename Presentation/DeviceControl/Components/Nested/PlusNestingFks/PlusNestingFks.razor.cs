// namespace DeviceControl.Components.Nested.PlusNestingFks;
//
// public sealed partial class PlusNestingFks : SectionBase<SqlPluNestingFkEntity>
// {
//     #region Public and private fields, properties, constructor
//     
//     [Parameter] public SqlPluEntity Plu { get; set; }
//     private SqlPluNestingFkRepository PluNestingFkRepository { get; } = new();
//     public PlusNestingFks() : base()
//     {
//         IsGuiShowFilterMarked = false;
//         ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
//     }
//
//     #endregion
//
//     #region Public and private methods
//
//     protected override void SetSqlSectionCast()
//     {
//         SqlSectionCast = PluNestingFkRepository.GetEnumerableByPluUidActual(Plu).ToList();
//     }
//
//     #endregion
// }
