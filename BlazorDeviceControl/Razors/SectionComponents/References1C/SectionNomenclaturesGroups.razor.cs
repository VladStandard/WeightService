// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusGroupsFks;
using DataCore.Sql.TableScaleModels.PlusGroups;

namespace BlazorDeviceControl.Razors.SectionComponents.References1C;

public partial class SectionNomenclaturesGroups : RazorComponentSectionBase<PluGroupModel> 
{
	#region Public and private fields, properties, constructor

    private List<PluGroupModel> AllData { get; set; }
    
    #endregion

	#region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigSection.CloneCast();
        //sqlCrudConfig.AddFilters(new SqlFieldFilterModel($"{nameof(PluGroupFkModel.IsGroup)}", false));
        //sqlCrudConfig.SetOrders(new(nameof(SqlTableBase.Name), SqlFieldOrderEnum.Asc));
        sqlCrudConfig.IsResultOrder = true;
        var pluGroupsFk = DataContext.GetListNotNullable<PluGroupFkModel>(new SqlCrudConfigModel());
        AllData = DataContext.GetListNotNullable<PluGroupModel>(sqlCrudConfig);
        foreach (PluGroupModel pluGroup in AllData)
        {
            var  temp = pluGroupsFk.Where(e => e.PluGroup.IdentityValueUid == pluGroup.IdentityValueUid).ToList();
            if (temp.Any())
                pluGroup.ParentGuid = temp.First().Parent.IdentityValueUid;
        }

        SqlSectionCast = AllData.Where(e=> e.ParentGuid == Guid.Empty).ToList();
    }

    void RowRender(RowRenderEventArgs<PluGroupModel> args)
    {
        args.Expandable = AllData.Any(e => e.ParentGuid == args.Data.IdentityValueUid);
    }

    private void LoadChildData(DataGridLoadChildDataEventArgs<PluGroupModel> args)
    {
	    args.Data = AllData.Where(e => e.ParentGuid == args.Item.IdentityValueUid);
    }

	#endregion
}
