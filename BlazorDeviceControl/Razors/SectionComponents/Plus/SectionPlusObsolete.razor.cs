//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace BlazorDeviceControl.Razors.Sections.Plus;

//public partial class SectionPlusObsolete : RazorComponentSectionBase<PluObsoleteModel>
//{
//    #region Public and private fields, properties, constructor

//    public SectionPlusObsolete()
//    {
//		RazorConfig.IsShowMarked = true;
//		RazorConfig.IsShowFilterMarked = true;
//		RazorConfig.IsShowFilterAdditional = true;
//    }

//	#endregion

//	#region Public and private methods

//    protected override void OnParametersSet()
//    {
//        RunActionsParametersSet(new()
//        {
//            () =>
//            {
//				SqlItemsCast = AppSettings.DataAccess.GetListPluObsoletes(RazorConfig.IsShowMarked, RazorConfig.IsShowOnlyTop, Item);

//				ButtonSettings = new(true, true, true, true, true, false, false);
//            }
//        });
//    }

//    private void SetFilterItems(List<DataCore.Sql.Tables.SqlTableBase>? items, long? scaleId)
//    {
//        if (items is not null)
//        {
//            Items = new();
//            foreach (DataCore.Sql.Tables.SqlTableBase item in items)
//            {
//                if (item is PluObsoleteModel plu && plu.Scale.Identity.Id == scaleId)
//                    Items.Add(item);
//            }
//        }
//    }

//    #endregion
//}
