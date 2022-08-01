// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Linq;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using NUnit.Framework;
using static DataCore.ShareEnums;

namespace DataCoreTests.Sql.TableScaleModels
{
    [TestFixture]
    internal class SystemEntityTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
                VersionEntity item = new();
                Assert.AreEqual(true, item.EqualsNew());
                Assert.AreEqual(true, item.EqualsDefault());
            });
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
                List<BaseEntity>? items = TestsUtils.DataAccess.Crud.GetEntities<VersionEntity>(null,
                    new(DbField.ReleaseDt), 10)
                    ?.ToList<BaseEntity>();
                if (items != null)
                {
                    List<VersionEntity> itemsCast = items.Select(x => (VersionEntity)x).ToList();
                    if (itemsCast.Count > 0)
                    {
                        foreach (VersionEntity item in itemsCast)
                        {
                            VersionEntity itemCopy = item.CloneCast();
                            Assert.AreEqual(true, item.Equals(itemCopy));
                            Assert.AreEqual(true, itemCopy.Equals(item));
                            VersionEntity itemChange = new()
                            {
                                IsMarked = true,
                            };
                            _ = itemChange.ToString();
                            Assert.AreNotEqual(true, itemChange.Equals(item));
                            Assert.AreNotEqual(true, item.Equals(itemChange));
                        }
                    }
                }
            });
        }
    }
}
