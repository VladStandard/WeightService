// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using WsDataCore;
//using NUnit.Framework;
//using System.Collections.Generic;

//namespace DataCoreTests.Sql.TableModels
//{
//    [TestFixture]
//    public sealed class OrderTypesEntityTests
//    {
//        [Test]
//        public void Model_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                OrderTypeEntity entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = new(entityNew);
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                    foreach (string s in TestsEnums.GetString())
//                    {
//                        OrderTypeEntity item = new()
//                        {
//                            Id = i,
//                            Description = s
//                        };
//                        _ = item.ToString();
//                        Assert.AreEqual(false, entityNew.Equals(item));
//                    }
//            });

//            TestsUtils.MethodComplete();
//        }

//        public OrderTypeEntity EntityCreate(string description)
//        {
//            if (!DataAccessUtilsTests.DataAccess.OrderTypesCrud.ExistsEntity<OrderTypeEntity>(new (
//                new Dictionary<string, object> { { DbField.Description.ToString(), description } }), null))
//            {
//                OrderTypeEntity item = new()
//                {
//                    Id = -1,
//                    Description = description
//                };
//                DataAccessUtilsTests.DataAccess.OrderTypesCrud.SaveEntity(item);
//            }
//            return DataAccessUtilsTests.DataAccess.OrderTypesCrud.GetItem<OrderTypeEntity>(new (
//                new Dictionary<string, object> { { DbField.Description.ToString(), description } }),
//                new FieldOrderModel(DbField.Id, DbOrderDirection.Desc));
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
//                foreach (OrderTypeEntity item in nomenclatureUnits)
//                {
//                    if (item.Description.Equals(description + " changed"))
//                    {
//                        // DeleteEntity
//                        //DataAccessUtils.DataAccess.OrderTypesCrud.DeleteEntity(item);
//                    }
//                }
//            });

//            TestsUtils.MethodComplete();
//        }
//    }
//}

namespace WsStorageCoreTests.Tables.TableModels;