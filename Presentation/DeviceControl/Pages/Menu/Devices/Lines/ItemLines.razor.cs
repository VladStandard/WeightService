// using Ws.StorageCore.Entities.SchemaRef.Hosts;
//
// namespace DeviceControl.Pages.Menu.Devices.Lines;
//
// public sealed partial class ItemLines : ItemBase<SqlLineEntity>
// {
//     #region Public and private fields, properties, constructor
//     
//     private List<string> ComPorts { get; set; }
//     private List<SqlPrinterEntity> PrinterModels { get; set; }
//     private List<SqlHostEntity> HostModels { get; set; }
//     private List<SqlWorkShopEntity> WorkShopModels { get; set; }
//
//     public ItemLines() : base()
//     {
//         ComPorts = new();
//         HostModels = new();
//         PrinterModels = new();
//         WorkShopModels = new();
//     }
//
//     #endregion
//
//     #region Public and private methods
//
//     protected override void SetSqlItemCast()
//     {
//         base.SetSqlItemCast();
//         PrinterModels = new SqlPrinterRepository().GetEnumerable(new()).ToList();
//         HostModels = new SqlHostRepository().GetEnumerable(new()).ToList();
//         WorkShopModels = new SqlWorkShopRepository().GetEnumerable(new()).ToList();
//         ComPorts = Enumerable.Range(1, 10).Select(i => $"COM{i}").ToList();;
//     }
//
//     #endregion
// }
