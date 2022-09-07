//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using DataProjectsCore.DAL.TableDwhModels;
//using MdmControlBlazor.Utils;
//using Microsoft.AspNetCore.Components;
//using Radzen;

//namespace MdmControlBlazor.Components
//{
//    public partial class Nomenclatures
//    {
//        #region Public and private fields and properties

//        private NomenclatureLightEntity _itemMaster;
//        public NomenclatureLightEntity ItemMaster
//        {
//            get => _itemMaster;
//            set
//            {
//                _itemMaster = value;
//                ObjectMaster = _itemMaster;
//            }
//        }
//        public object ObjectMaster { get; set; }
//        private NomenclatureLightEntity _itemNonNormilize;
//        public NomenclatureLightEntity ItemNonNormilize
//        {
//            get => _itemNonNormilize;
//            set
//            {
//                _itemNonNormilize = value;
//                ObjectNonNormilize = _itemNonNormilize;
//            }
//        }
//        public object ObjectNonNormilize { get; set; }
//        public IEnumerable<NomenclatureLightEntity> ItemsMaster { get; set; }
//        public string ItemsMasterCount => $"{LocalizationStrings.TableRowsCount}: {ItemsMaster?.Count()}";
//        public IEnumerable<NomenclatureLightEntity> ItemsNonNormilize { get; set; }
//        public string ItemsNonNormilizeCount => $"{LocalizationStrings.TableRowsCount}: {ItemsNonNormilize?.Count()}";
//        public bool UseIsProductNonNormilize { get; set; } = true;

//        #endregion

//        #region Public and private methods

//        private async Task GuiRefreshAsync()
//        {
//            await InvokeAsync(StateHasChanged).ConfigureAwait(false);
//        }

//        protected override async Task OnInitializedAsync()
//        {
//            await base.OnInitializedAsync().ConfigureAwait(true);

//            BlazorSettings.Setup(JsonAppSettings, Notification, Dialog, Navigation, Tooltip, JsRuntime);
//        }

//        public override async Task SetParametersAsync(ParameterView parameters)
//        {
//            await base.SetParametersAsync(parameters);

//            await GetDataAsync(TableDwh.NomenclatureMaster).ConfigureAwait(false);
//            await GetDataAsync(TableDwh.NomenclatureNonNormalize).ConfigureAwait(false);
//        }

//        private void RowSelect(TableDwh table, NomenclatureLightEntity item)
//        {
//            switch (table)
//            {
//                case TableDwh.NomenclatureMaster:
//                    ItemMaster = item;
//                    break;
//                case TableDwh.NomenclatureNonNormalize:
//                    ItemNonNormilize = item;
//                    break;
//            }
//        }

//        private async Task RowSelectAsync(TableDwh table, NomenclatureLightEntity item)
//        {
//            Task task = new Task(() => RowSelect(table, item));
//            await BlazorSettings.RunTasks(LocalizationStrings.TableSelect, "", LocalizationStrings.DialogResultFail, "",
//                new List<Task> { task }, GuiRefreshAsync).ConfigureAwait(false);
//        }

//        private async Task RowDoubleClickAsync(TableDwh table, NomenclatureLightEntity item, bool isNewWindow)
//        {
//            Task task = new Task(() => ActionEditAsync(table, item, isNewWindow).ConfigureAwait(false));
//            await BlazorSettings.RunTasks(LocalizationStrings.TableEdit, "", LocalizationStrings.DialogResultFail, "",
//            new List<Task> { task }, GuiRefreshAsync).ConfigureAwait(false);
//        }

//        private void GetDataMaster()
//        {
//            ItemMaster = null;
//            ItemsMaster = BlazorSettings.SqlDataAccess.NomenclatureLightCrud.GetEntitiesAsIEnumerable(new (new Dictionary<DbField, object>{
//            { DbField.IsProduct, true },
//            { $"InformationSystem.{DbField.IdentityId}", 7 },
//            }),
//                new FieldOrderModel(DbField.Name, DbOrderDirection.Asc), 0);
//        }

//        private void GetDataNonNormilise()
//        {
//            ItemNonNormilize = null;
//            ItemsNonNormilize = new List<NomenclatureLightEntity>();
//            if (UseIsProductNonNormilize)
//                ItemsNonNormilize = BlazorSettings.SqlDataAccess.NomenclatureLightCrud.GetEntitiesAsIEnumerable(new (new Dictionary<DbField, object>{
//                { DbField.IsProduct, true },
//                { DbField.MasterId, null },
//                }),
//                    new FieldOrderModel(DbField.Name, DbOrderDirection.Asc), 0);
//            else
//                ItemsNonNormilize = BlazorSettings.SqlDataAccess.NomenclatureLightCrud.GetEntitiesAsIEnumerable(new (new Dictionary<DbField, object>{
//                { DbField.MasterId, null },
//                }),
//                    new FieldOrderModel(DbField.Name, DbOrderDirection.Asc), 0);
//        }

