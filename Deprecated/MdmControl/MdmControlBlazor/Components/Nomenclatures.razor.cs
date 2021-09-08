// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MdmControlBlazor.Utils;
using MdmControlCore;
using MdmControlCore.DAL;
using MdmControlCore.DAL.TableModels;
using Microsoft.AspNetCore.Components;
using Radzen;
using Toolbelt.Blazor.HotKeys;

namespace MdmControlBlazor.Components
{
    public partial class Nomenclatures
    {
        #region Public and private fields and properties

        private NomenclatureLightEntity _itemMaster;
        public NomenclatureLightEntity ItemMaster
        {
            get => _itemMaster;
            set
            {
                _itemMaster = value;
                ObjectMaster = _itemMaster;
            }
        }
        public object ObjectMaster { get; set; }
        private NomenclatureLightEntity _itemNonNormilize;
        public NomenclatureLightEntity ItemNonNormilize
        {
            get => _itemNonNormilize;
            set
            {
                _itemNonNormilize = value;
                ObjectNonNormilize = _itemNonNormilize;
            }
        }
        public object ObjectNonNormilize { get; set; }
        public IEnumerable<NomenclatureLightEntity> ItemsMaster { get; set; }
        public string ItemsMasterCount => $"{LocalizationStrings.TableRowsCount}: {ItemsMaster?.Count()}";
        public IEnumerable<NomenclatureLightEntity> ItemsNonNormilize { get; set; }
        public string ItemsNonNormilizeCount => $"{LocalizationStrings.TableRowsCount}: {ItemsNonNormilize?.Count()}";
        public bool UseIsProductNonNormilize { get; set; } = true;

        #endregion

        #region Public and private methods - Hotkeys

        private async Task HotKeysTabAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        }

