using System;
using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
using BlazorCore.DAL.TableModels;
using NUnit.Framework;

namespace BlazorCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class ZebraPrinterTypeTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new WorkshopEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in EnumValues.GetInt())
                foreach (var s in EnumValues.GetString())
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

            Utils.MethodComplete();
        }

        [Test]
        public void Entity_Crud_Throws()
        {
            Utils.MethodStart();

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

            Utils.MethodComplete();
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            Utils.MethodStart();

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

            Utils.MethodComplete();
        }
    }
}
