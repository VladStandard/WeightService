// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using static DataCore.ShareEnums;

namespace DataCoreTests.DAL.TableScaleModels
{
    [TestFixture]
    internal class AccessEntityTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
                AccessEntity item = new();
                Assert.AreEqual(true, item.EqualsNew());
                Assert.AreEqual(true, item.EqualsDefault());
            });
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
                foreach (bool isShowMarkedItems in TestsEnums.GetBool())
                {
                    foreach (bool isShowTop in TestsEnums.GetBool())
                    {
                        List<BaseEntity>? items = TestsUtils.DataAccess.Crud.GetEntities<AccessEntity>(
                               (isShowMarkedItems == true) ? null
                                   : new FieldListEntity(new Dictionary<string, object?> { { DbField.IsMarked.ToString(), false } }),
                               new FieldOrderEntity(DbField.User, DbOrderDirection.Asc), isShowTop ? 0_100 : 0)
                           ?.ToList<BaseEntity>();
                        if (items != null)
                        {
                            List<AccessEntity> itemsCast = items.Select(x => (AccessEntity)x).ToList();
                            TestsUtils.DataAccess.Crud.GetEntities<AccessEntity>(null, null);
                            if (itemsCast.Count > 0)
                            {
                                foreach (AccessEntity item in itemsCast)
                                {
                                    AccessEntity itemCopy = item.CloneCast();
                                    Assert.AreEqual(true, item.Equals(itemCopy));
                                    Assert.AreEqual(true, itemCopy.Equals(item));
                                    AccessEntity itemChange = new()
                                    {
                                        User = $"{item.User}_changed",
                                    };
                                    _ = itemChange.ToString();
                                    Assert.AreNotEqual(true, itemChange.Equals(item));
                                    Assert.AreNotEqual(true, item.Equals(itemChange));
                                }
                            }
                        }
                    }
                }
            });
        }
    }
}
