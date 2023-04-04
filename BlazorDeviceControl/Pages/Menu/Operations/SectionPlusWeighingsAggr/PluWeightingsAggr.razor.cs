// // This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// // PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
//
// using DataCore.Sql.TableScaleFkModels.Aggregations;
// using DataCore.Sql.TableScaleFkModels.PlusWeighingsFks;
//
// namespace BlazorDeviceControl.Pages.Menu.Operations.PlusWeighingsAggrSection;
//
// public sealed partial class PluWeightingsAggr : RazorComponentSectionBase<WeithingFactSummaryModel>
// {
//     #region Public and private fields, properties, constructor
//
//     private bool IsAddPlu { get; set; }
//     private List<PluAggrModel> PluAggrs { get; set; }
//
//     public PluWeightingsAggr()
//     {
//         PluAggrs = new();
//         ButtonSettings = new(false, false, false, false, false, false, false);
//     }
//
// 	#endregion
//
// 	#region Public and private methods
//
//     protected override void SetSqlSectionCast()
//     {
//         object[] objects = IsAddPlu 
//             ? DataAccess.GetArrayObjectsNotNullable(SqlQueries.DbScales.Tables.PluWeighings.GetWeighingsAggrWithPlu(
//                 SqlCrudConfigSection.IsResultShowOnlyTop ? DataAccess.JsonSettings.Local.SelectTopRowsCount : 0))
//             : DataAccess.GetArrayObjectsNotNullable(SqlQueries.DbScales.Tables.PluWeighings.GetWeighingsAggrWithoutPlu(
//                 SqlCrudConfigSection.IsResultShowOnlyTop ? DataAccess.JsonSettings.Local.SelectTopRowsCount : 0));
//         PluAggrs = new();
//         foreach (object obj in objects)
//         {
//             if (IsAddPlu)
//             {
//                 if (obj is object[] { Length: 5 } item)
//                 {
//                     PluAggrs.Add(new(Convert.ToDateTime(item[0]),
//                         Convert.ToInt32(item[1]), Convert.ToString(item[2]) ?? string.Empty,
//                         Convert.ToString(item[3]) ?? string.Empty, Convert.ToString(item[4]) ?? string.Empty)
//                     );
//                 }
//             }
//             else
//             {
//                 if (obj is object[] { Length: 4 } item)
//                 {
//                     PluAggrs.Add(new(Convert.ToDateTime(item[0]),
//                         Convert.ToInt32(item[1]), Convert.ToString(item[2]) ?? string.Empty,
//                         Convert.ToString(item[3]) ?? string.Empty)
//                     );
//                 }
//             }
//         }
//         //SqlSectionCast = items;
//     }
//
//     #endregion
// }
