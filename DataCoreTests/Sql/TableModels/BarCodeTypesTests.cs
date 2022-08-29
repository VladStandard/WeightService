// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore;
//using NUnit.Framework;
//using System.Collections.Generic;

//namespace DataCoreTests.Sql.TableModels
//{
//    [TestFixture]
//    internal class BarCodeTypesTests
//    {
//        [Test]
//        public void Entity_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                BarcodeTypeEntityV2 entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = entityNew.CloneCast();
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                    foreach (string s in TestsEnums.GetString())
//                    {
//                        BarcodeTypeEntityV2 item = new()
//                        {
//                            Id = i,
//                            Name = s
//                        };
//                        _ = item.ToString();
//                        Assert.AreEqual(false, entityNew.Equals(item));
//                    }
//            });

//            TestsUtils.MethodComplete();
//        }

//        public BarcodeTypeEntityV2 EntityCreate(string name)
//        {
//            //var item = new BarCodeTypesEntity
//            //{
//            //    //Id = -1,
//            //    Name = name
//            //};
//            //// Не сохранять.
//            //DataAccessUtils.DataAccess.BarCodeTypesCrud.SaveEntity(item);
//            return DataAccessUtilsTests.DataAccess.Crud.GetItem<BarcodeTypeEntityV2>(
//                new FieldListEntity(new Dictionary<DbField, object?> { { ShareEnums.DbField.Name, name } }),
//                new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc));
//        }

//        [Test]
//        public void Entity_Crud_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                string name = "Code128";
//                BarcodeTypeEntityV2 entityNew = EntityCreate(name);
//                // UpdateEntity - Не изменять
//                //entityNew.Name += " changed";
//                //DataAccessUtils.DataAccess.BarCodeTypesCrud.UpdateEntity(entityNew);
//                // GetEntities
//                BarcodeTypeEntityV2[] entities = DataAccessUtilsTests.DataAccess.Crud.GetEntities<BarcodeTypeEntityV2>(null, null);
//                Assert.AreEqual(true, entities.Length > 0);
//                foreach (BarcodeTypeEntityV2 item in entities)
//                {
//                    if (item.Name.Equals(name + " changed"))
//                    {
//                        // DeleteEntity - Не удалять
//                        //DataAccessUtils.DataAccess.BarCodeTypesCrud.DeleteEntity(item);
//                    }
//                }
//            });

//            TestsUtils.MethodComplete();
//        }
//    }
//}
