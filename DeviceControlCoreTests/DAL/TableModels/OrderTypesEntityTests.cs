using System.Collections.Generic;
using DeviceControlCore.DAL;
using DeviceControlCore.DAL.DataModels;
using DeviceControlCore.DAL.TableModels;
using NUnit.Framework;

namespace DeviceControlCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class OrderTypesEntityTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new OrderTypesEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in EnumValues.GetInt())
                foreach (var s in EnumValues.GetString())
                {
                    var entity = new OrderTypesEntity
                    {
                        Id = i,
                        Description = s
                    };
                    _ = entity.ToString();
                    Assert.AreEqual(false, entityNew.Equals(entity));
                }
            });

            Utils.MethodComplete();
        }

        public OrderTypesEntity EntityCreate(string description)
        {
            if (!DataAccessUtils.DataAccess.OrderTypesCrud.ExistsEntity(new FieldListEntity(
                new Dictionary<string, object> { { EnumField.Description.ToString(), description } }), null))
            {
                OrderTypesEntity entity = new OrderTypesEntity
                {
                    Id = -1,
                    Description = description
                };
                DataAccessUtils.DataAccess.OrderTypesCrud.SaveEntity(entity);
            }
            return DataAccessUtils.DataAccess.OrderTypesCrud.GetEntity(new FieldListEntity(
                new Dictionary<string, object> { { EnumField.Description.ToString(), description } }),
                new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc));
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var description = "TEST ORDER TYPE";
                var entityNew = EntityCreate(description);

                // UpdateEntity
                entityNew.Description += " changed";
                DataAccessUtils.DataAccess.OrderTypesCrud.UpdateEntity(entityNew);
                // GetEntities
                var nomenclatureUnits = DataAccessUtils.DataAccess.OrderTypesCrud.GetEntities(null, null);
                Assert.AreEqual(true, nomenclatureUnits.Length > 0);
                foreach (var entity in nomenclatureUnits)
                {
                    if (entity.Description.Equals(description + " changed"))
                    {
                        // DeleteEntity
                        //DataAccessUtils.DataAccess.OrderTypesCrud.DeleteEntity(entity);
                    }
                }
            });

            Utils.MethodComplete();
        }
    }
}
