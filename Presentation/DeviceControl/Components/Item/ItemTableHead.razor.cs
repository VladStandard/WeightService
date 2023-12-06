// namespace DeviceControl.Components.Item;
//
// public partial class ItemTableHead : ComponentBase
// {
//     #region Public and private fields, properties, constructor
//
//     private CssStyleTableHeadModel TableHeadModel { get; set; }
//     [Parameter] public List<int> HeadWidth { get; set; } = new();
//
//     #endregion
//
//     protected override void OnInitialized()
//     {
//         TableHeadModel = HeadWidth.Any() ? 
//             new(HeadWidth) : new CssStyleTableHeadModel();
//     }
//     
// }
