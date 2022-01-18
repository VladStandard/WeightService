using CoreTests;
using DataProjectsCore.DAL.TableScaleModels;
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
                PluEntity entityNew = new();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                object entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (int i in TestsEnums.GetInt())
                foreach (string s in TestsEnums.GetString())
                foreach (byte bytes in TestsEnums.GetBytes())
                foreach (decimal d in TestsEnums.GetDecimal())
                foreach (System.DateTime dt in TestsEnums.GetDateTime())
                foreach (bool b in TestsEnums.GetBool())
                {
                    PluEntity entity = new()
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
