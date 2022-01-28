using System;
using CoreTests;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataShareCore;
using NUnit.Framework;

namespace BlazorCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class ZebraPrinterTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                WorkshopEntity entityNew = new();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                object entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (int i in TestsEnums.GetInt())
                foreach (DateTime dt in TestsEnums.GetDateTime())
                foreach (string s in TestsEnums.GetString())
                foreach (short sh in TestsEnums.GetShort())
                foreach (bool b in TestsEnums.GetBool())
                {
                                    PrinterEntity entity = new()
                                    {
                        Id = i,
                        CreateDate = dt,
                        ModifiedDate = dt,
                        Name = s,
                        Ip = s,
                        Port = sh,
                        Password = s,
                        PrinterType = new PrinterTypeEntity(),
                        MacAddress = new DataShareCore.DAL.Models.MacAddressEntity(s),
                        PeelOffSet = b,
                        DarknessLevel = sh,
                    };
                    _ = entity.ToString();
                    Assert.AreEqual(false, entityNew.Equals(entity));
                }
            });

            TestsUtils.MethodComplete();
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                const string name = "ZebraPrinterEntity test";
                const string name2 = "ZebraPrinterEntity test 2";
                foreach (PrinterTypeEntity zebraPrinterTypeEntity in DataAccessUtils.DataAccess.Crud.GetEntities<PrinterTypeEntity>(null, null))
                {
                    // SaveEntity
                    PrinterEntity zebraPrinterType = new()
                    {
                        Id = -1,
                        CreateDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Name = name,
                        Ip = default,
                        Port = default,
                        Password = null,
                        PrinterType = zebraPrinterTypeEntity,
                        MacAddress = new DataShareCore.DAL.Models.MacAddressEntity(),
                        PeelOffSet = default,
                        DarknessLevel = default,
                    };
                    DataAccessUtils.DataAccess.PrintersCrud.SaveEntity(zebraPrinterType);
                }
                // GetEntities
                foreach (PrinterEntity entity in DataAccessUtils.DataAccess.PrintersCrud.GetEntities<PrinterEntity>(null,
                    new FieldOrderEntity { Use = true, Name = ShareEnums.DbField.Name, Direction = ShareEnums.DbOrderDirection.Asc }))
                {
                    if (entity.Name.Equals(name))
                    {
                        // UpdateEntity
                        entity.Name = name2;
                        DataAccessUtils.DataAccess.PrintersCrud.UpdateEntity(entity);
                    }
                }
                // GetEntities
                foreach (PrinterEntity entity in DataAccessUtils.DataAccess.PrintersCrud.GetEntities<PrinterEntity>(null,
                    new FieldOrderEntity { Use = true, Name = ShareEnums.DbField.Name, Direction = ShareEnums.DbOrderDirection.Asc }))
                {
                    if (entity.Name.Equals(name2))
                    {
                        // DeleteEntity
                        DataAccessUtils.DataAccess.PrintersCrud.DeleteEntity(entity);
                    }
                }
            }
            );

            TestsUtils.MethodComplete();
        }
    }
}
