// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using NUnit.Framework;

//namespace DataCoreTests.Sql.TableModels
//{
//    [TestFixture]
//    internal class PluTests
//    {
//        [Test]
//        public void Model_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                PluEntity entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = entityNew.CloneCast();
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                    foreach (string s in TestsEnums.GetString())
//                        foreach (byte bytes in TestsEnums.GetBytes())
//                            foreach (decimal d in TestsEnums.GetDecimal())
//                                foreach (System.DateTime dt in TestsEnums.GetDateTime())
//                                    foreach (bool b in TestsEnums.GetBool())
//                                    {
//                                        PluEntity item = new()
//                                        {
//                                            Id = i,
//                                            CreateDate = dt,
//                                            ChangeDt = dt,
//                                            Templates = new TemplateEntity(),
//                                            Scale = new ScaleEntity(),
//                                            Nomenclature = new NomenclatureModel(),
//                                            GoodsName = s,
//                                            GoodsFullName = s,
//                                            GoodsDescription = s,
//                                            Gtin = s,
//                                            Ean13 = s,
//                                            Itf14 = s,
//                                            GoodsShelfLifeDays = bytes,
//                                            GoodsTareWeight = d,
//                                            GoodsBoxQuantly = i,
//                                            Plu = i,
//                                            Active = b
//                                        };
//                                        _ = item.ToString();
//                                        Assert.AreEqual(false, entityNew.Equals(item));
//                                    }
//            });

//            TestsUtils.MethodComplete();
//        }
//    }
//}
