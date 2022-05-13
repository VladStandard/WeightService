// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using NUnit.Framework;

//namespace DataCoreTests.Sql.DataModels
//{
//    [TestFixture]
//    internal class DeviceEntityTests
//    {
//        [Test]
//        public void Entity_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                DeviceEntity entityNew = new DeviceEntity();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = entityNew.CloneCast();
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                {
//                    DeviceEntity item = new DeviceEntity
//                    {
//                        Id = i,
//                        Scales = new ScaleEntity(),
//                    };
//                    _ = item.ToString();
//                    Assert.AreEqual(false, entityNew.Equals(item));
//                }
//            });

//            TestsUtils.MethodComplete();
//        }

//        [Test]
//        public void Entity_Crud_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                int i = 0;
//                foreach (ScaleEntity scalesEntity in DataAccessUtilsTests.DataAccess.Crud.GetEntities<ScaleEntity>(null, null))
//                {
//                    DeviceEntity item = new DeviceEntity
//                    {
//                        Id = i,
//                        Scales = scalesEntity,
//                    };
//                }
//            });

//            TestsUtils.MethodComplete();
//        }
//    }
//}
