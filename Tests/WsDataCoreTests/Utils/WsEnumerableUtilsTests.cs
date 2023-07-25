// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.ObjectModel;
using WsDataCore.Common;

namespace WsDataCoreTests.Utils;

[TestFixture]
public sealed class WsEnumerableUtilsTests
{
    [Test]
    public void Change_new_copy_collection_int()
    {
        Assert.DoesNotThrow(() =>
        {
            Collection<int> col1 = new() { 1, 2, 3 };
            Collection<int> col2 = WsEnumerableUtils.CopyCollection(col1);
            col2[1] = -1;
            TestContext.WriteLine($"{nameof(col1)}: {string.Join(" | ", col1)}");
            TestContext.WriteLine($"{nameof(col2)}: {string.Join(" | ", col2)}");

            bool isEqual = true;
            for (int i = 0; i < col2.Count; i++)
            {
                if (!col2[i].Equals(col1[i]))
                {
                    isEqual = false;
                    break;
                }
            }

            Assert.IsFalse(isEqual);
        });
    }

    [Test]
    public void Change_new_copy_collection_string()
    {
        Assert.DoesNotThrow(() =>
        {
            Collection<string> col1 = new() { "1", "2", "3" };
            Collection<string> col2 = WsEnumerableUtils.CopyCollection(col1);
            col2[1] = "-1";
            TestContext.WriteLine($"{nameof(col1)}: {string.Join(" | ", col1)}");
            TestContext.WriteLine($"{nameof(col2)}: {string.Join(" | ", col2)}");

            bool isEqual = true;
            for (int i = 0; i < col2.Count; i++)
            {
                if (!col2[i].Equals(col1[i]))
                {
                    isEqual = false;
                    break;
                }
            }

            Assert.IsFalse(isEqual);
        });
    }

    [Test]
    public void Change_new_copy_collection_WsViewModelBase()
    {
        Assert.DoesNotThrow(() =>
        {
            Collection<WsViewModelBase> col1 = new() { new(), new(), new() };
            Collection<WsViewModelBase> col2 = WsEnumerableUtils.CopyClassesCollection(col1);
            TestContext.WriteLine($"{nameof(col1)}: {string.Join(" | ", col1)}");
            TestContext.WriteLine($"{nameof(col2)}: {string.Join(" | ", col2)}");

            col2[1].Uid = Guid.NewGuid();
            TestContext.WriteLine($"{nameof(col1)}: {string.Join(" | ", col1)}");
            TestContext.WriteLine($"{nameof(col2)}: {string.Join(" | ", col2)}");

            bool isEqual = true;
            for (int i = 0; i < col2.Count; i++)
            {
                if (!col2[i].Uid.Equals(col1[i].Uid))
                {
                    isEqual = false;
                    break;
                }
            }

            Assert.IsFalse(isEqual);
        });
    }

    [Test]
    public void Change_new_copy_list_int()
    {
        Assert.DoesNotThrow(() =>
        {
            List<int> list1 = new() { 1, 2, 3 };
            List<int> list2 = WsEnumerableUtils.CopyList(list1);
            list2[1] = -1;
            TestContext.WriteLine($"{nameof(list1)}: {string.Join(" | ", list1)}");
            TestContext.WriteLine($"{nameof(list2)}: {string.Join(" | ", list2)}");

            bool isEqual = true;
            for (int i = 0; i < list2.Count; i++)
            {
                if (!list2[i].Equals(list1[i]))
                {
                    isEqual = false;
                    break;
                }
            }

            Assert.IsFalse(isEqual);
        });
    }

    [Test]
    public void Change_new_copy_list_string()
    {
        Assert.DoesNotThrow(() =>
        {
            List<string> list1 = new() { "1", "2", "3" };
            List<string> list2 = WsEnumerableUtils.CopyList(list1);
            list2[1] = "-1";
            TestContext.WriteLine($"{nameof(list1)}: {string.Join(" | ", list1)}");
            TestContext.WriteLine($"{nameof(list2)}: {string.Join(" | ", list2)}");

            bool isEqual = true;
            for (int i = 0; i < list2.Count; i++)
            {
                if (!list2[i].Equals(list1[i]))
                {
                    isEqual = false;
                    break;
                }
            }

            Assert.IsFalse(isEqual);
        });
    }

    [Test]
    public void Change_new_copy_list_WsViewModelBase()
    {
        Assert.DoesNotThrow(() =>
        {
            List<WsViewModelBase> col1 = new() { new(), new(), new() };
            List<WsViewModelBase> col2 = WsEnumerableUtils.CopyClassesList(col1);
            TestContext.WriteLine($"{nameof(col1)}: {string.Join(" | ", col1)}");
            TestContext.WriteLine($"{nameof(col2)}: {string.Join(" | ", col2)}");

            col2[1].Uid = Guid.NewGuid();
            TestContext.WriteLine($"{nameof(col1)}: {string.Join(" | ", col1)}");
            TestContext.WriteLine($"{nameof(col2)}: {string.Join(" | ", col2)}");

            bool isEqual = true;
            for (int i = 0; i < col2.Count; i++)
            {
                if (!col2[i].Uid.Equals(col1[i].Uid))
                {
                    isEqual = false;
                    break;
                }
            }

            Assert.IsFalse(isEqual);
        });
    }

