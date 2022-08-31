// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace DataCoreTests.Sql.TableModels
//{
//    [TestFixture]
//    internal class ZebraPrinterTypeTests
//    {
//        [Test]
//        public void Model_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                WorkshopEntity entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = entityNew.CloneCast();
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                foreach (string s in TestsEnums.GetString())
//                {
//                        PrinterTypeEntity item = new()
//                    {
//                        Name = s,
//                    };
//                    item.Identity.Id = i;
//                    _ = item.ToString();
//                    Assert.AreEqual(false, entityNew.Equals(item));
//                }
//            });

//            TestsUtils.MethodComplete();
//        }

//        [Test]
//        public void Entity_Crud_Throws()
//        {
//            TestsUtils.MethodStart();

//            Assert.Throws<Exception>(() =>
//            {
//                const string name = "ZebraPrinterType test";
//                // SaveEntity
//                PrinterTypeEntity zebraPrinterType = new()
//                {
//                    Name = name,
//                };
//                zebraPrinterType.Identity.Id = -1;
//                DataAccessUtilsTests.DataAccess.Crud.SaveEntity(zebraPrinterType);
//            });

//            TestsUtils.MethodComplete();
//        }

//        [Test]
//        public void Entity_Crud_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//                {
//                    const string name = "ZebraPrinterType test";
//                    const string name2 = "ZebraPrinterType test 2";
//                    // GetEntities
//                    foreach (PrinterEntity item in DataAccessUtilsTests.DataAccess.Crud.GetEntities<PrinterEntity>(null,
//                        new FieldOrderModel { Use = true, Name = ShareEnums.DbField.Name, Direction = ShareEnums.DbOrderDirection.Asc }))
//                    {
//                        if (item.Name.Equals(name))
//                        {
//                            // UpdateEntity
//                            item.Name = name2;
//                            DataAccessUtilsTests.DataAccess.Crud.UpdateEntity(item);
//                        }
//                    }
//                    // GetEntities
//                    foreach (PrinterEntity item in DataAccessUtilsTests.DataAccess.Crud.GetEntities<PrinterEntity>(null,
//                        new FieldOrderModel { Use = true, Name = ShareEnums.DbField.Name, Direction = ShareEnums.DbOrderDirection.Asc }))
//                    {
//                        if (item.Name.Equals(name2))
//                        {
//                            // DeleteEntity
//                            DataAccessUtilsTests.DataAccess.Crud.DeleteEntity(item);
//                        }
//                    }
//                }
//            );

//            TestsUtils.MethodComplete();
//        }
//    }
//}
