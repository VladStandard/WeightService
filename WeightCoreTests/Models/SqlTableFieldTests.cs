// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.Utils;
using NUnit.Framework;
using System;
using System.Diagnostics;
using WeightCore.Models;

namespace WeightCoreTests.Models
{
    public class SqlTableFieldTests
    {
        #region Test methods

        /// <summary>
        /// Setup private fields.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(Setup)} start.");
            //
            TestContext.WriteLine($@"{nameof(Setup)} complete.");
        }

        /// <summary>
        /// Reset private fields to default state.
        /// </summary>
        [TearDown]
        public void Teardown()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(Teardown)} start.");
            //
            TestContext.WriteLine($@"{nameof(Teardown)} complete.");
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
        }

        #endregion

        #region Public methods

        [Test]
        public void Constructor_Create_Error()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(Constructor_Create_Error)} start.");
            Stopwatch sw = Stopwatch.StartNew();

            Assert.Catch<ArgumentException>(() => { SqlTableField<string> field = new SqlTableField<string>(); });
            foreach (string name in EnumValuesUtils.GetString())
            {
                Assert.Catch<ArgumentException>(() => { SqlTableField<string> field = new SqlTableField<string>(name); });
            }

            sw.Stop();
            TestContext.WriteLine($@"{nameof(Constructor_Create_Error)} complete. Elapsed time: {sw.Elapsed}");
        }

        [Test]
        public void Constructor_Create_Correct()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(Constructor_Create_Correct)} start.");
            Stopwatch sw = Stopwatch.StartNew();

            SqlTableField<string> field = new SqlTableField<string>("FieldName");
            Assert.AreEqual("FieldName", field.Name);
            Assert.AreEqual(string.Empty, field.Value);
            Assert.AreEqual(string.Empty, field.Default);

            foreach (string value in EnumValuesUtils.GetString())
            {
                field = new SqlTableField<string>("FieldName", value);
                Assert.AreEqual("FieldName", field.Name);
                Assert.AreEqual(string.Empty, field.Value);
                Assert.AreEqual(string.Empty, field.Default);
                foreach (string defValue in EnumValuesUtils.GetString())
                {
                    field = new SqlTableField<string>("FieldName", value, defValue);
                    Assert.AreEqual("FieldName", field.Name);
                    Assert.AreEqual(string.Empty, field.Value);
                    Assert.AreEqual(string.Empty, field.Default);
                }
            }

            sw.Stop();
            TestContext.WriteLine($@"{nameof(Constructor_Create_Correct)} complete. Elapsed time: {sw.Elapsed}");
        }

        #endregion
    }
}