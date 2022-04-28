// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace DataCoreTests.Sql.TableModels
//{
//    [TestFixture]
//    internal class ZebraPrinterTests
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
//                object entityCopy = entityNew.CloneCast;
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                foreach (DateTime dt in TestsEnums.GetDateTime())
//                foreach (string s in TestsEnums.GetString())
//                foreach (short sh in TestsEnums.GetShort())
//                foreach (bool b in TestsEnums.GetBool())
//                {
//                                    PrinterEntity item = new()
//                                    {
//                        Id = i,
//                        CreateDate = dt,
//                        ChangeDt = dt,
//                        Name = s,
//                        Ip = s,
//                        Port = sh,
//                        Password = s,
//                        PrinterType = new PrinterTypeEntity(),
//                        MacAddress = new DataShareCore.DAL.Models.MacAddressEntity(s),
//                        PeelOffSet = b,
//                        DarknessLevel = sh,
//                    };
//                    _ = item.ToString();
//                    Assert.AreEqual(false, entityNew.Equals(item));
//                }
//            });

//            TestsUtils.MethodComplete();
//        }

//        [Test]
//        public void Entity_Crud_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                const string name = "ZebraPrinterEntity test";
//                const string name2 = "ZebraPrinterEntity test 2";
//                foreach (PrinterTypeEntity zebraPrinterTypeEntity in DataAccessUtilsTests.DataAccess.Crud.GetEntities<PrinterTypeEntity>(null, null))
//                {
//                    // SaveEntity
//                    PrinterEntity zebraPrinterType = new()
//                    {
//                        Id = -1,
//                        CreateDate = DateTime.Now,
//                        ChangeDt = DateTime.Now,
//                        Name = name,
//                        Ip = default,
//                        Port = default,
//                        Password = null,
//                        PrinterType = zebraPrinterTypeEntity,
//                        MacAddress = new DataShareCore.DAL.Models.MacAddressEntity(),
//                        PeelOffSet = default,
//                        DarknessLevel = default,
//                    };
//                    DataAccessUtilsTests.DataAccess.PrintersCrud.SaveEntity(zebraPrinterType);
//                }
//                // GetEntities
//                foreach (PrinterEntity item in DataAccessUtilsTests.DataAccess.PrintersCrud.GetEntities<PrinterEntity>(null,
//                    new FieldOrderEntity { Use = true, Name = ShareEnums.DbField.Name, Direction = ShareEnums.DbOrderDirection.Asc }))
//                {
//                    if (item.Name.Equals(name))
//                    {
//                        // UpdateEntity
//                        item.Name = name2;
//                        DataAccessUtilsTests.DataAccess.PrintersCrud.UpdateEntity(item);
//                    }
//                }
//                // GetEntities
//                foreach (PrinterEntity item in DataAccessUtilsTests.DataAccess.PrintersCrud.GetEntities<PrinterEntity>(null,
//                    new FieldOrderEntity { Use = true, Name = ShareEnums.DbField.Name, Direction = ShareEnums.DbOrderDirection.Asc }))
//                {
//                    if (item.Name.Equals(name2))
//                    {
//                        // DeleteEntity
//                        DataAccessUtilsTests.DataAccess.PrintersCrud.DeleteEntity(item);
//                    }
//                }
//            }
//            );

//            TestsUtils.MethodComplete();
//        }
//    }
//}
