using DeviceControlCore.DAL.TableModels;
using NUnit.Framework;

namespace DeviceControlCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class PluTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new PluEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in EnumValues.GetInt())
                foreach (var s in EnumValues.GetString())
                foreach (var bytes in EnumValues.GetByte())
                foreach (var d in EnumValues.GetDecimal())
                foreach (var dt in EnumValues.GetDateTime())
                foreach (var b in EnumValues.GetBool())
                {
                    var entity = new PluEntity
                    {
                        Id = i,
                        CreateDate = dt,
                        ModifiedDate = dt,
                        Templates = new TemplatesEntity(),
                        Scale = new ScalesEntity(),
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

            Utils.MethodComplete();
        }
    }
}
