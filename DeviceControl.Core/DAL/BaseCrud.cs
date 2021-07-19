using DeviceControl.Core.DAL.DataModels;
using DeviceControl.Core.DAL.TableModels;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DeviceControl.Core.DAL
{
    public class BaseCrud<T> where T : BaseEntity, new()
    {
        #region Public and private fields and properties

        public DataAccessEntity DataAccess;

        #endregion

        #region Constructor and destructor

        public BaseCrud(DataAccessEntity dataAccess)
        {
            DataAccess = dataAccess;
        }

        #endregion

        #region Public and private methods

        public void FillReferences(T entity)
        {
            // Datas.
            if (typeof(T) == typeof(DeviceEntity))
            {
                var deviceEntity = (DeviceEntity)(object)entity;
                if (!deviceEntity.EqualsEmpty())
                {
                    if (deviceEntity.Scales != null)
                        deviceEntity.Scales = DataAccess.ScalesCrud.GetEntity(deviceEntity.Scales.Id);
                }
            }
            // Tables.
            else if (typeof(T) == typeof(AppEntity))
            {
                //
            }
            else if (typeof(T) == typeof(BarCodeTypesEntity))
            {
                var barCodeTypesEntity = (BarCodeTypesEntity)(object)entity;
                if (!barCodeTypesEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(ContragentsEntity))
            {
                var contragentsEntity = (ContragentsEntity)(object)entity;
                if (!contragentsEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(LabelsEntity))
            {
                var labelsEntity = (LabelsEntity)(object)entity;
                if (!labelsEntity.EqualsEmpty())
                {
                    if (labelsEntity.WeithingFact != null)
                        labelsEntity.WeithingFact = DataAccess.WeithingFactCrud.GetEntity(labelsEntity.WeithingFact.Id);
                }
            }
            else if (typeof(T) == typeof(LogEntity))
            {
                var logEntity = (LogEntity)(object)entity;
                if (!logEntity.EqualsEmpty())
                {
                    if (logEntity.App != null)
                        logEntity.App = DataAccess.AppCrud.GetEntity(logEntity.App.Uid);
                    if (logEntity.Host != null)
                        logEntity.Host = DataAccess.HostsCrud.GetEntity(logEntity.Host.Id);
                }
            }
            else if (typeof(T) == typeof(NomenclatureEntity))
            {
                var nomenclatureEntity = (NomenclatureEntity)(object)entity;
                if (!nomenclatureEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(OrdersEntity))
            {
                var ordersEntity = (OrdersEntity)(object)entity;
                if (!ordersEntity.EqualsEmpty())
                {
                    if (ordersEntity.OrderTypes != null)
                        ordersEntity.OrderTypes = DataAccess.OrderTypesCrud.GetEntity(ordersEntity.OrderTypes.Id);
                    if (ordersEntity.Scales != null)
                        ordersEntity.Scales = DataAccess.ScalesCrud.GetEntity(ordersEntity.Scales.Id);
                    if (ordersEntity.Plu != null)
                        ordersEntity.Plu = DataAccess.PluCrud.GetEntity(ordersEntity.Plu.Id);
                    if (ordersEntity.Templates != null)
                        ordersEntity.Templates = DataAccess.TemplatesCrud.GetEntity(ordersEntity.Templates.Id);
                }
            }
            else if (typeof(T) == typeof(OrderStatusEntity))
            {
                var orderStatusEntity = (OrderStatusEntity)(object)entity;
                if (!orderStatusEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(OrderTypesEntity))
            {
                var orderTypesEntity = (OrderTypesEntity)(object)entity;
                if (!orderTypesEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(PluEntity))
            {
                var pluEntity = (PluEntity)(object)entity;
                if (!pluEntity.EqualsEmpty())
                {
                    if (pluEntity.Templates != null)
                        pluEntity.Templates = DataAccess.TemplatesCrud.GetEntity(pluEntity.Templates.Id);
                    if (pluEntity.Scale != null)
                        pluEntity.Scale = DataAccess.ScalesCrud.GetEntity(pluEntity.Scale.Id);
                    if (pluEntity.Nomenclature != null)
                        pluEntity.Nomenclature = DataAccess.NomenclatureCrud.GetEntity(pluEntity.Nomenclature.Id);
                }
            }
            else if (typeof(T) == typeof(ProductionFacilityEntity))
            {
                var productionFacilityEntity = (ProductionFacilityEntity)(object)entity;
                if (!productionFacilityEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(ProductSeriesEntity))
            {
                var productSeriesEntity = (ProductSeriesEntity)(object)entity;
                if (!productSeriesEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(ScalesEntity))
            {
                var scalesEntity = (ScalesEntity)(object)entity;
                if (!scalesEntity.EqualsEmpty())
                {
                    if (scalesEntity.TemplateDefault != null)
                        scalesEntity.TemplateDefault = DataAccess.TemplatesCrud.GetEntity(scalesEntity.TemplateDefault.Id);
                    if (scalesEntity.TemplateSeries != null)
                        scalesEntity.TemplateSeries = DataAccess.TemplatesCrud.GetEntity(scalesEntity.TemplateSeries.Id);
                    if (scalesEntity.WorkShop != null)
                        scalesEntity.WorkShop = DataAccess.WorkshopCrud.GetEntity(scalesEntity.WorkShop.Id);
                    if (scalesEntity.Printer != null)
                        scalesEntity.Printer = DataAccess.ZebraPrinterCrud.GetEntity(scalesEntity.Printer.Id);
                    if (scalesEntity.Host != null)
                        scalesEntity.Host = DataAccess.HostsCrud.GetEntity(scalesEntity.Host.Id);
                }
            }
            else if (typeof(T) == typeof(TemplateResourcesEntity))
            {
                var templateResourcesEntity = (TemplateResourcesEntity)(object)entity;
                if (!templateResourcesEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TemplatesEntity))
            {
                var templatesEntity = (TemplatesEntity)(object)entity;
                if (!templatesEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(WeithingFactEntity))
            {
                var weithingFactEntity = (WeithingFactEntity)(object)entity;
                if (!weithingFactEntity.EqualsEmpty())
                {
                    if (weithingFactEntity.Plu != null)
                        weithingFactEntity.Plu = DataAccess.PluCrud.GetEntity(weithingFactEntity.Plu.Id);
                    if (weithingFactEntity.Scales != null)
                        weithingFactEntity.Scales = DataAccess.ScalesCrud.GetEntity(weithingFactEntity.Scales.Id);
                    if (weithingFactEntity.Series != null)
                        weithingFactEntity.Series = DataAccess.ProductSeriesCrud.GetEntity(weithingFactEntity.Series.Id);
                    if (weithingFactEntity.Orders != null)
                        weithingFactEntity.Orders = DataAccess.OrdersCrud.GetEntity(weithingFactEntity.Orders.Id);
                }
            }
            else if (typeof(T) == typeof(WorkshopEntity))
            {
                var workshopEntity = (WorkshopEntity)(object)entity;
                if (!workshopEntity.EqualsEmpty())
                {
                    if (workshopEntity.ProductionFacility != null)
                        workshopEntity.ProductionFacility = DataAccess.ProductionFacilityCrud.GetEntity(workshopEntity.ProductionFacility.Id);
                }
            }
            else if (typeof(T) == typeof(ZebraPrinterEntity))
            {
                var zebraPrinterEntity = (ZebraPrinterEntity)(object)entity;
                if (!zebraPrinterEntity.EqualsEmpty())
                {
                    if (zebraPrinterEntity.PrinterType != null)
                        zebraPrinterEntity.PrinterType = DataAccess.ZebraPrinterTypeCrud.GetEntity(zebraPrinterEntity.PrinterType.Id);
                }
            }
            else if (typeof(T) == typeof(ZebraPrinterResourceRefEntity))
            {
                var zebraPrinterResourceRefEntity = (ZebraPrinterResourceRefEntity)(object)entity;
                if (!zebraPrinterResourceRefEntity.EqualsEmpty())
                {
                    if (zebraPrinterResourceRefEntity.Printer != null)
                        zebraPrinterResourceRefEntity.Printer =
                            DataAccess.ZebraPrinterCrud.GetEntity(zebraPrinterResourceRefEntity.Printer.Id);
                    if (zebraPrinterResourceRefEntity.Resource != null)
                        zebraPrinterResourceRefEntity.Resource =
                            DataAccess.TemplateResourcesCrud.GetEntity(zebraPrinterResourceRefEntity.Resource.Id);
                }
            }
            else if (typeof(T) == typeof(ZebraPrinterTypeEntity))
            {
                var zebraPrinterTypeEntity = (ZebraPrinterTypeEntity)(object)entity;
                if (!zebraPrinterTypeEntity.EqualsEmpty())
                {
                    //
                }
            }
        }

        public T GetEntity(FieldListEntity fieldList, FieldOrderEntity order,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            T entity = DataAccess.GetEntity<T>(fieldList, order, filePath, lineNumber, memberName);
            FillReferences(entity);
            return entity;
        }

        public T GetEntity(int id)
        {
            return GetEntity(
                new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), id } }),
                new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc));
        }

        public T GetEntity(Guid uid)
        {
            return GetEntity(
                new FieldListEntity(new Dictionary<string, object> { { EnumField.Uid.ToString(), uid } }),
                new FieldOrderEntity(EnumField.Uid, EnumOrderDirection.Desc));
        }

        public T[] GetEntities(FieldListEntity fieldList, FieldOrderEntity order, int maxResults = 0,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            var entities = DataAccess.GetEntities<T>(fieldList, order, maxResults, filePath, lineNumber, memberName);
            foreach (var entity in entities)
            {
                FillReferences(entity);
            }
            return entities;
        }

        //public T[] GetEntitiesNative(string[] fieldsSelect, string from, object[] valuesParams,
        //    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        //{
        //    return DataAccess.GetEntitiesNative<T>(fieldsSelect, from, valuesParams, filePath, lineNumber, memberName);
        //}

        public T[] GetEntitiesNativeMapping(string query,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            return DataAccess.GetEntitiesNativeMapping<T>(query, filePath, lineNumber, memberName);
        }

        public object[] GetEntitiesNativeObject(string query,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            return DataAccess.GetEntitiesNativeObject(query, filePath, lineNumber, memberName);
        }

        public int ExecQueryNative(string query, Dictionary<string, object> parameters,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            return DataAccess.ExecQueryNative(query, parameters, filePath, lineNumber, memberName);
        }

        public void SaveEntity(T entity,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (entity.EqualsEmpty()) return;
            if (entity is BaseIdEntity idEntity)
            {
                if (!entity.Equals(GetEntity(idEntity.Id)))
                {
                    if (typeof(T) == typeof(ContragentsEntity))
                    {
                        throw new Exception("SaveEntity for [ContragentsEntity] is deny!");
                    }
                    if (typeof(T) == typeof(NomenclatureEntity))
                    {
                        throw new Exception("SaveEntity for [NomenclatureEntity] is deny!");
                    }
                    if (typeof(T) == typeof(ZebraPrinterTypeEntity))
                    {
                        Console.WriteLine($"SaveEntity: {entity}");
                    }
                    DataAccess.SaveEntity(entity, filePath, lineNumber, memberName);
                }
            }
            else
            {
                if (entity is BaseUidEntity uidEntity)
                {
                    if (!entity.Equals(GetEntity(uidEntity.Uid)))
                    {
                        if (typeof(T) == typeof(ContragentsEntity))
                        {
                            throw new Exception("SaveEntity for [ContragentsEntity] is deny!");
                        }
                        if (typeof(T) == typeof(NomenclatureEntity))
                        {
                            throw new Exception("SaveEntity for [NomenclatureEntity] is deny!");
                        }
                        if (typeof(T) == typeof(ZebraPrinterTypeEntity))
                        {
                            Console.WriteLine($"SaveEntity: {entity}");
                        }
                        DataAccess.SaveEntity(entity, filePath, lineNumber, memberName);
                    }
                }
            }
        }

        public void UpdateEntity(T entity,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (entity.EqualsEmpty()) return;
            
            if (typeof(T) == typeof(AppEntity))
            {
                //
            }
            else if (typeof(T) == typeof(BarCodeTypesEntity))
            {
                //
            }
            else if (typeof(T) == typeof(ContragentsEntity))
            {
                ((ContragentsEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            else if (typeof(T) == typeof(HostsEntity))
            {
                ((HostsEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            else if (typeof(T) == typeof(LabelsEntity))
            {
                //
            }
            else if (typeof(T) == typeof(LogEntity))
            {
                //
            }
            else if (typeof(T) == typeof(OrdersEntity))
            {
                ((OrdersEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            else if (typeof(T) == typeof(OrderStatusEntity))
            {
                //
            }
            else if (typeof(T) == typeof(OrderTypesEntity))
            {
                //
            }
            else if (typeof(T) == typeof(PluEntity))
            {
                //
            }
            else if (typeof(T) == typeof(ProductionFacilityEntity))
            {
                //
            }
            else if (typeof(T) == typeof(ProductSeriesEntity))
            {
                //
            }
            else if (typeof(T) == typeof(ScalesEntity))
            {
                ((ScalesEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            else if (typeof(T) == typeof(TemplateResourcesEntity))
            {
                ((TemplateResourcesEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            else if (typeof(T) == typeof(TemplatesEntity))
            {
                ((TemplatesEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            else if (typeof(T) == typeof(WeithingFactEntity))
            {
                //
            }
            else if (typeof(T) == typeof(WorkshopEntity))
            {
                ((WorkshopEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            else if (typeof(T) == typeof(ZebraPrinterEntity))
            {
                ((ZebraPrinterEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            else if (typeof(T) == typeof(ZebraPrinterResourceRefEntity))
            {
                ((ZebraPrinterResourceRefEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            else if (typeof(T) == typeof(ZebraPrinterTypeEntity))
            {
                //
            }
            else if (typeof(T) == typeof(LogEntity))
            {
                //
            }
            else if (typeof(T) == typeof(AppEntity))
            {
                //
            }
            
            DataAccess.UpdateEntity(entity, filePath, lineNumber, memberName);
        }

        public void DeleteEntity(T entity,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (entity.EqualsEmpty()) return;
            
            DataAccess.DeleteEntity(entity, filePath, lineNumber, memberName);
        }

        public void MarkedEntity(T entity,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (entity.EqualsEmpty()) return;
            
            if (typeof(T) == typeof(AppEntity))
            {
                //
            }
            else if (typeof(T) == typeof(BarCodeTypesEntity))
            {
                //
            }
            else if (typeof(T) == typeof(ContragentsEntity))
            {
                ((ContragentsEntity)(object)entity).Marked = true;
            }
            else if (typeof(T) == typeof(HostsEntity))
            {
                ((HostsEntity)(object)entity).Marked = true;
            }
            else if (typeof(T) == typeof(LabelsEntity))
            {
                //
            }
            else if (typeof(T) == typeof(LogEntity))
            {
                //
            }
            else if (typeof(T) == typeof(OrdersEntity))
            {
                //
            }
            else if (typeof(T) == typeof(OrderStatusEntity))
            {
                //
            }
            else if (typeof(T) == typeof(OrderTypesEntity))
            {
                //
            }
            else if (typeof(T) == typeof(PluEntity))
            {
                ((PluEntity)(object)entity).Marked = true;
            }
            else if (typeof(T) == typeof(ProductionFacilityEntity))
            {
                ((ProductionFacilityEntity)(object)entity).Marked = true;
            }
            else if (typeof(T) == typeof(ProductSeriesEntity))
            {
                //
            }
            else if (typeof(T) == typeof(ScalesEntity))
            {
                ((ScalesEntity)(object)entity).Marked = true;
            }
            else if (typeof(T) == typeof(TemplateResourcesEntity))
            {
                ((TemplateResourcesEntity)(object)entity).Marked = true;
            }
            else if (typeof(T) == typeof(TemplatesEntity))
            {
                ((TemplatesEntity)(object)entity).Marked = true;
            }
            else if (typeof(T) == typeof(WeithingFactEntity))
            {
                //
            }
            else if (typeof(T) == typeof(WorkshopEntity))
            {
                ((WorkshopEntity)(object)entity).Marked = true;
            }
            else if (typeof(T) == typeof(ZebraPrinterEntity))
            {
                ((ZebraPrinterEntity)(object)entity).Marked = true;
            }
            else if (typeof(T) == typeof(ZebraPrinterResourceRefEntity))
            {
                //
            }
            else if (typeof(T) == typeof(ZebraPrinterTypeEntity))
            {
                ((ZebraPrinterEntity)(object)entity).Marked = true;
            }
            
            DataAccess.UpdateEntity(entity, filePath, lineNumber, memberName);
        }

        public bool ExistsEntity(T entity,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (entity.EqualsEmpty()) return false;
            return DataAccess.ExistsEntity(entity, filePath, lineNumber, memberName);
        }

        public bool ExistsEntity(FieldListEntity fieldList, FieldOrderEntity order,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            return DataAccess.ExistsEntity<T>(fieldList, order, filePath, lineNumber, memberName);
        }

        #endregion
    }
}
