using NUnit.Framework;
using System.Collections.Generic;

namespace BlazorCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class BarCodeTypesTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new BarcodeTypeEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in EnumValues.GetInt())
                foreach (var s in EnumValues.GetString())
                {
                    var entity = new BarcodeTypeEntity()
                    {
                        Id = i,
                        Name = s
                    };
                    _ = entity.ToString();
                    Assert.AreEqual(false, entityNew.Equals(entity));
                }
            });

            Utils.MethodComplete();
        }

        public BarcodeTypeEntity EntityCreate(string name)
        {
            //var entity = new BarCodeTypesEntity
            //{
            //    //Id = -1,
            //    Name = name
            //};
            //// Не сохранять.
            //DataAccessUtils.DataAccess.BarCodeTypesCrud.SaveEntity(entity);
            return DataAccessUtils.DataAccess.BarcodeTypesCrud.GetEntity(
                new FieldListEntity(new Dictionary<string, object> { { EnumField.Name.ToString(), name } }),
                new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc));
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var name = "Code128";
                var entityNew = EntityCreate(name);
                // UpdateEntity - Не изменять
                //entityNew.Name += " changed";
                //DataAccessUtils.DataAccess.BarCodeTypesCrud.UpdateEntity(entityNew);
                // GetEntities
                var entities = DataAccessUtils.DataAccess.BarcodeTypesCrud.GetEntities(null, null);
                Assert.AreEqual(true, entities.Length > 0);
                foreach (var entity in entities)
                {
                    if (entity.Name.Equals(name + " changed"))
                    {
                        // DeleteEntity - Не удалять
                        //DataAccessUtils.DataAccess.BarCodeTypesCrud.DeleteEntity(entity);
                    }
                }
            });

            Utils.MethodComplete();
        }
    }
}