//        private async Task GetDataAsync(TableDwh table)
//        {
//            switch (table)
//            {
//                case TableDwh.NomenclatureMaster:
//                    Task taskMaster = new Task(GetDataMaster);
//                    await BlazorSettings.RunTasks(LocalizationStrings.TableMasterRead,
//                        LocalizationStrings.DialogResultSuccess, LocalizationStrings.DialogResultFail, LocalizationStrings.DialogResultCancel,
//                        new List<Task> { taskMaster }, GuiRefreshAsync);
//                    break;
//                case TableDwh.NomenclatureNonNormalize:
//                    Task taskNonNormalize = new Task(GetDataNonNormilise);
//                    await BlazorSettings.RunTasks(LocalizationStrings.TableNonNormalizeRead,
//                        LocalizationStrings.DialogResultSuccess, LocalizationStrings.DialogResultFail, LocalizationStrings.DialogResultCancel,
//                        new List<Task> { taskNonNormalize }, GuiRefreshAsync);
//                    break;
//            }
//        }

//        private void ClearEntity(TableDwh table)
//        {
//            switch (table)
//            {
//                case TableDwh.NomenclatureMaster:
//                    ItemMaster = null;
//                    break;
//                case TableDwh.NomenclatureNonNormalize:
//                    ItemNonNormilize = null;
//                    break;
//            }
//        }

//        private async Task ClearEntityAsync(TableDwh table)
//        {
//            Task task = new Task(() => ClearEntity(table));
//            await BlazorSettings.RunTasks(LocalizationStrings.TableMasterClear,
//                "", LocalizationStrings.DialogResultFail, "",
//                new List<Task> { task }, GuiRefreshAsync).ConfigureAwait(false);
//        }

//        private void SetEntityRelevance(NomenclatureLightEntity item, short? relevance)
//        {
//            if (item == null || item.EqualsDefault())
//                return;
//            BlazorSettings.SqlDataAccess.NomenclatureCrud.ExecQueryNative(
//                "execute [MDM].[NomenclatureUpdateRelevance] :Id, :Value",
//                new Dictionary<string, object> { { "Id", item.Id }, { "Value", relevance } });

//            GetDataAsync(TableDwh.NomenclatureNonNormalize).ConfigureAwait(true);
//        }

//        private async Task SetEntityRelevanceAsync(TableDwh table, short? relevance)
//        {
//            Task task = null;
//            switch (table)
//            {
//                case TableDwh.NomenclatureMaster:
//                    task = new Task(() => SetEntityRelevance(ItemMaster, relevance));
//                    await BlazorSettings.RunTasksWithQeustion(relevance == 1 ? LocalizationStrings.TableMasterSetRelevanceTrue : LocalizationStrings.TableMasterSetRelevanceFalse,
//                        "", LocalizationStrings.DialogResultFail, "",
//                        new List<Task> { task }, GuiRefreshAsync, ItemMaster.Name)
//                        .ConfigureAwait(false);
//                    break;
//                case TableDwh.NomenclatureNonNormalize:
//                    task = new Task(() => SetEntityRelevance(ItemNonNormilize, relevance));
//                    await BlazorSettings.RunTasksWithQeustion(relevance == 1 ? LocalizationStrings.TableNonNormalizeSetRelevanceTrue : LocalizationStrings.TableNonNormalizeSetRelevanceFalse,
//                        "", LocalizationStrings.DialogResultFail, "",
//                        new List<Task> { task }, GuiRefreshAsync, ItemNonNormilize.Name)
//                        .ConfigureAwait(false);
//                    break;
//            }
//        }

//        private async Task ActionEditAsync(TableDwh table, NomenclatureLightEntity item, bool isNewWindow)
//        {
//            Task task = null;
//            string title = string.Empty;
//            switch (table)
//            {
//                case TableDwh.NomenclatureMaster:
//                    task = new Task(() => {
//                        BlazorSettings.ActionAsync(table, DbTableAction.Edit, item, LocalizationStrings.UriRouteNomenclatureMaster, isNewWindow)
//                            .ConfigureAwait(true);
//                    });
//                    title = LocalizationStrings.TableMasterEdit;
//                    break;
//                case TableDwh.NomenclatureNonNormalize:
//                    task = new Task(() => {
//                        BlazorSettings.ActionAsync(table, DbTableAction.Edit, item, LocalizationStrings.UriRouteNomenclatureNonNormilise, isNewWindow)
//                            .ConfigureAwait(true);
//                    });
//                    title = LocalizationStrings.TableNonNormalizeEdit;
//                    break;
//            }
//            await BlazorSettings.RunTasks(title, "", LocalizationStrings.DialogResultFail, "",
//                new List<Task> { task }, null).ConfigureAwait(false);
//        }

