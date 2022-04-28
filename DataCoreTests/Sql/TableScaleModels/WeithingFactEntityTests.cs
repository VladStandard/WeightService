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
    internal class WeithingFactEntityTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
                WeithingFactEntity item = new();
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
                    List<BaseEntity>? items = TestsUtils.DataAccess.Crud.GetEntities<WeithingFactEntity>(
                            (isShowMarkedItems == true) ? null
                                : new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IsMarked, false } }),
                            null, 10)
                        ?.ToList<BaseEntity>();
                    if (items != null)
                    {
                        List<WeithingFactEntity> itemsCast = items.Select(x => (WeithingFactEntity)x).ToList();
                        if (itemsCast.Count > 0)
                        {
                            foreach (WeithingFactEntity item in itemsCast)
                            {
                                WeithingFactEntity itemCopy = item.CloneCast;
                                Assert.AreEqual(true, item.Equals(itemCopy));
                                Assert.AreEqual(true, itemCopy.Equals(item));
                                WeithingFactEntity itemChange = new()
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
