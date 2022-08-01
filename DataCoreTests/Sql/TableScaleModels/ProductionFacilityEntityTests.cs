// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using static DataCore.ShareEnums;
// ReSharper disable MethodTooLong
// ReSharper disable CognitiveComplexity
// ReSharper disable ExcessiveIndentation

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class ProductionFacilityEntityTests
{
    [Test]
    public void Entity_Equals_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            // Arrange.
            ProductionFacilityEntity item = new();
            //ProductionFacilityEntity item = Substitute.For<ProductionFacilityEntity>();
            //item.GetHashCode().Returns(0);
            // Act.
            bool success = item.EqualsNew();
            // Assert.
            Assert.True(success);
            // Act.
            success = item.EqualsDefault();
            // Assert.
            Assert.True(success);
        });
    }

    [Test]
    public void Entity_Crud_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            foreach (bool isShowMarkedItems in TestsEnums.GetBool())
            {
                List<BaseEntity>? items = TestsUtils.DataAccess.Crud.GetEntities<ProductionFacilityEntity>(
                        (isShowMarkedItems == true) ? null
                            : new FieldListEntity(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                        new(DbField.User, DbOrderDirection.Asc),
                        10)
                    ?.ToList<BaseEntity>();
                if (items != null)
                {
                    List<ProductionFacilityEntity> itemsCast = items.Select(x => (ProductionFacilityEntity)x).ToList();
                    if (itemsCast.Count > 0)
                    {
                        foreach (ProductionFacilityEntity item in itemsCast)
                        {
                            ProductionFacilityEntity itemCopy = item.CloneCast();
                            Assert.AreEqual(true, item.Equals(itemCopy));
                            Assert.AreEqual(true, itemCopy.Equals(item));
                            ProductionFacilityEntity itemChange = new()
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