//        private async Task ActionMasterEditAsync()
//        {
//            await ActionEditAsync(TableDwh.NomenclatureMaster, ItemMaster, true);
//        }

//        private void MasterRecordCreate()
//        {
//            if (ItemNonNormilize == null || ItemNonNormilize.EqualsDefault())
//                return;
//            BlazorSettings.SqlDataAccess.NomenclatureCrud.ExecQueryNative(
//                "execute [MDM].[NomenclatureMasterRowMake] :Id",
//                new Dictionary<string, object> { { "Id", ItemNonNormilize.Id } });

//            GetDataAsync(TableDwh.NomenclatureMaster).ConfigureAwait(true);
//            GetDataAsync(TableDwh.NomenclatureNonNormalize).ConfigureAwait(true);
//        }

//        private async Task MasterRecordCreateAsync()
//        {
//            Task task = new Task(MasterRecordCreate);
//            await BlazorSettings.RunTasksWithQeustion(LocalizationStrings.TableMasterCreate,
//                LocalizationStrings.DialogResultSuccess, LocalizationStrings.DialogResultFail, LocalizationStrings.DialogResultCancel,
//                new List<Task> { task }, GuiRefreshAsync);
//        }

//        private void MasterRecordDelete()
//        {
//            if (ItemMaster == null || ItemMaster.EqualsDefault())
//                return;
//            BlazorSettings.SqlDataAccess.NomenclatureCrud.ExecQueryNative(
//                "execute [MDM].[NomenclatureMasterRowRemove] :MasterId",
//                new Dictionary<string, object> { { "MasterId", ItemMaster.MasterId } });

//            GetDataAsync(TableDwh.NomenclatureMaster).ConfigureAwait(true);
//            GetDataAsync(TableDwh.NomenclatureNonNormalize).ConfigureAwait(true);
//        }

//        private async Task MasterRecordDeleteAsync()
//        {
//            Task task = new Task(MasterRecordDelete);
//            await BlazorSettings.RunTasksWithQeustion(LocalizationStrings.TableMasterDelete,
//                LocalizationStrings.DialogResultSuccess, LocalizationStrings.DialogResultFail, LocalizationStrings.DialogResultCancel,
//            new List<Task> { task }, GuiRefreshAsync);
//        }

//        private void MasterRecordInclude()
//        {
//            if (ItemMaster == null || ItemMaster.EqualsDefault())
//                return;
//            if (ItemNonNormilize == null || ItemNonNormilize.EqualsDefault())
//                return;
//            NomenclatureLightEntity storeEntityMaster = ItemMaster;
//            BlazorSettings.SqlDataAccess.NomenclatureCrud.ExecQueryNative(
//                "execute [MDM].[NomenclatureUnderRowInclude] :Id, :MasterId",
//                new Dictionary<string, object>
//                {
//        { "Id", ItemNonNormilize.Id },
//        { "MasterId", ItemMaster.Id },
//                });

//            GetDataAsync(TableDwh.NomenclatureMaster).ConfigureAwait(true);
//            GetDataAsync(TableDwh.NomenclatureNonNormalize).ConfigureAwait(true);
//            Notification.Notify(NotificationSeverity.Info, storeEntityMaster.Name);

//            ItemMaster = storeEntityMaster;
//        }

//        private async Task MasterRecordIncludeAsync()
//        {
//            Task task = new Task(MasterRecordInclude);
//            await BlazorSettings.RunTasksWithQeustion(LocalizationStrings.TableMasterInclude,
//                LocalizationStrings.DialogResultSuccess, LocalizationStrings.DialogResultFail, LocalizationStrings.DialogResultCancel,
//            new List<Task> { task }, GuiRefreshAsync);
//        }

//        private async Task OnChangeIsProduct()
//        {
//            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

//            Task task = new Task(() => {
//                UseIsProductNonNormilize = !UseIsProductNonNormilize;
//                GetDataAsync(TableDwh.NomenclatureNonNormalize).ConfigureAwait(true);
//            });
//            await BlazorSettings.RunTasksWithQeustion(UseIsProductNonNormilize
//                ? LocalizationStrings.TableNonNormalizeSetIsProductFalse
//                : LocalizationStrings.TableNonNormalizeSetIsProductTrue,
//                LocalizationStrings.DialogResultSuccess, LocalizationStrings.DialogResultFail, LocalizationStrings.DialogResultCancel,
//                new List<Task> { task }, GuiRefreshAsync);
//        }

//        #endregion
//    }
//}