// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusGroupsFks;
using WsStorageCore.TableScaleModels.PlusGroups;

namespace DeviceControl.Pages.Menu.References1C.SectionNomenclaturesGroups;

public sealed partial class NomenclaturesGroups : RazorComponentSectionBase<WsSqlPluGroupModel>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlPluGroupModel> AllData { get; set; }

    public NomenclaturesGroups() : base()
    {
        ButtonSettings = new(false, false, true, true, false, false, false);
    }

    #endregion

    #region Public and private methods

    [Obsolete(@"AllData проинициализируй в конструкторе")]
    protected override void SetSqlSectionCast()
    {
        var pluGroupsFk = ContextManager.AccessManager.AccessList.GetListNotNullable<WsSqlPluGroupFkModel>(new WsSqlCrudConfigModel());
        AllData = ContextManager.AccessManager.AccessList.GetListNotNullable<WsSqlPluGroupModel>(new WsSqlCrudConfigModel() {IsResultOrder = true});
        foreach (WsSqlPluGroupModel pluGroup in AllData)
        {
            var temp = pluGroupsFk.Where(e => e.PluGroup.IdentityValueUid == pluGroup.IdentityValueUid).ToList();
            if (temp.Any())
                pluGroup.ParentGuid = temp.First().Parent.IdentityValueUid;
        }

        SqlSectionCast = AllData.Where(e => e.ParentGuid == Guid.Empty).ToList();
    }

    private new void RowRender(RowRenderEventArgs<WsSqlPluGroupModel> args)
    {
        args.Expandable = AllData.Any(e => e.ParentGuid == args.Data.IdentityValueUid);
    }

    private void LoadChildData(DataGridLoadChildDataEventArgs<WsSqlPluGroupModel> args)
    {
        args.Data = AllData.Where(e => e.ParentGuid == args.Item.IdentityValueUid);
    }

    #endregion
}
