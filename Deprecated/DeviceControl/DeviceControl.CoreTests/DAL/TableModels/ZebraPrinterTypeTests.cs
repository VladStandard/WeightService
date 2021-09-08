using System;
using System.Collections.ObjectModel;
using DeviceControl.Core.DAL;
using DeviceControl.Core.DAL.DataModels;
using DeviceControl.Core.DAL.TableModels;
using NUnit.Framework;

namespace DeviceControl.CoreTests.DAL.TableModels
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
                    var entity = new ZebraPrinterTypeEntity()
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
                var zebraPrinterType = new ZebraPrinterTypeEntity
                {
                    Id = -1,
                    Name = name,
                };
                DataAccessUtils.DataAccess.ZebraPrinterTypeCrud.SaveEntity(zebraPrinterType);
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
                    foreach (var entity in DataAccessUtils.DataAccess.ZebraPrinterTypeCrud.GetEntities(null,
                        new FieldOrderEntity { Use = true, Name = EnumField.Name, Direction = EnumOrderDirection.Asc }))
                    {
                        if (entity.Name.Equals(name))
                        {
                            // UpdateEntity
                            entity.Name = name2;
                            DataAccessUtils.DataAccess.ZebraPrinterTypeCrud.UpdateEntity(entity);
                        }
                    }
                    // GetEntities
                    foreach (var entity in DataAccessUtils.DataAccess.ZebraPrinterTypeCrud.GetEntities(null,
                        new FieldOrderEntity { Use = true, Name = EnumField.Name, Direction = EnumOrderDirection.Asc }))
                    {
                        if (entity.Name.Equals(name2))
                        {
                            // DeleteEntity
                            DataAccessUtils.DataAccess.ZebraPrinterTypeCrud.DeleteEntity(entity);
                        }
                    }
                }
            );

            Utils.MethodComplete();
        }
    }
}
