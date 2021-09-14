using NUnit.Framework;

namespace BlazorCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class PluTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new PluEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in TestsEnums.GetInt())
                foreach (var s in TestsEnums.GetString())
                foreach (var bytes in TestsEnums.GetByte())
                foreach (var d in TestsEnums.GetDecimal())
                foreach (var dt in TestsEnums.GetDateTime())
                foreach (var b in TestsEnums.GetBool())
                {
                    var entity = new PluEntity
                    {
                        Id = i,
                        CreateDate = dt,
                        ModifiedDate = dt,
                        Templates = new TemplateEntity(),
                        Scale = new ScaleEntity(),
                        Nomenclature = new NomenclatureEntity(),
                        GoodsName = s,
                        GoodsFullName = s,
                        GoodsDescription = s,
                        Gtin = s,
                        Ean13 = s,
                        Itf14 = s,
                        GoodsShelfLifeDays = bytes,
                        GoodsTareWeight = d,
                        GoodsBoxQuantly = i,
                        Plu = i,
                        Active = b
                    };
                    _ = entity.ToString();
                    Assert.AreEqual(false, entityNew.Equals(entity));
                }
            });

            TestsUtils.MethodComplete();
        }
    }
}
