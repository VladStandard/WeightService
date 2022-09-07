//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore;
//using DataCore.Sql.Models;
//using DataCore.Sql.TableDwhModels;
//using DataCore.Sql.TableScaleModels;
//using MdmControlBlazor.Utils;
//using MdmControlCore;
//using MdmControlCore.Utils;
//using Microsoft.AspNetCore.Components;
//using System;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using System.Threading.Tasks;

//namespace MdmControlBlazor.Components
//{
//    public partial class Nomenclature
//    {
//        #region Public and private fields and properties

//        [Parameter]
//        public long? ItemId { get; set; }
//        public NomenclatureModel Item { get; set; }
//        public IEnumerable<BrandModel> BrandEntities { get; set; }
//        public IEnumerable<InformationSystemModel> InformationSystemEntities { get; set; }
//        public IEnumerable<NomenclatureGroupModel> NomenclatureGroupEntities { get; set; }
//        public IEnumerable<NomenclatureGroupModel> NomenclatureGroupCostEntities { get; set; }
//        public IEnumerable<NomenclatureTypeModel> NomenclatureTypeEntities { get; set; }
//        public IEnumerable<StatusModel> StatusEntities { get; set; }
//        public IEnumerable<TypeModel<short>> RelevanceStatuses { get; set; }
//        public IEnumerable<TypeModel<short>> NormilizationStatuses { get; set; }

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
//            Item = BlazorSettings.SqlDataAccess.NomenclatureCrud.GetItem(new (new Dictionary<DbField, object?>
//            { { DbField.Id, ItemId } }));

//            BrandEntities = BlazorSettings.SqlDataAccess.BrandCrud.GetEntities(null,
//                new FieldOrderModel(DbField.Name, DbOrderDirection.Asc), 0);

//            InformationSystemEntities = BlazorSettings.SqlDataAccess.InformationSystemCrud.GetEntities(null,
//                new FieldOrderModel(DbField.Name, DbOrderDirection.Asc), 0);

//            NomenclatureGroupEntities = BlazorSettings.SqlDataAccess.NomenclatureGroupCrud.GetEntities(null,
//                new FieldOrderModel(DbField.Name, DbOrderDirection.Asc), 0);

//            NomenclatureGroupCostEntities = BlazorSettings.SqlDataAccess.NomenclatureGroupCrud.GetEntities(null,
//                new FieldOrderModel(DbField.Name, DbOrderDirection.Asc), 0);

//            NomenclatureTypeEntities = BlazorSettings.SqlDataAccess.NomenclatureTypeCrud.GetEntities(null,
//                new FieldOrderModel(DbField.Name, DbOrderDirection.Asc), 0);

//            StatusEntities = BlazorSettings.SqlDataAccess.StatusCrud.GetEntities(null,
//                new FieldOrderModel(DbField.Name, DbOrderDirection.Asc), 0);

//            RelevanceStatuses = UtilsEnum.GetEnumRelevenaceStatusesRus();
//            NormilizationStatuses = UtilsEnum.GetEnumNormilizationStatusesRus();
//        }

//        private async Task GetDataAsync([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
//        {
//            Task task = new Task(GetData);
//            await BlazorSettings.RunTasks(LocalizationStrings.TableRead,
//                "", LocalizationStrings.DialogResultFail, "",
//            new List<Task> { task }, GuiRefreshAsync, filePath, lineNumber, memberName).ConfigureAwait(false);
//        }

