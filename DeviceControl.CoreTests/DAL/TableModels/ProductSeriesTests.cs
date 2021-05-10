using System;
using DeviceControl.Core.DAL.TableModels;
using NUnit.Framework;

namespace DeviceControl.CoreTests.DAL.TableModels
{
    [TestFixture]
    internal class ProductSeriesTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new ProductSeriesEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in EnumValues.GetInt())
                foreach (var dt in EnumValues.GetDateTime())
                foreach (var duid in EnumValues.GetGuid())
                foreach (var b in EnumValues.GetBool())
                foreach (var s in EnumValues.GetString())
                {
                    var entity = new ProductSeriesEntity
                    {
                        Id = i,
                        //ScaleId = scaleId,
                        Scale = new ScalesEntity(),
                        CreateDate = dt,
                        Uid = duid,
                        IsClose = b,
                        Sscc = s
                    };
                    _ = entity.ToString();
                    Assert.AreEqual(false, entityNew.Equals(entity));
                }
            });

            Utils.MethodComplete();
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var iStart = -10;
                var iEnd = 0;
                var sscc1 = "SSCC 1";
                var sscc2 = "SSCC 2";
                // SaveEntity
                for (var i = iStart; i < iEnd; i++)
                {
                    var entity = new ProductSeriesEntity()
                    {
                        //ScaleId = i.ToString(),
                        Scale = new ScalesEntity(),
                        CreateDate = DateTime.Now,
                        Uid = Guid.Empty,
                        IsClose = false,
                        Sscc = sscc1,
                    };
                    DataAccessUtils.DataAccess.ProductSeriesCrud.SaveEntity(entity);
                }
                // UpdateEntity
                foreach (var entity in DataAccessUtils.DataAccess.ProductSeriesCrud.GetEntities(null, null))
                {
                    if (entity.Scale.Id < 0)
                    {
                        entity.Sscc = sscc2;
                        DataAccessUtils.DataAccess.ProductSeriesCrud.UpdateEntity(entity);
                    }
                }
                // GetEntities
                var entities = DataAccessUtils.DataAccess.ProductSeriesCrud.GetEntities(null, null);
                Assert.AreEqual(true, entities.Length > 0);
                foreach (var entity in entities)
                {
                    if (!entity.EqualsDefault())
                    {
                        if (entity.Scale.Id < 0)
                        {
                            // DeleteEntity
                            DataAccessUtils.DataAccess.ProductSeriesCrud.DeleteEntity(entity);
                        }
                    }
                }
            }
            );

            Utils.MethodComplete();
        }
    }
}
