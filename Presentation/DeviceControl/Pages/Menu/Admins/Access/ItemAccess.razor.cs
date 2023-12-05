// namespace DeviceControl.Pages.Menu.Admins.Access;
//
// public sealed partial class ItemAccess : ItemBase<SqlAccessEntity>
// {
//     #region Public and private fields, properties, constructor
//     
//     private List<EnumAccessRights> TemplateAccessRights { get; set; }
//
//     private EnumAccessRights Rights
//     {
//         get => (EnumAccessRights)SqlItemCast.Rights;
//         set => SqlItemCast.Rights = (byte)value;
//     }
//     
//     #endregion
//
//     #region Public and private methods
//
//     protected override void SetSqlItemCast()
//     {
//         base.SetSqlItemCast();
//         TemplateAccessRights = GetTemplateAccessRights();
//     }
//     
//     private List<EnumAccessRights> GetTemplateAccessRights()
//     {
//         List<EnumAccessRights> result = new()
//         {
//             EnumAccessRights.None,
//             EnumAccessRights.Read,
//             EnumAccessRights.Write
//         };
//         if (SqlItemCast.Rights >= (byte)EnumAccessRights.Write)
//             result.Add(EnumAccessRights.Admin);
//         return result;
//     }
//
//     #endregion
// }
