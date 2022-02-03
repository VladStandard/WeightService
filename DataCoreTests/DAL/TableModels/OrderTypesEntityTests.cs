// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore;
//using NUnit.Framework;
//using System.Collections.Generic;

//namespace DataCoreTests.DAL.TableModels
//{
//    [TestFixture]
//    internal class OrderTypesEntityTests
//    {
//        [Test]
//        public void Entity_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                OrderTypeEntity entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = entityNew.Clone();
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                    foreach (string s in TestsEnums.GetString())
//                    {
//                        OrderTypeEntity entity = new()
//                        {
//                            Id = i,
//                            Description = s
//                        };
//                        _ = entity.ToString();
//                        Assert.AreEqual(false, entityNew.Equals(entity));
//                    }
//            });

//            TestsUtils.MethodComplete();
//        }

//        public OrderTypeEntity EntityCreate(string description)
//        {
//            if (!DataAccessUtilsTests.DataAccess.OrderTypesCrud.ExistsEntity<OrderTypeEntity>(new FieldListEntity(
//                new Dictionary<string, object> { { ShareEnums.DbField.Description.ToString(), description } }), null))
//            {
//                OrderTypeEntity entity = new()
//                {
//                    Id = -1,
//                    Description = description
//                };
//                DataAccessUtilsTests.DataAccess.OrderTypesCrud.SaveEntity(entity);
//            }
//            return DataAccessUtilsTests.DataAccess.OrderTypesCrud.GetEntity<OrderTypeEntity>(new FieldListEntity(
//                new Dictionary<string, object> { { ShareEnums.DbField.Description.ToString(), description } }),
//                new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc));
//        }

//        [Test]
//        public void Entity_Crud_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                string description = "TEST ORDER TYPE";
//                OrderTypeEntity entityNew = EntityCreate(description);

//                // UpdateEntity
//                entityNew.Description += " changed";
//                DataAccessUtilsTests.DataAccess.OrderTypesCrud.UpdateEntity(entityNew);
//                // GetEntities
//                OrderTypeEntity[] nomenclatureUnits = DataAccessUtilsTests.DataAccess.OrderTypesCrud.GetEntities<OrderTypeEntity>(null, null);
//                Assert.AreEqual(true, nomenclatureUnits.Length > 0);
//                foreach (OrderTypeEntity entity in nomenclatureUnits)
//                {
//                    if (entity.Description.Equals(description + " changed"))
//                    {
//                        // DeleteEntity
//                        //DataAccessUtils.DataAccess.OrderTypesCrud.DeleteEntity(entity);
//                    }
//                }
//            });

//            TestsUtils.MethodComplete();
//        }
//    }
//}
