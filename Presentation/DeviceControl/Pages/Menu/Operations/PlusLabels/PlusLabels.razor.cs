// namespace DeviceControl.Pages.Menu.Operations.PlusLabels;
//
// public sealed partial class PlusLabels : SectionBase<SqlViewPluLabelModel>
// {
//     #region Public and private fields, properties, constructor
//
//     private SqlViewPluLabelRepository PluLabelRepository { get; } = new();
//     
//     public PlusLabels() : base()
//     {
//         ButtonSettings = ButtonSettingsModel.CreateForStatic1CSection();
//     }
//
//     #endregion
//
//     #region Public and private methods
//
//     protected override void SetSqlSectionCast()
//     {
//         SqlSectionCast = PluLabelRepository.GetList(SqlCrudConfigSection).ToList();
//     }
//
//     #endregion
// }
