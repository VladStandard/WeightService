using BlazorCore.DAL.DataModels;
using BlazorCore.DAL.TableModels;
using NUnit.Framework;

namespace BlazorCoreTests.DAL.DataModels
{
    [TestFixture]
    internal class DeviceEntityTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new DeviceEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in EnumValues.GetInt())
                {
                    var entity = new DeviceEntity
                    {
                        Id = i,
                        Scales = new ScalesEntity(),
                    };
                    _ = entity.ToString();
                    Assert.AreEqual(false, entityNew.Equals(entity));
                }
            });

            Utils.MethodComplete();
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var i = 0;
                foreach (var scalesEntity in DataAccessUtils.DataAccess.ScalesCrud.GetEntities(null, null))
                {
                    var entity = new DeviceEntity
                    {
                        Id = i,
                        Scales = scalesEntity,
                    };
                }
            });

            Utils.MethodComplete();
        }
    }
}
