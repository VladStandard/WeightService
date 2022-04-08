// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace DataCoreTests.DAL.TableModels
//{
//    [TestFixture]
//    internal class ZebraPrinterTypeTests
//    {
//        [Test]
//        public void Entity_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                WorkshopEntity entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = entityNew.Clone();
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                foreach (string s in TestsEnums.GetString())
//                {
//                        PrinterTypeEntity entity = new()
//                    {
//                        Name = s,
//                    };
//                    entity.IdentityId = i;
//                    _ = entity.ToString();
//                    Assert.AreEqual(false, entityNew.Equals(entity));
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
//                zebraPrinterType.IdentityId = -1;
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
//                    foreach (PrinterEntity entity in DataAccessUtilsTests.DataAccess.Crud.GetEntities<PrinterEntity>(null,
//                        new FieldOrderEntity { Use = true, Name = ShareEnums.DbField.Name, Direction = ShareEnums.DbOrderDirection.Asc }))
//                    {
//                        if (entity.Name.Equals(name))
//                        {
//                            // UpdateEntity
//                            entity.Name = name2;
//                            DataAccessUtilsTests.DataAccess.Crud.UpdateEntity(entity);
//                        }
//                    }
//                    // GetEntities
//                    foreach (PrinterEntity entity in DataAccessUtilsTests.DataAccess.Crud.GetEntities<PrinterEntity>(null,
//                        new FieldOrderEntity { Use = true, Name = ShareEnums.DbField.Name, Direction = ShareEnums.DbOrderDirection.Asc }))
//                    {
//                        if (entity.Name.Equals(name2))
//                        {
//                            // DeleteEntity
//                            DataAccessUtilsTests.DataAccess.Crud.DeleteEntity(entity);
//                        }
//                    }
//                }
//            );

//            TestsUtils.MethodComplete();
//        }
//    }
//}
