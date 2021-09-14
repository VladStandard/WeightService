using NUnit.Framework;

namespace BlazorCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class HostsTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new HostEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in TestsEnums.GetInt())
                foreach (var s in TestsEnums.GetString())
                foreach (var dt in TestsEnums.GetDateTime())
                foreach (var guid in TestsEnums.GetGuid())
                foreach (var b in TestsEnums.GetBool())
                foreach (var bytes in TestsEnums.GetBytes())
                {
                    var entity = new HostEntity
                    {
                        Id = i,
                        CreateDate = dt,
                        ModifiedDate = dt,
                        Name = s,
                        Ip = s,
                        Mac = s,
                        IdRRef = guid,
                        Marked = b,
                        SettingsFile = s,
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
                foreach (var entity in DataAccessUtils.DataAccess.HostsCrud.GetEntities(null,
                    new FieldOrderEntity { Use = true, Name = EnumField.Name, Direction = EnumOrderDirection.Desc }))
                {
                    TestContext.WriteLine(entity.ToString());
                    TestContext.WriteLine($"SettingsFile: {entity.SettingsFile}");
                    TestContext.WriteLine();
                }
            });

            TestsUtils.MethodComplete();
        }

        [Test]
        public void Entity_GetFreeHosts_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() => {
                // GetEntities.
                foreach (var entity in DataAccessUtils.DataAccess.HostsCrud.GetFreeHosts(null))
                {
                    TestContext.WriteLine(entity.ToString());
                    TestContext.WriteLine($"SettingsFile: {entity.SettingsFile}");
                    TestContext.WriteLine();
                }
            });

            TestsUtils.MethodComplete();
        }
    }
}