//        private void OnChange(object value, string name)
//        {
//            switch (name)
//            {
//                case "Brands":
//                    if (value is long idBrand)
//                    {
//                        Item.Brand = BlazorSettings.SqlDataAccess.BrandCrud.GetItem(
//                            new (new Dictionary<DbField, object> { { DbField.Id, idBrand } }));
//                        Item.BrandBytes = Item.Brand.CodeInIs;
//                    }
//                    break;
//                case "NomenclatureGroups":
//                    if (value is long idNomenclatureGroup)
//                    {
//                        Item.NomenclatureGroup = BlazorSettings.SqlDataAccess.NomenclatureGroupCrud.GetItem(
//                            new (new Dictionary<DbField, object> { { DbField.Id, idNomenclatureGroup } }), null);
//                        Item.NomenclatureGroupBytes = Item.NomenclatureGroup.CodeInIs;
//                    }
//                    break;
//                case "NomenclatureGroupsCost":
//                    if (value is long idNomenclatureGroupCost)
//                    {
//                        Item.NomenclatureGroupCost = BlazorSettings.SqlDataAccess.NomenclatureGroupCrud.GetItem(
//                            new (new Dictionary<DbField, object> { { DbField.Id, idNomenclatureGroupCost } }));
//                        Item.NomenclatureGroupCostBytes = Item.NomenclatureGroupCost.CodeInIs;
//                    }
//                    break;
//                case "NomenclatureTypes":
//                    if (value is long idNomenclatureTypes)
//                    {
//                        Item.NomenclatureType = BlazorSettings.SqlDataAccess.NomenclatureTypeCrud.GetItem(
//                            new (new Dictionary<DbField, object> { { DbField.Id, idNomenclatureTypes } }));
//                        Item.NomenclatureTypeBytes = Item.NomenclatureType.CodeInIs;
//                    }
//                    break;
//                case "Statuses":
//                    if (value is long idStatus)
//                    {
//                        Item.Status = BlazorSettings.SqlDataAccess.StatusCrud.GetItem(
//                            new (new Dictionary<DbField, object> { { DbField.Id, idStatus } }));
//                    }
//                    break;
//                case "InformationSystems":
//                    if (value is long idInformationSystem)
//                    {
//                        Item.InformationSystem = BlazorSettings.SqlDataAccess.InformationSystemCrud.GetItem(
//                            new (new Dictionary<DbField, object> { { DbField.Id, idInformationSystem } }));
//                    }
//                    break;
//                case "RelevanceStatuses":
//                    if (value is short relevanceStatus)
//                    {
//                        Item.RelevanceStatus = relevanceStatus;
//                    }
//                    break;
//                case "NormilizationStatuses":
//                    if (value is short normilizationStatus)
//                    {
//                        Item.NormalizationStatus = normilizationStatus;
//                    }
//                    break;
//            }
//            StateHasChanged();
//        }

//        private void Save()
//        {
//            if (Item == null || Item.EqualsDefault())
//                return;
//            if (ItemId == 0)
//            {
//                BlazorSettings.SqlDataAccess.NomenclatureCrud.SaveEntity(Item);
//            }
//            else
//            {
//                BlazorSettings.SqlDataAccess.NomenclatureCrud.UpdateEntity(Item);
//            }
//            Navigation.NavigateTo($"{LocalizationStrings.UriRouteNomenclatures}");
//        }

//        private async Task SaveAsync()
//        {
//            Task task = new Task(Save);
//            await BlazorSettings.RunTasksWithQeustion(LocalizationStrings.TableRecordSave,
//                LocalizationStrings.DialogResultSuccess, LocalizationStrings.DialogResultFail, LocalizationStrings.DialogResultCancel,
//                new List<Task> { task }, GuiRefreshAsync);
//        }

//        private void Cancel()
//        {
//            if (Item == null || Item.EqualsDefault())
//                return;
//            Navigation.NavigateTo($"{LocalizationStrings.UriRouteNomenclatures}");
//        }

//        private async Task CancelAsync()
//        {
//            Task task = new Task(Cancel);
//            await BlazorSettings.RunTasks(LocalizationStrings.TableMasterCancel,
//                LocalizationStrings.DialogResultSuccess, LocalizationStrings.DialogResultFail, LocalizationStrings.DialogResultCancel,
//                new List<Task> { task }, GuiRefreshAsync);
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