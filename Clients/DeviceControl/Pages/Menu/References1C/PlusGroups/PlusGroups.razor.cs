// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableScaleFkModels.PlusGroupsFks;

namespace DeviceControl.Pages.Menu.References1C.PlusGroups;

public sealed partial class PlusGroups : SectionBase<WsSqlPluGroupModel>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlPluGroupModel> AllData { get; set; }
    private WsSqlPluGroupFkRepository PluGroupFkRepository { get; } = new();
    private WsSqlPluGroupRepository PluGroupRepository { get; } = new();
    
    public PlusGroups() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion

    #region Public and private methods
    
    protected override void SetSqlSectionCast()
    {
        List<WsSqlPluGroupFkModel> pluGroupsFk = PluGroupFkRepository.GetList(new());
        AllData = PluGroupRepository.GetList(new());
        
        foreach (WsSqlPluGroupModel pluGroup in AllData)
        {
            List<WsSqlPluGroupFkModel> temp = pluGroupsFk.Where(e => e.PluGroup.IdentityValueUid == pluGroup.IdentityValueUid).ToList();
            if (temp.Any())
                pluGroup.ParentGuid = temp.First().Parent.IdentityValueUid;
        }

        SqlSectionCast = AllData.Where(e => e.ParentGuid == Guid.Empty).ToList();
    }

    private void RowRender(RowRenderEventArgs<WsSqlPluGroupModel> args)
    {
        args.Expandable = AllData.Any(e => e.ParentGuid == args.Data.IdentityValueUid);
    }

    private void LoadChildData(DataGridLoadChildDataEventArgs<WsSqlPluGroupModel> args)
    {
        args.Data = AllData.Where(e => e.ParentGuid == args.Item.IdentityValueUid);
    }

    #endregion
}