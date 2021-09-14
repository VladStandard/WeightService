using NUnit.Framework;

namespace BlazorCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class OrderStatusEntityTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new OrderStatusEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in TestsEnums.GetInt())
                foreach (var dt in TestsEnums.GetDateTime())
                foreach (var s in TestsEnums.GetString())
                foreach (var bytes in TestsEnums.GetByte())
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

            TestsUtils.MethodComplete();
        }
    }
}
