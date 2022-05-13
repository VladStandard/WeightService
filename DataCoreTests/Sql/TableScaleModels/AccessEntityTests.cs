// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using static DataCore.ShareEnums;

namespace DataCoreTests.Sql.TableScaleModels
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
                    List<BaseEntity>? items = TestsUtils.DataAccess.Crud.GetEntities<AccessEntity>(
                            (isShowMarkedItems == true) ? null
                                : new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IsMarked, false } }),
                            new FieldOrderEntity(DbField.User, DbOrderDirection.Asc), 
                            10)
                        ?.ToList<BaseEntity>();
                    if (items != null)
                    {
                        List<AccessEntity> itemsCast = items.Select(x => (AccessEntity)x).ToList();
                        if (itemsCast.Count > 0)
                        {
                            foreach (AccessEntity item in itemsCast)
                            {
                                AccessEntity itemCopy = item.CloneCast();
                                Assert.AreEqual(true, item.Equals(itemCopy));
                                Assert.AreEqual(true, itemCopy.Equals(item));
                                AccessEntity itemChange = new()
                                {
                                    IsMarked = true,
                                };
                                _ = itemChange.ToString();
                                Assert.AreNotEqual(true, itemChange.Equals(item));
                                Assert.AreNotEqual(true, item.Equals(itemChange));
                            }
                        }
                    }
                }
            });
        }
    }
}
