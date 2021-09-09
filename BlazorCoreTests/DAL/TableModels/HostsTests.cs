using NUnit.Framework;

namespace BlazorCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class HostsTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new HostEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in EnumValues.GetInt())
                foreach (var s in EnumValues.GetString())
                foreach (var dt in EnumValues.GetDateTime())
                foreach (var guid in EnumValues.GetGuid())
                foreach (var b in EnumValues.GetBool())
                foreach (var bytes in EnumValues.GetBytes())
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

            Utils.MethodComplete();
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            Utils.MethodStart();

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

            Utils.MethodComplete();
        }

        [Test]
        public void Entity_GetFreeHosts_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() => {
                // GetEntities.
                foreach (var entity in DataAccessUtils.DataAccess.HostsCrud.GetFreeHosts(null))
                {
                    TestContext.WriteLine(entity.ToString());
                    TestContext.WriteLine($"SettingsFile: {entity.SettingsFile}");
                    TestContext.WriteLine();
                }
            });

            Utils.MethodComplete();
        }
    }
}
