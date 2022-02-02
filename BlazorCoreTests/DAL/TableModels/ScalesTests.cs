using CoreTests;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataShareCore;
using NUnit.Framework;

namespace BlazorCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class ScalesTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                ScaleEntity entityNew = new();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                object entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (int i in TestsEnums.GetInt())
                foreach (string s in TestsEnums.GetString())
                foreach (System.Guid guid in TestsEnums.GetGuid())
                foreach (short sh in TestsEnums.GetShort())
                foreach (bool b in TestsEnums.GetBool())
                foreach (System.DateTime dt in TestsEnums.GetDateTime())
                {
                                        ScaleEntity entity = new()
                                        {
                        Id = i,
                        CreateDate = dt,
                        ModifiedDate = dt,
                        TemplateDefault = new TemplateEntity(),
                        TemplateSeries = new TemplateEntity(),
                        WorkShop = new WorkshopEntity(),
                        Host = new HostEntity(),
                        Description = s,
                        IdRRef = guid,
                        DeviceIp = s,
                        DevicePort = sh,
                        DeviceMac = s,
                        DeviceSendTimeout = sh,
                        DeviceReceiveTimeout = sh,
                        DeviceComPort = s,
                        ZebraIp = s,
                        ZebraPort = sh,
                        UseOrder = b,
                        VerScalesUi = s,
                        DeviceNumber = i,
                        ScaleFactor = i
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

            Assert.DoesNotThrow(() => {
                // GetEntities.
                foreach (ScaleEntity entity in DataAccessUtils.DataAccess.Crud.GetEntities<ScaleEntity>(null,
                    new FieldOrderEntity { Use = true, Name = ShareEnums.DbField.Description, Direction = ShareEnums.DbOrderDirection.Desc }))
                {
                    TestContext.WriteLine(entity.ToString());
                    TestContext.WriteLine();
                }
            });

            TestsUtils.MethodComplete();
        }
    }
}
