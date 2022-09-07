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
//    public partial class NomenclatureMaster
//    {
//        #region Public and private fields and properties

//        [Parameter]
//        public int? ItemId { get; set; }
//        private NomenclatureModel _includeEntity;
//        public NomenclatureModel IncludeModel
//        {
//            get => _includeEntity;
//            set
//            {
//                _includeEntity = value;
//                ObjectIncludeModel = _includeEntity;
//            }
//        }
//        public object ObjectIncludeModel { get; set; }
//        public IEnumerable<NomenclatureModel> IncludeEntities { get; set; }
//        public string IncludeEntitiesCount => $"{LocalizationStrings.TableRowsCount}: {IncludeEntities?.Count()}";

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

//            ItemId ??= 0;
//            await GetDataAsync().ConfigureAwait(false);
//        }

//        private void GetData()
//        {
//            IncludeEntities = BlazorSettings.SqlDataAccess.NomenclatureCrud.GetEntitiesAsIEnumerable(new (new Dictionary<DbField, object> {
//            //{ DbField.IsProduct, true },
//            { DbField.MasterId.ToString(), ItemId },
//        }),
//                new FieldOrderModel(DbField.Name, DbOrderDirection.Asc), 0);
//            IncludeEntities = IncludeEntities.Select(x => x).Where(x => x.MasterId != x.Id && x.InformationSystem.Id != 7).ToArray();
//        }

//        private async Task GetDataAsync()
//        {
//            Task task = new Task(GetData);
//            await BlazorSettings.RunTasks(LocalizationStrings.TableRead,
//                "", LocalizationStrings.DialogResultFail, "",
//            new List<Task> { task }, GuiRefreshAsync).ConfigureAwait(false);
//        }

//        private void OnChange(object value, string name)
//        {
//            StateHasChanged();
//        }

//        private void RowSelect(NomenclatureModel item)
//        {
//            IncludeModel = item;
//        }

//        private async Task RowSelectAsync(NomenclatureModel item)
//        {
//            Task task = new Task(() => RowSelect(item));
//            await BlazorSettings.RunTasks(LocalizationStrings.TableSelect, "", LocalizationStrings.DialogResultFail, "",
//                new List<Task> { task }, GuiRefreshAsync).ConfigureAwait(false);
//        }

//        private async Task RowDoubleClickAsync(NomenclatureModel item, bool isNewWindow)
//        {
//            Task task = new Task(() => ActionEditAsync(item, isNewWindow).ConfigureAwait(false));
//            await BlazorSettings.RunTasks(LocalizationStrings.TableEdit, "", LocalizationStrings.DialogResultFail, LocalizationStrings.DialogResultCancel,
//                new List<Task> { task }, GuiRefreshAsync).ConfigureAwait(false);
//        }

//        private async Task ActionEditAsync(NomenclatureModel item, bool isNewWindow)
//        {
//            Task task = new Task(() => { ActionEdit(TableDwh.Nomenclature, item, LocalizationStrings.UriRouteNomenclature, isNewWindow); });
//            await BlazorSettings.RunTasksWithQeustion(LocalizationStrings.TableEdit,
//                LocalizationStrings.DialogResultSuccess, LocalizationStrings.DialogResultFail, LocalizationStrings.DialogResultCancel,
//                new List<Task> { task }, GuiRefreshAsync, item?.Name).ConfigureAwait(false);
//        }

//        private void ExcludeModel(NomenclatureModel item)
//        {
//            if (item == null || item.EqualsDefault())
//                return;
//            BlazorSettings.SqlDataAccess.NomenclatureCrud.ExecQueryNative(
//                "execute [MDM].[NomenclatureSetNotRelevance] :Id",
//                new Dictionary<string, object> { { "Id", item.Id } });

//            GetDataAsync().ConfigureAwait(true);
//        }

//        private async Task ExcludeModelAsync(NomenclatureModel item)
//        {
//            Task task = new Task(() => { ExcludeModel(item); });
//            await BlazorSettings.RunTasksWithQeustion(LocalizationStrings.TableMasterExclude,
//                LocalizationStrings.DialogResultSuccess, LocalizationStrings.DialogResultFail, LocalizationStrings.DialogResultCancel,
//                new List<Task> { task }, GuiRefreshAsync, item?.Name).ConfigureAwait(false);
//        }

//        #endregion
//    }
//}