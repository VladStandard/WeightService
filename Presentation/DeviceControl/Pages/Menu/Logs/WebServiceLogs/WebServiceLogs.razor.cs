// namespace DeviceControl.Pages.Menu.Logs.WebServiceLogs;
//
// public sealed partial class WebServiceLogs : SectionBase<SqlLogWebEntity>
// {
//     #region Public and private fields, properties, constructor
//
//     public WebServiceLogs() : base()
//     {
//         IsGuiShowFilterMarked = false;
//         SqlCrudConfigSection.IsResultOrder = true;
//         ButtonSettings = ButtonSettingsModel.CreateForStatic1CSection();
//         
//     }
//
//     #endregion
//
//     #region Public and private methods
//
//     protected override void SetSqlSectionCast()
//     {
//         SqlCrudConfigModel crud = SqlCrudConfigFactory.GetCrudAll();
//         SqlSectionCast = new SqlLogWebRepository().GetList(crud).ToList();
//     }
//
//     #endregion
// }
