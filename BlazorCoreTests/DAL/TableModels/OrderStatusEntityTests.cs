using CoreTests;
using DataProjectsCore.DAL.TableScaleModels;
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
                OrderStatusEntity entityNew = new();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                object entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (int i in TestsEnums.GetInt())
                foreach (System.DateTime dt in TestsEnums.GetDateTime())
                foreach (string s in TestsEnums.GetString())
                foreach (byte bytes in TestsEnums.GetBytes())
                {
                    OrderStatusEntity entity = new()
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
