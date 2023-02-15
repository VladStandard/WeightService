// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore;
//using NUnit.Framework;

//namespace DataCoreTests.Sql.TableModels
//{
//    [TestFixture]
//    internal class ScalesTests
//    {
//        [Test]
//        public void Model_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                ScaleEntity entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = entityNew.CloneCast();
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                    foreach (string s in TestsEnums.GetString())
//                        foreach (System.Guid guid in TestsEnums.GetGuid())
//                            foreach (short sh in TestsEnums.GetShort())
//                                foreach (bool b in TestsEnums.GetBool())
//                                    foreach (System.DateTime dt in TestsEnums.GetDateTime())
//                                    {
//                                        ScaleEntity item = new()
//                                        {
//                                            Id = i,
//                                            CreateDate = dt,
//                                            ChangeDt = dt,
//                                            TemplateDefault = new TemplateEntity(),
//                                            TemplateSeries = new TemplateEntity(),
//                                            WorkShop = new WorkshopEntity(),
//                                            Host = new HostEntity(),
//                                            Description = s,
//                                            IdRRef = guid,
//                                            DeviceIp = s,
//                                            DevicePort = sh,
//                                            DeviceMac = s,
//                                            DeviceSendTimeout = sh,
//                                            DeviceReceiveTimeout = sh,
//                                            DeviceComPort = s,
//                                            ZebraIp = s,
//                                            ZebraPort = sh,
//                                            UseOrder = b,
//                                            VerScalesUi = s,
//                                            Number = i,
//                                            ScaleFactor = i
//                                        };
//                                        _ = item.ToString();
//                                        Assert.AreEqual(false, entityNew.Equals(item));
//                                    }
//            });

//            TestsUtils.MethodComplete();
//        }

//        [Test]
//        public void Entity_Crud_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                // GetEntities.
//                foreach (ScaleEntity item in DataAccessUtilsTests.DataAccess.GetEntities<ScaleEntity>(null,
//                    new FieldOrderModel { Use = true, Name = DbField.Description, Direction = DbOrderDirection.Desc }))
//                {
//                    TestContext.WriteLine(item.ToString());
//                    TestContext.WriteLine();
//                }
//            });

//            TestsUtils.MethodComplete();
//        }
//    }
//}
