// namespace DeviceControl.Components.Item;
//
// public sealed partial class ItemTableBody : ComponentBase
// {
//     #region Public and private fields, properties, constructor
//
//     [Parameter] public SqlEntityBase SqlItem { get; set; }
//
//     private bool IsSqlItem1C => SqlItem is SqlTable1CBase;
//
//     private string Guid1C => IsSqlItem1C && SqlItem is SqlTable1CBase sqlTable && sqlTable.Uid1C != Guid.Empty
//         ? $"{sqlTable.Uid1C}"
//         : "Нет в базе 1С";
//
//     private string IdentityName =>
//         SqlItem.Identity.Name switch
//         {
//             SqlEnumFieldIdentity.Id => LocaleCore.Table.Id,
//             SqlEnumFieldIdentity.Uid => LocaleCore.Table.Uid,
//             _ => string.Empty
//         };
//
//     private string IdentityNameId =>
//         SqlItem.Identity.Name switch
//         {
//             SqlEnumFieldIdentity.Id => SqlItem.IdentityValueId != 0 ? $"{SqlItem.IdentityValueId}" : "Новая запись",
//             SqlEnumFieldIdentity.Uid => SqlItem.IdentityValueUid != Guid.Empty ? $"{SqlItem.IdentityValueUid}" : "Новая запись",
//             _ => "Новая запись"
//         };
//
//     #endregion
// }
