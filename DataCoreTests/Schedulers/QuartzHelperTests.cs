// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Schedulers;
using NUnit.Framework;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DataCoreTests.Schedulers
{
    [TestFixture]
    internal class QuartzHelperTests
    {
        #region Public and private fields and properties

        private static QuartzHelper Quartz { get; set; } = QuartzHelper.Instance;

        #endregion

        internal void Method([CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            TestContext.WriteLine($"{DateTime.Now}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}");
        }

        [Test]
        public void OpenClose_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                TestContext.WriteLine("Open");
                Quartz.AddJob(QuartzUtils.CronExpression.EverySeconds(), delegate { Method(); }, "jobName", "triggerName", "triggerGroup");

                Quartz.Close();
                TestContext.WriteLine("Close");
            });
            TestContext.WriteLine();

            TestsUtils.MethodComplete();
        }

        //[Test]
        //public void OpenClose_Throw()
        //{
        //    TestsUtils.MethodStart();

        //    Assert.Throws<ArgumentException>(() =>
        //    {
        //        TestContext.WriteLine("Open");
        //        Quartz.AddJob(QuartzUtils.CronExpression.EverySeconds(), delegate { Method(); }, "jobName", "triggerName", "triggerGroup");

        //        TestContext.WriteLine("Close");
        //        Quartz.Close();
        //    });
        //    TestContext.WriteLine();

        //    TestsUtils.MethodComplete();
        //}
    }
}
