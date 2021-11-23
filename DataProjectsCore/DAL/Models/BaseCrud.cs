// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore;
using DataShareCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DataProjectsCore.DAL.Models
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
            FillReferencesSystem(entity);
            FillReferencesDatas(entity);
            FillReferencesScales(entity);
            FillReferencesDwh(entity);
        }

        private void FillReferencesSystem(T entity)
        {
            if (typeof(T) == typeof(TableSystemModels.AppEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableSystemModels.LogEntity))
            {
                TableSystemModels.LogEntity? logEntity = (TableSystemModels.LogEntity)(object)entity;
                if (!logEntity.EqualsEmpty())
                {
                    if (logEntity.App != null)
                        logEntity.App = DataAccess.AppsCrud.GetEntity(logEntity.App.Uid);
                    if (logEntity.Host != null)
                        logEntity.Host = DataAccess.HostsCrud.GetEntity(logEntity.Host.Id);
                }
            }
        }

        private void FillReferencesDatas(T entity)
        {
            if (typeof(T) == typeof(DataModels.DeviceEntity))
            {
                DataModels.DeviceEntity? deviceEntity = (DataModels.DeviceEntity)(object)entity;
                if (!deviceEntity.EqualsEmpty())
                {
                    if (deviceEntity.Scales != null)
                        deviceEntity.Scales = DataAccess.ScalesCrud.GetEntity(deviceEntity.Scales.Id);
                }
            }
        }

        private void FillReferencesScales(T entity)
        {
            if (typeof(T) == typeof(TableScaleModels.BarcodeTypeEntity))
            {
                TableScaleModels.BarcodeTypeEntity? barCodeTypesEntity = (TableScaleModels.BarcodeTypeEntity)(object)entity;
                if (!barCodeTypesEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.ContragentEntity))
            {
                TableScaleModels.ContragentEntity? contragentsEntity = (TableScaleModels.ContragentEntity)(object)entity;
                if (!contragentsEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.LabelEntity))
            {
                TableScaleModels.LabelEntity? labelsEntity = (TableScaleModels.LabelEntity)(object)entity;
                if (!labelsEntity.EqualsEmpty())
                {
                    if (labelsEntity.WeithingFact != null)
                        labelsEntity.WeithingFact = DataAccess.WeithingFactsCrud.GetEntity(labelsEntity.WeithingFact.Id);
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.NomenclatureEntity))
            {
                TableScaleModels.NomenclatureEntity? nomenclatureEntity = (TableScaleModels.NomenclatureEntity)(object)entity;
                if (!nomenclatureEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderEntity))
            {
                TableScaleModels.OrderEntity? ordersEntity = (TableScaleModels.OrderEntity)(object)entity;
                if (!ordersEntity.EqualsEmpty())
                {
                    if (ordersEntity.OrderTypes != null)
                        ordersEntity.OrderTypes = DataAccess.OrderTypesCrud.GetEntity(ordersEntity.OrderTypes.Id);
                    if (ordersEntity.Scales != null)
                        ordersEntity.Scales = DataAccess.ScalesCrud.GetEntity(ordersEntity.Scales.Id);
                    if (ordersEntity.Plu != null)
                        ordersEntity.Plu = DataAccess.PlusCrud.GetEntity(ordersEntity.Plu.Id);
                    if (ordersEntity.Templates != null)
                        ordersEntity.Templates = DataAccess.TemplatesCrud.GetEntity(ordersEntity.Templates.Id);
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderStatusEntity))
            {
                TableScaleModels.OrderStatusEntity? orderStatusEntity = (TableScaleModels.OrderStatusEntity)(object)entity;
                if (!orderStatusEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderTypeEntity))
            {
                TableScaleModels.OrderTypeEntity? orderTypesEntity = (TableScaleModels.OrderTypeEntity)(object)entity;
                if (!orderTypesEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.PluEntity))
            {
                TableScaleModels.PluEntity? pluEntity = (TableScaleModels.PluEntity)(object)entity;
                if (!pluEntity.EqualsEmpty())
                {
                    if (pluEntity.Templates != null)
                        pluEntity.Templates = DataAccess.TemplatesCrud.GetEntity(pluEntity.Templates.Id);
                    if (pluEntity.Scale != null)
                        pluEntity.Scale = DataAccess.ScalesCrud.GetEntity(pluEntity.Scale.Id);
                    if (pluEntity.Nomenclature != null)
                        pluEntity.Nomenclature = DataAccess.NomenclaturesCrud.GetEntity(pluEntity.Nomenclature.Id);
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.ProductionFacilityEntity))
            {
                TableScaleModels.ProductionFacilityEntity? productionFacilityEntity = (TableScaleModels.ProductionFacilityEntity)(object)entity;
                if (!productionFacilityEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.ProductSeriesEntity))
            {
                TableScaleModels.ProductSeriesEntity? productSeriesEntity = (TableScaleModels.ProductSeriesEntity)(object)entity;
                if (!productSeriesEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.ScaleEntity))
            {
                TableScaleModels.ScaleEntity? scalesEntity = (TableScaleModels.ScaleEntity)(object)entity;
                if (!scalesEntity.EqualsEmpty())
                {
                    if (scalesEntity.TemplateDefault != null)
                        scalesEntity.TemplateDefault = DataAccess.TemplatesCrud.GetEntity(scalesEntity.TemplateDefault.Id);
                    if (scalesEntity.TemplateSeries != null)
                        scalesEntity.TemplateSeries = DataAccess.TemplatesCrud.GetEntity(scalesEntity.TemplateSeries.Id);
                    if (scalesEntity.WorkShop != null)
                        scalesEntity.WorkShop = DataAccess.WorkshopsCrud.GetEntity(scalesEntity.WorkShop.Id);
                    if (scalesEntity.Printer != null)
                        scalesEntity.Printer = DataAccess.PrintersCrud.GetEntity(scalesEntity.Printer.Id);
                    if (scalesEntity.Host != null)
                        scalesEntity.Host = DataAccess.HostsCrud.GetEntity(scalesEntity.Host.Id);
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.TemplateResourceEntity))
            {
                TableScaleModels.TemplateResourceEntity? templateResourcesEntity = (TableScaleModels.TemplateResourceEntity)(object)entity;
                if (!templateResourcesEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.TemplateEntity))
            {
                TableScaleModels.TemplateEntity? templatesEntity = (TableScaleModels.TemplateEntity)(object)entity;
                if (!templatesEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.WeithingFactEntity))
            {
                TableScaleModels.WeithingFactEntity? weithingFactEntity = (TableScaleModels.WeithingFactEntity)(object)entity;
                if (!weithingFactEntity.EqualsEmpty())
                {
                    if (weithingFactEntity.Plu != null)
                        weithingFactEntity.Plu = DataAccess.PlusCrud.GetEntity(weithingFactEntity.Plu.Id);
                    if (weithingFactEntity.Scales != null)
                        weithingFactEntity.Scales = DataAccess.ScalesCrud.GetEntity(weithingFactEntity.Scales.Id);
                    if (weithingFactEntity.Series != null)
                        weithingFactEntity.Series = DataAccess.ProductSeriesCrud.GetEntity(weithingFactEntity.Series.Id);
                    if (weithingFactEntity.Orders != null)
                        weithingFactEntity.Orders = DataAccess.OrdersCrud.GetEntity(weithingFactEntity.Orders.Id);
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.WorkshopEntity))
            {
                TableScaleModels.WorkshopEntity? workshopEntity = (TableScaleModels.WorkshopEntity)(object)entity;
                if (!workshopEntity.EqualsEmpty())
                {
                    if (workshopEntity.ProductionFacility != null)
                        workshopEntity.ProductionFacility = DataAccess.ProductionFacilitiesCrud.GetEntity(workshopEntity.ProductionFacility.Id);
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterEntity))
            {
                TableScaleModels.PrinterEntity? zebraPrinterEntity = (TableScaleModels.PrinterEntity)(object)entity;
                if (!zebraPrinterEntity.EqualsEmpty())
                {
                    if (zebraPrinterEntity.PrinterType != null)
                        zebraPrinterEntity.PrinterType = DataAccess.PrinterTypesCrud.GetEntity(zebraPrinterEntity.PrinterType.Id);
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterResourceEntity))
            {
                TableScaleModels.PrinterResourceEntity? zebraPrinterResourceRefEntity = (TableScaleModels.PrinterResourceEntity)(object)entity;
                if (!zebraPrinterResourceRefEntity.EqualsEmpty())
                {
                    if (zebraPrinterResourceRefEntity.Printer != null)
                        zebraPrinterResourceRefEntity.Printer =
                            DataAccess.PrintersCrud.GetEntity(zebraPrinterResourceRefEntity.Printer.Id);
                    if (zebraPrinterResourceRefEntity.Resource != null)
                        zebraPrinterResourceRefEntity.Resource =
                            DataAccess.TemplateResourcesCrud.GetEntity(zebraPrinterResourceRefEntity.Resource.Id);
                }
            }
            if (typeof(T) == typeof(TableScaleModels.PrinterTypeEntity))
            {
                TableScaleModels.PrinterTypeEntity? zebraPrinterTypeEntity = (TableScaleModels.PrinterTypeEntity)(object)entity;
                if (!zebraPrinterTypeEntity.EqualsEmpty())
                {
                    //
                }
            }
        }

        private void FillReferencesDwh(T entity)
        {
            if (typeof(T) == typeof(TableDwhModels.BrandEntity))
            {
                TableDwhModels.BrandEntity brandEntity = (TableDwhModels.BrandEntity)(object)entity;
                if (!brandEntity.EqualsEmpty())
                {
                    if (brandEntity.InformationSystem != null)
                        brandEntity.InformationSystem = DataAccess.InformationSystemCrud.GetEntity(brandEntity.InformationSystem.Id);
                }
            }
            if (typeof(T) == typeof(TableDwhModels.NomenclatureEntity))
            {
                TableDwhModels.NomenclatureEntity? nomenclatureEntity = (TableDwhModels.NomenclatureEntity)(object)entity;
                if (!nomenclatureEntity.EqualsEmpty())
                {
                    //if (nomenclatureEntity.BrandBytes != null && nomenclatureEntity.BrandBytes.Length > 0)
                    //    nomenclatureEntity.Brand = DataAccess.BrandCrud.GetEntity(ShareEnums.DbField.CodeInIs, nomenclatureEntity.BrandBytes);
                    //if (nomenclatureEntity.InformationSystem != null)
                    //    nomenclatureEntity.InformationSystem = DataAccess.InformationSystemCrud.GetEntity(nomenclatureEntity.InformationSystem.Id);
                    //if (nomenclatureEntity.NomenclatureGroupCostBytes != null && nomenclatureEntity.NomenclatureGroupCostBytes.Length > 0)
                    //    nomenclatureEntity.NomenclatureGroupCost = DataAccess.NomenclatureGroupCrud.GetEntity(ShareEnums.DbField.CodeInIs, nomenclatureEntity.NomenclatureGroupCostBytes);
                    //if (nomenclatureEntity.NomenclatureGroupBytes != null && nomenclatureEntity.NomenclatureGroupBytes.Length > 0)
                    //    nomenclatureEntity.NomenclatureGroup = DataAccess.NomenclatureGroupCrud.GetEntity(ShareEnums.DbField.CodeInIs, nomenclatureEntity.NomenclatureGroupBytes);
                    //if (nomenclatureEntity.NomenclatureTypeBytes != null && nomenclatureEntity.NomenclatureTypeBytes.Length > 0)
                    //    nomenclatureEntity.NomenclatureType = DataAccess.NomenclatureTypeCrud.GetEntity(ShareEnums.DbField.CodeInIs, nomenclatureEntity.NomenclatureTypeBytes);
                    if (nomenclatureEntity.Status != null)
                        nomenclatureEntity.Status = DataAccess.StatusCrud.GetEntity(nomenclatureEntity.Status.Id);
                }
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureLightEntity))
            {
                TableDwhModels.NomenclatureLightEntity nomenclatureLightEntity = (TableDwhModels.NomenclatureLightEntity)(object)entity;
                if (!nomenclatureLightEntity.EqualsEmpty())
                {
                    if (nomenclatureLightEntity.InformationSystem != null)
                        nomenclatureLightEntity.InformationSystem = DataAccess.InformationSystemCrud.GetEntity(nomenclatureLightEntity.InformationSystem.Id);
                }
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureGroupEntity))
            {
                TableDwhModels.NomenclatureGroupEntity nomenclatureGroupEntity = (TableDwhModels.NomenclatureGroupEntity)(object)entity;
                if (!nomenclatureGroupEntity.EqualsEmpty())
                {
                    if (nomenclatureGroupEntity.InformationSystem != null)
                        nomenclatureGroupEntity.InformationSystem = DataAccess.InformationSystemCrud.GetEntity(nomenclatureGroupEntity.InformationSystem.Id);
                }
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureTypeEntity))
            {
                TableDwhModels.NomenclatureTypeEntity nomenclatureTypeEntity = (TableDwhModels.NomenclatureTypeEntity)(object)entity;
                if (!nomenclatureTypeEntity.EqualsEmpty())
                {
                    if (nomenclatureTypeEntity.InformationSystem != null)
                        nomenclatureTypeEntity.InformationSystem = DataAccess.InformationSystemCrud.GetEntity(nomenclatureTypeEntity.InformationSystem.Id);
                }
            }
        }

        public T GetEntity(FieldListEntity? fieldList, FieldOrderEntity order,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            T entity = DataAccess.GetEntity<T>(fieldList, order, filePath, lineNumber, memberName);
            FillReferences(entity);
            return entity;
        }

        public T GetEntity(int id)
        {
            return GetEntity(
                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), id } }),
                new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc));
        }

        public T GetEntity(Guid uid)
        {
            return GetEntity(
                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Uid.ToString(), uid } }),
                new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc));
        }

        public T[] GetEntities(FieldListEntity fieldList, FieldOrderEntity order, int maxResults = 0,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            T[]? entities = DataAccess.GetEntities<T>(fieldList, order, maxResults, filePath, lineNumber, memberName);
            foreach (T? entity in entities)
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
                    if (typeof(T) == typeof(TableScaleModels.ContragentEntity))
                    {
                        throw new Exception("SaveEntity for [ContragentsEntity] is deny!");
                    }
                    if (typeof(T) == typeof(TableScaleModels.NomenclatureEntity))
                    {
                        throw new Exception("SaveEntity for [NomenclatureEntity] is deny!");
                    }
                    if (typeof(T) == typeof(TableScaleModels.PrinterTypeEntity))
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
                        if (typeof(T) == typeof(TableScaleModels.ContragentEntity))
                        {
                            throw new Exception("SaveEntity for [ContragentsEntity] is deny!");
                        }
                        if (typeof(T) == typeof(TableScaleModels.NomenclatureEntity))
                        {
                            throw new Exception("SaveEntity for [NomenclatureEntity] is deny!");
                        }
                        if (typeof(T) == typeof(TableScaleModels.PrinterTypeEntity))
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

            // SYSTEM.
            if (typeof(T) == typeof(TableSystemModels.AppEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableSystemModels.LogEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableSystemModels.HostEntity))
            {
                ((TableSystemModels.HostEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            // SCALES.
            else if (typeof(T) == typeof(TableScaleModels.BarcodeTypeEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.ContragentEntity))
            {
                ((TableScaleModels.ContragentEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            else if (typeof(T) == typeof(TableScaleModels.LabelEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderEntity))
            {
                ((TableScaleModels.OrderEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderStatusEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderTypeEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.PluEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.ProductionFacilityEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.ProductSeriesEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.ScaleEntity))
            {
                ((TableScaleModels.ScaleEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            else if (typeof(T) == typeof(TableScaleModels.TemplateResourceEntity))
            {
                ((TableScaleModels.TemplateResourceEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            else if (typeof(T) == typeof(TableScaleModels.TemplateEntity))
            {
                ((TableScaleModels.TemplateEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            else if (typeof(T) == typeof(TableScaleModels.WeithingFactEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.WorkshopEntity))
            {
                ((TableScaleModels.WorkshopEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterEntity))
            {
                ((TableScaleModels.PrinterEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterResourceEntity))
            {
                ((TableScaleModels.PrinterResourceEntity)(object)entity).ModifiedDate = DateTime.Now;
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterTypeEntity))
            {
                //
            }
            // DWH.
            else if (typeof(T) == typeof(TableDwhModels.BrandEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.InformationSystemEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureGroupEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureLightEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureTypeEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.StatusEntity))
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

            // SYSTEM.
            if (typeof(T) == typeof(TableSystemModels.AppEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableSystemModels.LogEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableSystemModels.HostEntity))
            {
                ((TableSystemModels.HostEntity)(object)entity).Marked = true;
            }

            // SCALES.
            else if (typeof(T) == typeof(TableScaleModels.BarcodeTypeEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.ContragentEntity))
            {
                ((TableScaleModels.ContragentEntity)(object)entity).Marked = true;
            }
            else if (typeof(T) == typeof(TableScaleModels.LabelEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderStatusEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderTypeEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.PluEntity))
            {
                ((TableScaleModels.PluEntity)(object)entity).Marked = true;
            }
            else if (typeof(T) == typeof(TableScaleModels.ProductionFacilityEntity))
            {
                ((TableScaleModels.ProductionFacilityEntity)(object)entity).Marked = true;
            }
            else if (typeof(T) == typeof(TableScaleModels.ProductSeriesEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.ScaleEntity))
            {
                ((TableScaleModels.ScaleEntity)(object)entity).Marked = true;
            }
            else if (typeof(T) == typeof(TableScaleModels.TemplateResourceEntity))
            {
                ((TableScaleModels.TemplateResourceEntity)(object)entity).Marked = true;
            }
            else if (typeof(T) == typeof(TableScaleModels.TemplateEntity))
            {
                ((TableScaleModels.TemplateEntity)(object)entity).Marked = true;
            }
            else if (typeof(T) == typeof(TableScaleModels.WeithingFactEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.WorkshopEntity))
            {
                ((TableScaleModels.WorkshopEntity)(object)entity).Marked = true;
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterEntity))
            {
                ((TableScaleModels.PrinterEntity)(object)entity).Marked = true;
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterResourceEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterTypeEntity))
            {
                ((TableScaleModels.PrinterEntity)(object)entity).Marked = true;
            }
            
            // DWH.
            else if (typeof(T) == typeof(TableDwhModels.BrandEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.InformationSystemEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureGroupEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureLightEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureTypeEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.StatusEntity))
            {
                //
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
