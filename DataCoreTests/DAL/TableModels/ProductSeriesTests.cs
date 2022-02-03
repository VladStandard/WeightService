// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using NUnit.Framework;
//using System;

//namespace DataCoreTests.DAL.TableModels
//{
//    [TestFixture]
//    internal class ProductSeriesTests
//    {
//        [Test]
//        public void Entity_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                ProductSeriesEntity entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = entityNew.Clone();
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                    foreach (DateTime dt in TestsEnums.GetDateTime())
//                        foreach (Guid duid in TestsEnums.GetGuid())
//                            foreach (bool b in TestsEnums.GetBool())
//                                foreach (string s in TestsEnums.GetString())
//                                {
//                                    ProductSeriesEntity entity = new()
//                                    {
//                                        Id = i,
//                                        //ScaleId = scaleId,
//                                        Scale = new ScaleEntity(),
//                                        CreateDate = dt,
//                                        Uid = duid,
//                                        IsClose = b,
//                                        Sscc = s
//                                    };
//                                    _ = entity.ToString();
//                                    Assert.AreEqual(false, entityNew.Equals(entity));
//                                }
//            });

//            TestsUtils.MethodComplete();
//        }

//        [Test]
//        public void Entity_Crud_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                int iStart = -10;
//                int iEnd = 0;
//                string sscc1 = "SSCC 1";
//                string sscc2 = "SSCC 2";
//                // SaveEntity
//                for (int i = iStart; i < iEnd; i++)
//                {
//                    ProductSeriesEntity entity = new()
//                    {
//                        //ScaleId = i.ToString(),
//                        Scale = new ScaleEntity(),
//                        CreateDate = DateTime.Now,
//                        Uid = Guid.Empty,
//                        IsClose = false,
//                        Sscc = sscc1,
//                    };
//                    DataAccessUtilsTests.DataAccess.ProductSeriesCrud.SaveEntity(entity);
//                }
//                // UpdateEntity
//                foreach (ProductSeriesEntity entity in DataAccessUtilsTests.DataAccess.Crud.GetEntities<ProductSeriesEntity>(null, null))
//                {
//                    if (entity.Scale.Id < 0)
//                    {
//                        entity.Sscc = sscc2;
//                        DataAccessUtilsTests.DataAccess.ProductSeriesCrud.UpdateEntity(entity);
//                    }
//                }
//                // GetEntities
//                ProductSeriesEntity[] entities = DataAccessUtilsTests.DataAccess.ProductSeriesCrud.GetEntities<ProductSeriesEntity>(null, null);
//                Assert.AreEqual(true, entities.Length > 0);
//                foreach (ProductSeriesEntity entity in entities)
//                {
//                    if (!entity.EqualsDefault())
//                    {
//                        if (entity.Scale.Id < 0)
//                        {
//                            // DeleteEntity
//                            DataAccessUtilsTests.DataAccess.ProductSeriesCrud.DeleteEntity(entity);
//                        }
//                    }
//                }
//            }
//            );

//            TestsUtils.MethodComplete();
//        }
//    }
//}
