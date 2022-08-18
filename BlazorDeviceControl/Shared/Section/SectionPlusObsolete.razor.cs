// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section;

public partial class SectionPlusObsolete : BlazorCore.Models.RazorBase
{
    #region Public and private fields, properties, constructor

    private List<PluObsoleteEntity> ItemsCast => Items == null ? new() : Items.Select(x => (PluObsoleteEntity)x).ToList();

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableScaleEntity(ProjectsEnums.TableScale.PlusObsolete);
        IsShowMarkedFilter = true;
        Items = new();
	}

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        RunActions(new()
        {
            () =>
            {
                long? scaleId = null;
                if (ItemFilter is ScaleEntity scale)
                    scaleId = scale.IdentityId;
                if (IsShowMarkedItems)
                {
                    if (scaleId == null)
                        Items = AppSettings.DataAccess.Crud.GetEntities<PluObsoleteEntity>(null,
                                new(DbField.GoodsName),
                                IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                            ?.ToList<BaseEntity>();
                    else
                    {
                        Items = AppSettings.DataAccess.Crud.GetEntities<PluObsoleteEntity>(
                                new(new() { new($"Scale.{DbField.IdentityId}", DbComparer.Equal, scaleId) }),
                                new(DbField.GoodsName),
                                IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                            ?.ToList<BaseEntity>();
                    }
                }
                else
                {
                    if (scaleId == null)
                        Items = AppSettings.DataAccess.Crud.GetEntities<PluObsoleteEntity>(
                            new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                            new(DbField.GoodsName),
                            IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                            ?.ToList<BaseEntity>();
                    else
                    {
                        Items = AppSettings.DataAccess.Crud.GetEntities<PluObsoleteEntity>(
                            new(new() { new($"Scale.{DbField.IdentityId}", DbComparer.Equal, scaleId),
                                new(DbField.IsMarked, DbComparer.Equal, false)
                            }),
                            new(DbField.GoodsName),
                            IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                            ?.ToList<BaseEntity>();
                    }
                }
                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    private void SetFilterItems(List<BaseEntity>? items, long? scaleId)
    {
        if (items != null)
        {
            Items = new();
            foreach (BaseEntity item in items)
            {
                if (item is PluObsoleteEntity plu && plu.Scale.IdentityId == scaleId)
                    Items.Add(item);
            }
        }
    }

    #endregion
}