    [Test]
    public void Create_new_copy_collection_int()
    {
        Assert.DoesNotThrow(() =>
        {
            Collection<int> col1 = new() { 1, 2, 3 };
            Collection<int> col2 = WsEnumerableUtils.CopyCollection(col1);
            TestContext.WriteLine($"{nameof(col1)}: {string.Join(" | ", col1)}");
            TestContext.WriteLine($"{nameof(col2)}: {string.Join(" | ", col2)}");

            bool isEqual = true;
            for (int i = 0; i < col2.Count; i++)
            {
                if (!col2[i].Equals(col1[i]))
                {
                    isEqual = false;
                    break;
                }
            }

            Assert.IsTrue(isEqual);
        });
    }

    [Test]
    public void Create_new_copy_collection_string()
    {
        Assert.DoesNotThrow(() =>
        {
            Collection<string> col1 = new() { "1", "2", "3" };
            Collection<string> col2 = WsEnumerableUtils.CopyCollection(col1);
            TestContext.WriteLine($"{nameof(col1)}: {string.Join(" | ", col1)}");
            TestContext.WriteLine($"{nameof(col2)}: {string.Join(" | ", col2)}");

            bool isEqual = true;
            for (int i = 0; i < col2.Count; i++)
            {
                if (!col2[i].Equals(col1[i]))
                {
                    isEqual = false;
                    break;
                }
            }

            Assert.IsTrue(isEqual);
        });
    }

    [Test]
    public void Create_new_copy_collection_WsViewModelBase()
    {
        Assert.DoesNotThrow(() =>
        {
            Collection<WsViewModelBase> col1 = new() { new(), new(), new() };
            Collection<WsViewModelBase> col2 = WsEnumerableUtils.CopyClassesCollection(col1);
            TestContext.WriteLine($"{nameof(col1)}: {string.Join(" | ", col1)}");
            TestContext.WriteLine($"{nameof(col2)}: {string.Join(" | ", col2)}");

            bool isEqual = true;
            for (int i = 0; i < col2.Count; i++)
            {
                if (!col2[i].Uid.Equals(col1[i].Uid))
                {
                    isEqual = false;
                    break;
                }
            }

            Assert.IsTrue(isEqual);
        });
    }

    [Test]
    public void Create_new_copy_list_int()
    {
        Assert.DoesNotThrow(() =>
        {
            List<int> list1 = new() { 1, 2, 3 };
            List<int> list2 = WsEnumerableUtils.CopyList(list1);
            TestContext.WriteLine($"{nameof(list1)}: {string.Join(" | ", list1)}");
            TestContext.WriteLine($"{nameof(list2)}: {string.Join(" | ", list2)}");

            bool isEqual = true;
            for (int i = 0; i < list2.Count; i++)
            {
                if (!list2[i].Equals(list1[i]))
                {
                    isEqual = false;
                    break;
                }
            }

            Assert.IsTrue(isEqual);
        });
    }

    [Test]
    public void Create_new_copy_list_string()
    {
        Assert.DoesNotThrow(() =>
        {
            List<string> list1 = new() { "1", "2", "3" };
            List<string> list2 = WsEnumerableUtils.CopyList(list1);
            TestContext.WriteLine($"{nameof(list1)}: {string.Join(" | ", list1)}");
            TestContext.WriteLine($"{nameof(list2)}: {string.Join(" | ", list2)}");

            bool isEqual = true;
            for (int i = 0; i < list2.Count; i++)
            {
                if (!list2[i].Equals(list1[i]))
                {
                    isEqual = false;
                    break;
                }
            }

            Assert.IsTrue(isEqual);
        });
    }

    [Test]
    public void Create_new_copy_list_WsViewModelBase()
    {
        Assert.DoesNotThrow(() =>
        {
            List<WsViewModelBase> col1 = new() { new(), new(), new() };
            List<WsViewModelBase> col2 = WsEnumerableUtils.CopyClassesList(col1);
            TestContext.WriteLine($"{nameof(col1)}: {string.Join(" | ", col1)}");
            TestContext.WriteLine($"{nameof(col2)}: {string.Join(" | ", col2)}");

            bool isEqual = true;
            for (int i = 0; i < col2.Count; i++)
            {
                if (!col2[i].Uid.Equals(col1[i].Uid))
                {
                    isEqual = false;
                    break;
                }
            }

            Assert.IsTrue(isEqual);
        });
    }
}