using DeviceControl.Core.DAL.TableModels;
using NUnit.Framework;

namespace DeviceControl.CoreTests.DAL.TableModels
{
    [TestFixture]
    internal class OrderStatusEntityTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new OrderStatusEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in EnumValues.GetInt())
                foreach (var dt in EnumValues.GetDateTime())
                foreach (var s in EnumValues.GetString())
                foreach (var bytes in EnumValues.GetByte())
                {
                    var entity = new OrderStatusEntity
                    {
                        Id = i,
                        OrderId = s,
                        CurrentDate = dt,
                        CurrentStatus = bytes
                    };
                    _ = entity.ToString();
                    Assert.AreEqual(false, entityNew.Equals(entity));
                }
            });

            Utils.MethodComplete();
        }
    }
}
