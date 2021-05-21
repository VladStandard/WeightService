using DeviceControl.Core.DAL;
using DeviceControl.Core.DAL.DataModels;
using DeviceControl.Core.DAL.TableModels;
using DeviceControl.Core.Models;
using Radzen;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Models
{
    public class AppSettings
    {
        #region Public and private fields and properties

        public NotificationService Notification { get; set; }
        public DialogService Dialog { get; set; }
        public DataAccessEntity DataAccess { get; set; }
        public EnumAccessRights AccessRights { get; set; }
        public bool ShowActionsButtons { get; set; }
        public bool ChartSmooth { get; set; }
        public bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
        public DataSourceEntity DataSource { get; set; } = new DataSourceEntity();

        #endregion

        #region Constructor and destructor

        public AppSettings() { }

        public void Setup(DataAccessConfig dataAccessService, NotificationService notification, DialogService dialog)
        {
            var appSettings = new AppSettingsEntity(dataAccessService.Server, dataAccessService.Db, dataAccessService.Trusted, dataAccessService.Username, dataAccessService.Password);
            DataAccess = new DataAccessEntity(appSettings);
            Notification = notification;
            AccessRights = EnumAccessRights.Guest;
            Dialog = dialog;
            ChartSmooth = false;
        }

        public async Task ActionAsync(EnumTable table, EnumTableAction tableAction, BaseEntity entity, BaseEntity parentEntity,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                entity = table switch
                {
                    EnumTable.BarCodeTypes => DataAccess.ActionGetEntity<BarCodeTypesEntity>(entity, tableAction),
                    EnumTable.Contragents => DataAccess.ActionGetEntity<ContragentsEntity>(entity, tableAction),
                    EnumTable.Hosts => DataAccess.ActionGetEntity<HostsEntity>(entity, tableAction),
                    EnumTable.Nomenclature => DataAccess.ActionGetEntity<NomenclatureEntity>(entity, tableAction),
                    EnumTable.OrderStatus => DataAccess.ActionGetEntity<OrderStatusEntity>(entity, tableAction),
                    EnumTable.OrderTypes => DataAccess.ActionGetEntity<OrderTypesEntity>(entity, tableAction),
                    EnumTable.Plu => DataAccess.ActionGetEntity<PluEntity>(entity, tableAction),
                    EnumTable.ProductionFacility => DataAccess.ActionGetEntity<ProductionFacilityEntity>(entity, tableAction),
                    EnumTable.ProductSeries => DataAccess.ActionGetEntity<ProductSeriesEntity>(entity, tableAction),
                    EnumTable.Scales => DataAccess.ActionGetEntity<ScalesEntity>(entity, tableAction),
                    EnumTable.TemplateResources => DataAccess.ActionGetEntity<TemplateResourcesEntity>(entity, tableAction),
                    EnumTable.Templates => DataAccess.ActionGetEntity<TemplatesEntity>(entity, tableAction),
                    EnumTable.WorkShop => DataAccess.ActionGetEntity<WorkshopEntity>(entity, tableAction),
                    EnumTable.WeithingFact => DataAccess.ActionGetEntity<WeithingFactEntity>(entity, tableAction),
                    EnumTable.ZebraPrinter => DataAccess.ActionGetEntity<ZebraPrinterEntity>(entity, tableAction),
                    EnumTable.ZebraPrinterResourceRef => DataAccess.ActionGetEntity<ZebraPrinterResourceRefEntity>(entity, tableAction),
                    EnumTable.PrinterType => DataAccess.ActionGetEntity<ZebraPrinterTypeEntity>(entity, tableAction),
                    _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
                };
                var title = table switch
                {
                    EnumTable.BarCodeTypes => $"Штрих-код. ID {entity.Id}",
                    EnumTable.Contragents => $"Контрагент. ID {entity.Id}",
                    EnumTable.Hosts => $"Хост. ID {entity.Id}",
                    EnumTable.Nomenclature => $"Номенклатура. ID {entity.Id}",
                    EnumTable.OrderStatus => $"Статус заказа. ID {entity.Id}",
                    EnumTable.OrderTypes => $"Тип заказа. ID {entity.Id}",
                    EnumTable.Plu => $"PLU. ID {entity.Id}",
                    EnumTable.ProductionFacility => $"Производственная площадка. ID {entity.Id}",
                    EnumTable.ProductSeries => $"Серия продукта. ID {entity.Id}",
                    EnumTable.Scales => $"Устройство. ID {entity.Id}",
                    EnumTable.TemplateResources => $"Ресурс шаблона. ID {entity.Id}",
                    EnumTable.Templates => $"Шаблон. ID {entity.Id}",
                    EnumTable.WorkShop => $"Цех. ID {entity.Id}",
                    EnumTable.WeithingFact => $"Взвешивание. ID {entity.Id}",
                    EnumTable.ZebraPrinter => $"Принтер Zebra. ID {entity.Id}",
                    EnumTable.ZebraPrinterResourceRef => $"Ресурс принтера. ID {entity.Id}",
                    EnumTable.PrinterType => $"Тип принтера. ID {entity.Id}",
                    _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
                };
                
                //Console.WriteLine($"ActionAsync. table: {table}");
                //Console.WriteLine($"table: {table}");
                //Console.WriteLine($"tableAction: {tableAction}");
                //Console.WriteLine($"entity: {entity}");
                //Console.WriteLine($"parentEntity: {parentEntity}");
                // Printer from ZebraPrinter.razor.
                if (entity is ZebraPrinterResourceRefEntity zebraPrinterResourceRefEntity)
                {
                    zebraPrinterResourceRefEntity.Printer = (ZebraPrinterEntity) parentEntity;
                }

                if (tableAction == EnumTableAction.Add)
                {
                    if (entity is PluEntity pluEntity)
                    {
                        pluEntity.Scale = (ScalesEntity) parentEntity;
                    }
                }

                switch (tableAction)
                {
                    case EnumTableAction.Add:
                    case EnumTableAction.Edit:
                    case EnumTableAction.Copy:
                        await Dialog.OpenAsync<BlazorDeviceControl.Components.EntityPage>(title,
                            new Dictionary<string, object>
                            {
                                {"Item", entity},
                                {"Table", table},
                                {"TableAction", tableAction},
                            },
                            new DialogOptions() {Width = "1400px", Height = "970px"}).ConfigureAwait(false);
                        break;
                    case EnumTableAction.Delete:
                        DataAccess.ActionDeleteEntity(entity);
                        break;
                    case EnumTableAction.Marked:
                        DataAccess.ActionMarkedEntity(entity);
                        break;
                }
            }
            catch (Exception ex)
            {
                var msg = new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Ошибка метода [{memberName}]!",
                    Detail = ex.Message,
                    Duration = Utils.LocalizationStrings.Timeout
                };
                Notification.Notify(msg);
                Console.WriteLine(ex.Message);
                Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
                DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        public string ChartDataFormat(object value)
        {
            return ((int)value).ToString("####", CultureInfo.InvariantCulture);
        }

        public ChartCountEntity[] GetContragentsChartEntities(EnumField field)
        {
            var result = new ChartCountEntity[0];
            var entities = DataAccess.ContragentsCrud.GetEntities(null,
                new FieldOrderEntity(EnumField.CreateDate, EnumOrderDirection.Asc));
            var i = 0;
            switch (field)
            {
                case EnumField.CreateDate:
                    var entitiesDateCreated = new List<ChartCountEntity>();
                    foreach (var entity in entities)
                    {
                        if (entity.CreateDate != null)
                            entitiesDateCreated.Add(new ChartCountEntity(((DateTime)entity.CreateDate).Date, 1));
                        i++;
                    }
                    var entitiesGroupCreated = entitiesDateCreated.GroupBy(entity => entity.Date).ToArray();
                    result = new ChartCountEntity[entitiesGroupCreated.Length];
                    i = 0;
                    foreach (var entity in entitiesGroupCreated)
                    {
                        result[i] = new ChartCountEntity(entity.Key, entity.Count());
                        i++;
                    }
                    break;
                case EnumField.ModifiedDate:
                    var entitiesDateModified = new List<ChartCountEntity>();
                    foreach (var entity in entities)
                    {
                        if (entity.ModifiedDate != null)
                            entitiesDateModified.Add(new ChartCountEntity(((DateTime)entity.ModifiedDate).Date, 1));
                        i++;
                    }
                    var entitiesGroupModified = entitiesDateModified.GroupBy(entity => entity.Date).ToArray();
                    result = new ChartCountEntity[entitiesGroupModified.Length];
                    i = 0;
                    foreach (var entity in entitiesGroupModified)
                    {
                        result[i] = new ChartCountEntity(entity.Key, entity.Count());
                        i++;
                    }
                    break;
            }
            return result;
        }

        public ChartCountEntity[] GetNomenclaturesChartEntities(EnumField field)
        {
            var result = new ChartCountEntity[0];
            var entities = DataAccess.NomenclatureCrud.GetEntities(null,
                new FieldOrderEntity(EnumField.CreateDate, EnumOrderDirection.Asc));
            var i = 0;
            switch (field)
            {
                case EnumField.CreateDate:
                    var entitiesDateCreated = new List<ChartCountEntity>();
                    foreach (var entity in entities)
                    {
                        if (entity.CreateDate != null)
                            entitiesDateCreated.Add(new ChartCountEntity(((DateTime)entity.CreateDate).Date, 1));
                        i++;
                    }
                    var entitiesGroupCreated = entitiesDateCreated.GroupBy(entity => entity.Date).ToArray();
                    result = new ChartCountEntity[entitiesGroupCreated.Length];
                    i = 0;
                    foreach (var entity in entitiesGroupCreated)
                    {
                        result[i] = new ChartCountEntity(entity.Key, entity.Count());
                        i++;
                    }
                    break;
                case EnumField.ModifiedDate:
                    var entitiesDateModified = new List<ChartCountEntity>();
                    foreach (var entity in entities)
                    {
                        if (entity.ModifiedDate != null)
                            entitiesDateModified.Add(new ChartCountEntity(((DateTime)entity.ModifiedDate).Date, 1));
                        i++;
                    }
                    
                    var entitiesModied = entitiesDateModified.GroupBy(entity => entity.Date).ToArray();
                    result = new ChartCountEntity[entitiesModied.Length];
                    i = 0;
                    foreach (var entity in entitiesModied)
                    {
                        result[i] = new ChartCountEntity(entity.Key, entity.Count());
                        i++;
                    }

                    break;
            }
            return result;
        }

        #endregion
    }
}
