// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using MdmControlCore.DAL.TableModels;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MdmControlCore.DAL
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
            if (typeof(T) == typeof(NomenclatureEntity))
            {
                var nomenclatureEntity = (NomenclatureEntity)(object)entity;
                if (!nomenclatureEntity.EqualsEmpty())
                {
                    if (nomenclatureEntity.BrandBytes != null && nomenclatureEntity.BrandBytes.Length > 0)
                        nomenclatureEntity.Brand = DataAccess.BrandCrud.GetEntity(EnumField.CodeInIs, nomenclatureEntity.BrandBytes);
                    if (nomenclatureEntity.InformationSystem != null)
                        nomenclatureEntity.InformationSystem = DataAccess.InformationSystemCrud.GetEntity(nomenclatureEntity.InformationSystem.Id);
                    if (nomenclatureEntity.NomenclatureGroupCostBytes != null && nomenclatureEntity.NomenclatureGroupCostBytes.Length > 0)
                        nomenclatureEntity.NomenclatureGroupCost = DataAccess.NomenclatureGroupCrud.GetEntity(EnumField.CodeInIs, nomenclatureEntity.NomenclatureGroupCostBytes);
                    if (nomenclatureEntity.NomenclatureGroupBytes != null && nomenclatureEntity.NomenclatureGroupBytes.Length > 0)
                        nomenclatureEntity.NomenclatureGroup = DataAccess.NomenclatureGroupCrud.GetEntity(EnumField.CodeInIs, nomenclatureEntity.NomenclatureGroupBytes);
                    if (nomenclatureEntity.NomenclatureTypeBytes != null && nomenclatureEntity.NomenclatureTypeBytes.Length > 0)
                        nomenclatureEntity.NomenclatureType = DataAccess.NomenclatureTypeCrud.GetEntity(EnumField.CodeInIs, nomenclatureEntity.NomenclatureTypeBytes);
                    if (nomenclatureEntity.Status != null)
                        nomenclatureEntity.Status = DataAccess.StatusCrud.GetEntity(nomenclatureEntity.Status.Id);
                }
            }
            if (typeof(T) == typeof(NomenclatureLightEntity))
            {
                var nomenclatureLightEntity = (NomenclatureLightEntity)(object)entity;
                if (!nomenclatureLightEntity.EqualsEmpty())
                {
                    if (nomenclatureLightEntity.InformationSystem != null)
                        nomenclatureLightEntity.InformationSystem = DataAccess.InformationSystemCrud.GetEntity(nomenclatureLightEntity.InformationSystem.Id);
                }
            }
            if (typeof(T) == typeof(BrandEntity))
            {
                var brandEntity = (BrandEntity)(object)entity;
                if (!brandEntity.EqualsEmpty())
                {
                    if (brandEntity.InformationSystem != null)
                        brandEntity.InformationSystem = DataAccess.InformationSystemCrud.GetEntity(brandEntity.InformationSystem.Id);
                }
            }
            if (typeof(T) == typeof(NomenclatureGroupEntity))
            {
                var nomenclatureGroupEntity = (NomenclatureGroupEntity)(object)entity;
                if (!nomenclatureGroupEntity.EqualsEmpty())
                {
                    if (nomenclatureGroupEntity.InformationSystem != null)
                        nomenclatureGroupEntity.InformationSystem = DataAccess.InformationSystemCrud.GetEntity(nomenclatureGroupEntity.InformationSystem.Id);
                }
            }
            if (typeof(T) == typeof(NomenclatureTypeEntity))
            {
                var nomenclatureTypeEntity = (NomenclatureTypeEntity)(object)entity;
                if (!nomenclatureTypeEntity.EqualsEmpty())
                {
                    if (nomenclatureTypeEntity.InformationSystem != null)
                        nomenclatureTypeEntity.InformationSystem = DataAccess.InformationSystemCrud.GetEntity(nomenclatureTypeEntity.InformationSystem.Id);
                }
            }
        }

        public T GetEntity(FieldListEntity fieldList, FieldOrderEntity order,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            var entity = DataAccess.GetEntity<T>(fieldList, order, filePath, lineNumber, memberName);
            FillReferences(entity);
            return entity;
        }

        public T GetEntity(int id)
        {
            return GetEntity(
                new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), id } }),
                new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc));
        }

        public T GetEntity(EnumField field, byte[] codeInIs)
        {
            return GetEntity(
                new FieldListEntity(new Dictionary<string, object> { { field.ToString(), codeInIs } }),
                new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc));
        }

        public T[] GetEntities(FieldListEntity fieldList, FieldOrderEntity order, int count,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            var entities = DataAccess.GetEntities<T>(fieldList, order, count, filePath, lineNumber, memberName);
            foreach (var entity in entities)
            {
                FillReferences(entity);
            }
            return entities;
        }

        public IEnumerable<T> GetEntitiesAsIEnumerable(FieldListEntity fieldList, FieldOrderEntity order, int count,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            var entities = DataAccess.GetEntitiesAsList<T>(fieldList, order, count, filePath, lineNumber, memberName);
            var entitiesAsList = entities.ToList();
            foreach (var entity in entitiesAsList)
            {
                FillReferences(entity);
            }
            return entitiesAsList;
        }

        public T[] GetEntitiesNativeMapping(string query,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            return DataAccess.GetEntitiesNativeMapping<T>(query, filePath, lineNumber, memberName);
        }

        public IEnumerable<T> GetEntitiesNativeMappingAsList(string query,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            return DataAccess.GetEntitiesNativeMappingAsList<T>(query, filePath, lineNumber, memberName);
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
            if (!entity.Equals(GetEntity(entity.Id)))
            {
                if (typeof(T) == typeof(NomenclatureEntity))
                {
                    //throw new Exception("SaveEntity for [NomenclatureEntity] is deny!");
                }
                DataAccess.SaveEntity(entity, filePath, lineNumber, memberName);
            }
        }

        public void UpdateEntity(T entity, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (entity.EqualsEmpty()) return;

            if (typeof(T) == typeof(NomenclatureEntity))
            {
                //((NomenclatureEntity)(object)entity).Dlm = DateTime.Now;
            }

            DataAccess.UpdateEntity(entity, filePath, lineNumber, memberName);
        }

        public void DeleteEntity(T entity, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (entity.EqualsEmpty()) return;
            
            DataAccess.DeleteEntity(entity, filePath, lineNumber, memberName);
        }

        public void MarkedEntity(T entity, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (entity.EqualsEmpty()) return;

            if (typeof(T) == typeof(NomenclatureEntity))
            {
                // ((Entity)(object)entity).Marked = true;
            }

            DataAccess.UpdateEntity(entity, filePath, lineNumber, memberName);
        }

        public bool ExistsEntity(T entity, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
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
