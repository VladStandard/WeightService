using CoreTests;
using DataProjectsCore.DAL.DataModels;
using DataProjectsCore.DAL.TableScaleModels;
using NUnit.Framework;

namespace BlazorCoreTests.DAL.DataModels
{
    [TestFixture]
    internal class DeviceEntityTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                DeviceEntity entityNew = new DeviceEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                object entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (int i in TestsEnums.GetInt())
                {
                    DeviceEntity entity = new DeviceEntity
                    {
                        Id = i,
                        Scales = new ScaleEntity(),
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
                int i = 0;
                foreach (ScaleEntity scalesEntity in DataAccessUtils.DataAccess.Crud.GetEntities<ScaleEntity>(null, null))
                {
                    DeviceEntity entity = new DeviceEntity
                    {
                        Id = i,
                        Scales = scalesEntity,
                    };
                }
            });

            TestsUtils.MethodComplete();
        }
    }
}