        private async Task HotKeysRAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            await GetDataAsync(EnumTable.NomenclatureMaster).ConfigureAwait(false);
            await GetDataAsync(EnumTable.NomenclatureNonNormalize).ConfigureAwait(false);
        }

        #endregion

        #region Public and private methods

        private async Task GuiRefreshAsync()
        {
            await InvokeAsync(StateHasChanged).ConfigureAwait(false);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);

            HotKeysItem = HotKeys.CreateContext()
                .Add(ModKeys.None, Keys.Tab, HotKeysTabAsync, LocalizationStrings.TableTab)
                .Add(ModKeys.None, Keys.F5, MasterRecordCreateAsync, LocalizationStrings.TableMasterCreate)
                .Add(ModKeys.None, Keys.F6, MasterRecordIncludeAsync, LocalizationStrings.TableMasterInclude)
                .Add(ModKeys.None, Keys.F8, MasterRecordDeleteAsync, LocalizationStrings.TableMasterDelete)
                .Add(ModKeys.None, Keys.Enter, ActionMasterEditAsync, LocalizationStrings.TableMasterEdit)
                .Add(ModKeys.Ctrl, Keys.R, HotKeysRAsync, LocalizationStrings.TableRead)
            ;
            BlazorSettings.Setup(JsonAppSettings, Notification, Dialog, Navigation, Tooltip, JsRuntime);
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            await GetDataAsync(EnumTable.NomenclatureMaster).ConfigureAwait(false);
            await GetDataAsync(EnumTable.NomenclatureNonNormalize).ConfigureAwait(false);
        }

        private void RowSelect(EnumTable table, NomenclatureLightEntity entity)
        {
            switch (table)
            {
                case EnumTable.NomenclatureMaster:
                    ItemMaster = entity;
                    break;
                case EnumTable.NomenclatureNonNormalize:
                    ItemNonNormilize = entity;
                    break;
            }
        }

        private async Task RowSelectAsync(EnumTable table, NomenclatureLightEntity entity)
        {
            Task task = new Task(() => RowSelect(table, entity));
            await BlazorSettings.RunTasks(LocalizationStrings.TableSelect, "", LocalizationStrings.DialogResultFail, "",
                new List<Task> { task }, GuiRefreshAsync).ConfigureAwait(false);
        }

        private async Task RowDoubleClickAsync(EnumTable table, NomenclatureLightEntity entity, bool isNewWindow)
        {
            Task task = new Task(() => ActionEditAsync(table, entity, isNewWindow).ConfigureAwait(false));
            await BlazorSettings.RunTasks(LocalizationStrings.TableEdit, "", LocalizationStrings.DialogResultFail, "",
            new List<Task> { task }, GuiRefreshAsync).ConfigureAwait(false);
        }

        private void GetDataMaster()
        {
            ItemMaster = null;
            ItemsMaster = BlazorSettings.SqlDataAccess.NomenclatureLightCrud.GetEntitiesAsIEnumerable(new FieldListEntity(new Dictionary<string, object>{
            { EnumField.IsProduct.ToString(), true },
            { "InformationSystem.Id", 7 },
            }),
                new FieldOrderEntity(EnumField.Name, EnumOrderDirection.Asc), 0);
        }

        private void GetDataNonNormilise()
        {
            ItemNonNormilize = null;
            ItemsNonNormilize = new List<NomenclatureLightEntity>();
            if (UseIsProductNonNormilize)
                ItemsNonNormilize = BlazorSettings.SqlDataAccess.NomenclatureLightCrud.GetEntitiesAsIEnumerable(new FieldListEntity(new Dictionary<string, object>{
                { EnumField.IsProduct.ToString(), true },
                { EnumField.MasterId.ToString(), null },
                }),
                    new FieldOrderEntity(EnumField.Name, EnumOrderDirection.Asc), 0);
            else
                ItemsNonNormilize = BlazorSettings.SqlDataAccess.NomenclatureLightCrud.GetEntitiesAsIEnumerable(new FieldListEntity(new Dictionary<string, object>{
                { EnumField.MasterId.ToString(), null },
                }),
                    new FieldOrderEntity(EnumField.Name, EnumOrderDirection.Asc), 0);
        }

        private async Task GetDataAsync(EnumTable table)
        {
            switch (table)
            {
                case EnumTable.NomenclatureMaster:
                    Task taskMaster = new Task(GetDataMaster);
                    await BlazorSettings.RunTasks(LocalizationStrings.TableMasterRead,
                        LocalizationStrings.DialogResultSuccess, LocalizationStrings.DialogResultFail, LocalizationStrings.DialogResultCancel,
                        new List<Task> { taskMaster }, GuiRefreshAsync);
                    break;
                case EnumTable.NomenclatureNonNormalize:
                    Task taskNonNormalize = new Task(GetDataNonNormilise);
                    await BlazorSettings.RunTasks(LocalizationStrings.TableNonNormalizeRead,
                        LocalizationStrings.DialogResultSuccess, LocalizationStrings.DialogResultFail, LocalizationStrings.DialogResultCancel,
                        new List<Task> { taskNonNormalize }, GuiRefreshAsync);
                    break;
            }
        }

        private void ClearEntity(EnumTable table)
        {
            switch (table)
            {
                case EnumTable.NomenclatureMaster:
                    ItemMaster = null;
                    break;
                case EnumTable.NomenclatureNonNormalize:
                    ItemNonNormilize = null;
                    break;
            }
        }

        private async Task ClearEntityAsync(EnumTable table)
        {
            Task task = new Task(() => ClearEntity(table));
            await BlazorSettings.RunTasks(LocalizationStrings.TableMasterClear,
                "", LocalizationStrings.DialogResultFail, "",
                new List<Task> { task }, GuiRefreshAsync).ConfigureAwait(false);
        }

        private void SetEntityRelevance(NomenclatureLightEntity entity, short? relevance)
        {
            if (entity == null || entity.EqualsDefault())
                return;
            BlazorSettings.SqlDataAccess.NomenclatureCrud.ExecQueryNative(
                "execute [MDM].[NomenclatureUpdateRelevance] :Id, :Value",
                new Dictionary<string, object> { { "Id", entity.Id }, { "Value", relevance } });

            GetDataAsync(EnumTable.NomenclatureNonNormalize).ConfigureAwait(true);
        }

        private async Task SetEntityRelevanceAsync(EnumTable table, short? relevance)
        {
            Task task = null;
            switch (table)
            {
                case EnumTable.NomenclatureMaster:
                    task = new Task(() => SetEntityRelevance(ItemMaster, relevance));
                    await BlazorSettings.RunTasksWithQeustion(relevance == 1 ? LocalizationStrings.TableMasterSetRelevanceTrue : LocalizationStrings.TableMasterSetRelevanceFalse,
                        "", LocalizationStrings.DialogResultFail, "",
                        new List<Task> { task }, GuiRefreshAsync, ItemMaster.Name)
                        .ConfigureAwait(false);
                    break;
                case EnumTable.NomenclatureNonNormalize:
                    task = new Task(() => SetEntityRelevance(ItemNonNormilize, relevance));
                    await BlazorSettings.RunTasksWithQeustion(relevance == 1 ? LocalizationStrings.TableNonNormalizeSetRelevanceTrue : LocalizationStrings.TableNonNormalizeSetRelevanceFalse,
                        "", LocalizationStrings.DialogResultFail, "",
                        new List<Task> { task }, GuiRefreshAsync, ItemNonNormilize.Name)
                        .ConfigureAwait(false);
                    break;
            }
        }

        private async Task ActionEditAsync(EnumTable table, NomenclatureLightEntity entity, bool isNewWindow)
        {
            Task task = null;
            string title = string.Empty;
            switch (table)
            {
                case EnumTable.NomenclatureMaster:
                    task = new Task(() => {
                        BlazorSettings.ActionAsync(table, EnumTableAction.Edit, entity, LocalizationStrings.UriRouteNomenclatureMaster, isNewWindow)
                            .ConfigureAwait(true);
                    });
                    title = LocalizationStrings.TableMasterEdit;
                    break;
                case EnumTable.NomenclatureNonNormalize:
                    task = new Task(() => {
                        BlazorSettings.ActionAsync(table, EnumTableAction.Edit, entity, LocalizationStrings.UriRouteNomenclatureNonNormilise, isNewWindow)
                            .ConfigureAwait(true);
                    });
                    title = LocalizationStrings.TableNonNormalizeEdit;
                    break;
            }
            await BlazorSettings.RunTasks(title, "", LocalizationStrings.DialogResultFail, "",
                new List<Task> { task }, null).ConfigureAwait(false);
        }

        private async Task ActionMasterEditAsync()
        {
            await ActionEditAsync(EnumTable.NomenclatureMaster, ItemMaster, true);
        }

        private void MasterRecordCreate()
        {
            if (ItemNonNormilize == null || ItemNonNormilize.EqualsDefault())
                return;
            BlazorSettings.SqlDataAccess.NomenclatureCrud.ExecQueryNative(
                "execute [MDM].[NomenclatureMasterRowMake] :Id",
                new Dictionary<string, object> { { "Id", ItemNonNormilize.Id } });

            GetDataAsync(EnumTable.NomenclatureMaster).ConfigureAwait(true);
            GetDataAsync(EnumTable.NomenclatureNonNormalize).ConfigureAwait(true);
        }

        private async Task MasterRecordCreateAsync()
        {
            Task task = new Task(MasterRecordCreate);
            await BlazorSettings.RunTasksWithQeustion(LocalizationStrings.TableMasterCreate,
                LocalizationStrings.DialogResultSuccess, LocalizationStrings.DialogResultFail, LocalizationStrings.DialogResultCancel,
                new List<Task> { task }, GuiRefreshAsync);
        }

        private void MasterRecordDelete()
        {
            if (ItemMaster == null || ItemMaster.EqualsDefault())
                return;
            BlazorSettings.SqlDataAccess.NomenclatureCrud.ExecQueryNative(
                "execute [MDM].[NomenclatureMasterRowRemove] :MasterId",
                new Dictionary<string, object> { { "MasterId", ItemMaster.MasterId } });

            GetDataAsync(EnumTable.NomenclatureMaster).ConfigureAwait(true);
            GetDataAsync(EnumTable.NomenclatureNonNormalize).ConfigureAwait(true);
        }

        private async Task MasterRecordDeleteAsync()
        {
            Task task = new Task(MasterRecordDelete);
            await BlazorSettings.RunTasksWithQeustion(LocalizationStrings.TableMasterDelete,
                LocalizationStrings.DialogResultSuccess, LocalizationStrings.DialogResultFail, LocalizationStrings.DialogResultCancel,
            new List<Task> { task }, GuiRefreshAsync);
        }

        private void MasterRecordInclude()
        {
            if (ItemMaster == null || ItemMaster.EqualsDefault())
                return;
            if (ItemNonNormilize == null || ItemNonNormilize.EqualsDefault())
                return;
            NomenclatureLightEntity storeEntityMaster = ItemMaster;
            BlazorSettings.SqlDataAccess.NomenclatureCrud.ExecQueryNative(
                "execute [MDM].[NomenclatureUnderRowInclude] :Id, :MasterId",
                new Dictionary<string, object>
                {
        { "Id", ItemNonNormilize.Id },
        { "MasterId", ItemMaster.Id },
                });

            GetDataAsync(EnumTable.NomenclatureMaster).ConfigureAwait(true);
            GetDataAsync(EnumTable.NomenclatureNonNormalize).ConfigureAwait(true);
            Notification.Notify(NotificationSeverity.Info, storeEntityMaster.Name);

            ItemMaster = storeEntityMaster;
        }

        private async Task MasterRecordIncludeAsync()
        {
            Task task = new Task(MasterRecordInclude);
            await BlazorSettings.RunTasksWithQeustion(LocalizationStrings.TableMasterInclude,
                LocalizationStrings.DialogResultSuccess, LocalizationStrings.DialogResultFail, LocalizationStrings.DialogResultCancel,
            new List<Task> { task }, GuiRefreshAsync);
        }

        private async Task OnChangeIsProduct()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            Task task = new Task(() => {
                UseIsProductNonNormilize = !UseIsProductNonNormilize;
                GetDataAsync(EnumTable.NomenclatureNonNormalize).ConfigureAwait(true);
            });
            await BlazorSettings.RunTasksWithQeustion(UseIsProductNonNormilize
                ? LocalizationStrings.TableNonNormalizeSetIsProductFalse
                : LocalizationStrings.TableNonNormalizeSetIsProductTrue,
                LocalizationStrings.DialogResultSuccess, LocalizationStrings.DialogResultFail, LocalizationStrings.DialogResultCancel,
                new List<Task> { task }, GuiRefreshAsync);
        }

        #endregion
    }
}