using System;
using NUnit.Framework;

namespace BlazorCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class ZebraPrinterTypeTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new WorkshopEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in TestsEnums.GetInt())
                foreach (var s in TestsEnums.GetString())
                {
                    var entity = new PrinterTypeEntity()
                    {
                        Id = i,
                        Name = s,
                    };
                    _ = entity.ToString();
                    Assert.AreEqual(false, entityNew.Equals(entity));
                }
            });

            TestsUtils.MethodComplete();
        }

        [Test]
        public void Entity_Crud_Throws()
        {
            TestsUtils.MethodStart();

            Assert.Throws<Exception>(() =>
            {
                const string name = "ZebraPrinterType test";
                // SaveEntity
                var zebraPrinterType = new PrinterTypeEntity
                {
                    Id = -1,
                    Name = name,
                };
                DataAccessUtils.DataAccess.PrinterTypesCrud.SaveEntity(zebraPrinterType);
            });

            TestsUtils.MethodComplete();
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
                {
                    const string name = "ZebraPrinterType test";
                    const string name2 = "ZebraPrinterType test 2";
                    // GetEntities
                    foreach (var entity in DataAccessUtils.DataAccess.PrinterTypesCrud.GetEntities(null,
                        new FieldOrderEntity { Use = true, Name = EnumField.Name, Direction = EnumOrderDirection.Asc }))
                    {
                        if (entity.Name.Equals(name))
                        {
                            // UpdateEntity
                            entity.Name = name2;
                            DataAccessUtils.DataAccess.PrinterTypesCrud.UpdateEntity(entity);
                        }
                    }
                    // GetEntities
                    foreach (var entity in DataAccessUtils.DataAccess.PrinterTypesCrud.GetEntities(null,
                        new FieldOrderEntity { Use = true, Name = EnumField.Name, Direction = EnumOrderDirection.Asc }))
                    {
                        if (entity.Name.Equals(name2))
                        {
                            // DeleteEntity
                            DataAccessUtils.DataAccess.PrinterTypesCrud.DeleteEntity(entity);
                        }
                    }
                }
            );

            TestsUtils.MethodComplete();
        }
    }
}
