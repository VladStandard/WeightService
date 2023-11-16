using System.Collections.ObjectModel;
using Ws.DataCore.Common;

namespace Ws.DataCoreTests.Utils;

[TestFixture]
public sealed class WsEnumerableUtilsTests
{
    [Test]
    public void Change_new_copy_collection_int()
    {
        Assert.DoesNotThrow(() =>
        {
            Collection<int> col1 = new() { 1, 2, 3 };
            Collection<int> col2 = EnumerableUtils.CopyCollection(col1);
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
            Collection<string> col2 = EnumerableUtils.CopyCollection(col1);
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
            Collection<ViewModelBase> col1 = new() { new(), new(), new() };
            Collection<ViewModelBase> col2 = EnumerableUtils.CopyClassesCollection(col1);
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
            List<int> list2 = EnumerableUtils.CopyList(list1);
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
            List<string> list2 = EnumerableUtils.CopyList(list1);
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
            List<ViewModelBase> col1 = new() { new(), new(), new() };
            List<ViewModelBase> col2 = EnumerableUtils.CopyClassesList(col1);
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
            Collection<int> col2 = EnumerableUtils.CopyCollection(col1);
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
            Collection<string> col2 = EnumerableUtils.CopyCollection(col1);
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
            Collection<ViewModelBase> col1 = new() { new(), new(), new() };
            Collection<ViewModelBase> col2 = EnumerableUtils.CopyClassesCollection(col1);
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
            List<int> list2 = EnumerableUtils.CopyList(list1);
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
            List<string> list2 = EnumerableUtils.CopyList(list1);
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
            List<ViewModelBase> col1 = new() { new(), new(), new() };
            List<ViewModelBase> col2 = EnumerableUtils.CopyClassesList(col1);
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